using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Iso8583Simu
{
  public class Iso8583Data
  {
    private byte[] Buffer = new byte[1024];
    public bool HasHeader = true;
    public E_EMVShowOption EMVShowOptions = E_EMVShowOption.None;
    public const int MAX_PACKAGE_SIZE = 1024;
    public const int MAX_BITS = 128;
    private int m_MessageType;
    protected BitAttribute[] m_BitAttributes;
    private TPDU m_TPDU;
    private int m_PackageSize;
    private int m_LastBitError;
    public int MessageLength;
    private bool m_LengthInAsc;

    public bool LengthInAsc
    {
      get
      {
        return this.m_LengthInAsc;
      }
      set
      {
        this.m_LengthInAsc = value;
      }
    }

    public Iso8583Data Clone()
    {
      return new Iso8583Data();
    }

    public BitAttribute this[int bitnumber]
    {
      get
      {
        return this.m_BitAttributes[bitnumber - 1];
      }
      set
      {
        if (value == null)
          return;
        BitAttribute bitAttribute = this.m_BitAttributes[bitnumber - 1];
        if (value.Data != null)
          bitAttribute.Data = (byte[]) value.Data.Clone();
        bitAttribute.Length = value.Length;
        bitAttribute.LengthAttribute = value.LengthAttribute;
        bitAttribute.MaxLength = value.MaxLength;
        bitAttribute.TypeAtribute = value.TypeAtribute;
      }
    }

    public Iso8583Data()
    {
      this.m_TPDU = new TPDU();
      this.m_BitAttributes = BitTemplate.GetGeneralTemplate();
    }

    public Iso8583Data(BitTemplate.Bit_Specific[] template)
    {
      this.m_TPDU = new TPDU();
      this.m_BitAttributes = BitTemplate.GetBitAttributeArray(template);
    }

    public static string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }

    public BitAttribute[] BitAttributes
    {
      get
      {
        return this.m_BitAttributes;
      }
      set
      {
        this.m_BitAttributes = value;
      }
    }

    public TPDU TPDUHeader
    {
      get
      {
        return this.m_TPDU;
      }
    }

    public void PackBit(int _iBitNumber1, string _strValue)
    {
      int index = _iBitNumber1 - 1;
      switch (this.m_BitAttributes[index].TypeAtribute)
      {
        case BitType.AN:
        case BitType.ANS:
          this.m_BitAttributes[index].Length = _strValue.Length;
          this.m_BitAttributes[index].Data = IsoUltil.StringToAsc(_strValue);
          break;
        case BitType.BCD:
          this.m_BitAttributes[index].Length = this.m_BitAttributes[index].LengthAttribute != BitLength.FIXED ? _strValue.Length : this.m_BitAttributes[index].MaxLength;
          this.m_BitAttributes[index].Data = IsoUltil.StringToBCD(_strValue, (this.m_BitAttributes[index].Length + 1) / 2);
          break;
        case BitType.BINARY:
          this.m_BitAttributes[index].Length = (_strValue.Length + 1) / 2;
          this.m_BitAttributes[index].Data = IsoUltil.StringToBCD(_strValue, this.m_BitAttributes[index].Length);
          break;
      }
      switch (this.m_BitAttributes[index].LengthAttribute)
      {
        case BitLength.LLVAR:
          if (this.m_BitAttributes[index].Length > 99)
            throw new Exception(string.Format("Field {0} 's length > 99 !!", (object) index));
          break;
        case BitLength.LLLVAR:
          if (this.m_BitAttributes[index].Length > 999)
            throw new Exception(string.Format("Field {0} 's length > 999 !!", (object) index));
          break;
      }
      this.m_BitAttributes[index].IsSet = true;
    }

    public void Reset()
    {
      for (int index = 0; index < 128; ++index)
        this.m_BitAttributes[index].IsSet = false;
    }

    public void PackBit(int _iBitNumber1, byte[] _Bytes)
    {
      int index = _iBitNumber1 - 1;
      this.m_BitAttributes[index].Data = _Bytes.Clone() as byte[];
      switch (this.m_BitAttributes[index].TypeAtribute)
      {
        case BitType.AN:
        case BitType.ANS:
          this.m_BitAttributes[index].Length = _Bytes.Length;
          break;
        case BitType.BCD:
          this.m_BitAttributes[index].Length = _Bytes.Length * 2;
          break;
        case BitType.BINARY:
          this.m_BitAttributes[index].Length = _Bytes.Length;
          break;
      }
      this.m_BitAttributes[index].IsSet = true;
    }

    public void BitSet(int _iBitNumber, bool _bSet)
    {
      this.m_BitAttributes[_iBitNumber].IsSet = _bSet;
    }

    public bool IsBitSet(int _iBitNumber)
    {
      return this.m_BitAttributes[_iBitNumber].IsSet;
    }

    public int MessageType
    {
      get
      {
        return this.m_MessageType;
      }
      set
      {
        this.m_MessageType = value;
      }
    }

    public byte[] Pack()
    {
      return this.Pack(EMessageLengthType.HL);
    }

    public byte[] Pack(EMessageLengthType lengthtype)
    {
      this.m_PackageSize = lengthtype != EMessageLengthType.None ? (lengthtype != EMessageLengthType.String4 ? 2 : 4) : 0;
      if (this.HasHeader)
      {
        this.m_TPDU.Pack().CopyTo((Array) this.Buffer, this.m_PackageSize);
        this.m_PackageSize += 5;
      }
      if (this.m_LengthInAsc)
      {
        Encoding.Default.GetBytes(this.MessageType.ToString("0000")).CopyTo((Array) this.Buffer, this.m_PackageSize);
        this.m_PackageSize += 4;
      }
      else
      {
        IsoUltil.BinToBcd(this.MessageType, 2).CopyTo((Array) this.Buffer, this.m_PackageSize);
        this.m_PackageSize += 2;
      }
      this.CreateBitmap().CopyTo((Array) this.Buffer, this.m_PackageSize);
      this.m_PackageSize += 8;
      int num1 = 128;
      if (!this.m_BitAttributes[0].IsSet)
        num1 = 64;
      int num2;
      for (int index = 0; index < num1; ++index)
      {
        if (this.m_BitAttributes[index].IsSet)
        {
          switch (this.m_BitAttributes[index].LengthAttribute)
          {
            case BitLength.FIXED:
              this.m_BitAttributes[index].Data.CopyTo((Array) this.Buffer, this.m_PackageSize);
              break;
            case BitLength.LLVAR:
              if (this.m_LengthInAsc)
              {
                Encoding encoding = Encoding.Default;
                num2 = this.m_BitAttributes[index].Length;
                string s = num2.ToString("00");
                encoding.GetBytes(s).CopyTo((Array) this.Buffer, this.m_PackageSize);
                this.m_PackageSize += 2;
                this.m_BitAttributes[index].Data.CopyTo((Array) this.Buffer, this.m_PackageSize);
                break;
              }
              IsoUltil.BinToBcd(this.m_BitAttributes[index].Length, 1).CopyTo((Array) this.Buffer, this.m_PackageSize);
              ++this.m_PackageSize;
              this.m_BitAttributes[index].Data.CopyTo((Array) this.Buffer, this.m_PackageSize);
              break;
            case BitLength.LLLVAR:
              if (this.m_LengthInAsc)
              {
                Encoding encoding = Encoding.Default;
                num2 = this.m_BitAttributes[index].Length;
                string s = num2.ToString("000");
                encoding.GetBytes(s).CopyTo((Array) this.Buffer, this.m_PackageSize);
                this.m_PackageSize += 3;
                this.m_BitAttributes[index].Data.CopyTo((Array) this.Buffer, this.m_PackageSize);
                break;
              }
              IsoUltil.BinToBcd(this.m_BitAttributes[index].Length, 2).CopyTo((Array) this.Buffer, this.m_PackageSize);
              this.m_PackageSize += 2;
              this.m_BitAttributes[index].Data.CopyTo((Array) this.Buffer, this.m_PackageSize);
              break;
          }
          switch (this.m_BitAttributes[index].TypeAtribute)
          {
            case BitType.AN:
            case BitType.ANS:
              this.m_PackageSize += this.m_BitAttributes[index].Length;
              break;
            case BitType.BCD:
              this.m_PackageSize += (this.m_BitAttributes[index].Length + 1) / 2;
              break;
            case BitType.BINARY:
              this.m_PackageSize += this.m_BitAttributes[index].Length;
              break;
          }
        }
      }
      if (lengthtype == EMessageLengthType.String4)
      {
        Encoding encoding = Encoding.Default;
        num2 = this.m_PackageSize - 4;
        string s = num2.ToString("0000");
        encoding.GetBytes(s).CopyTo((Array) this.Buffer, 0);
      }
      else if (lengthtype != EMessageLengthType.None)
        IsoUltil.IntToMessageLength(this.m_PackageSize - 2, lengthtype).CopyTo((Array) this.Buffer, 0);
      byte[] numArray = new byte[this.m_PackageSize];
      Array.Copy((Array) this.Buffer, (Array) numArray, this.m_PackageSize);
      return numArray;
    }

    public byte[] CreateBitmap()
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8]
      {
        (byte) 128,
        (byte) 64,
        (byte) 32,
        (byte) 16,
        (byte) 8,
        (byte) 4,
        (byte) 2,
        (byte) 1
      };
      for (int index = 0; index < 128; ++index)
      {
        if (this.m_BitAttributes[index].IsSet)
        {
          if (index >= 64)
          {
            if (this[1].Data == null)
              this[1].Data = new byte[8];
            this[1].Data[(index - 64) / 8] |= numArray2[(index - 64) % 8];
          }
          else
            numArray1[index / 8] |= numArray2[index % 8];
        }
      }
      if (this[1].Data != null)
      {
        numArray1[0] |= (byte) 128;
        this[1].IsSet = true;
        this[1].Length = 8;
      }
      return numArray1;
    }

    private void AnalyzeBitmap(byte[] array)
    {
      BitArray bitArray1 = new BitArray(IsoUltil.GetBytesFromBytes(array, 0, 8));
      for (int index = 0; index < bitArray1.Length; ++index)
        this.m_BitAttributes[index].IsSet = bitArray1[7 - index % 8 + index / 8 * 8];
      if (array.Length != 16)
        return;
      BitArray bitArray2 = new BitArray(IsoUltil.GetBytesFromBytes(array, 8, 8));
      for (int index = 0; index < bitArray2.Length; ++index)
        this.m_BitAttributes[index + 64].IsSet = bitArray2[7 - index % 8 + index / 8 * 8];
    }

    public byte[] RawMessage
    {
      get
      {
        byte[] _arDest = new byte[this.MessageLength];
        IsoUltil.BytesCopy(_arDest, this.Buffer, 0, 0, this.MessageLength);
        return _arDest;
      }
    }

    public virtual void Unpack(byte[] arInput)
    {
      this.Unpack(arInput, 0, arInput.Length);
    }

    public virtual void Unpack(byte[] arInput, int from, int length)
    {
      this.MessageLength = length + from;
      Array.Copy((Array) arInput, 0, (Array) this.Buffer, 0, this.MessageLength);
      int index1 = from;
      if (this.HasHeader)
      {
        byte[] ar = new byte[5];
        Array.Copy((Array) this.Buffer, index1, (Array) ar, 0, 5);
        index1 += 5;
        this.m_TPDU.UnPack(ar);
      }
      int offset;
      if (this.m_LengthInAsc)
      {
        this.MessageType = int.Parse(Encoding.Default.GetString(this.Buffer, index1, 4));
        offset = index1 + 4;
      }
      else
      {
        this.MessageType = IsoUltil.BcdToBin(new byte[2]
        {
          this.Buffer[index1],
          this.Buffer[index1 + 1]
        });
        offset = index1 + 2;
      }
      if (((int) arInput[offset] & 128) > 0)
        this.AnalyzeBitmap(IsoUltil.GetBytesFromBytes(arInput, offset, 16));
      else
        this.AnalyzeBitmap(IsoUltil.GetBytesFromBytes(arInput, offset, 8));
      int index2 = offset + 8;
      int num = 128;
      if (!this.m_BitAttributes[0].IsSet)
        num = 64;
      for (int index3 = 0; index3 < num; ++index3)
      {
        if (this.m_BitAttributes[index3].IsSet)
        {
          this.m_LastBitError = index3;
          switch (this.m_BitAttributes[index3].LengthAttribute)
          {
            case BitLength.FIXED:
              this.m_BitAttributes[index3].Length = this.m_BitAttributes[index3].MaxLength;
              break;
            case BitLength.LLVAR:
              if (this.m_LengthInAsc)
              {
                this.m_BitAttributes[index3].Length = int.Parse(Encoding.Default.GetString(this.Buffer, index2, 2));
                index2 += 2;
                break;
              }
              byte[] _arInput1 = new byte[1]
              {
                this.Buffer[index2]
              };
              this.m_BitAttributes[index3].Length = IsoUltil.BcdToBin(_arInput1);
              ++index2;
              break;
            case BitLength.LLLVAR:
              if (this.m_LengthInAsc)
              {
                this.m_BitAttributes[index3].Length = int.Parse(Encoding.Default.GetString(this.Buffer, index2, 3));
                index2 += 3;
                break;
              }
              byte[] _arInput2 = new byte[2]
              {
                this.Buffer[index2],
                this.Buffer[index2 + 1]
              };
              this.m_BitAttributes[index3].Length = IsoUltil.BcdToBin(_arInput2);
              index2 += 2;
              break;
          }
          switch (this.m_BitAttributes[index3].TypeAtribute)
          {
            case BitType.AN:
            case BitType.ANS:
              this.m_BitAttributes[index3].Data = new byte[this.m_BitAttributes[index3].Length];
              Array.Copy((Array) this.Buffer, index2, (Array) this.m_BitAttributes[index3].Data, 0, this.m_BitAttributes[index3].Length);
              index2 += this.m_BitAttributes[index3].Length;
              break;
            case BitType.BCD:
              this.m_BitAttributes[index3].Data = new byte[(this.m_BitAttributes[index3].Length + 1) / 2];
              Array.Copy((Array) this.Buffer, index2, (Array) this.m_BitAttributes[index3].Data, 0, (this.m_BitAttributes[index3].Length + 1) / 2);
              index2 += (this.m_BitAttributes[index3].Length + 1) / 2;
              break;
            case BitType.BINARY:
              this.m_BitAttributes[index3].Data = new byte[this.m_BitAttributes[index3].Length];
              Array.Copy((Array) this.Buffer, index2, (Array) this.m_BitAttributes[index3].Data, 0, this.m_BitAttributes[index3].Length);
              index2 += this.m_BitAttributes[index3].Length;
              break;
          }
        }
      }
    }

    private void WaitForData(Stream _stream, int _iTimeOut)
    {
      if (!(_stream is NetworkStream))
        return;
      NetworkStream networkStream = (NetworkStream) _stream;
      while (!networkStream.DataAvailable)
        Thread.Sleep(20);
      DateTime now = DateTime.Now;
      do
        ;
      while (DateTime.Now.Subtract(now).TotalSeconds < (double) _iTimeOut);
    }

    public string LogFormat()
    {
      return this.LogFormat(128);
    }

    public string LogFormat(int _EndBits)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Message Type = " + this.MessageType.ToString() + "\r\n");
      for (int index = 0; index < _EndBits; ++index)
      {
        if (this.m_BitAttributes[index].IsSet)
        {
          stringBuilder.Append("Field ");
          stringBuilder.Append((index + 1).ToString());
          stringBuilder.Append(" = \"");
          stringBuilder.Append(this.m_BitAttributes[index].ToString());
          stringBuilder.Append("\"\r\n");
          if ((index == 54 || index == 55) && this.EMVShowOptions != E_EMVShowOption.None)
            stringBuilder.Append(EMVAnalyzer.GetFullDescription(this.m_BitAttributes[index].Data, E_EMVShowOption.VALUE | E_EMVShowOption.NAME | E_EMVShowOption.DESCRIPTION | E_EMVShowOption.BITS));
        }
      }
      return stringBuilder.ToString();
    }

    public int LastBitError
    {
      get
      {
        return this.m_LastBitError;
      }
    }
  }
}
