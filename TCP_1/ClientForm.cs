using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TCP_1
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            double a, b, x, y;
            if (double.TryParse(aTextBox.Text, out a) && double.TryParse(bTextBox.Text, out b) &&
                double.TryParse(xTextBox.Text, out x) && double.TryParse(yTextBox.Text, out y))
            {
                Client client = new Client("127.0.0.1", 8888); // Припустимо, що сервер працює на localhost з портом 8888.
                client.ConnectAndSendData(a, b, x, y);
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numbers.");
            }
        }

    }
}
