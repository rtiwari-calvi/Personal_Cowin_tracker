using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;

namespace CowinCheck
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                try
                {
                    string json = run_cmd("python.exe",@"pyscript\script.py");
                    Root account = JsonConvert.DeserializeObject<Root>(json);
                    foreach (var cen in account.centers)
                    {
                        foreach (var ses in cen.sessions)
                        {
                            if (ses.available_capacity > 0)
                            {
                                Console.WriteLine(cen.name + " " + ses.available_capacity + " " + cen.pincode + " ");
                                var soundPlayer = new System.Media.SoundPlayer(@"pyscript\sound.wav");
                                while (true)
                                {
                                    soundPlayer.Play();
                                    Thread.Sleep(60000);
                                }
                                // run_cmd("cmd",  " start https://www.youtube.com/watch?v=Ya8aenp1QYs");
                                //break;
                            }
                        }
                    }
                }
                catch
                {

                }
                Thread.Sleep(300000);
            }
        }

        private static string run_cmd(string  exe,string cmd)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = exe;
            start.Arguments =  cmd;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            string result = string.Empty;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                     result = reader.ReadToEnd();
                   // Console.Write(result);
                }
            }

            return result;
        }
    }
}
