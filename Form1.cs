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
            numericUpDown2.Value = 5;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            startPort = Convert.ToInt32(numericUpDown1.Value);
            endPort = Convert.ToInt32(numericUpDown2.Value);

            progressBar1.Value = 0;
            progressBar1.Maximum = endPort - startPort + 1;

            Cursor.Current = Cursors.WaitCursor;
            textBox2.Clear();
            label4.Text = "SCANNING...";

            for (int i = startPort; i <= endPort; i++)
            {
                await Task.Run(() =>
                {
                    TcpClient tcpClient = new TcpClient();
                    try
                    {
                        tcpClient.Connect(textBox1.Text, i);
                        Invoke(new Action(() => textBox2.AppendText($"Port {i} open\r\n")));
                    }
                    catch
                    {
                        Invoke(new Action(() => textBox2.AppendText($"Port {i} closed\r\n")));
                    }
                    finally
                    {
                        tcpClient.Close();
                        Invoke(new Action(() => progressBar1.PerformStep()));
                    }
                });
            }

            Cursor.Current = Cursors.Default;
            textBox2.AppendText("Pemindaian telah selesai.\r\n");
            label4.Text = "SELESAI";
        }
    }
}
