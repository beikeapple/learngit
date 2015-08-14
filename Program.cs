using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

namespace SmartlinkClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //string ssid = "P2PClient";
            //string pswd = "22222222";
            string ssid = "TP-LINK-ANNA";
            string pswd = "123456789";
            //string ssid = "zmodooem";
            //string pswd = "hw19-i882-d6a8";
            int interval_char = 2;  //毫秒
            int interval =60;  //毫秒
            int totalTimes = 10000; 

            int port = 12345;
            byte[] content = new byte[1];
            System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient();

            for (int jj = 0; jj < totalTimes; jj++)
            {
                Console.Out.WriteLine(jj);

                //head:
                {
                    string dest = "238.80." + (128 + ssid.Length).ToString() + "." + (128 + pswd.Length).ToString();
                    client.Send(content, content.Length, dest, port);
                    System.Threading.Thread.Sleep(interval_char);
                }
                //ssid:
                int count_ssid = (ssid.Length + 1) / 2;
                for (int i = 0; i < count_ssid; i++)
                {
                    string a1 = "238";
                    string a2 = (80 + i).ToString();
                    string a3 = (Convert.ToChar(ssid.Substring(i * 2, 1)) + 0).ToString();
                    string a4 = (ssid.Length % 2 == 1 && i == count_ssid - 1) ? "128" : (Convert.ToChar(ssid.Substring(i * 2 + 1, 1)) + 128).ToString();
                    string dest = a1 + "." + a2 + "." + a3 + "." + a4;
                    client.Send(content, content.Length, dest, port);
                    System.Threading.Thread.Sleep(interval_char);
                }
                //pswd:
                int count_pswd = (pswd.Length + 1) / 2;
                for (int i = 0; i < count_pswd; i++)
                {
                    string a1 = "238";
                    string a2 = (80 + i).ToString();
                    string a3 = (Convert.ToChar(pswd.Substring(i * 2, 1)) + 128).ToString();
                    string a4 = (pswd.Length % 2 == 1 && i == count_pswd - 1) ? "0" : (Convert.ToChar(pswd.Substring(i * 2 + 1, 1)) + 0).ToString();
                    string dest = a1 + "." + a2 + "." + a3 + "." + a4;
                    client.Send(content, content.Length, dest, port);
                    System.Threading.Thread.Sleep(interval_char);
                }
                //langusge:
                {
                    string dest = "238.81.129.128";
                    client.Send(content, content.Length, dest, port);
                    System.Threading.Thread.Sleep(interval_char);
                }
                //interval:
                System.Threading.Thread.Sleep(interval);
            }

            client.Close();
        }
    }
}
