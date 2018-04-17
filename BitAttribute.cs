
using System;
using System.Text;

namespace Iso8583Simu
{
  [Serializable]
  public class BitAttribute
  {
    private bool m_IsSet = false;
    private EAddtionalOption m_Option = EAddtionalOption.None;
    private BitLength m_LengthAttribute;
    private int m_MaxLength;
    private int m_Length;
    private byte[] m_Data;
    private BitType m_BitType;

    public bool IsSet
    {
      get
      {
        return this.m_IsSet;
      }
      set
      {
        this.m_IsSet = value;
      }
    }

    public BitLength LengthAttribute
    {
      set
      {
        this.m_LengthAttribute = value;
      }
      get
      {
        return this.m_LengthAttribute;
      }
    }

    public int MaxLength
    {
      set
      {
        this.m_MaxLength = value;
      }
      get
      {
        return this.m_MaxLength;
      }
    }

    public int Length
    {
      get
      {
        return this.m_Length;
      }
      set
      {
        this.m_Length = value;
      }
    }

    public string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }

    public BitType TypeAtribute
    {
      set
      {
        this.m_BitType = value;
      }
      get
      {
        return this.m_BitType;
      }
    }

    public byte[] Data
    {
      set
      {
        this.m_Data = value;
      }
      get
      {
        return this.m_Data;
      }
    }

    public EAddtionalOption AddtionalOption
    {
      get
      {
        return this.m_Option;
      }
      set
      {
        this.m_Option = value;
      }
    }

    public string GetString()
    {
      if (this.TypeAtribute != BitType.BCD)
        return IsoUltil.AscToString(this.m_Data);
      string str = IsoUltil.BcdToString(this.m_Data);
      if (this.Length % 2 == 1)
        str = this.Length >= 6 ? str.Substring(0, str.Length - 1) : str.Substring(1, str.Length - 1);
      return str;
    }

    public int GetInt()
    {
      if (this.TypeAtribute == BitType.BCD)
        return int.Parse(IsoUltil.BcdToString(this.m_Data));
      return int.Parse(IsoUltil.AscToString(this.m_Data));
    }

    public override string ToString()
    {
      if (this.m_Option == EAddtionalOption.HideAll)
        return "*";
      switch (this.TypeAtribute)
      {
        case BitType.AN:
        case BitType.ANS:
          string str1 = Encoding.Default.GetString(this.m_Data).Replace(char.MinValue, '.');
          if (this.m_Option == EAddtionalOption.Hide12DigitsOfTrack2 && str1.Length > 12)
            str1 = "************" + str1.Substring(12, str1.Length > 21 ? 9 : str1.Length - 12);
          return str1;
        case BitType.BCD:
          string str2 = IsoUltil.BcdToString(this.m_Data);
          if (this.m_Option == EAddtionalOption.Hide12DigitsOfTrack2 && str2.Length > 12)
            str2 = "************" + str2.Substring(12, str2.Length > 21 ? 9 : str2.Length - 12);
          return str2;
        case BitType.BINARY:
          return IsoUltil.BytesToHexString(this.m_Data, 32, false);
        default:
          return "";
      }
    }
  }
}
