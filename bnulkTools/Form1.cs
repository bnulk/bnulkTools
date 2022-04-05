using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bnulkTools.Help;

namespace bnulkTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guassian_1_ReadOuts_Click(object sender, EventArgs e)
        {
            Gaussian.App.ReadOuts readOuts = new Gaussian.App.ReadOuts();
            readOuts.Run();
        }

        private void gaussianToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void readOutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Gaussian_ReadOuts helpReadOuts = new Form_Gaussian_ReadOuts();
            helpReadOuts.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_About helpAbout = new Form_About();
            helpAbout.ShowDialog();
        }

        private void ConvertData_Click(object sender, EventArgs e)
        {
            Common.Convert_Data.FormUnitConvert formUnitConvert = new Common.Convert_Data.FormUnitConvert();
            formUnitConvert.ShowDialog();
        }

        private void TDA_Steep_Click(object sender, EventArgs e)
        {
            Gaussian.App.ReadTdaSteepProcess readTdaSteepProcess = new Gaussian.App.ReadTdaSteepProcess();
            readTdaSteepProcess.Run();
        }
    }
}
