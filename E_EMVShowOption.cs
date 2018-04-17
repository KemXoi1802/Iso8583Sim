
// Type: Iso8583Simu.E_EMVShowOption




using System;

namespace Iso8583Simu
{
  [Flags]
  public enum E_EMVShowOption
  {
    None = 0,
    Len = 1,
    VALUE = 2,
    NAME = 4,
    DESCRIPTION = 8,
    BITS = 16,
  }
}
