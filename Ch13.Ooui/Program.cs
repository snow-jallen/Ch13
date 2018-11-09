using Ooui;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Ch13.Ooui
{
    class Program
    {
        static void Main(string[] args)
        {
            Forms.Init();
            UI.Publish("/", new MainPage().GetOouiElement());
#if DEBUG
            UI.Port = 12345;
            UI.Host = "localhost";
            Process.Start("explorer", $"http://{UI.Host}:{UI.Port}");
            Console.ReadLine();
#endif
        }
    }
}
