using System.Net.Sockets;

namespace port_scanner
{
    public partial class Form1 : Form
    {
        protected int startPort;
        protected int endPort;

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "192.168.0.1";
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startPort = Convert.ToInt32(numericUpDown1.Value);
            endPort = Convert.ToInt32(numericUpDown2.Value);

            progressBar1.Value = 0;
            progressBar1.Maximum = endPort - startPort + 1;

            Cursor.Current = Cursors.WaitCursor;
            textBox2.Clear();

            for (int i = startPort; i <= endPort; i++) 
            { 
                TcpClient tcpClient = new TcpClient();

                try
                {
                    tcpClient.Connect(textBox1.Text, i);
                    textBox2.AppendText("Port " + i + " open\r\n");
                } 
                catch
                {
                    textBox2.AppendText("Port " + i + " closed\r\n");
                }
                finally
                {
                    progressBar1.PerformStep();
                }
            }

            Cursor.Current = Cursors.Arrow;
        }
    }
}
