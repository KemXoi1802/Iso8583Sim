using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Iso8583Simu
{
    public class SslSetup
    {
        public static readonly string STORE_CERT_NAME = "CAPubPriv2Pfx";
        public static readonly StoreLocation STORE_LOCATION = StoreLocation.CurrentUser;
        public static readonly string STORE_EXT = ".pfx";
        public static string STORE_PASSWORD = "123456";


        //install certificate to local machine
        public static bool InstallCertificate()
        {
            X509Store store = new X509Store(STORE_CERT_NAME, STORE_LOCATION);
            store.Open(OpenFlags.ReadOnly);
            if (store.Certificates.Count > 0)
            {
                //Certificate is found.
                return true;
            }
            else
            {
                store.Open(OpenFlags.ReadWrite);
                string fileName = STORE_CERT_NAME + STORE_EXT;
                X509Certificate2 certificate2;

                try
                {
                    certificate2 = new X509Certificate2(fileName, STORE_PASSWORD, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
                }
                catch
                {
                    return false;
                }

                store.Add(certificate2);
                store.Close();
                return true;
            }
        }
        public static X509CertificateCollection GetClientCertificates()
        {
            X509Store store = new X509Store(STORE_CERT_NAME, STORE_LOCATION);
            try
            {
                store.Open(OpenFlags.OpenExistingOnly);
                return store.Certificates;
            }
            catch { return null; }
        }
        public static X509Certificate2Collection GetAllCertificates()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                return store.Certificates;
            }
            catch { return null; }
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public static byte[] HexString2Bytes(string hexString, int inLengthFormat, out string szLogMsg)
        {
            //inLengthFormat=0: NONE, inLengthFormat=1: HEX, inLengthFormat=2: BCD
            szLogMsg = "";

            //check for null
            if (hexString == null)
                return null;

            //get length
            int len = hexString.Length;
            if (len % 2 == 1) return null;
            int len_half = len / 2;
            string szLen = len_half.ToString().PadLeft(4, '0');
            int loop = -1;
            if (inLengthFormat == 0)
                loop = len_half;
            else loop = len_half + 2;  //2 byte length

            //create a byte array
            byte[] bs = new byte[loop];
            try
            {
                //convert the hexstring to bytes
                for (int i = 0; i != loop; i++)
                {
                    if (inLengthFormat == 0)
                    {
                        szLogMsg += hexString.Substring(i * 2, 2);
                        bs[i] = (byte)Int32.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            if (inLengthFormat == 1)
                            {
                                string hexValue = int.Parse(szLen.Substring(0, 2)).ToString("X");
                                szLogMsg += hexValue.PadLeft(2, '0');
                                bs[i] = (byte)Int32.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                            }
                            else if (inLengthFormat == 2)
                            {
                                bs[i] = (byte)Int32.Parse(szLen.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                                szLogMsg += szLen.Substring(0, 2);
                            }
                        }
                        else if (i == 1)
                        {
                            if (inLengthFormat == 1)
                            {
                                string hexValue = int.Parse(szLen.Substring(2, 2)).ToString("X");
                                szLogMsg += hexValue.PadLeft(2, '0');
                                bs[i] = (byte)Int32.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                            }
                            else if (inLengthFormat == 2)
                            {
                                bs[i] = (byte)Int32.Parse(szLen.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                                szLogMsg += szLen.Substring(2, 2);
                            }
                        }
                        else
                        {
                            bs[i] = (byte)Int32.Parse(hexString.Substring((i - 2) * 2, 2), System.Globalization.NumberStyles.HexNumber);
                            szLogMsg += hexString.Substring((i - 2) * 2, 2);
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                // WriteLog("Exception : " + ex.Message);
            }

            //return the byte array
            return bs;
        }
    }
}
