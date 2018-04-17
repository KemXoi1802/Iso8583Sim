using System;

namespace Iso8583Simu
{
  public class Iso8583Exception : Exception
  {
    public Iso8583Exception(string str)
      : base(str)
    {
    }

    public static string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }
  }
}
