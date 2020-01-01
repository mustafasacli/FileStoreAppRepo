using System;

namespace FileStoreAdmin.Source.Extensions
{
    internal static class ObjectExtension
    {
        internal static int ToInt(this object obj)
        {
            int result = 0;

            try
            {
                int.TryParse(string.Format("{0}", obj), out result);
            }
            catch (Exception)
            {
            }

            return result;
        }

        internal static string ToStr(this object obj)
        {
            string result = string.Empty;
            try
            {
                result = string.Format("{0}", obj);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }

        internal static DateTime LastTimeOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }

        internal static DateTime FirstTimeOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        internal static string TrimAll(this string str)
        {
            try
            {
                if (str == null)
                    return str;

                if (str.Length == 0)
                    return str;

                return str.Replace(" ", "");
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static int LengthOfString(this string str)
        {
            try
            {
                if (str == null)
                    return -1;

                return str.Length;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static bool IsEqual(this string str1, string str2)
        {
            try
            {
                if (str1 == null && str2 == null)
                {
                    return true;
                }


                if (str1 != null && str2 != null)
                {
                    return string.Equals(str1, str2);
                }

                return false;

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}