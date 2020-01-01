using FileStoreAdmin.Source.Variables;
using System.Windows.Forms;

namespace FileStoreAdmin.Source.MessageUtil
{
    static class Messaging
    {

        internal static void Error(string errorString)
        {
            MessageBox.Show(errorString, Constants.ErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static void Warning(string warningString)
        {
            MessageBox.Show(warningString, Constants.WarningHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        internal static DialogResult Confirm(string confirmString)
        {
            return MessageBox.Show(confirmString, Constants.ConfirmHeader, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        internal static void Info(string infoString)
        {
            MessageBox.Show(infoString, Constants.InfoHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
