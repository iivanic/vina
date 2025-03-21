using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
namespace vina.Server
{
    public class Seeder
    {
        public const string seed_script = "db.pgsql";
        public const string dbName = "vina_ivanic_hr";
        const string connString = @"Server=localhost;Port=5433;Username=postgres;Password=n;database={DATABASE};";
        string connStringDefaultDb = connString.Replace("{DATABASE}", "postgres");
        public string ConnStringMyDb = connString.Replace("{DATABASE}", dbName);

        private static Seeder? _instance;
        public static Seeder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Seeder();
                }
                return _instance;
            }
        }
        public async Task<bool> DbExists()
        {
            //does our database already exists?
            var dBcs = new DBcs.DBcs(connStringDefaultDb);
            var o = await dBcs.RunScalarAsync(
                $"Select count(*) from pg_database where datname='{dbName}' and datistemplate = false;\n");
            return (o?.ToString() ?? "0") != "0";
        }
        public async Task DbReCreateEmpty()
        {
            var dBcs = new DBcs.DBcs(connStringDefaultDb);
            var dbExists = await DbExists();
            //if doesnt exists, create database
            if (!dbExists)
            {
                await dBcs.RunNonQueryAsync($"CREATE DATABASE {dbName};");
                Console.Write($"Database {dbName} created.");
            }
            else
            {
                await dBcs.RunNonQueryAsync($"DROP DATABASE IF EXISTS {dbName} WITH(FORCE);");
                Console.Write("Database deleted.");
                await dBcs.RunNonQueryAsync($"CREATE DATABASE {dbName};");
                Console.Write("Database created.");

            }
        }
        public async Task<int> DbEnsureCratedAndSeed(WebApplication app)
        {
            const string AdminRole="Housekeeper";

            int ret=0;
            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var context = services.GetRequiredService<NPDataContext>();
                    context.Database.EnsureCreated();
                    // Look for any students.
                    if (context.Users.Any())
                    {
                        return ret;   // DB has been seeded
                    }
                    var users = new IdentityUser[]
                    {
                        new IdentityUser {UserName = "Igor", Email = "info@vina-ivanic.hr"},
                        new IdentityUser {UserName = "Alberto", Email = "alberto@example.net"},
                    };
                    foreach (IdentityUser u in users)
                    {
                        await userManager.CreateAsync(u);
                    }
                    var roles = new IdentityRole[]
                    {
                        new IdentityRole {Name = AdminRole, NormalizedName =AdminRole.ToLower()},
                    };
                    foreach (IdentityRole r in roles)
                    {
                        await roleManager.CreateAsync(r);
                    }
                    _ = userManager.AddToRoleAsync(users[0], AdminRole);
                    context.SaveChanges();

                    var dBcs = new DBcs.DBcs(ConnStringMyDb);
                    //create schema and data
                     ret = await dBcs.RunNonQueryAsync(await LoadScriptFromResource(seed_script));
                    Console.Write($"Script {seed_script} executed.");
                    return ret;
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            return ret;

        }
        public async Task DbDrop()
        {
            var dBcs = new DBcs.DBcs(connStringDefaultDb);
            var dbExists = await DbExists();
            //if doesnt exists, create database
            if (dbExists)
            {
                await dBcs.RunNonQueryAsync($"DROP DATABASE IF EXISTS {dbName} WITH(FORCE);");
                Console.Write("Database deleted.");
            }
        }
        private async Task<string> LoadScriptFromResource(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(scriptName));

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Resource '{resourceName}' not found.");
                }
                using StreamReader reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }


        }
        public async Task<string> GetClasses(string[] classNames, string queries)
        {
            /*
                You can get all tables with:
                    select 
                        'select * from public.' || t.table_name || ';'
                    from
                        information_schema.tables t  
                    where 
                        t.table_schema = 'public' 
                        and 
                        table_type='BASE TABLE';
            */
            var dBcs = new DBcs.DBcs(ConnStringMyDb);
            var classes = await dBcs.GetClassCodeString(classNames,queries );
            return classes;
        }
        public async Task<string> GetClasses()
        {
          return  await  GetClasses(
                [
                    "DBCustomer",
                    "DBToken",
                    "DBTranslation",
                    "DBOrder",
                    "DBOrderItem",
                    "DBProduct",
                    "DBCountry",
                    "DBOrderStatus",
                    "DBCategory",
                ],
                @"
                select * from public.customers;
                select * from public.tokens;
                select * from public.translations;
                select * from public.orders;
                select * from public.order_items;
                select * from public.products;
                select * from public.countries;
                select * from public.order_status;
                select * from public.categories;
                "
            );
        }
    }
}