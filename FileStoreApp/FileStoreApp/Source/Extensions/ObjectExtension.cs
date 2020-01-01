using System;

namespace FileStoreApp.Source.Extensions
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
    }
}