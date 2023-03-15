namespace Logatron.Models.Logbook
{
    public class DatabaseManager
    {
        private readonly string _filename;

        public DatabaseManager(string filename)
        {
            _filename = filename;
        }

        public Database Context()
        {
            return new Database(_filename);
        }
    }
}
