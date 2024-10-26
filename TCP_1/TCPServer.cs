using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace TCP_1
{
    public class TCPServer
    {
        private TcpListener server;

        public TCPServer()
        {
            server = null;
        }

        public void Start()
        {
            try
            {
                // Прослуховувати порт 8888.
                Int32 port = 8888;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);
                server.Start();

                while (true)
                {
                    // Очікування підключення клієнта.
                    MessageBox.Show("Waiting for a connection...");

                    using (TcpClient client = server.AcceptTcpClient())
                    {
                        MessageBox.Show("Connected!");

                        ProcessClient(client);
                    }
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show($"SocketException: {e}");
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }

            MessageBox.Show("\nHit enter to continue...");
            Console.Read();
        }

        private void ProcessClient(TcpClient client)
        {
            Byte[] bytes = new Byte[256];
            String data = null;

            NetworkStream stream = client.GetStream();

            int i;

            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                // Обробити дані, отримані від клієнта і відправити відповідь.
                string result = ProcessData(data);

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(result);

                stream.Write(msg, 0, msg.Length);
            }
        }

        // Функція для обробки отриманих даних і обчислення ряду.
        private string ProcessData(string data)
        {
            // Розбиваємо дані на окремі значення.
            string[] values = data.Split(',');

            // Парсимо отримані значення.
            double a = double.Parse(values[0]);
            double y = double.Parse(values[1]);
            double x = double.Parse(values[2]);
            double b = double.Parse(values[3]);

            // Обчислюємо результат.
            double result = CalculateSeries(a, y, x, b);

            // Повертаємо результат у вигляді рядка.
            return result.ToString();
        }

        // Функція для обчислення ряду.
        private double CalculateSeries(double a, double y, double x, double b)
        {
            double result = 0;

            for (int i = 0; i < 10; i++)
            {
                double numerator = Math.Pow(-1, i) * (Math.Log(x) + b * y + a * Math.Pow(x, 2));
                double denominator = Factorial(i) * Math.E;

                result += numerator / denominator;
            }

            return result;
        }

        // Функція для обчислення факторіала.
        private int Factorial(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }
    }
}
