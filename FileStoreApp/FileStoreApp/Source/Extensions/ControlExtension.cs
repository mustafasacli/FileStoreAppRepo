using System;
using System.Windows.Forms;

namespace FileStoreApp.Source.Extensions
{
    internal static class ControlExtension
    {
        internal static void PrepareGrid(this DataGridView grid, string columnListSource, char delimiter)
        {
            try
            {
                columnListSource = string.Format("{0}", columnListSource);
                string[] cols = columnListSource.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

                if (cols == null)
                    cols = new string[] { };

                if (cols.Length == grid.Columns.Count)
                {
                    for (int counter = 0; counter < cols.Length; counter++)
                    {
                        grid.Columns[counter].HeaderText = cols[counter];
                        if (grid.Columns[counter].HeaderText.Contains("[i]"))
                        {
                            grid.Columns[counter].Visible = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}