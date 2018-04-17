





using System;
using System.ComponentModel;
using System.IO;
//using System.Runtime.Serialization.Formatters.Soap;

namespace Iso8583Simu
{
  public class BitTemplate
  {
    public static string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }

    //public static BitTemplate.Bit_Specific[] GetBINARYpecificArray(string filename)
    //{
    //  SoapFormatter soapFormatter = new SoapFormatter();
    //  FileStream fileStream = new FileStream(filename, FileMode.Open);
    //  BitTemplate.Bit_Specific[] bitSpecificArray = soapFormatter.Deserialize((Stream) fileStream) as BitTemplate.Bit_Specific[];
    //  fileStream.Close();
    //  return bitSpecificArray;
    //}

    //public static void WriteBitArributeArray(BitTemplate.Bit_Specific[] _input, string filename)
    //{
    //  SoapFormatter soapFormatter = new SoapFormatter();
    //  FileStream fileStream = new FileStream(filename, FileMode.Create);
    //  soapFormatter.Serialize((Stream) fileStream, (object) _input);
    //  fileStream.Close();
    //}

    public static BitAttribute[] GetBitAttributeArray(BitTemplate.Bit_Specific[] BINARY)
    {
      BitAttribute[] bitAttributeArray = new BitAttribute[128];
      for (int index = 0; index < BINARY.Length; ++index)
      {
        bitAttributeArray[(int) BINARY[index].bitNumber - 1] = new BitAttribute();
        bitAttributeArray[(int) BINARY[index].bitNumber - 1].LengthAttribute = BINARY[index].bitLength;
        bitAttributeArray[(int) BINARY[index].bitNumber - 1].TypeAtribute = BINARY[index].bitType;
        bitAttributeArray[(int) BINARY[index].bitNumber - 1].MaxLength = BINARY[index].maxLength;
        bitAttributeArray[(int) BINARY[index].bitNumber - 1].AddtionalOption = BINARY[index].addtionalOption;
      }
      for (int index = 0; index < 128; ++index)
      {
        if (bitAttributeArray[index] == null)
        {
          bitAttributeArray[index] = new BitAttribute();
          bitAttributeArray[index].TypeAtribute = BitType.NOT_SPECIFIC;
        }
      }
      return bitAttributeArray;
    }

    public static BitTemplate.Bit_Specific[] GetTemplate_Standard()
    {
      return new BitTemplate.Bit_Specific[67]
      {
        new BitTemplate.Bit_Specific((byte) 1, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 2, BitLength.LLVAR, BitType.BCD, 20),
        new BitTemplate.Bit_Specific((byte) 3, BitLength.FIXED, BitType.BCD, 6),
        new BitTemplate.Bit_Specific((byte) 4, BitLength.FIXED, BitType.BCD, 12),
        new BitTemplate.Bit_Specific((byte) 5, BitLength.FIXED, BitType.BCD, 12),
        new BitTemplate.Bit_Specific((byte) 6, BitLength.FIXED, BitType.BCD, 12),
        new BitTemplate.Bit_Specific((byte) 7, BitLength.FIXED, BitType.BCD, 10),
        new BitTemplate.Bit_Specific((byte) 8, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 9, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 10, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 11, BitLength.FIXED, BitType.BCD, 6),
        new BitTemplate.Bit_Specific((byte) 12, BitLength.FIXED, BitType.BCD, 6),
        new BitTemplate.Bit_Specific((byte) 13, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 14, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 15, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 16, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 17, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 18, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 19, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 20, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 21, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 22, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 23, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 24, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 25, BitLength.FIXED, BitType.BCD, 2),
        new BitTemplate.Bit_Specific((byte) 26, BitLength.FIXED, BitType.BCD, 2),
        new BitTemplate.Bit_Specific((byte) 27, BitLength.FIXED, BitType.BCD, 1),
        new BitTemplate.Bit_Specific((byte) 28, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 29, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 30, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 31, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 32, BitLength.LLVAR, BitType.BCD, 11),
        new BitTemplate.Bit_Specific((byte) 33, BitLength.LLVAR, BitType.BCD, 11),
        new BitTemplate.Bit_Specific((byte) 34, BitLength.LLVAR, BitType.BCD, 28),
        new BitTemplate.Bit_Specific((byte) 35, BitLength.LLVAR, BitType.BCD, 37),
        new BitTemplate.Bit_Specific((byte) 36, BitLength.LLLVAR, BitType.BCD, 104),
        new BitTemplate.Bit_Specific((byte) 37, BitLength.FIXED, BitType.ANS, 12),
        new BitTemplate.Bit_Specific((byte) 38, BitLength.FIXED, BitType.AN, 6),
        new BitTemplate.Bit_Specific((byte) 39, BitLength.FIXED, BitType.AN, 2),
        new BitTemplate.Bit_Specific((byte) 40, BitLength.LLLVAR, BitType.BINARY, 0),
        new BitTemplate.Bit_Specific((byte) 41, BitLength.FIXED, BitType.AN, 8),
        new BitTemplate.Bit_Specific((byte) 42, BitLength.FIXED, BitType.AN, 15),
        new BitTemplate.Bit_Specific((byte) 43, BitLength.FIXED, BitType.AN, 40),
        new BitTemplate.Bit_Specific((byte) 44, BitLength.LLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 45, BitLength.LLVAR, BitType.AN, 76),
        new BitTemplate.Bit_Specific((byte) 46, BitLength.LLLVAR, BitType.AN, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 47, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 48, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 49, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 52, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 53, BitLength.FIXED, BitType.BCD, 16),
        new BitTemplate.Bit_Specific((byte) 54, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 55, BitLength.LLLVAR, BitType.AN, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 56, BitLength.LLLVAR, BitType.AN, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 57, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 60, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 61, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 62, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 63, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 64, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 70, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 90, BitLength.FIXED, BitType.BCD, 42),
        new BitTemplate.Bit_Specific((byte) 102, BitLength.LLVAR, BitType.ANS, 28),
        new BitTemplate.Bit_Specific((byte) 103, BitLength.LLVAR, BitType.ANS, 28),
        new BitTemplate.Bit_Specific((byte) 105, BitLength.LLLVAR, BitType.ANS, 999),
        new BitTemplate.Bit_Specific((byte) 120, BitLength.LLLVAR, BitType.ANS, 999),
        new BitTemplate.Bit_Specific((byte) 128, BitLength.FIXED, BitType.BINARY, 8)
      };
    }

    //public static BitAttribute[] GetBitAttributeArray(string filename)
    //{
    //  //return BitTemplate.GetBitAttributeArray(BitTemplate.GetBINARYpecificArray(filename));
    //}

    public static BitAttribute[] GetGeneralTemplate()
    {
      return BitTemplate.GetBitAttributeArray(new BitTemplate.Bit_Specific[67]
      {
        new BitTemplate.Bit_Specific((byte) 1, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 2, BitLength.LLVAR, BitType.BCD, 20),
        new BitTemplate.Bit_Specific((byte) 3, BitLength.FIXED, BitType.BCD, 6),
        new BitTemplate.Bit_Specific((byte) 4, BitLength.FIXED, BitType.BCD, 12),
        new BitTemplate.Bit_Specific((byte) 5, BitLength.FIXED, BitType.BCD, 12),
        new BitTemplate.Bit_Specific((byte) 6, BitLength.FIXED, BitType.BCD, 12),
        new BitTemplate.Bit_Specific((byte) 7, BitLength.FIXED, BitType.BCD, 10),
        new BitTemplate.Bit_Specific((byte) 8, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 9, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 10, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 11, BitLength.FIXED, BitType.BCD, 6),
        new BitTemplate.Bit_Specific((byte) 12, BitLength.FIXED, BitType.BCD, 6),
        new BitTemplate.Bit_Specific((byte) 13, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 14, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 15, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 16, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 17, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 18, BitLength.FIXED, BitType.BCD, 4),
        new BitTemplate.Bit_Specific((byte) 19, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 20, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 21, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 22, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 23, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 24, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 25, BitLength.FIXED, BitType.BCD, 2),
        new BitTemplate.Bit_Specific((byte) 26, BitLength.FIXED, BitType.BCD, 2),
        new BitTemplate.Bit_Specific((byte) 27, BitLength.FIXED, BitType.BCD, 1),
        new BitTemplate.Bit_Specific((byte) 28, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 29, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 30, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 31, BitLength.FIXED, BitType.BCD, 8),
        new BitTemplate.Bit_Specific((byte) 32, BitLength.LLVAR, BitType.BCD, 11),
        new BitTemplate.Bit_Specific((byte) 33, BitLength.LLVAR, BitType.BCD, 11),
        new BitTemplate.Bit_Specific((byte) 34, BitLength.LLVAR, BitType.BCD, 28),
        new BitTemplate.Bit_Specific((byte) 35, BitLength.LLVAR, BitType.BCD, 37),
        new BitTemplate.Bit_Specific((byte) 36, BitLength.LLLVAR, BitType.BCD, 104),
        new BitTemplate.Bit_Specific((byte) 37, BitLength.FIXED, BitType.AN, 12),
        new BitTemplate.Bit_Specific((byte) 38, BitLength.FIXED, BitType.AN, 6),
        new BitTemplate.Bit_Specific((byte) 39, BitLength.FIXED, BitType.AN, 2),
        new BitTemplate.Bit_Specific((byte) 40, BitLength.LLLVAR, BitType.BINARY, 0),
        new BitTemplate.Bit_Specific((byte) 41, BitLength.FIXED, BitType.AN, 8),
        new BitTemplate.Bit_Specific((byte) 42, BitLength.FIXED, BitType.AN, 15),
        new BitTemplate.Bit_Specific((byte) 43, BitLength.FIXED, BitType.AN, 40),
        new BitTemplate.Bit_Specific((byte) 44, BitLength.LLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 45, BitLength.LLVAR, BitType.AN, 76),
        new BitTemplate.Bit_Specific((byte) 46, BitLength.LLLVAR, BitType.AN, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 47, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 48, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 49, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 52, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 53, BitLength.FIXED, BitType.BCD, 16),
        new BitTemplate.Bit_Specific((byte) 54, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 55, BitLength.LLLVAR, BitType.ANS, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 56, BitLength.LLLVAR, BitType.ANS, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 57, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 60, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 61, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 62, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 63, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 64, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 70, BitLength.FIXED, BitType.BCD, 3),
        new BitTemplate.Bit_Specific((byte) 90, BitLength.FIXED, BitType.BCD, 42),
        new BitTemplate.Bit_Specific((byte) 102, BitLength.LLVAR, BitType.ANS, 28),
        new BitTemplate.Bit_Specific((byte) 103, BitLength.LLVAR, BitType.ANS, 28),
        new BitTemplate.Bit_Specific((byte) 105, BitLength.LLLVAR, BitType.ANS, 999),
        new BitTemplate.Bit_Specific((byte) 120, BitLength.LLLVAR, BitType.ANS, 999),
        new BitTemplate.Bit_Specific((byte) 128, BitLength.FIXED, BitType.BINARY, 8)
      });
    }

    public static BitTemplate.Bit_Specific[] GetSmartlinkTemplate()
    {
      return new BitTemplate.Bit_Specific[67]
      {
        new BitTemplate.Bit_Specific((byte) 1, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 2, BitLength.LLVAR, BitType.ANS, 20),
        new BitTemplate.Bit_Specific((byte) 3, BitLength.FIXED, BitType.ANS, 6),
        new BitTemplate.Bit_Specific((byte) 4, BitLength.FIXED, BitType.ANS, 12),
        new BitTemplate.Bit_Specific((byte) 5, BitLength.FIXED, BitType.ANS, 12),
        new BitTemplate.Bit_Specific((byte) 6, BitLength.FIXED, BitType.ANS, 12),
        new BitTemplate.Bit_Specific((byte) 7, BitLength.FIXED, BitType.ANS, 10),
        new BitTemplate.Bit_Specific((byte) 8, BitLength.FIXED, BitType.ANS, 8),
        new BitTemplate.Bit_Specific((byte) 9, BitLength.FIXED, BitType.ANS, 8),
        new BitTemplate.Bit_Specific((byte) 10, BitLength.FIXED, BitType.ANS, 8),
        new BitTemplate.Bit_Specific((byte) 11, BitLength.FIXED, BitType.ANS, 6),
        new BitTemplate.Bit_Specific((byte) 12, BitLength.FIXED, BitType.ANS, 6),
        new BitTemplate.Bit_Specific((byte) 13, BitLength.FIXED, BitType.ANS, 4),
        new BitTemplate.Bit_Specific((byte) 14, BitLength.FIXED, BitType.ANS, 4),
        new BitTemplate.Bit_Specific((byte) 15, BitLength.FIXED, BitType.ANS, 4),
        new BitTemplate.Bit_Specific((byte) 16, BitLength.FIXED, BitType.ANS, 4),
        new BitTemplate.Bit_Specific((byte) 17, BitLength.FIXED, BitType.ANS, 4),
        new BitTemplate.Bit_Specific((byte) 18, BitLength.FIXED, BitType.ANS, 4),
        new BitTemplate.Bit_Specific((byte) 19, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 20, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 21, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 22, BitLength.FIXED, BitType.ANS, 4),
        new BitTemplate.Bit_Specific((byte) 23, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 24, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 25, BitLength.FIXED, BitType.ANS, 2),
        new BitTemplate.Bit_Specific((byte) 26, BitLength.FIXED, BitType.ANS, 2),
        new BitTemplate.Bit_Specific((byte) 27, BitLength.FIXED, BitType.ANS, 1),
        new BitTemplate.Bit_Specific((byte) 28, BitLength.FIXED, BitType.ANS, 8),
        new BitTemplate.Bit_Specific((byte) 29, BitLength.FIXED, BitType.ANS, 8),
        new BitTemplate.Bit_Specific((byte) 30, BitLength.FIXED, BitType.ANS, 8),
        new BitTemplate.Bit_Specific((byte) 31, BitLength.FIXED, BitType.ANS, 8),
        new BitTemplate.Bit_Specific((byte) 32, BitLength.LLVAR, BitType.ANS, 11),
        new BitTemplate.Bit_Specific((byte) 33, BitLength.LLVAR, BitType.ANS, 11),
        new BitTemplate.Bit_Specific((byte) 34, BitLength.LLVAR, BitType.ANS, 28),
        new BitTemplate.Bit_Specific((byte) 35, BitLength.LLVAR, BitType.ANS, 37),
        new BitTemplate.Bit_Specific((byte) 36, BitLength.LLLVAR, BitType.ANS, 104),
        new BitTemplate.Bit_Specific((byte) 37, BitLength.FIXED, BitType.AN, 12),
        new BitTemplate.Bit_Specific((byte) 38, BitLength.FIXED, BitType.AN, 6),
        new BitTemplate.Bit_Specific((byte) 39, BitLength.FIXED, BitType.AN, 2),
        new BitTemplate.Bit_Specific((byte) 40, BitLength.LLLVAR, BitType.BINARY, 0),
        new BitTemplate.Bit_Specific((byte) 41, BitLength.FIXED, BitType.AN, 8),
        new BitTemplate.Bit_Specific((byte) 42, BitLength.FIXED, BitType.AN, 15),
        new BitTemplate.Bit_Specific((byte) 43, BitLength.FIXED, BitType.AN, 40),
        new BitTemplate.Bit_Specific((byte) 44, BitLength.LLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 45, BitLength.LLVAR, BitType.AN, 76),
        new BitTemplate.Bit_Specific((byte) 46, BitLength.LLLVAR, BitType.AN, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 47, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 48, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 49, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 52, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 53, BitLength.FIXED, BitType.ANS, 16),
        new BitTemplate.Bit_Specific((byte) 54, BitLength.LLLVAR, BitType.AN, 0),
        new BitTemplate.Bit_Specific((byte) 55, BitLength.LLLVAR, BitType.ANS, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 56, BitLength.LLLVAR, BitType.ANS, (int) byte.MaxValue),
        new BitTemplate.Bit_Specific((byte) 57, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 60, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 61, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 62, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 63, BitLength.LLLVAR, BitType.ANS, 0),
        new BitTemplate.Bit_Specific((byte) 64, BitLength.FIXED, BitType.BINARY, 8),
        new BitTemplate.Bit_Specific((byte) 70, BitLength.FIXED, BitType.ANS, 3),
        new BitTemplate.Bit_Specific((byte) 90, BitLength.FIXED, BitType.ANS, 42),
        new BitTemplate.Bit_Specific((byte) 102, BitLength.LLVAR, BitType.ANS, 28),
        new BitTemplate.Bit_Specific((byte) 103, BitLength.LLVAR, BitType.ANS, 28),
        new BitTemplate.Bit_Specific((byte) 105, BitLength.LLLVAR, BitType.ANS, 999),
        new BitTemplate.Bit_Specific((byte) 120, BitLength.LLLVAR, BitType.ANS, 999),
        new BitTemplate.Bit_Specific((byte) 128, BitLength.FIXED, BitType.BINARY, 8)
      };
    }

    public class Bit_SpecificConverter : TypeConverter
    {
      public override bool GetPropertiesSupported(ITypeDescriptorContext context)
      {
        return true;
      }

      public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
      {
        return TypeDescriptor.GetProperties(typeof (BitTemplate.Bit_Specific));
      }

      public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
      {
        return base.GetStandardValues(context);
      }

      public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
      {
        return true;
      }
    }

    [TypeConverter(typeof (BitTemplate.Bit_SpecificConverter))]
    [Serializable]
    public struct Bit_Specific
    {
      public byte bitNumber;
      public BitLength bitLength;
      public BitType bitType;
      public int maxLength;
      public EAddtionalOption addtionalOption;

      public byte BitNumber
      {
        get
        {
          return this.bitNumber;
        }
        set
        {
          this.bitNumber = value;
        }
      }

      public BitLength LengthType
      {
        get
        {
          return this.bitLength;
        }
        set
        {
          this.bitLength = value;
        }
      }

      public BitType FormatType
      {
        get
        {
          return this.bitType;
        }
        set
        {
          this.bitType = value;
        }
      }

      public int MaxLength
      {
        set
        {
          this.maxLength = value;
        }
        get
        {
          return this.maxLength;
        }
      }

      public override string ToString()
      {
        return string.Format("{0,-4}{1,-10}{2,-4}{3,-10}", (object) this.bitNumber.ToString(), (object) this.bitLength.ToString(), (object) this.maxLength.ToString(), (object) this.bitType.ToString());
      }

      public Bit_Specific(byte bitNo, BitLength bitLenAtrr, BitType bitTypeAtrr, int maxLen)
      {
        this.addtionalOption = EAddtionalOption.None;
        this.bitNumber = bitNo;
        this.bitLength = bitLenAtrr;
        this.bitType = bitTypeAtrr;
        this.maxLength = maxLen;
      }

      public Bit_Specific(byte bitNo, BitLength bitLenAtrr, BitType bitTypeAtrr, int maxLen, EAddtionalOption option)
      {
        this.bitNumber = bitNo;
        this.bitLength = bitLenAtrr;
        this.bitType = bitTypeAtrr;
        this.maxLength = maxLen;
        this.addtionalOption = option;
      }
    }
  }
}
