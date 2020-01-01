using System.Windows.Forms;

namespace FileStoreAdmin.Source.Views.Logs
{
    public partial class FrmStackTrace : Form
    {
        public FrmStackTrace(string stackTrace)
        {
            InitializeComponent();
            txtStackTrace.Text = stackTrace;
        }
    }
}
