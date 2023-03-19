using M0LTE.AdifLib;

namespace Logatron.Adif
{
    public class Parser
    {
        public static File Parse(string file)
        {
            var a = new AdifFile();
            bool successful = AdifFile.TryParse(file, out a);

            File newFile = new();

            return newFile;
        }
    }
}
