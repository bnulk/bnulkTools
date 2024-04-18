using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bnulkTools.Gaussian.OniomTools
{
    public partial class Form_ActivateHighLevelPeripheralAtoms : Form
    {

        private bool isOk=false;
        private double range = 0;

        public bool IsOk { get => isOk; set => isOk = value; }
        public double Range { get => range; set => range = value; }

        public Form_ActivateHighLevelPeripheralAtoms()
        {
            InitializeComponent();
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!=null)
            {
                try
                {
                    range= Convert.ToDouble(textBox1.Text);
                    isOk = true;
                    this.Close();
                }
                catch
                {
                    isOk = false;
                    this.Close();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            isOk=false;
            this.Close();
        }
    }
}
