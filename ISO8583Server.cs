using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Iso8583Simu
{
  public class ISO8583Server
  {
    private bool m_Started = false;
    private string m_LogFileName = "log";
    private int m_TransactionCount = 0;
    private int m_MaxLogSizeInMB = 2;
    private int m_TransactionTimeOut = 30;
    private TcpListener m_Listener;
    private int m_Port;
    private Thread m_ServerThread;
    private IPAddress m_IPAddress;

    public event ErrorRaisedDelegate OnError;

    public event TransactionDelegate OnReceived;

    public event TransactionDelegate BeforeReceive;

    public event GeneralDelagate BeforeWriteLog;

    public int MaxLogSize
    {
      get
      {
        return this.m_MaxLogSizeInMB;
      }
      set
      {
        if (value < 1 || value > 1025)
          throw new Exception(" value must from 1 to 1024 ");
        this.m_MaxLogSizeInMB = value;
      }
    }

    public string LogFileName
    {
      get
      {
        return this.m_LogFileName;
      }
      set
      {
        this.m_LogFileName = value;
      }
    }

    public int TransactionTimeOut
    {
      get
      {
        return this.m_TransactionTimeOut;
      }
      set
      {
        this.m_TransactionTimeOut = value;
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

    public void Start(string _IpAddress, int _port)
    {
      if (this.m_Started)
        return;
      this.m_Port = _port;
      this.m_IPAddress = IPAddress.Parse(_IpAddress);
      this.m_Listener = new TcpListener(this.m_IPAddress, this.m_Port);
      this.m_ServerThread = new Thread(new ThreadStart(this.DoListener));
      this.m_Listener.Start();
      this.m_Started = true;
      this.WriteLog("\r\n" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + "---------------------------SERVER STARDED ------------------\r\n");
      this.m_ServerThread.Start();
    }

    public void Start(int _port)
    {
      if (this.m_Started)
        return;
      this.m_Port = _port;
      this.m_Listener = new TcpListener(this.m_Port);
      this.m_ServerThread = new Thread(new ThreadStart(this.DoListener));
      this.m_Listener.Start();
      this.m_Started = true;
      this.WriteLog("\r\n" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + "---------------------------SERVER STARDED ------------------\r\n");
      this.m_ServerThread.Start();
    }

    public void Stop()
    {
      this.m_Started = false;
      this.m_ServerThread.Abort();
      this.m_Listener.Stop();
      this.WriteLog("\r\n" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + "---------------------------SERVER STOPED ------------------\r\n");
    }

    public bool IsStarted
    {
      get
      {
        return this.m_Started;
      }
    }

    public void DoListener()
    {
      do
      {
        try
        {
          ISO8583Client isO8583Client = new ISO8583Client(this);
          isO8583Client.Client = this.m_Listener.AcceptTcpClient();
          this.WriteLog("\r\n A remote client is connected from IP = " + (object) IPAddress.Parse(((IPEndPoint) isO8583Client.Client.Client.RemoteEndPoint).Address.ToString()));
          if (this.OnError != null)
            isO8583Client.OnError += (ErrorRaisedDelegate) this.OnError.GetInvocationList()[0];
          if (this.OnReceived != null)
            isO8583Client.OnReceived += (TransactionDelegate) this.OnReceived.GetInvocationList()[0];
          if (this.BeforeReceive != null)
            isO8583Client.BeforeReceive += (TransactionDelegate) this.BeforeReceive.GetInvocationList()[0];
          isO8583Client.TimeOut = this.m_TransactionTimeOut;
          isO8583Client.DoReceiveAndSend();
        }
        catch (Exception ex)
        {
          try
          {
            if (!this.m_Started)
            {
              this.WriteLog("\r\n" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + "---------------- LISTENER STOPPED ---------------");
            }
            else
            {
              this.WriteLog("\r\n" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + "----ERROR ------------- \r\n" + ex.ToString());
              if (this.OnError != null)
                this.OnError(ex);
            }
          }
          catch
          {
          }
        }
      }
      while (this.m_Started);
    }

    internal int TransactionCount
    {
      get
      {
        return this.m_TransactionCount;
      }
      set
      {
        this.m_TransactionCount = value;
      }
    }

    public static string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }

    public bool CheckLicense()
    {
      return false;
    }

    public void WriteLog(string s)
    {
      if (this.BeforeWriteLog != null)
        this.BeforeWriteLog(s);
      if (!(this.m_LogFileName != ""))
        return;
      System.IO.File.AppendAllText(this.m_LogFileName, s, (Encoding) new ASCIIEncoding());
      if (this.m_TransactionCount % 10 == 0)
      {
        FileInfo fileInfo = new FileInfo(this.m_LogFileName);
        if (fileInfo.Length > (long) (this.m_MaxLogSizeInMB * 1024 * 1024))
        {
          string[] strArray = fileInfo.Name.Split('.')[0].Split('_');
          int num = 1;
          if (strArray.Length > 1)
          {
            try
            {
              num = int.Parse(strArray[1]);
            }
            catch
            {
              num = 1;
            }
          }
          while (true)
          {
            if (System.IO.File.Exists(fileInfo.DirectoryName + "\\" + strArray[0] + "_" + num.ToString() + fileInfo.Extension))
              ++num;
            else
              break;
          }
          this.m_LogFileName = fileInfo.DirectoryName + "\\" + strArray[0] + "_" + num.ToString() + fileInfo.Extension;
        }
      }
    }
  }
}
