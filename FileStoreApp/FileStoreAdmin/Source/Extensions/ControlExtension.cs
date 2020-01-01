using FileStoreAdmin.Source.Variables;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FileStoreAdmin.Source.Extensions
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

                if (cols.Length == grid.ColumnCount)
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

        internal static void LoadString2ComboBox(this ComboBox cmbx, string resource, char keyLimiter, char valueLimiter)
        {
            try
            {
                if (cmbx == null)
                    throw new Exception("Combobox can not be null.");

                if (resource == null)
                {
                    cmbx.DataSource = null;
                    cmbx.Refresh();
                    return;
                }

                List<KeyValueObject> keys = new List<KeyValueObject>();
                string[] keysArr = resource.Split(new char[] { keyLimiter }, StringSplitOptions.RemoveEmptyEntries);
                string[] valsArr = null;
                KeyValueObject keyVal;

                foreach (var item in keysArr)
                {
                    valsArr = null;
                    valsArr = item.Split(new char[] { valueLimiter }, StringSplitOptions.RemoveEmptyEntries);
                    if (valsArr != null)
                    {
                        if (valsArr.Length == 2)
                        {
                            keyVal = new KeyValueObject() { Text = valsArr[0], Value = valsArr[1] };
                            keys.Add(keyVal);
                        }
                    }
                }

                cmbx.DataSource = keys;
                cmbx.DisplayMember = "Text";
                cmbx.ValueMember = "Value";
                cmbx.SelectedIndex = -1;
                cmbx.Refresh();
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void LoadInt2Combo(this ComboBox cmbx, int endInteger)
        {
            try
            {
                if (cmbx == null)
                    throw new Exception("Combobox can not be null.");

                cmbx.Items.Clear();

                if (endInteger < 0)
                    throw new Exception("end ineteger can not be less than 1.");

                for (int counter = 0; counter <= endInteger; counter++)
                {
                    cmbx.Items.Add(counter + 1);
                }

                cmbx.SelectedIndex = cmbx.Items.Count > 0 ? 0 : -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetSelectedText(this ComboBox cmbx)
        {
            try
            {
                if (cmbx == null)
                    throw new Exception("Combobox can not be null.");

                if (cmbx.SelectedItem == null)
                    return null;

                string str = cmbx.GetItemText(cmbx.SelectedItem);

                return str;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Disable(this Control ctrl)
        {
            try
            {
                if (ctrl == null)
                    throw new Exception("Control can not be null.");

                ctrl.Enabled = false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void LoadKeyValueList2Combo(this ComboBox cmbx, List<KeyValueObject> list)
        {
            try
            {
                if (cmbx == null)
                    throw new Exception("Combobox can not be null.");

                cmbx.DataSource = list;
                cmbx.DisplayMember = "Text";
                cmbx.ValueMember = "Value";
                cmbx.SelectedIndex = -1;
                cmbx.Refresh();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}