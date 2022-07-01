using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        const int PORT = 8008;
        const string IP_ADDR = "127.0.0.1";
        IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP_ADDR), PORT);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Form1()
        {
            InitializeComponent();
            this.Closing += Form1_Closing;
            try
            {
                socket.Connect(iPEnd);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          

        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {

            MessageBox.Show("Are you shure?");      

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (socket.Connected)
            {
                string str = "text";
                byte[] dataname = Encoding.Unicode.GetBytes(str);
                socket.Send(dataname);
                string msg = this.textBox1.Text;
                byte[] data = Encoding.Unicode.GetBytes(msg);
                socket.Send(data);

            }
            else
            {
                MessageBox.Show("Socket not connected");
            }

            this.textBox1.Text = string.Empty;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.label1.Text = dialog.FileName;

                }
            }   
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string str = this.label1.Text;
            string type = str.Substring(str.LastIndexOf('.'));
            if (socket.Connected)
            {
               
                    string filetype = type;
                    byte[] datafiletype = Encoding.Unicode.GetBytes(filetype);
                    socket.Send(datafiletype);
                    string filepath = this.label1.Text;

                    FileInfo file = new FileInfo(filepath);
                    string size = Convert.ToString(file.Length);
                    byte[] datasize = Encoding.Unicode.GetBytes(size);
                    socket.Send(datasize);

                    byte[] datafile = File.ReadAllBytes(filepath);
                    socket.Send(datafile);
                

            }
            else
            {
                MessageBox.Show("Socket not connected");
            }

            this.label1.Text = string.Empty;
        }
    }
}
