﻿namespace CortexAccess
{
    static class Config
    {
        public static int DebitNumber = 5;

        public static string LicenseId = "";
        public static string AppClientId = "41HNxTe3rzaQYcjtdibRIqgLpvbdwITE74uLulzx";
        public static string AppClientSecret = "vNwPBI6ldD7nl9W3AaQeqs40cV5iJfoa4te2R6En7INyZRXeQrlXedrHTyhv8cSxfBqhbeRjnmIHg7vB2QZTlWWZOyD0waybH8UWDcoXI4WFExg8rnOgO3Ug7Mzv4LrJ";

        // default debit number

    }

    public static class WarningCode
    {
        public const int StreamStop = 0;
        public const int SessionAutoClosed = 1;
        public const int UserLogin = 2;
        public const int UserLogout = 3;
        public const int ExtenderExportSuccess = 4;
        public const int ExtenderExportFailed = 5;
        public const int UserNotAcceptLicense = 6;
        public const int UserNotHaveAccessRight = 7;
        public const int UserRequestAccessRight = 8;
        public const int AccessRightGranted = 9;
        public const int AccessRightRejected = 10;
        public const int CannotDetectOSUSerInfo = 11;
        public const int CannotDetectOSUSername = 12;
        public const int ProfileLoaded = 13;
        public const int ProfileUnloaded = 14;
        public const int CortexAutoUnloadProfile = 15;
        public const int UserLoginOnAnotherOsUser = 16;
        public const int EULAAccepted = 17;
        public const int StreamWritingClosed = 18;
    }
}
