using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer_Winforms
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 8008;
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            DirectoryInfo di = Directory.CreateDirectory(@"C:\Users\1\OneDrive\Рабочий стол\Servak");
            Console.WriteLine("SERVER START");
            int id = 1;

            try
            {
                socket.Bind(iPEnd);
                socket.Listen(10);

                Socket clientSocket = socket.Accept();
                while (clientSocket.Connected)
                {
                    
                    Console.WriteLine("SERVER CATCH");
                    int byteCount = 0;
                    byte[] buffer = new byte[256];
                    StringBuilder stringBuilder = new StringBuilder();
                    do
                    {
                        byteCount = clientSocket.Receive(buffer);
                        stringBuilder.Append(Encoding.Unicode.GetString(buffer, 0, byteCount));
                    } while (clientSocket.Available > 0);
                    string rez = stringBuilder.ToString();
                    if (rez.Equals("text"))
                    {
                        Console.WriteLine("SERVER CATCH TEXT");
                        int byteCount2 = 0;
                        byte[] buffer2 = new byte[256];
                        StringBuilder stringBuilder2 = new StringBuilder();
                        do
                        {
                            byteCount2 = clientSocket.Receive(buffer2);
                            stringBuilder2.Append(Encoding.Unicode.GetString(buffer2, 0, byteCount2));
                        } while (clientSocket.Available > 0);

                        string rez2 = stringBuilder2.ToString();
                        string filename = $"\\{DateTime.Now.ToShortDateString()}{iPEnd.Address.ToString()}MESAGGES.txt";
                        File.AppendAllText(di.FullName + filename, rez2 + "\n");
                    }
                    if (rez.Equals(".txt"))
                    {
                        Console.WriteLine("SERVER CATCH FILE TXT");
                        int byteCount2 = 0;
                        byte[] buffer2 = new byte[256];
                        StringBuilder sizeBuilder = new StringBuilder();
                        do
                        {
                            byteCount2 = clientSocket.Receive(buffer2);
                            sizeBuilder.Append(Encoding.Unicode.GetString(buffer2, 0, byteCount2));

                        } while (clientSocket.Available > 0);

                        long size = Convert.ToUInt32(sizeBuilder.ToString());

                        int byteCount3 = 0;
                        byte[] buffer3 = new byte[size];
                        do
                        {
                            byteCount3 = clientSocket.Receive(buffer3);

                        } while (clientSocket.Available > 0)
                        ;
                        string filename = $"\\{DateTime.Now.ToShortDateString()}{iPEnd.Address.ToString()}copyTXT.txt";
                        if (File.Exists(di.FullName + filename))
                        {
                            filename = $"\\{DateTime.Now.ToShortDateString()}{iPEnd.Address.ToString()}copyTXT{id}.txt";
                            File.WriteAllBytes(di.FullName + filename, buffer3);
                            id++;
                        }
                        else

                        { File.WriteAllBytes(di.FullName + filename, buffer3); }
                    }
                    if (rez.Equals(".png"))
                    {
                        Console.WriteLine("SERVER CATCH FILE PNG");
                        int byteCount2 = 0;
                        byte[] buffer2 = new byte[256];
                        StringBuilder sizeBuilder = new StringBuilder();
                        do
                        {
                            byteCount2 = clientSocket.Receive(buffer2);
                            sizeBuilder.Append(Encoding.Unicode.GetString(buffer2, 0, byteCount2));

                        } while (clientSocket.Available > 0);
                        long size = Convert.ToUInt32(sizeBuilder.ToString());

                        int byteCount3 = 0;
                        byte[] buffer3 = new byte[size];

                        do
                        {
                            byteCount3 = clientSocket.Receive(buffer3);

                        } while (clientSocket.Available > 0);
                        string filename = $"\\{DateTime.Now.ToShortDateString()}{iPEnd.Address.ToString()}CopyPNG.png";
                        if (File.Exists(di.FullName + filename))
                        {
                            filename = $"\\{DateTime.Now.ToShortDateString()}{iPEnd.Address.ToString()}CopyPNG{id}.png";
                            File.WriteAllBytes(di.FullName + filename, buffer3);
                            id++;
                        }
                        else

                        { File.WriteAllBytes(di.FullName + filename, buffer3); }

                    }
                    if (rez.Equals(".jpg"))
                    {
                        Console.WriteLine("SERVER CATCH FILE JPG");
                        int byteCount2 = 0;
                        byte[] buffer2 = new byte[256];
                        StringBuilder sizeBuilder = new StringBuilder();
                        do
                        {
                            byteCount2 = clientSocket.Receive(buffer2);
                            sizeBuilder.Append(Encoding.Unicode.GetString(buffer2, 0, byteCount2));

                        } while (clientSocket.Available > 0);
                        long size = Convert.ToUInt32(sizeBuilder.ToString());

                        int byteCount3 = 0;
                        byte[] buffer3 = new byte[size];

                        do
                        {
                            byteCount3 = clientSocket.Receive(buffer3);

                        } while (clientSocket.Available > 0);

                        string filename = $"\\{DateTime.Now.ToShortDateString()}{iPEnd.Address.ToString()}CopyJPG.jpg";
                        if (File.Exists(di.FullName + filename))
                        {
                             filename = $"\\{DateTime.Now.ToShortDateString()}{iPEnd.Address.ToString()}CopyJPG{id}.jpg";
                            File.WriteAllBytes(di.FullName + filename, buffer3);
                            id++;
                        }
                        else

                        { File.WriteAllBytes(di.FullName + filename, buffer3); }

                    }

                }
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("SERVER END");
            Console.ReadKey();
        }
    }
}
