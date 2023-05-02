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
        internal static Compass Compass;
        internal static void Main()
        {
            Compass = new Compass();
            ScaleFormHelperMethods.Start(Compass);
            Settings.Initialize();
            parseSuccessful =Enum.TryParse(Settings.HeadingOrigin, true, out chosenOrigin);
            CheckParse();
            HeadingHandler.Start();
            
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
                Game.DisplayNotification("commonmenu", "shop_tick_icon", "Compass", "~b~Heading origin changed", "Switched to ~y~PLAYER~y~");
                Settings.HeadingOrigin = HeadingOrigin.PLAYER.ToString();
                Settings.UpdateINI();
            }
            else
            {
                chosenOrigin = HeadingOrigin.CAMERA;
                Game.DisplayNotification("commonmenu", "shop_tick_icon", "Compass", "~b~Heading origin changed", "Switched to ~y~CAMERA~y~");
                Settings.HeadingOrigin = HeadingOrigin.CAMERA.ToString();
                Settings.UpdateINI();
            }
        }
    }
}