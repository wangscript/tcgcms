using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net.Sockets;
using System.Net;

namespace TCG.Utils
{
    public class socket
    {
        public static string DoSocketGet(string server)
        {
            //Set up variables and String to write to the server.
            Encoding ASCII = Encoding.ASCII;
            string Get = "GET / HTTP/1.1\r\nHost: " + server +
                         "\r\nConnection: Close\r\n\r\n";
            Byte[] ByteGet = ASCII.GetBytes(Get);
            Byte[] RecvBytes = new Byte[256];
            String strRetPage = null;

            try
            {
                Socket s = null;
                IPEndPoint hostEndPoint;
                IPAddress hostAddress = null;
                int conPort = 13000;

                // Get DNS host information.
                IPHostEntry hostInfo = Dns.GetHostEntry(server);
                // Get the DNS IP addresses associated with the host.
                IPAddress[] IPaddresses = hostInfo.AddressList;

                // Evaluate the socket and receiving host IPAddress and IPEndPoint. 
                for (int index = 0; index < IPaddresses.Length; index++)
                {
                    hostAddress = IPaddresses[index];
                    hostEndPoint = new IPEndPoint(hostAddress, conPort);


                    // Creates the Socket to send data over a TCP connection.
                    s = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);



                    // Connect to the host using its IPEndPoint.
                    s.Connect(hostEndPoint);

                    if (!s.Connected)
                    {
                        // Connection failed, try next IPaddress.
                        strRetPage = "Unable to connect to host";
                        s = null;
                        continue;
                    }

                    // Sent the GET request to the host.
                    s.Send(ByteGet, ByteGet.Length, 0);


                } // End of the for loop.      



                // Receive the host home page content and loop until all the data is received.
                Int32 bytes = s.Receive(RecvBytes, RecvBytes.Length, 0);
                strRetPage = "Default HTML page on " + server + ":\r\n";
                strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, bytes);

                //while (bytes > 0)
                //{
                //    bytes = s.Receive(RecvBytes, RecvBytes.Length, 0);
                //    strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, bytes);
                //}


            } // End of the try block.

            catch (SocketException e)
            {
                HttpContext.Current.Response.Write("SocketException caught!!!");
                HttpContext.Current.Response.Write("Source : " + e.Source);
                HttpContext.Current.Response.Write("Message : " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                HttpContext.Current.Response.Write("ArgumentNullException caught!!!");
                HttpContext.Current.Response.Write("Source : " + e.Source);
                HttpContext.Current.Response.Write("Message : " + e.Message);
            }
            catch (NullReferenceException e)
            {
                HttpContext.Current.Response.Write("NullReferenceException caught!!!");
                HttpContext.Current.Response.Write("Source : " + e.Source);
                HttpContext.Current.Response.Write("Message : " + e.Message);
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("Exception caught!!!");
                HttpContext.Current.Response.Write("Source : " + e.Source);
                HttpContext.Current.Response.Write("Message : " + e.Message);
            }

            return strRetPage;

        }

        private void Connect(IPEndPoint hostEndPoint)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
