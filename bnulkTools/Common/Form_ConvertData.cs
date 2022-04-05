using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace bnulkTools.Common.Convert_Data
{
    /// <summary>
    /// Form1 的摘要说明。
    /// </summary>
    public class FormUnitConvert : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox hartree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox kj;
        private System.Windows.Forms.TextBox kc;
        private System.Windows.Forms.TextBox ev;
        private System.Windows.Forms.TextBox nm;
        private System.Windows.Forms.Button ok;
        private bool Right_Flag = false;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.Button About;
        private System.Windows.Forms.TextBox cm;
        private System.Windows.Forms.Label label6;
        //标志输入是否正确
        struct DoubleData
        {
            public double double_hartree;
            public double double_kj;
            public double double_kc;
            public double double_ev;
            public double double_nm;
            public double double_cm;
        }
        struct StrData
        {
            public string string_hartree;
            public string string_kj;
            public string string_kc;
            public string string_ev;
            public string string_nm;
            public string string_cm;
        }
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FormUnitConvert()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.hartree = new System.Windows.Forms.TextBox();
            this.kj = new System.Windows.Forms.TextBox();
            this.kc = new System.Windows.Forms.TextBox();
            this.ev = new System.Windows.Forms.TextBox();
            this.nm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ok = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.About = new System.Windows.Forms.Button();
            this.cm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hartree
            // 
            this.hartree.Location = new System.Drawing.Point(32, 40);
            this.hartree.Name = "hartree";
            this.hartree.Size = new System.Drawing.Size(264, 26);
            this.hartree.TabIndex = 0;
            // 
            // kj
            // 
            this.kj.Location = new System.Drawing.Point(32, 87);
            this.kj.Name = "kj";
            this.kj.Size = new System.Drawing.Size(264, 26);
            this.kj.TabIndex = 1;
            // 
            // kc
            // 
            this.kc.Location = new System.Drawing.Point(32, 134);
            this.kc.Name = "kc";
            this.kc.Size = new System.Drawing.Size(264, 26);
            this.kc.TabIndex = 2;
            // 
            // ev
            // 
            this.ev.Location = new System.Drawing.Point(32, 181);
            this.ev.Name = "ev";
            this.ev.Size = new System.Drawing.Size(264, 26);
            this.ev.TabIndex = 3;
            // 
            // nm
            // 
            this.nm.Location = new System.Drawing.Point(32, 228);
            this.nm.Name = "nm";
            this.nm.Size = new System.Drawing.Size(264, 26);
            this.nm.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(296, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "hartree";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(296, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "kJ/mol";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(296, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "kcal/mol";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(296, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 26);
            this.label4.TabIndex = 8;
            this.label4.Text = "eV";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(296, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "nm";
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(408, 120);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 40);
            this.ok.TabIndex = 10;
            this.ok.Text = "OK";
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(408, 40);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(75, 40);
            this.New.TabIndex = 12;
            this.New.Text = "New";
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // About
            // 
            this.About.Location = new System.Drawing.Point(408, 208);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(75, 40);
            this.About.TabIndex = 13;
            this.About.Text = "About";
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // cm
            // 
            this.cm.Location = new System.Drawing.Point(32, 272);
            this.cm.Name = "cm";
            this.cm.Size = new System.Drawing.Size(264, 26);
            this.cm.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(296, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 15;
            this.label6.Text = "cm-1";
            // 
            // UnitConvert
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(488, 357);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cm);
            this.Controls.Add(this.About);
            this.Controls.Add(this.New);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nm);
            this.Controls.Add(this.ev);
            this.Controls.Add(this.kc);
            this.Controls.Add(this.kj);
            this.Controls.Add(this.hartree);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UnitConvert";
            this.Text = "UnitConvert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion



        private void ok_Click(object sender, System.EventArgs e)
        {
            Right_Flag = false;
            int Number_of_Double;
            StrData StrValue = new StrData();
            StrValue = GainValue();
            DoubleData DoubleValue = new DoubleData();
            DoubleValue = StrToDouble(StrValue, out Number_of_Double);
            //
            if (Number_of_Double == 5)
                Right_Flag = true;
            if (Right_Flag == true)
            {
                DoubleValue = ConvertUnit(DoubleValue);
                hartree.Text = DoubleValue.double_hartree.ToString();
                kj.Text = DoubleValue.double_kj.ToString();
                kc.Text = DoubleValue.double_kc.ToString();
                nm.Text = DoubleValue.double_nm.ToString();
                ev.Text = DoubleValue.double_ev.ToString();
                cm.Text = DoubleValue.double_cm.ToString();
            }
            else
            {
                MessageBox.Show("输入有误", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        StrData GainValue()
        {
            StrData Value = new StrData();
            Value.string_ev = ev.Text;
            Value.string_hartree = hartree.Text;
            Value.string_kc = kc.Text;
            Value.string_kj = kj.Text;
            Value.string_nm = nm.Text;
            Value.string_cm = cm.Text;
            return Value;
        }

        DoubleData StrToDouble(StrData StrValue, out int Number_of_Double)
        {
            DoubleData DoubleValue = new DoubleData();
            Number_of_Double = 0;
            //ev
            try
            {
                DoubleValue.double_ev = Convert.ToDouble(StrValue.string_ev);
                if (DoubleValue.double_ev == 0)
                    Number_of_Double++;
            }
            catch
            {
                Number_of_Double++;
                DoubleValue.double_ev = 0;
            }
            //hartree
            try
            {
                DoubleValue.double_hartree = Convert.ToDouble(StrValue.string_hartree);
                if (DoubleValue.double_hartree == 0)
                    Number_of_Double++;
            }
            catch
            {
                Number_of_Double++;
                DoubleValue.double_hartree = 0;
            }
            //kc
            try
            {
                DoubleValue.double_kc = Convert.ToDouble(StrValue.string_kc);
                if (DoubleValue.double_kc == 0)
                    Number_of_Double++;
            }
            catch
            {
                Number_of_Double++;
                DoubleValue.double_kc = 0;
            }
            //kj
            try
            {
                DoubleValue.double_kj = Convert.ToDouble(StrValue.string_kj);
                if (DoubleValue.double_kj == 0)
                    Number_of_Double++;
            }
            catch
            {
                Number_of_Double++;
                DoubleValue.double_kj = 0;
            }
            //nm
            try
            {
                DoubleValue.double_nm = Convert.ToDouble(StrValue.string_nm);
                if (DoubleValue.double_nm == 0)
                    Number_of_Double++;
            }
            catch
            {
                Number_of_Double++;
                DoubleValue.double_nm = 0;
            }
            //cm
            try
            {
                DoubleValue.double_cm = Convert.ToDouble(StrValue.string_cm);
                if (DoubleValue.double_cm == 0)
                    Number_of_Double++;
            }
            catch
            {
                Number_of_Double++;
                DoubleValue.double_cm = 0;
            }
            return DoubleValue;
        }


        DoubleData ConvertUnit(DoubleData DoubleValue)
        {
            //把输入的值转换为hartree
            if (DoubleValue.double_ev != 0)
                DoubleValue.double_hartree = EvToHartree(DoubleValue.double_ev);
            if (DoubleValue.double_kc != 0)
                DoubleValue.double_hartree = KcToHartree(DoubleValue.double_kc);
            if (DoubleValue.double_kj != 0)
                DoubleValue.double_hartree = KjToHartree(DoubleValue.double_kj);
            if (DoubleValue.double_nm != 0)
                DoubleValue.double_hartree = NmToHartree(DoubleValue.double_nm);
            if (DoubleValue.double_cm != 0)
                DoubleValue.double_hartree = CmToHartree(DoubleValue.double_cm);
            //把hartree转换为其它的值
            DoubleValue.double_ev = HartreeToEv(DoubleValue.double_hartree);
            DoubleValue.double_kc = HartreeToKc(DoubleValue.double_hartree);
            DoubleValue.double_kj = HartreeToKj(DoubleValue.double_hartree);
            DoubleValue.double_nm = HartreeToNm(DoubleValue.double_hartree);
            DoubleValue.double_cm = HartreeToCm(DoubleValue.double_hartree);
            //返回值
            return DoubleValue;
        }

        //其它到hartree
        double KjToHartree(double kj)
        {
            double hartree = kj / 2625.5;
            return hartree;
        }
        double KcToHartree(double kc)
        {
            double hartree = kc / 627.5095;
            return hartree;
        }
        double EvToHartree(double ev)
        {
            double hartree = ev / 27.2116;
            return hartree;
        }
        double NmToHartree(double nm)
        {
            double hartree = 45.56335 / nm;
            return hartree;
        }
        double CmToHartree(double cm)
        {
            double hartree = cm / 219474.7;
            return hartree;
        }
        //hartree到其它
        double HartreeToKj(double hartree)
        {
            double kj = 2625.5 * hartree;
            return kj;
        }
        double HartreeToKc(double hartree)
        {
            double kc = 627.5095 * hartree;
            return kc;
        }
        double HartreeToEv(double hartree)
        {
            double ev = 27.2116 * hartree;
            return ev;
        }
        double HartreeToNm(double hartree)
        {
            double nm = 45.56335 / hartree;
            return nm;
        }
        double HartreeToCm(double hartree)
        {
            double cm = 219474.7 * hartree;
            return cm;
        }

        private void New_Click(object sender, System.EventArgs e)
        {
            ev.Text = "";
            hartree.Text = "";
            kj.Text = "";
            kc.Text = "";
            nm.Text = "";
            cm.Text = "";
        }

        private void About_Click(object sender, System.EventArgs e)
        {
            FormDlg AboutDlg = new FormDlg();
            AboutDlg.ShowDialog();
        }


    }
}
