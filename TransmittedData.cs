using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Iso8583Simu
{
    public class TransmittedData
    {
        protected byte[] _FixedHeader = new byte[5];
        public const int FIXEDHEADER_SIZE = 5;
        protected byte[] m_ReadMessage;
        protected EMessageLengthType m_LengthType;
        protected byte[] m_WrittenMessage;



        public EMessageLengthType LengthType
        {
            get
            {
                return this.m_LengthType;
            }
            set
            {
                this.m_LengthType = value;
            }
        }

        public byte[] FixedHeader
        {
            get
            {
                return this._FixedHeader;
            }
            set
            {
                this._FixedHeader = value;
            }
        }

      //  public static byte[] PackAdminResponse(string _ErrorCode, string _Content)
      //  {
      //      byte[] numArray1 = TagLengthValue.PackTAGs(new SortedDictionary<GWHeaderTAG, byte[]>()
      //{
      //  {
      //    GWHeaderTAG.TAG_MESSAGETYPE,
      //    BitConverter.GetBytes((ushort) 45072)
      //  },
      //  {
      //    GWHeaderTAG.TAG_AMDIN_CONTENT,
      //    Encoding.ASCII.GetBytes(_Content)
      //  },
      //  {
      //    GWHeaderTAG.TAG_ERROR_CODE,
      //    new byte[2]{ (byte) 48, (byte) 48 }
      //  }
      //});
      //      byte[] numArray2 = new byte[numArray1.Length + 7];
      //      IsoUltil.IntToMessageLength(numArray2.Length - 2, EMessageLengthType.BCD).CopyTo((Array)numArray2, 0);
      //      numArray1.CopyTo((Array)numArray2, 7);
      //      return numArray2;
      //  }

      //  public static byte[] PackAdminRequest(string _Command, string _Content)
      //  {
      //      byte[] numArray1 = TagLengthValue.PackTAGs(new SortedDictionary<GWHeaderTAG, byte[]>()
      //{
      //  {
      //    GWHeaderTAG.TAG_MESSAGETYPE,
      //    BitConverter.GetBytes((ushort) 45056)
      //  },
      //  {
      //    GWHeaderTAG.TAG_ADMIN_SECRET,
      //    KeyMangement.SecretKey.Encrypt(BitConverter.GetBytes(DateTime.Now.Ticks))
      //  },
      //  {
      //    GWHeaderTAG.TAG_AMDIN_CONTENT,
      //    Encoding.ASCII.GetBytes(_Content)
      //  },
      //  {
      //    GWHeaderTAG.TAG_ADMIN_COMMAND,
      //    Encoding.ASCII.GetBytes(_Command)
      //  }
      //});
      //      byte[] numArray2 = new byte[numArray1.Length + 7];
      //      IsoUltil.IntToMessageLength(numArray2.Length - 2, EMessageLengthType.BCD).CopyTo((Array)numArray2, 0);
      //      numArray1.CopyTo((Array)numArray2, 7);
      //      return numArray2;
      //  }

        public byte[] WrittenMessage
        {
            get
            {
                return this.m_WrittenMessage;
            }
            set
            {
                this.m_WrittenMessage = value;
            }
        }

        public TransmittedData(EMessageLengthType _LengthType)
        {
            this.m_LengthType = _LengthType;
        }
        public byte[] ReadMessage
        {
            get
            {
                return this.m_ReadMessage;
            }
            set
            {
                this.m_ReadMessage = value;
            }
        }

        private void Read(NetworkStream stream, bool isSource)
        {
            byte[] numArray = new byte[2];
            DateTime now = DateTime.Now;
            try
            {
                if (stream.Read(numArray, 0, 2) < 2)
                {
                    if (DateTime.Now.Subtract(now).Milliseconds >= stream.ReadTimeout)
                        throw new VerificationException("CAN NOT READ FROM THE NETWORK STREAM", EVerificationError.TimeOut);
                    throw new VerificationException("THE CONNECTION WAS CLOSED BY REMOTE COMPUTER/TERMINAL", isSource ? EVerificationError.DisconnectedFromSource : EVerificationError.DisconnectedFromDestination);
                }
            }
            catch (IOException ex)
            {
                throw new VerificationException(ex.Message, isSource ? EVerificationError.DisconnectedFromSource : EVerificationError.DisconnectedFromDestination);
            }
            this.m_ReadMessage = new byte[IsoUltil.MessageLengthToInt(numArray, this.m_LengthType)];
            if (stream.Read(this.m_ReadMessage, 0, this.m_ReadMessage.Length) < this.m_ReadMessage.Length)
            {
                if (DateTime.Now.Subtract(now).TotalMilliseconds >= (double)stream.ReadTimeout)
                    throw new VerificationException("CAN NOT READ FROM THE NETWORK STREAM", EVerificationError.TimeOut);
                throw new VerificationException("THE CONNECTION WAS CLOSED BY REMOTE COMPUTER/TERMINAL", isSource ? EVerificationError.DisconnectedFromSource : EVerificationError.DisconnectedFromDestination);
            }
            if (stream.DataAvailable)
                throw new VerificationException("THERE ARE REDUNDANT BYTES", EVerificationError.MessageLengthError);
        }


        public void Read(TcpClient client, int TimeOut, bool isSource)
        {
            client.GetStream().ReadTimeout = TimeOut * 1000;
            this.Read(client.GetStream(), isSource);
        }

        public void Write(NetworkStream stream)
        {
            if (this.m_LengthType != EMessageLengthType.None)
            {
                byte[] buffer = new byte[this.m_WrittenMessage.Length + 2];
                IsoUltil.IntToMessageLength(this.m_WrittenMessage.Length, this.m_LengthType).CopyTo((Array)buffer, 0);
                this.m_WrittenMessage.CopyTo((Array)buffer, 2);
                stream.Write(buffer, 0, buffer.Length);
            }
            else
                stream.Write(this.m_WrittenMessage, 0, this.m_WrittenMessage.Length);
        }

        public void Write(NetworkStream stream, byte[] dataToWrite)
        {
            this.m_WrittenMessage = dataToWrite;
            this.Write(stream);
        }
    }
}
