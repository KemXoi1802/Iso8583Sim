using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Iso8583Simu
{
  public class ISO8583Client
  {
    public int TimeOut = 30;
    public EMessageLengthType MessageLengthType = EMessageLengthType.BCD;
    private TcpClient m_Client;
    private IPAddress m_IPAddress;
    private ISO8583Server Server;
    private Iso8583Data m_ResquestData;
    private Iso8583Data m_ResponseData;
    private Thread TransThread;
    private int m_Port;

    public event ErrorRaisedDelegate OnError;

    public event TransactionDelegate OnReceived;

    public event TransactionDelegate BeforeReceive;

    public ISO8583Client(string _IpAddress, int _port)
    {
      this.m_Port = _port;
      this.m_IPAddress = IPAddress.Parse(_IpAddress);
      this.m_Client = new TcpClient(_IpAddress, this.Port);
    }

    public ISO8583Client(ISO8583Server _server)
    {
      this.Server = _server;
    }

    public static string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }

    public ISO8583Client()
    {
    }

    public void DoReceiveAndSend()
    {
      this.TransThread = new Thread(new ThreadStart(this.ReceiveAndSend));
      this.TransThread.Start();
    }

    public void WriteServerLog(string s)
    {
      lock (this.Server)
      {
        if (this.Server == null)
          return;
        this.Server.WriteLog("\r\n" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + s);
      }
    }

    public void ReceiveAndSend()
    {
      try
      {
        if (this.BeforeReceive != null)
          this.BeforeReceive(this);
        byte[] numArray = this.Receive();
        if (this.ResquestData == null)
          this.ResquestData = new Iso8583Data();
        if (this.ResponseData == null)
          this.ResponseData = new Iso8583Data();
        for (; numArray != null; numArray = this.Receive())
        {
          ++this.Server.TransactionCount;
          this.WriteServerLog("\r\n RAW MESSAGE --------------------\r\n" + IsoUltil.BytesToHexString(numArray, 20, true) + "\r\n");
          try
          {
            if (this.MessageLengthType == EMessageLengthType.None)
              this.ResquestData.Unpack(numArray);
            else
              this.ResquestData.Unpack(numArray, 2, numArray.Length - 2);
          }
          catch (Exception ex)
          {
            throw new Exception(ex.ToString() + "\r\n Ocurring when parsing bit" + this.ResquestData.LastBitError.ToString() + "\r\n --- The parsing result is\r\n" + this.ResquestData.LogFormat(this.ResquestData.LastBitError));
          }
          this.WriteServerLog("---------------TRANSACTION " + this.Server.TransactionCount.ToString() + "------------------\r\n" + this.ResquestData.LogFormat());
          if (this.OnReceived != null)
            this.OnReceived(this);
        }
      }
      catch (Exception ex)
      {
        try
        {
          this.WriteServerLog("--ERROR ------------------\r\n" + ex.ToString());
          if (this.OnError == null)
            return;
          this.OnError(ex);
        }
        catch
        {
        }
      }
    }

    public TcpClient Client
    {
      get
      {
        return this.m_Client;
      }
      set
      {
        this.m_Client = value;
      }
    }

    public int Port
    {
      get
      {
        return this.m_Port;
      }
      set
      {
        this.m_Port = value;
      }
    }

    public Iso8583Data ResquestData
    {
      get
      {
        return this.m_ResquestData;
      }
      set
      {
        this.m_ResquestData = value;
      }
    }

    public Iso8583Data ResponseData
    {
      set
      {
        this.m_ResponseData = value;
      }
      get
      {
        return this.m_ResponseData;
      }
    }

    public void SendAndReceive()
    {
    }

    public void SendResponse()
    {
      this.WriteServerLog("-------------SERVER RESPONSE-------------\r\n" + this.ResponseData.LogFormat());
      byte[] buffer = this.ResponseData.Pack();
      this.Client.GetStream().Write(buffer, 0, buffer.Length);
    }

    public void SendRequest()
    {
      byte[] buffer = this.ResquestData.Pack();
      this.Client.GetStream().Write(buffer, 0, buffer.Length);
    }

    public byte[] Receive()
    {
      DateTime now1 = DateTime.Now;
      DateTime now2;
      TimeSpan timeSpan;
      while (true)
      {
        int num;
        if (!this.Client.GetStream().DataAvailable)
        {
          now2 = DateTime.Now;
          timeSpan = now2.Subtract(now1);
          num = timeSpan.TotalSeconds < (double) this.TimeOut ? 1 : 0;
        }
        else
          num = 0;
        if (num != 0)
          Thread.Sleep(30);
        else
          break;
      }
      now2 = DateTime.Now;
      timeSpan = now2.Subtract(now1);
      if (timeSpan.TotalSeconds >= (double) this.TimeOut)
        return (byte[]) null;
      byte[] numArray = new byte[2];
      this.Client.GetStream().Read(numArray, 0, 2);
      int num1 = 0;
      switch (this.MessageLengthType)
      {
        case EMessageLengthType.BCD:
          num1 = IsoUltil.BcdToBin(numArray);
          break;
        case EMessageLengthType.HL:
          num1 = (int) numArray[0] * 256 + (int) numArray[1];
          break;
        case EMessageLengthType.LH:
          num1 = (int) numArray[1] * 256 + (int) numArray[0];
          break;
      }
      if (num1 > 1024)
        throw new Exception("Error Message length > 1024");
      byte[] buffer = new byte[num1 + 2];
      numArray.CopyTo((Array) buffer, 0);
      int offset = 2;
      while (offset <= num1 + 2)
      {
        offset += this.Client.GetStream().Read(buffer, offset, num1 - offset + 2);
        if (!this.Client.GetStream().DataAvailable)
        {
          if (offset != num1 + 2)
            throw new Exception("Message length ERROR");
          return buffer;
        }
      }
      throw new Exception("Message length ERROR");
    }
  }
}
