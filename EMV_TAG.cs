
// Type: Iso8583Simu.EMV_TAG




using System;
using System.Collections;
using System.Text;

namespace Iso8583Simu
{
  public class EMV_TAG
  {
    public string TAG;
    public byte[] value;
    public int Length;
    private DS_EMV_INFO.EMV_TAGSRow EMVRow;
    public Exception HasException;

    public EMV_TAG(byte[] source, ref int index)
    {
      try
      {
        this.TAG = IsoUltil.BcdToString(IsoUltil.GetBytesFromBytes(source, index, 2));
        this.EMVRow = EMVAnalyzer.EMV_INFO.EMV_TAGS.FindByTAG(this.TAG.Substring(0, 2));
        if (this.EMVRow != null)
        {
          this.TAG = this.TAG.Substring(0, 2);
          this.Length = (int) source[index + 1];
          this.value = new byte[this.Length];
          IsoUltil.BytesCopy(this.value, source, 0, index + 2, this.Length);
          index += this.Length + 2;
        }
        else
        {
          this.EMVRow = EMVAnalyzer.EMV_INFO.EMV_TAGS.FindByTAG(this.TAG);
          if (this.EMVRow == null)
            throw new Exception("Invalid EMV tag " + this.TAG);
          this.Length = (int) source[index + 2];
          this.value = new byte[this.Length];
          IsoUltil.BytesCopy(this.value, source, 0, index + 3, this.Length);
          index += this.Length + 3;
        }
      }
      catch (Exception ex)
      {
        this.HasException = ex;
      }
    }

    public string ToString(E_EMVShowOption options)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.TAG + " Len=" + this.Length.ToString());
      if ((options & E_EMVShowOption.NAME) > E_EMVShowOption.None)
        stringBuilder.Append(" " + this.EMVRow.Name);
      if ((options & E_EMVShowOption.VALUE) > E_EMVShowOption.None)
      {
        if (this.EMVRow.Type.Substring(0, 1).ToLower() == "a")
          stringBuilder.Append(" \"" + Encoding.Default.GetString(this.value) + "\"");
        else
          stringBuilder.Append(" \"" + IsoUltil.BcdToString(this.value) + "\"");
      }
      if ((options & E_EMVShowOption.DESCRIPTION) > E_EMVShowOption.None)
        stringBuilder.Append("\r\n  " + this.EMVRow.Description);
      if ((options & E_EMVShowOption.BITS) > E_EMVShowOption.None)
      {
        DS_EMV_INFO.TAG_BITSRow[] tagBitsRows = this.EMVRow.GetTAG_BITSRows();
        if (tagBitsRows.Length > 0)
        {
          BitArray bitArray = new BitArray(this.value);
          foreach (DS_EMV_INFO.TAG_BITSRow tagBitsRow in tagBitsRows)
            stringBuilder.Append("\r\n      " + tagBitsRow.Description + (bitArray[tagBitsRow.BIT - 1] ? ": Yes" : ": No"));
        }
      }
      return stringBuilder.ToString();
    }
  }
}
