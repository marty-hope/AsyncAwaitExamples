using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SampleWPFApp.Commands
{
    internal static class ExtensionMethods
    {
        internal static String SecureStringToString(this SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        internal static double MillisecondsToSeconds(this long millseconds)
        {
            return (double)millseconds / (double)1000;
        }
    }
}
