using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;

namespace Iso8583Simu
{
  public class IsoUltil
  {
    public static string BinToString(int _iInput, int _iLength)
    {
      string str = _iInput.ToString();
      for (int length = str.Length; length < _iLength * 2; ++length)
        str = "0" + str;
      return str;
    }

    public static int StringHexToInteger(string str)
    {
      byte[] bcd = IsoUltil.StringToBCD(str, str.Length / 2);
      int num = 0;
      for (int index = 0; index < bcd.Length; ++index)
        num = num * 256 + (int) bcd[index];
      return num;
    }

    public static byte CharHexToByte(char _chInput)
    {
      if ((int) _chInput >= 48 && (int) _chInput <= 57)
        return (byte) ((uint) (short) _chInput - 48U);
      if ((int) _chInput >= 97 && (int) _chInput <= 102)
        return (byte) ((int) (short) _chInput - 97 + 10);
      if ((int) _chInput >= 65 && (int) _chInput <= 70)
        return (byte) ((int) (short) _chInput - 65 + 10);
      throw new Exception(_chInput.ToString() + " is not a HexValue");
    }

    public static string BcdToString(byte[] _arInput, int offset, int count)
    {
      string str = "";
      for (int index = offset * 2; index < (count - offset) * 2; ++index)
      {
        int _i = index % 2 != 0 ? (int) _arInput[index / 2] % 16 : (int) _arInput[index / 2] / 16;
                //str += (string) (object) IsoUltil.ByteToHex(_i);
                str += IsoUltil.ByteToHex(_i);
      }
      return str;
    }

    public static string BcdToString(byte[] _arInput)
    {
      return IsoUltil.BcdToString(_arInput, 0, _arInput.Length);
    }

    public static int BcdToBin(byte[] _arInput)
    {
      return int.Parse(IsoUltil.BcdToString(_arInput));
    }

    public static byte[] StringToAsc(string _strInput)
    {
      return Encoding.Default.GetBytes(_strInput);
    }

    public static string AscToString(byte[] _ar)
    {
      return Encoding.Default.GetString(_ar);
    }

    public static int AscToBin(byte[] _ar)
    {
      return int.Parse(IsoUltil.AscToString(_ar));
    }

    public static byte[] BinToBcd(int _iInput, int _iLength)
    {
      byte[] numArray = new byte[_iLength];
      string str = IsoUltil.BinToString(_iInput, _iLength);
      for (int index = 0; index < str.Length; ++index)
      {
        if (index % 2 == 0)
          numArray[index / 2] = (byte) ((uint) IsoUltil.CharHexToByte(str[index]) * 16U);
        else
          numArray[index / 2] += IsoUltil.CharHexToByte(str[index]);
      }
      return numArray;
    }

    public static byte[] BinToAsc(int _iInput, int _iLength)
    {
      return Encoding.Default.GetBytes(IsoUltil.BinToString(_iLength, _iLength));
    }

    public static byte[] StringToBCD(string _strInput, int _iLength)
    {
      byte[] numArray = new byte[_iLength];
      string str = _strInput;
      for (int length = str.Length; length < _iLength * 2; ++length)
        str = "0" + str;
      for (int index = 0; index < str.Length; ++index)
      {
        if (index % 2 == 0)
          numArray[index / 2] = (byte) ((uint) IsoUltil.CharHexToByte(str[index]) * 16U);
        else
          numArray[index / 2] += IsoUltil.CharHexToByte(str[index]);
      }
      return numArray;
    }

    public static void BytesCopy(byte[] _arDest, byte[] _arSource, int _iDestIndex, int _iSourceIndex, int count)
    {
      Array.Copy((Array) _arSource, _iSourceIndex, (Array) _arDest, _iDestIndex, count);
    }

    public static byte[] CreatBytesFromArray(byte[] _ar, int iFrom, int iCount)
    {
      byte[] numArray = new byte[iCount];
      for (int index = 0; index < iCount; ++index)
        numArray[index] = _ar[index + iFrom];
      return numArray;
    }

    public static char ByteToHex(int _i)
    {
      if (_i > 9)
        return (char) (_i - 10 + 65);
      return (char) (_i + 48);
    }

    public static string BytesToHexString(byte[] _ar, int _lineLength, bool _PrintASC)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < _ar.Length; ++index)
      {
        stringBuilder.Append(IsoUltil.ByteToHex((int) _ar[index] / 16));
        stringBuilder.Append(IsoUltil.ByteToHex((int) _ar[index] % 16));
        stringBuilder.Append(' ');
        if ((index + 1) % _lineLength == 0)
        {
          if (_PrintASC)
          {
            stringBuilder.Append("\t");
            stringBuilder.Append(Encoding.Default.GetString(_ar, index + 1 - _lineLength, _lineLength).Replace(char.MinValue, '.').Replace('\n', '.').Replace('\t', '.').Replace('\r', '.'));
          }
          stringBuilder.Append("\r\n");
        }
      }
      if (_ar.Length % _lineLength > 0 && _PrintASC)
      {
        for (int index = 0; index < _lineLength - _ar.Length % _lineLength; ++index)
          stringBuilder.Append("   ");
        stringBuilder.Append("\t");
        stringBuilder.Append(Encoding.Default.GetString(_ar, _ar.Length - _ar.Length % _lineLength, _ar.Length % _lineLength).Replace(char.MinValue, '.').Replace('\n', '.').Replace('\t', '.').Replace('\r', '.'));
      }
      return stringBuilder.ToString();
    }

    public static string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }

    public static string BytesToHexString(byte[] _ar)
    {
      return IsoUltil.BytesToHexString(_ar, 50, false);
    }

    public static string FillStringRight(string _strIn, int _Length, char _FillChar)
    {
      if (_Length <= _strIn.Length)
        return _strIn;
      StringBuilder stringBuilder = new StringBuilder(_strIn, _Length);
      for (int length = _strIn.Length; length < _Length; ++length)
        stringBuilder.Append(_FillChar);
      return stringBuilder.ToString();
    }

    public static string AmountToString12(Decimal d)
    {
      return d.ToString("0000000000.00").Replace(".", "");
    }

    public static Decimal String12ToAmount(string _str)
    {
      return Decimal.Parse(_str.Insert(10, "."));
    }

    public static int MessageLengthToInt(byte[] _input, EMessageLengthType _type)
    {
      int num1 = 0;
      switch (_type)
      {
        case EMessageLengthType.BCD:
          byte num2 = _input[0];
          byte num3 = _input[1];
          num1 = (int) num2 / 16 * 1000 + (int) num2 % 16 * 100 + (int) num3 / 16 * 10 + (int) num3 % 16;
          break;
        case EMessageLengthType.HL:
          num1 = (int) _input[0] * 256 + (int) _input[1];
          break;
        case EMessageLengthType.LH:
          num1 = (int) _input[1] * 256 + (int) _input[0];
          break;
      }
      return num1;
    }

    public static byte[] IntToMessageLength(int _input, EMessageLengthType _type)
    {
      byte[] numArray = new byte[2];
      switch (_type)
      {
        case EMessageLengthType.BCD:
          return IsoUltil.BinToBcd(_input, 2);
        case EMessageLengthType.HL:
          numArray[0] = (byte) (_input / 256);
          numArray[1] = (byte) (_input % 256);
          break;
        case EMessageLengthType.LH:
          numArray[1] = (byte) (_input / 256);
          numArray[0] = (byte) (_input % 256);
          break;
      }
      return numArray;
    }

    public static byte[] GetBytesFromBytes(byte[] _input, int offset, int len)
    {
      byte[] _arDest = new byte[len];
      IsoUltil.BytesCopy(_arDest, _input, 0, offset, len);
      return _arDest;
    }

    public static Encoding GetEncoding(ETextEncoding encoding)
    {
      switch (encoding)
      {
        case ETextEncoding.ASCII:
          return Encoding.ASCII;
        case ETextEncoding.BigEndianUnicode:
          return Encoding.BigEndianUnicode;
        case ETextEncoding.ANSI:
          return Encoding.Default;
        case ETextEncoding.Unicode:
          return Encoding.Unicode;
        case ETextEncoding.UTF32:
          return Encoding.UTF32;
        case ETextEncoding.UTF7:
          return Encoding.UTF7;
        case ETextEncoding.UTF8:
          return Encoding.UTF8;
        default:
          return (Encoding) null;
      }
    }
    public static byte[] ConvertToTIDIALERRule(byte[] _input, EMessageLengthType _LengthType)
    {
      byte[] numArray;
      if (_LengthType == EMessageLengthType.None)
      {
        numArray = new byte[_input.Length + 3];
        _input.CopyTo((Array) numArray, 1);
      }
      else
      {
        numArray = new byte[_input.Length + 5];
        IsoUltil.IntToMessageLength(_input.Length, _LengthType).CopyTo((Array) numArray, 1);
        _input.CopyTo((Array) numArray, 3);
      }
      byte num = 0;
      numArray[0] = (byte) 2;
      numArray[numArray.Length - 2] = (byte) 3;
      for (int index = 1; index < numArray.Length - 1; ++index)
        num ^= numArray[index];
      numArray[numArray.Length - 1] = num;
      return numArray;
    }

    public static bool BytesEqualled(byte[] ar1, byte[] ar2, int index1, int index2, int count)
    {
      if (ar1.Length < index1 + count || ar2.Length < index2 + count)
        return false;
      for (int index = 0; index < count; ++index)
      {
        if ((int) ar1[index + index1] != (int) ar2[index + index2])
          return false;
      }
      return true;
    }

    public static bool BytesEqualled(byte[] ar1, byte[] ar2)
    {
      if (ar1.Length != ar2.Length)
        return false;
      for (int index = 0; index < ar1.Length; ++index)
      {
        if ((int) ar1[index] != (int) ar2[index])
          return false;
      }
      return true;
    }

    public static byte[] HashSHA1(string str)
    {
      return SHA1.Create().ComputeHash(Encoding.Default.GetBytes(str));
    }  
  }
}
