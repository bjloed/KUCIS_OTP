using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace KUCIS_OTP
{
    public partial class Client : Form
    {
        Socket key_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        Socket sender_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        byte[] receiveKey = new byte[32];

        public Client()
        {
            InitializeComponent();
            key_sock.Bind(new IPEndPoint(IPAddress.Any, 4444));
            key_sock.Listen(1);
            Socket transfer_sock = key_sock.Accept();

            transfer_sock.Receive(receiveKey);

            print_monitor(Encoding.UTF8.GetString(receiveKey));
            print_monitor("The key exchange was successful!");
            transfer_sock.Close();
            key_sock.Close();
        }

        public void print_monitor(string msg)
        {
            string before_socket_monitor_strings = socket_monitor.Text;

            if (!before_socket_monitor_strings.Equals(""))
                before_socket_monitor_strings += "\r\n";
            before_socket_monitor_strings += "[Sender]: " + msg;
            socket_monitor.Text = before_socket_monitor_strings;
        }

        private void socket_button_Click(object sender, EventArgs e)
        {
            string ip = ip_textbox.Text;
            byte[] receiveKey = new byte[256];
            int receiveKeyCount = 0;

            Regex ip_regex = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            Match match = ip_regex.Match(ip);

            if (!match.Success)
            {
                print_monitor("Invalid IP address. Try Again.");
                return;
            }                                   

            sender_sock.Connect(new IPEndPoint(IPAddress.Parse(ip), 5555));
            
            print_monitor("Success! Connection starts..");
            print_monitor("Please upload the file or text. The priority is higher for file path.");                           

            filepathBox.Visible = true;
            filepathLabel.Visible = true;
            textBox.Visible = true;
            textLabel.Visible = true;
            okButton.Visible = true;
        }             

        /*public void GetXORkey(byte[] hash, byte[] data, int[] xorKey)
        {
            int[] tempData = data.Select(x => (int)x).ToArray();
            int[] tempHash = hash.Select(y => (int)y).ToArray();

            int[] tempXOR = new int[32];

            for (int i = 0; i < 32; i++)
                tempXOR[i] = tempHash[i] ^ tempData[i];

            Array.Copy(xorKey, 0, tempXOR, 0, 32);
        }*/

        public byte[] SHA256Hashing(byte[] hash)
        {
            byte[] tempBuffer = new byte[hash.Length];
            Buffer.BlockCopy(hash, 0, tempBuffer, 0, tempBuffer.Length);

            SHA256Managed sha256Managed = new SHA256Managed();
            byte[] encryptBytes = sha256Managed.ComputeHash(tempBuffer);

            return encryptBytes;
        }

        /*public byte[] HMACSHA256Hashing(byte[] sha256Key, byte[] buffer, int whence)
        {
            byte[] tempBuffer = new byte[32];

            for(int i=0; i<32; i++)
            {
                tempBuffer[i] = buffer[whence + i];
            }

            var hmac_key = sha256Key;
            var timeStamp = DateTime.UtcNow;
            var timeSpan = (timeStamp - new DateTime(1970, 1, 1, 0, 0, 0));
            var hmac_timeStamp = (long)timeSpan.TotalMilliseconds;

            using(HMACSHA256 sha = new HMACSHA256(hmac_key))
            {
                var bytes = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(buffer) + hmac_timeStamp);
                string base64 = Convert.ToBase64String(bytes);
                var message = Encoding.UTF8.GetBytes(base64);

                var hashing = sha.ComputeHash(message);
                return hashing;
            }
        }*/

        private void file_read(string path)
        {                        
            byte[] buffer = File.ReadAllBytes(@path);
            int paddingLen;
            int totalLength = buffer.Length;

            if(buffer.Length % 32 != 0)
            {
                int power = 32;
                while(true)
                {
                    if (power > buffer.Length)
                        break;
                    power += 32;
                }

                paddingLen = power - buffer.Length;

                byte[] paddingByte = new byte[paddingLen]; //padding byte size
                byte[] paddingBuffer = new byte[power]; //최종 버퍼 size

                for(int i=0; i < paddingLen; i++)
                {
                    paddingByte[i] = 0x0;
                }

                Array.Copy(buffer, paddingBuffer, buffer.Length);
                Array.Copy(paddingByte, 0, paddingBuffer, buffer.Length, paddingByte.Length);

                totalLength = paddingBuffer.Length;
                buffer = paddingBuffer;
            }

            ////////////////////////////////////////////////////////////////////////////////            
            byte[,] totalCipher = new byte[totalLength / 32, 32]; //전체 암호문을 담는 Ci들            
            int[,] totalSHAKey = new int[totalLength / 32, 32]; //전체 XOR 연산을 할 Key를 담는 배열 Ki들   
            
            for (int i = 0; i < 32; i++) //함수 대체 (getXORkey)
                totalCipher[0, i] = (byte)(receiveKey[i] ^ buffer[i]); //첫 C0 얻는 로직
            for (int i = 0; i < 32; i++)
                totalSHAKey[0, i] = receiveKey[i]; //첫 K0 얻는 로직

            ////////////////////////////////////////////////////////////////////////////////

            int whence = 32;

            for (int loop = 1; loop < totalLength / 32; loop++) //Ki 생성 로직 
            {
                byte[] sha256Key;
                byte[] tempsha256Key = new byte[32];

                if (loop == 1)
                    sha256Key = SHA256Hashing(receiveKey); //sha256 hashing key Ki
                else
                {
                    for (int i = 0; i < 32; i++)
                        tempsha256Key[i] = (byte)totalSHAKey[loop - 1, i];
                    sha256Key = SHA256Hashing(tempsha256Key);
                }

                for (int i = 0; i < 32; i++)
                    totalSHAKey[loop, i] = sha256Key[i];

                for (int i = 0; i < 32; i++)
                    totalCipher[loop, i] = (byte)(totalSHAKey[loop, i] ^ buffer[i + whence]);

                whence += 32;
            }

            byte[] realFinalCipher = new byte[totalCipher.Length];
            for (int i = 0; i < totalLength / 32; i++)
            {
                for (int q = 0; q < 32; q++)
                    realFinalCipher[i * 32 + q] = totalCipher[i, q];
            }

            byte[] sendSHAkeys = new byte[totalSHAKey.Length];
            for (int i = 0; i < totalLength / 32; i++)
            {
                for (int q = 0; q < 32; q++)
                    sendSHAkeys[i * 32 + q] = (byte)totalSHAKey[i, q];
            }
  
            sender_sock.Send(BitConverter.GetBytes(realFinalCipher.Length));
            sender_sock.Send(realFinalCipher); //send cipher text
            Thread.Sleep(50);
            sender_sock.Send(BitConverter.GetBytes(sendSHAkeys.Length));
            sender_sock.Send(sendSHAkeys); //send sha keys                       
        }

        private void text_send(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            int paddingLen;
            int totalLength = buffer.Length;

            if (buffer.Length % 32 != 0)
            {
                int power = 32;
                while (true)
                {
                    if (power > buffer.Length)
                        break;
                    power += 32;
                }

                paddingLen = power - buffer.Length;

                byte[] paddingByte = new byte[paddingLen]; //padding byte size
                byte[] paddingBuffer = new byte[power]; //최종 버퍼 size

                for (int i = 0; i < paddingLen; i++)
                {
                    paddingByte[i] = 0x0;
                }

                //Array.Copy(buffer, 0, paddingByte, 0, buffer.Length);
                Array.Copy(buffer, paddingBuffer, buffer.Length);
                Array.Copy(paddingByte, 0, paddingBuffer, buffer.Length, paddingByte.Length);
                //Array.Copy(paddingByte, 0, paddingBuffer, 0, paddingByte.Length);

                totalLength = paddingBuffer.Length;
                buffer = paddingBuffer;
                //print_monitor(Encoding.UTF8.GetString(paddingBuffer));
            }
            
            ////////////////////////////////////////////////////////////////////////////////            
            byte[,] totalCipher = new byte[totalLength / 32, 32]; //전체 암호문을 담는 Ci들            
            int[,] totalSHAKey = new int[totalLength / 32, 32]; //전체 XOR 연산을 할 Key를 담는 배열 Ki들   

            for (int i = 0; i < 32; i++) //함수 대체 (getXORkey)
                totalCipher[0, i] = (byte)(receiveKey[i] ^ buffer[i]); //첫 C0 얻는 로직
            for (int i = 0; i < 32; i++)
                totalSHAKey[0, i] = receiveKey[i]; //첫 K0 얻는 로직

            print_monitor(Encoding.UTF8.GetString(buffer));
            ////////////////////////////////////////////////////////////////////////////////

            int whence = 32;

            for (int loop = 1; loop < totalLength / 32; loop++) //Ki 생성 로직 
            {
                byte[] sha256Key;
                byte[] tempsha256Key = new byte[32];

                if (loop == 1)
                    sha256Key = SHA256Hashing(receiveKey); //sha256 hashing key Ki
                else
                {
                    for (int i = 0; i < 32; i++)
                        tempsha256Key[i] = (byte)totalSHAKey[loop - 1, i];
                    sha256Key = SHA256Hashing(tempsha256Key);
                }

                for (int i = 0; i < 32; i++)
                    totalSHAKey[loop, i] = sha256Key[i];

                /*StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in sha256Key)
                    stringBuilder.AppendFormat("{0:x2}", b);
                string sha256string = stringBuilder.ToString();    */

                //byte[] hmacKey = HMACSHA256Hashing(sha256Key, buffer, whence); //Ci 얻는 로직

                for (int i = 0; i < 32; i++)
                    totalCipher[loop, i] = (byte)(totalSHAKey[loop, i] ^ buffer[i + whence]);

                whence += 32;
            }

            /*StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < totalLength / 32; i++)
            {
                for (int q = 0; q < 32; q++)
                    stringBuilder.AppendFormat("{0:x2}", totalCipher[i, q]);
            }

            string realFinalCipher = stringBuilder.ToString();
            print_monitor(realFinalCipher);*/

            byte[] realFinalCipher = new byte[totalCipher.Length];
            for(int i=0; i<totalLength/32; i++)
            {
                for (int q = 0; q < 32; q++)
                    realFinalCipher[i * 32 + q] = totalCipher[i, q];
            }

            /////////////////////////////////////////////////////////////////////////////     
            //print_monitor(totalSHAKey.Length.ToString()); //32
            byte[] sendSHAkeys = new byte[totalSHAKey.Length];
            for(int i=0; i<totalLength/32; i++)
            {
                for (int q = 0; q < 32; q++)
                    sendSHAkeys[i*32 + q] = (byte)totalSHAKey[i, q];
            }
            
            //print_monitor(Encoding.UTF8.GetString(sendSHAkeys));
            //print_monitor(Encoding.UTF8.GetString(buffer));

            /*for(int i=0; i<totalLength/32; i++)
            {
                byte[] test = new byte[32];
                for (int q = 0; q < 32; q++)
                    test[q] = (byte)(sendSHAkeys[q] ^ totalCipher[0, q]);
                print_monitor(Encoding.UTF8.GetString(test));
            }*/
            
            /////////////////////////////////////////////////////////////////////////////
            sender_sock.Send(BitConverter.GetBytes(realFinalCipher.Length));
            sender_sock.Send(realFinalCipher); //send cipher text
            Thread.Sleep(50);  
            sender_sock.Send(BitConverter.GetBytes(sendSHAkeys.Length));
            sender_sock.Send(sendSHAkeys); //send sha keys
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!filepathBox.Text.Equals(""))
                file_read(filepathBox.Text);
            else if (filepathBox.Text.Equals("") && !textBox.Text.Equals(""))
                text_send(textBox.Text);
            else            
                print_monitor("Invalid Input..");                            
        }
    }
}
 