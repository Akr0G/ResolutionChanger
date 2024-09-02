using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace ResolutionChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration
            var config = LoadConfiguration();

            // Get settings from the configuration
            int desktopWidth = config["DesktopResolution"]["Width"].Value<int>();
            int desktopHeight = config["DesktopResolution"]["Height"].Value<int>();
            int targetWidth = config["TargetResolution"]["Width"].Value<int>();
            int targetHeight = config["TargetResolution"]["Height"].Value<int>();
            string[] targetProcesses = config["TargetProcesses"].ToObject<string[]>();

            // Track whether the resolution has been changed
            bool isAnyTargetRunning = false;

            Console.WriteLine("Press 'Q' to quit.");

            while (true)
            {
                try
                {
                    // Check for key press to exit
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                    {
                        Console.WriteLine("Exiting program...");
                        break;
                    }

                    // Check if any of the target processes are running
                    bool anyTargetRunning = targetProcesses.Any(processName =>
                    {
                        try
                        {
                            return Process.GetProcessesByName(processName).Length > 0;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error checking process {processName}: {ex.Message}");
                            return false;
                        }
                    });

                    if (anyTargetRunning)
                    {
                        if (!isAnyTargetRunning) // Only change resolution if it's not already changed
                        {
                            Console.WriteLine("One of the target applications is running. Changing resolution to 1720x1080.");
                            ResolutionHelper.ChangeResolution(targetWidth, targetHeight);
                            isAnyTargetRunning = true;
                        }
                    }
                    else
                    {
                        if (isAnyTargetRunning) // Only restore resolution if it was changed before
                        {
                            Console.WriteLine("None of the target applications are running. Restoring resolution to 2560x1440.");
                            ResolutionHelper.ChangeResolution(desktopWidth, desktopHeight);
                            isAnyTargetRunning = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Wait for 5 seconds before checking again
                Thread.Sleep(5000);
            }
        }

        static JObject LoadConfiguration()
        {
            string configFile = "config.json";
            if (!File.Exists(configFile))
            {
                Console.WriteLine($"Configuration file {configFile} not found.");
                Environment.Exit(1);
            }

            string json = File.ReadAllText(configFile);
            return JObject.Parse(json);
        }
    }
}
