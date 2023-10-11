using System;
using System.IO;


namespace lolLauncher
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = "lolLauncher - A tool for unnecessary changes done by skilled devs...";
            if (File.Exists("ygeR.cfg"))
            {
                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "ygeR.cfg");
            }
            clsMain main = new clsMain();
            main.StartProcess();
        }
        
    }
}
