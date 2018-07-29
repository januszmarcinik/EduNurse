namespace EduNurse.Exams
{
    internal static class Settings
    {
        public static string ConnectionString { get; private set; }
        public static bool UseInMemory { get; private set; }

        public static void Initialize(string connectionString, bool useInMemory)
        {
            ConnectionString = connectionString;
            UseInMemory = useInMemory;
        }
    }
}
