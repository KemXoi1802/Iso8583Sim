
// Type: Iso8583Simu.EMVAnalyzer




using System;
using System.Collections.Generic;
using System.Text;

namespace Iso8583Simu
{
  public class EMVAnalyzer
  {
    private static DS_EMV_INFO m_EMV_INFO;

    public static DS_EMV_INFO EMV_INFO
    {
      get
      {
        if (EMVAnalyzer.m_EMV_INFO == null)
        {
          EMVAnalyzer.m_EMV_INFO = new DS_EMV_INFO();
          try
          {
            int num = (int) EMVAnalyzer.m_EMV_INFO.ReadXml("emvinfo.xml");
          }
          catch (Exception ex)
          {
          }
        }
        return EMVAnalyzer.m_EMV_INFO;
      }
    }

    public static void SaveEMVInfo(DS_EMV_INFO data)
    {
      data.WriteXml("emvinfo.xml");
    }

    public static List<EMV_TAG> GetEMVTAGs(byte[] source)
    {
      List<EMV_TAG> emvTagList = new List<EMV_TAG>();
      int index = 0;
      while (index + 3 < source.Length)
      {
        EMV_TAG emvTag = new EMV_TAG(source, ref index);
        if (emvTag.HasException == null)
          emvTagList.Add(emvTag);
        else
          break;
      }
      return emvTagList;
    }

    public static string GetFullDescription(byte[] source, E_EMVShowOption options)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (EMV_TAG emvtaG in EMVAnalyzer.GetEMVTAGs(source))
      {
        stringBuilder.Append(emvtaG.ToString(options));
        stringBuilder.Append("\r\n");
      }
      return stringBuilder.ToString();
    }
  }
}
