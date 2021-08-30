using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;

namespace KUCIS_OTP_Receiver
{
    public partial class Server : Form
    {
        Socket key_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        Socket client_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

        public Server()
        {
            InitializeComponent();            
        }

        public byte[] genHash()
        {
            using(var rijndael = Rijndael.Create())
            {
                rijndael.GenerateKey();
                //var key = Convert.ToBase64String(rijndael.Key);
                return rijndael.Key;
            }
        }

        public void print_monitor(string msg)
        {
            string before_socket_monitor_strings = socket_monitor.Text;

            if (!before_socket_monitor_strings.Equals(""))
                before_socket_monitor_strings += "\r\n";
            before_socket_monitor_strings += "[Receiver]: " + msg;
            socket_monitor.Text = before_socket_monitor_strings;
        }


        private void connectionButton_Click_1(object sender, EventArgs e)
        {
            print_monitor("Listen port number 4444..");
            Thread.Sleep(100);
            client_sock.Bind(new IPEndPoint(IPAddress.Any, 5555));
            client_sock.Listen(1);
            Socket transfer_sock = client_sock.Accept();
            
            print_monitor("Success! Connection starts..");

            Thread.Sleep(500);
            
            byte[] cipher_size = new byte[8];
            transfer_sock.Receive(cipher_size);

            byte[] cipher_text = new byte[BitConverter.ToInt32(cipher_size, 0)];
            print_monitor(BitConverter.ToInt32(cipher_size, 0).ToString());
            transfer_sock.Receive(cipher_text);
            /////////////////////////////////////////////////////////////////////
            byte[] sha256_size = new byte[8];
            transfer_sock.Receive(sha256_size);

            byte[] sha256Keys = new byte[BitConverter.ToInt32(sha256_size, 0)];            
            transfer_sock.Receive(sha256Keys);

            print_monitor("Ciphertext = " + Encoding.UTF8.GetString(cipher_text));
            print_monitor("Key = " + Encoding.UTF8.GetString(sha256Keys));

            byte[] plain_text = new byte[cipher_text.Length];
            for (int i = 0; i < plain_text.Length; i++)
                plain_text[i] = (byte)(cipher_text[i] ^ sha256Keys[i]);

            print_monitor("Plaintext = " + Encoding.UTF8.GetString(plain_text));
            /* ipLabel.Visible = true;
             ipBox.Visible = true;
             ipButton.Visible = true;
             connectionButton.Visible = false;*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = ipBox.Text;

            Regex ip_regex = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            Match match = ip_regex.Match(ip);

            if (!match.Success)
            {
                print_monitor("Invalid IP address. Try Again.");
                return;
            }
            
            key_sock.Connect(new IPEndPoint(IPAddress.Parse(ip), 4444));

            byte[] hash = genHash();
            print_monitor("Generating hash.. Success!");
            print_monitor("Sending hash!");

            key_sock.Send(hash);
            Thread.Sleep(500);
            key_sock.Close();

            ipLabel.Visible = false;
            ipBox.Visible = false;
            ipButton.Visible = false;
            connectionButton.Visible = true;        
        }       
    }
}
