using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitExamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string directoryPath = "C:\\logs";
            try
            {
                if (!Directory.Exists($"{directoryPath}"))
                {
                    Directory.CreateDirectory($"{directoryPath}");

                }
                var list = args.ToList();
                var fileName = $"{directoryPath}\\sample.log";
                var tasks = new List<Task<string>>();
                foreach (var s in list)
                {

                    var writeFile = new WriteFile($"{fileName}");
                    tasks.Add(writeFile.AddLineToFileAsync(s));
                }
                foreach (var t in tasks)
                {
                    if (t.IsCompleted)
                    {
                        Console.WriteLine(t.Result);
                    }
                }
                var result = Task.WhenAll(tasks);
                Process.Start("notepad.exe", fileName);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
