public class Seeder
{
    private static Seeder? _instance;
    public static Seeder Instance {
        get {
            if (_instance == null)
            {
                _instance = new Seeder();
            }
            return _instance;
        }
    }
   public bool DbExists()
    {
        return false;
    }
    public string DbSeed()
    {
        return "Seeded";
    }
    public string DbDrop()
    {
        return "Seeded";
    }
}