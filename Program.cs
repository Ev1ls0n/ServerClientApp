using System;

namespace ServerClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        private static void MainMenu()
        {
            Console.WriteLine("(i) Welcome to the test version of the client-server console application.");
            Console.WriteLine("Input the menu item number:\n1 - to use the app as a client\n2 - to use the app as a host\n3 - exit the app");
            Console.Write("Input: ");

            int menuItem;

            while (true)
            {
                bool isCorrect = Int32.TryParse(Console.ReadLine(), out menuItem);

                if (isCorrect)
                    break;

                Console.WriteLine("(!) Incorrect input. Please try again.");
            }

            Console.Clear();

            switch (menuItem)
            {
                case 1:
                    ClientController();
                    break;
                case 2:
                    HostController();
                    break;
                case 3:
                    return;
				// Забыл default
            }

            Console.ReadKey();
        }

        private static void ClientController()
        {
            string username = Console.ReadLine();

            Client client = new Client(username);
            client.Connect("127.0.0.1", 8888);

            while (true)
            {
                Console.WriteLine("(i) You can send messages to the host.");
                Console.Write("Message: ");
                string msg = Console.ReadLine();

                client.SendMessage(msg);
            }
        }

        private static void HostController()
        {
            Host host = new Host();
            host.HostStart(); // Старт работы хоста
        }
    }
}
