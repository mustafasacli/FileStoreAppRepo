using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStoreAdmin.Source.Extensions
{
    static class DataExtension
    {


        /// <summary>
        /// Copies just DataColumn List.
        /// </summary>
        /// <param name="dt">DataTable object</param>
        /// <returns>returns a DataTable</returns>
        public static DataTable CopyColumns(this DataTable dt)
        {
            try
            {
                if (dt == null)
                    return null;

                DataTable dtN = new DataTable();

                foreach (DataColumn col in dt.Columns)
                {
                    dtN.Columns.Add(col.ColumnName, col.DataType);
                }

                return dtN;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
