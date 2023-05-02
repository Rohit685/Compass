using System.Collections.Generic;
using Rage;

namespace Compass
{
    internal static class ScaleFormHelperMethods
    {
        private static Dictionary<Movie, GameFiber> tests = new Dictionary<Movie, GameFiber> { };
        internal static void Start(Movie x)
        {
            Stop(x);
            GameFiber f = GameFiber.StartNew(delegate
            {
                x.LoadAndWait();
                x.TestStart();
                while (true)
                {
                    GameFiber.Yield();
                    x.TestTick();
                    if (Game.IsKeyDown(System.Windows.Forms.Keys.End)) break;
                }
                x.TestEnd();
                x.Release();
                tests.Remove(x);
            }, "Scaleform Test");
            tests[x] = f;
        }
        internal static void Stop(Movie x)
        {
            if (!tests.ContainsKey(x)) return;
            if (tests[x] != null && tests[x].IsAlive)
            {
                tests[x].Abort();
                x.TestEnd();
                x.Release();
            }
            tests.Remove(x);
        }
        internal static void Stop()
        {
            foreach (var x in tests)
            {
                if (x.Value != null && x.Value.IsAlive)
                {
                    x.Value.Abort();
                    x.Key.TestEnd();
                    x.Key.Release();
                }
            }
            tests.Clear();
        }
    }
}