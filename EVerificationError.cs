// Decompiled with JetBrains decompiler
// Type: TI.OnlineProcessing.EVerificationError
// Assembly: TI.OnlineProcessing, Version=1.2.11.2007, Culture=neutral, PublicKeyToken=null
// MVID: C5411956-B316-4CBA-92AE-51F12E6410B1
// Assembly location: C:\Users\thuocnv\Desktop\TI Security Gateway\TI.OnlineProcessing.dll

namespace Iso8583Simu
{
    public enum EVerificationError
    {
        NoError = 0,
        DisconnectedFromSource = 1,
        DisconnectedFromDestination = 2,
        Others = 3,
        Unknown = 4,
        MessageLengthError = 5,
        TimeOut = 6,
        ReceiveFromSourceErr = 7,
        ReceiveFromDestinationErr = 8,
        WrongHeader = 9,
        WrongSignature = 10,
        PackDataError = 11,
        DatabaseError = 12,
        WrongConfiguration = 13,
        WrongMAC = 14,
        NotSendLogonBefore = 13107,
        Declined = 13108,
        NoLicense = 13109,
        InvalidNII = 13110,
        SocketError = 13111,
        SSLError = 13112,
        ExceptionHandled = 13113,
    }
}
