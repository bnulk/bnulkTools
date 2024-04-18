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

        private void button_mixedBasisSet_Click(object sender, EventArgs e)
        {
            Common.Form_ForMixBasisSet form_ForMixBasisSet = new Common.Form_ForMixBasisSet();
            form_ForMixBasisSet.ShowDialog();
        }

        private void button_FreezeNonHydrogenAtom_Click(object sender, EventArgs e)
        {
            Gaussian.OniomTools.FreezeNonHydrogenAtom freezeNonHydrogenAtom= new Gaussian.OniomTools.FreezeNonHydrogenAtom();
            freezeNonHydrogenAtom.Run();
        }

        private void TDDFT_Steep_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_ActivateHighLevelPeripheralAtoms_Click(object sender, EventArgs e)
        {
            Gaussian.OniomTools.ActivateHighLevelPeripheralAtoms activateHighLevelPeripheralAtoms= new Gaussian.OniomTools.ActivateHighLevelPeripheralAtoms();
            activateHighLevelPeripheralAtoms.Run();
        }

        private void gaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button_FreezeLowLevel_Click(object sender, EventArgs e)
        {
            Gaussian.OniomTools.FreezeLowLevel freezeLowLevel= new Gaussian.OniomTools.FreezeLowLevel();
            freezeLowLevel.Run();
        }

        private void button_FreezeMediumLowLevel_Click(object sender, EventArgs e)
        {

        }
    }
}
