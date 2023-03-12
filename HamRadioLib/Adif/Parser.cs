using M0LTE.AdifLib;

namespace HamRadioLib.Adif
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
