using System;

namespace FileStoreAdmin.Source.Util
{
    public class MathUtility
    {

        public static int ComputeTotalPage(int totalRows, int rowCount)
        {
            try
            {
                /*
                double d1 = (double)totalRows;
                double d2 = (double)rowCount;
                double d3 = d1 / d2;
                d3 = Math.Ceiling(d3);
                int totalPage = (int)d3;
                */
                int totalPage = (int)Math.Ceiling((double)totalRows / rowCount) - 1;
                totalPage = totalPage != -1 ? totalPage : 0;
                return totalPage;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}