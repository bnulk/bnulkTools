
using System.Windows.Forms;

namespace bnulkTools.ErrorInfo
{
    internal class DisposeError_App
    {
        string errorInfo;
        public DisposeError_App(string errorInfo) 
        {
            this.errorInfo = errorInfo;
        }

        public void Run()
        {
            MessageBox.Show(this.errorInfo);
        }

    }
}
