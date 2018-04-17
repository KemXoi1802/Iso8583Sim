using System;

namespace Iso8583Simu
{
  public class TPDU
  {
    public byte[] rawTPDU = new byte[5]
    {
      (byte) 96,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0
    };
    public TPDU.TPDUType Id;

    public int DestAddr
    {
      get
      {
        return IsoUltil.BcdToBin(IsoUltil.CreatBytesFromArray(this.rawTPDU, 1, 2));
      }
      set
      {
        IsoUltil.BinToBcd(value, 2).CopyTo((Array) this.rawTPDU, 1);
      }
    }

    public int OrigAddr
    {
      get
      {
        return IsoUltil.BcdToBin(IsoUltil.CreatBytesFromArray(this.rawTPDU, 3, 2));
      }
      set
      {
        IsoUltil.BinToBcd(value, 2).CopyTo((Array) this.rawTPDU, 3);
      }
    }

    public byte[] Pack()
    {
      return this.rawTPDU.Clone() as byte[];
    }

    public static string AboutUs
    {
      get
      {
        return "Thuocnv, ETC-Solution";
      }
    }

    public void UnPack(byte[] ar)
    {
      this.rawTPDU = (byte[]) ar.Clone();
    }

    public void SwapNII()
    {
      byte[] numArray = new byte[2];
      IsoUltil.BytesCopy(numArray, this.rawTPDU, 0, 1, 2);
      IsoUltil.BytesCopy(this.rawTPDU, this.rawTPDU, 1, 3, 2);
      IsoUltil.BytesCopy(this.rawTPDU, numArray, 3, 0, 2);
    }

    public enum TPDUType
    {
      Transactions,
      NMS_TNMS,
    }
  }
}
