using Rage;
using System.Windows.Forms;
namespace Compass
{
    internal class Settings
    {
        internal static string Heading = EntryPoint.HeadingOrigin.CAMERA.ToString();
        internal static InitializationFile iniFile;
        internal static void Initialize()
        {
            try
            {
                iniFile = new InitializationFile(@"Plugins/Compass.ini");
                iniFile.Create();
                Heading = iniFile.ReadString("Customization", "Heading", Heading);
            }
            catch(System.Exception e)
            {
                string error = e.ToString();
                Game.LogTrivial("Compass: ERROR IN 'Settings.cs, Initialize()': " + error);
                Game.DisplayNotification("Compass: Error Occured");
            }
        }
        internal static void UpdateINI()
        {
            try
            {
                iniFile.Write("Customization", "Heading", Heading);
            }
            catch (System.Exception ex)
            {
                Game.LogTrivial(ex.ToString());
            }
        }
    }
}