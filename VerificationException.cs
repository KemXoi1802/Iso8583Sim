// Decompiled with JetBrains decompiler
// Type: TI.OnlineProcessing.VerificationException
// Assembly: TI.OnlineProcessing, Version=1.2.11.2007, Culture=neutral, PublicKeyToken=null
// MVID: C5411956-B316-4CBA-92AE-51F12E6410B1
// Assembly location: C:\Users\thuocnv\Desktop\TI Security Gateway\TI.OnlineProcessing.dll

using System;

namespace Iso8583Simu
{
    public class VerificationException : Exception
    {
        private EVerificationError m_Error = EVerificationError.Unknown;

        public EVerificationError Error
        {
            get
            {
                return this.m_Error;
            }
        }

        public VerificationException(string s)
          : base(s)
        {
        }

        public VerificationException(string s, EVerificationError error)
          : base(s)
        {
            this.m_Error = error;
        }

        public override string ToString()
        {
            switch (this.m_Error)
            {
                case EVerificationError.DisconnectedFromSource:
                case EVerificationError.DisconnectedFromDestination:
                    return this.Message;
                case EVerificationError.MessageLengthError:
                    return "MESSAGE LENGTH ERROR: " + this.Message;
                case EVerificationError.TimeOut:
                    return "TIME OUT: " + this.Message;
                case EVerificationError.WrongHeader:
                    return "HEADER WAS INCORRECT: " + this.Message;
                case EVerificationError.WrongSignature:
                    return "INVALID SIGNATURE/PASSWORD";
                case EVerificationError.WrongMAC:
                    return "INVALID MAC (MESSAGE AUTHENTICATION CODE)";
                case EVerificationError.NotSendLogonBefore:
                    return "NOT LOGON";
                case EVerificationError.Declined:
                    return "DECLINED: " + this.Message;
                case EVerificationError.NoLicense:
                    return "NO LICENSE OR LICENSE EXPIRED";
                case EVerificationError.InvalidNII:
                    return "INVALID NII";
                case EVerificationError.ExceptionHandled:
                    return "";
                default:
                    return this.Message;
            }
        }
    }
}
