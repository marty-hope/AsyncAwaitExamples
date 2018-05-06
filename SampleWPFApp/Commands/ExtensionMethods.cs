using System;
using System.Collections.Generic;
using System.Linq;
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

        public static int IndexOf<T>(this IEnumerable<T> obj, T value)
        {
            return obj.IndexOf(value, null);
        }

        private static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            var found = obj
                .Select((a, i) => new { a, i })
                .FirstOrDefault(x => comparer.Equals(x.a, value));
            return found?.i ?? -1;
        }



    }
}
