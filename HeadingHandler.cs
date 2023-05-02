using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System;
using System.Drawing;
using Rage.Attributes;

namespace Compass
{
    internal static class HeadingHandler
    {
        internal static ResText HeadingText { get; private set; } = new ResText("",new Point(0, 0), 1f, Color.FromArgb(255, Color.White), Common.EFont.Monospace, ResText.Alignment.Left)
        {
            Outline = true
        };

        internal static void Start()
        {
            SetPosition();
            Game.FrameRender += FrameRender;
            HeadingText.Draw();
            GameFiber.StartNew(Display);
        }

        internal static void Stop()
        {
            Game.FrameRender -= FrameRender;
        }

        internal static void FrameRender(object sender, GraphicsEventArgs e)
        {
            if (!Game.IsPaused)
            {
                HeadingText.Draw();
            }
        }


        internal static void Display()
        {
            while (true)
            {
                GameFiber.Yield();
                if (!Game.IsPaused)
                {
                    HeadingText.Caption = Math.Abs(EntryPoint.Compass.Heading).ToString();
                }
            }
        }
        internal static void SetPosition()
        {
            HeadingText.Position = new Point(953, 90);
            HeadingText.Scale = (float)69 / 100f;
        }

        [ConsoleCommand]
        private static void SetPosition(int x, int y)
        {
            HeadingText.Position = new Point(x, y);
        }
        [ConsoleCommand]
        private static void SetScale(int x)
        {
            HeadingText.Scale = (float)x / 100f;
        }
    }
}