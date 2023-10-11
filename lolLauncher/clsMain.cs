using Core;
using System;
using System.Threading;

namespace lolLauncher
{
    public class clsMain
    {
        clsWriter w = new clsWriter();
        ygeR h = new ygeR();


        public string printLogo =
 @"
                         __________ 
      ___.__. ____   ____\______   \
     <   |  |/ ___\_/ __ \|       _/
      \___  / /_/  >  ___/|    |   \
      / ____\___  / \___  >____|_  /
      \/   /_____/      \/  2023 \/ 

     =================================
    | lolLauncher - Language Selector |
     =================================

";

        #region BINARY CHECKS
        void checkGameBinary()
        {
            if (h.ReadFromConfig("gameBinary") == "null")
            {
                w.WriteLine("d", "{FC=DarkYellow}Could not find path for game binary.{/FC}");
                gameBinaryNotExist();
            }
            else
            {

                w.WriteLine("i", "{FC=Green}Game binary found. Proceeding with Overwolf binary check.{/FC}");
                checkOverwolfBinary();
            }

        }
        void checkOverwolfBinary()
        {
            if (h.ReadFromConfig("overwolfBinary") == "null")
            {
                w.WriteLine("d", "{FC=DarkYellow}Could not find path for Overwolf binary.{/FC}");
                overwolfBinaryNotExist();
            }
            else
            {
                w.WriteLine("i", "{FC=Green}Overwolf binary found. Attempting to run executables with parameters....{/FC}");
                runApplications();
            }
        }
        void gameBinaryNotExist()
        {
            Console.Clear();
            Console.WriteLine(printLogo);
            w.WriteLine("d", "{FC=DarkYellow}Unable to verify game binary.{/FC}");
            w.WriteLine("i", "{FC=Yellow}To save binary write down executable name including extension below." + Environment.NewLine + "Example: LeaugeClient.exe{/FC}");
            string binaryName = Console.ReadLine();
            try
            {
                h.SaveToConfig("gameBinary", binaryName);
                w.WriteLine("i", "{FC=Green}Saved.{/FC}");
                w.WriteLine("i", "{FC=Yellow}Press a key to continue...{/FC}");
                Console.ReadLine();
                overwolfBinaryNotExist();
                return;
            }
            catch (Exception ex)
            {

                w.WriteLine("e", "{FC=Red}" + ex.Message + "{/FC}");
            }
        }
        void overwolfBinaryNotExist()
        {
            Console.Clear();
            Console.WriteLine(printLogo);
            w.WriteLine("d", "{FC=DarkYellow}Unable to verify overwolf binary.{/FC}");
            w.WriteLine("i", "{FC=Yellow}To save binary location, please enter FULL PATH + binary." + Environment.NewLine + "Example: C:\\Program Files(x86)\\Overwolf\\OverwolfLauncher.exe{/FC}");
            string binary = Console.ReadLine();
            try
            {
                h.SaveToConfig("overwolfBinary", binary);
                w.WriteLine("i", "{FC=Green}Saved.{/FC}");
                w.WriteLine("d", "{FC=Yellow}Please restart application {/FC}");
                Console.ReadLine();
                writeNewConfig();
            }
            catch (Exception ex)
            {
                w.WriteLine("e", "{FC=Red}" + ex.Message + "{/FC}");
            }

        }
        #endregion

        void writeNewConfig()
        {
            w.WriteLine("d", "{FC=Yellow}Writing config to file...{/FC}");

            if (h.FileExists("ygeR.cfg"))
            {
                return;
            }
            else
            {
                try
                {
                    h.FileCopy("lolLauncher.exe.config", "ygeR.cfg");
                    h.FileDelete("lolLauncher.exe.config");
                    w.WriteLine("i", "{FC=Green}Config file created. Please restart application.{/FC}");
                }
                catch (Exception ex)
                {
                    w.WriteLine("E", "{FC=Red}"+ex.Message+"{/FC}");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

            }
        }
        public void StartProcess()
        {
            Console.WriteLine(printLogo);
            checkGameBinary();
        }
        void runApplications()
        {
            try
            {

                w.WriteLine("i", "{FC=White}Runing with args: Overwolf.exe{/FC}");
                h.ProcessStartArgs(h.ReadFromConfig("overwolfBinary"), h.ReadFromConfig("overwolfParam"));
                w.WriteLine("d", "{FC=Yellow}Executed: Overwolf.exe{/FC}");
                w.WriteLine("i", "{FC=White}Runing with args: LeagueClient.exe{/FC}");
                h.ProcessStartArgs(h.ReadFromConfig("gameBinary"), h.ReadFromConfig("gameLanguage"));
                w.WriteLine("d", "{FC=Yellow}Executed: LeagueClient.exe{/FC}");
                launcherDone();

            }
            catch (Exception ex)
            {

                w.WriteLine("e,", "{FC=Yellow}" + ex.Message + "{/FC}");
                Console.ReadLine();
            }

        }
        void launcherDone()
        {
            Console.WriteLine("Exiting in 3...");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }



    }
}