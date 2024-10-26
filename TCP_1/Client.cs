using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_1
{
    public class Client
    {
        private TcpClient client;
        private string serverIpAddress;
        private int serverPort;

        public Client(string ipAddress, int port)
        {
            serverIpAddress = ipAddress;
            serverPort = port;
        }

        public void ConnectAndSendData(double a, double b, double x, double y)
        {
            try
            {
                client = new TcpClient(serverIpAddress, serverPort);

                NetworkStream stream = client.GetStream();

                string data = $"{a},{b},{x},{y}";
                byte[] bytes = Encoding.ASCII.GetBytes(data);

                // Відправка даних на сервер.
                stream.Write(bytes, 0, bytes.Length);

                // Отримання результату від сервера.
                bytes = new byte[256];
                int bytesRead = stream.Read(bytes, 0, bytes.Length);
                string result = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                // Виведення результату на форму.
                ShowResult(result);

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ShowResult(string result)
        {
            // Виведення результату на форму.
            // Припустимо, що у вашій формі є контрол з ім'ям resultLabel.
            // Ви можете змінити це ім'я відповідно до вашого реалізації.
            // resultLabel.Text = result;
            MessageBox.Show(result, "Result");
        }
    }
}
