using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Iso8583Simu
{
    public class SslClient
    {
        string _hostIP;
        int _port, _inTimeout;
        SslProtocols _protocol;

        public TcpClient _client;
        public SslStream _sslStream;

        public SslClient(string hostIP, int port, SslProtocols protocol, int _inTimeout)
        {
            this._hostIP = hostIP;
            this._port = port;
            this._protocol = protocol;

            this._inTimeout = _inTimeout;
        }

        private bool ServerValidationCallback(
             object sender,
             X509Certificate certificate,
             X509Chain chain,
             SslPolicyErrors sslPolicyErrors)
        {
            //if (sslPolicyErrors == SslPolicyErrors.None)
            //return true;

            // Accept all certificates
            return true;
        }
        ///   <summary>     
        ///  Certificate selection callback.     
        ///   </summary>      
        public static X509Certificate ClientCertificateSelectionCallback(
            object sender,
            string targetHost,
            X509CertificateCollection localCertificates,
            X509Certificate remoteCertificate,
            string[] acceptableIssuers)
        {
            //perform some checks on the certificate... 
            // ...
            // ...                                       //return the selected certificate. If null is returned a 
            // NotSupported exception is thrown. 
            return localCertificates[0];
        }
        public int Connect()
        {
            int inRet = 0;
            try
            {
                _client = new TcpClient(_hostIP, _port);
                _client.SendTimeout = 30 * 1000; // _inSendTimeout * 1000;//30000;//90000;
                _client.ReceiveTimeout = 30 * 1000; // _inReceiveTimeout * 1000;// 30000;// 420000;
            }
            catch
            {
                return -1;
            }

            bool leaveInnerStreamOpen = false;
            EncryptionPolicy encryptionPolicy = EncryptionPolicy.RequireEncryption;

            //create the SSL stream starting from the NetworkStream associated
            //with the TcpClient instance
            _sslStream = new SslStream(_client.GetStream(),
              leaveInnerStreamOpen, new RemoteCertificateValidationCallback(ServerValidationCallback)
              , new LocalCertificateSelectionCallback(ClientCertificateSelectionCallback), encryptionPolicy);

            X509CertificateCollection clientCertificates = SslSetup.GetAllCertificates();
            bool checkCertificateRevocation = true;

            try
            {
                //Start the handshake
                _sslStream.AuthenticateAsClient(_hostIP, clientCertificates, _protocol, checkCertificateRevocation);
                _sslStream.ReadTimeout = 5 * 1000;
                _sslStream.WriteTimeout = 5 * 1000;
            }
            catch
            {
                _client.Close();
                inRet = -2;
            }

            return inRet;
        }

        public void Send(byte[] data)
        {
            _sslStream.Write(data);
        }

        public int Receive(ref byte[] buffer)
        {
            buffer = readByteMessage();
            if (buffer == null)
                return -1;
            return buffer.Length;
            //return _sslStream.Read(buffer, 0, buffer.Length);
        }

        public void Disconnect()
        {
            try
            {
                if (_client.Connected)
                    _client.Close();
            }
            catch { }
        }

        public static double start, end;
        public byte[] readByteMessage()
        {
            try
            {
                start = DateTime.Now.Ticks / 10000000;
                byte[] header = new byte[2];
                byte[] data = null;
                int i = 0;
                int len = 0;
                string szLen = "";
                while (true)
                {
                    int inData = _sslStream.ReadByte();
                    if (inData < 0)
                    {
                        end = DateTime.Now.Ticks / 10000000;
                        if (end - start >= _inTimeout)
                        {
                            return null; ;
                        }
                        else continue;
                    }


                    if (inData >= 0 && i < 2)
                    {
                        header[i] = (byte)inData;
                        if (i == 1)
                        {
                            szLen = SslSetup.ByteArrayToString(header);
                            if (szLen.Trim() == "")
                                return null;
                            else
                            {
                                len = int.Parse(szLen);
                                if (len == 0)
                                    return null;
                                else
                                {
                                    data = new byte[len + 2];
                                    data[0] = header[0];
                                    data[1] = header[1];
                                }
                            }
                        }

                        i++;
                        continue;
                    }
                    else
                    {
                        data[i] = (byte)inData;
                        if (i == len)
                        {
                            break;
                        }
                        else
                        {
                            i++;
                            continue;
                        }
                    }
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}
