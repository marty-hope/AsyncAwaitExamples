using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitExamples
{
    internal class WriteFile
    {
        private readonly string _filePath;


        internal WriteFile(string filePath)
        {
            _filePath = filePath;

        }

        public async Task<string> AddLineToFileAsync(string line)
        {
            FileStream f = null;
            try
            {
                if (line.Length % 2 == 0)
                {
                    Thread.Sleep(1000);
                }
                var b = Encoding.ASCII.GetBytes($"{line}{Environment.NewLine}");
                f = new FileStream(_filePath, FileMode.Append,
                    FileAccess.Write, FileShare.None, 4096, true);
                await f.WriteAsync(b, 0, b.Length);
                f.Flush();
            }
            catch(Exception ex)

            {
                return $"{ex}";

            }
            finally
            {
                f?.Close();

            }
            return "Success";
        }

       
    }
}
