using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bnulkTools.Common
{
    public partial class Form_ForMixBasisSet : Form
    {
        public Form_ForMixBasisSet()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int startNumber = Convert.ToInt32(textBox_Start.Text);
            int endNumber = Convert.ToInt32(textBox_End.Text);

            int cycle = endNumber - startNumber;
            for(int i = startNumber; i <= endNumber; i++) 
            {
                sb.Append(i.ToString()+" ");
            }

            richTextBox_Result.Text = sb.ToString();

            sb.Clear();
        }
    }
}
