using System;
using Rage;
using Rage.Attributes;

[assembly: Rage.Attributes.Plugin("Compass", Author = "Roheat",PrefersSingleInstance = true,Description = "Compass for singleplayer")]
namespace Compass
{
    
    internal static class EntryPoint
    {
        internal static bool parseSuccessful;
        internal static Compass Compass;
        internal static void Main()
        {
            Compass = new Compass();
            ScaleFormHelperMethods.Start(Compass);
            Settings.Initialize();
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
    }
}