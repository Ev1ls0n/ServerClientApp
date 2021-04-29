using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerClientApp
{
    class Host
    {
        private const string ip = "127.0.0.1";
        private const int port = 8888;

        // Метод для запуска сервера
        internal void HostStart()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();

            ConnectionHandler(listener);
        }

        // Метод, который отвечает за присоединение к хосту новых клиентов
        private void ConnectionHandler(TcpListener listener)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("(i) Waiting for a connection...");

                    TcpClient client = listener.AcceptTcpClient(); // Подтверждение запроса на подключение
                    Console.WriteLine("(i) Connected!");

                    ThreadPool.QueueUserWorkItem(ThreadProc, client); // Открытие отдельного потока для нового клиента
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex}");
            }
            finally
            {
                listener.Stop();
            }
        }

        // Метод для обработки данных от клиента (взаимодействия с ним)
        private static void ThreadProc(object obj)
        {
            var client = (TcpClient)obj;

            NetworkStream networkStream = client.GetStream();

            byte[] bytes = new byte[256];
            string data;

            int i;
            while ((i = networkStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);
            }
        }
    }
}
