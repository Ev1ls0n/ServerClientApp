using System;
using System.Text;
using System.Net.Sockets;

namespace ServerClientApp
{
    class Client
    {
        internal string ClientName { get; private set; } // Имя клиента
        internal bool IsConnected { get; private set; } = false; // Соединён ли клиент с хостом

        private NetworkStream networkStream; // Поток клиента для обмена данными с хостом
        
        internal Client(string clientName)
        {
            ClientName = clientName;
        }

        // Метод, который отвечает за соединение клиента с хостом
        internal void Connect(string hostIP, int hostPort)
        {
            try
            {
                TcpClient client = new TcpClient(hostIP, hostPort);
                IsConnected = true;

                byte[] data = Encoding.ASCII.GetBytes($"(i) Client: {ClientName} - connected."); // Конвертация текстового сообщения в байты

                networkStream = client.GetStream(); // Получение потока клиента
                networkStream.Write(data, 0, data.Length); // Отправка сообщения хосту

                Console.WriteLine("(i) Host connection established.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"ArgumentNullException: {ex}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: {ex}");
            }
        }

        // Метод для отправки текстовых сообщений от клиента к хосту
        internal void SendMessage(string message)
        {
            // Если клиент присоединён к какому-то хосту:
            if (IsConnected)
            {
                byte[] data = Encoding.ASCII.GetBytes(message); // Преобразование текстового сообщения в байты

                networkStream.Write(data, 0, data.Length); // Отправка сообщения серверу
                Console.WriteLine($"> Sent: {message}"); // Вывод отправленного сообщения на консоль клиента (пользователя)
            }
            else
                Console.WriteLine("(!) Message not sent. No connection to host.");
        }
    }
}
