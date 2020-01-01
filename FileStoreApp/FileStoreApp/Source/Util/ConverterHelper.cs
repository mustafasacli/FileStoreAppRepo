using System;

namespace FileStoreApp.Source.Util
{
    public class ConverterHelper
    {
        public static int ToInt(object obj)
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

        public static int ToInt(string str)
        {
            int result = 0;
            try
            {
                int.TryParse(string.Format("{0}", str), out result);
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}