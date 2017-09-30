using System;
using System.IO;
using System.Threading.Tasks;

namespace _2.SliceFile
{
    public class SliceFile
    {
        public static void Main(string[] args)
        {
            string videoPath = Console.ReadLine();
            string destination = Console.ReadLine();
            int pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destination, pieces);

            Console.WriteLine("Anything else?");
            while (true) Console.ReadLine();

        }

        private static void SliceAsync(string videoPath, string destination, int pieces)
        {
            Task.Run(() =>
           {
               Slice(videoPath, destination, pieces);
           });
        }

        private static void Slice(string sourceFile , string destinationPath,int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourceFile,FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(sourceFile);
                long partLenght = (source.Length / parts) + 1;
                long currentByte = 0;

                for (int currentPart = 1; currentPart < parts; currentPart++)
                {
                    string filePath = $"{destinationPath}/Part-{currentPart}{fileInfo.Extension}";

                    using (var destination = new FileStream(filePath,FileMode.Create))
                    {
                        byte[] buffer = new byte[1024];
                        while (currentByte <= partLenght*currentPart)
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }

                    Console.WriteLine("Slice Complete");
                }
            }
        }
    }
}
