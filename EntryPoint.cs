using System;
using Rage;
using Rage.Attributes;

[assembly: Rage.Attributes.Plugin("Compass", Author = "Roheat",PrefersSingleInstance = true,Description = "Compass for singleplayer")]
namespace Compass
{
    
    internal static class EntryPoint
    {
        internal enum HeadingOrigin
        {
            PLAYER,
            CAMERA
        }
        internal static bool parseSuccessful;
        internal static HeadingOrigin chosenOrigin;
        
        internal static void Main()
        {
            DroneCam Compass = new DroneCam();
            ScaleFormHelperMethods.Start(Compass);
            parseSuccessful =Enum.TryParse(Settings.Heading, true, out chosenOrigin);
            CheckParse();
            while (true)
            {
                GameFiber.Yield();
            }
        }

        internal static void OnUnload(bool Exit)
        {
            ScaleFormHelperMethods.Stop();
        }
        internal static void CheckParse()
        {
            if (!parseSuccessful)
            {
                chosenOrigin = HeadingOrigin.CAMERA;
            }
        }

        [ConsoleCommand]
        private static void ChangeHeadingOrigin()
        {
            if (chosenOrigin == HeadingOrigin.CAMERA)
            {
                chosenOrigin = HeadingOrigin.PLAYER;
                Settings.Heading = HeadingOrigin.PLAYER.ToString();
                Settings.UpdateINI();
            }
            else
            {
                chosenOrigin = HeadingOrigin.CAMERA;
                Settings.Heading = HeadingOrigin.CAMERA.ToString();
                Settings.UpdateINI();
            }
        }
    }
}