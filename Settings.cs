using Rage;
using System.Windows.Forms;
namespace Compass
{
    internal class Settings
    {
        internal static string HeadingOrigin = EntryPoint.HeadingOrigin.CAMERA.ToString();
        internal static int Scale = 30;
        internal static int PosX = 953;
        internal static int PosY = 90;
        internal static InitializationFile iniFile;
        internal static void Initialize()
        {
            try
            {
                iniFile = new InitializationFile(@"Plugins/Compass.ini");
                iniFile.Create();
                HeadingOrigin = iniFile.ReadString("Customization", "Heading", HeadingOrigin);
                PosX = iniFile.ReadInt32("Customization", "PosX", PosX);
                PosY = iniFile.ReadInt32("Customization", "PosY", PosY);
                Scale = iniFile.ReadInt32("Customization", "Scale", Scale);
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
                iniFile.Write("Customization", "Heading", HeadingOrigin);
                iniFile.Write("Customization", "PosX", PosX);
                iniFile.Write("Customization", "PosY", PosY);
                iniFile.Write("Customization", "Scale", Scale);
            }
            catch (System.Exception ex)
            {
                Game.LogTrivial(ex.ToString());
            }
        }
    }
}