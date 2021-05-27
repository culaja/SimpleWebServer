using System;
using System.Diagnostics;

namespace WebApplication.Data
{
    public sealed class CpuTemperatureService
    {
        public decimal? TryRead()
        {
            try
            {
                return ReadInternal();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        private decimal? ReadInternal()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cat",
                    Arguments = "/sys/class/thermal/thermal_zone0/temp",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return process.ExitCode == 0
                ? int.Parse(result) / 1000m
                : null;
        }
    }
}