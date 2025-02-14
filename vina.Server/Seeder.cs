 using System.IO;
 using System.Reflection;
namespace vina.Server
{
    public class Seeder
    {
        public const string seed_script = "db.pgsql";
        public const string dbName = "vina_ivanic_hr";
        const string connString = @"Server=localhost;Port=5433;Username=n;Password=n;database={DATABASE};";
        string connStringDefaultDb = connString.Replace("{DATABASE}", "postgres");
        string connStringMyDb = connString.Replace("{DATABASE}", dbName);

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
        public async Task DbReCreate()
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
        public async Task<int> DbSeed()
        {
            var dBcs = new DBcs.DBcs(connStringMyDb);
            //create schema and data
            int ret = await dBcs.RunNonQueryAsync( await LoadScriptFromResource(seed_script));
            Console.Write($"Script {seed_script} executed.");
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
    }

}