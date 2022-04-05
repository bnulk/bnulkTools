using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bnulkTools.Gaussian.App
{
    internal class ReadTdaSteepProcess
    {
        private string path = String.Empty;                 //目录

        public void Run()
        {
            double[,] result;
            StringBuilder outputStr = new StringBuilder();
            //获取文件路径
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileName;
            }
            else
            {
                return;
            }

            //读out文件
            try
            {
                Read(path, out result);
            }
            catch
            {
                MessageBox.Show("读Out文件出错", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }

            //写入文本
            try
            {
                string directoryPath = Path.GetDirectoryName(path);
                Output.WriteOutput.Write(directoryPath + "\\result.txt");
                int row = result.GetLength(0);
                int col = result.GetLength(1);

                for(int i = 0; i < row; i++)
                {
                    outputStr.Append(" " + i.ToString().PadLeft(5));
                    for (int j = 0; j < col; j++)
                    {
                        outputStr.Append(result[i, j].ToString("0.00000000").PadLeft(20));
                    }
                    outputStr.Append("\r\n");
                }
                
                Output.WriteOutput.WriteStr(outputStr);
            }
            catch
            {
                MessageBox.Show("写result.txt出错", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            //写入Word
            try
            {
                string directoryPath = Path.GetDirectoryName(path);
                WriteWord(directoryPath + "\\result.docx", result);
            }
            catch
            {
                MessageBox.Show("写result.docx出错", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void Read(string path, out double[,] result)
        {
            ReadGaussianOut readOut = new ReadGaussianOut(path);
            int numberOfSteps = readOut.GetNumberOfSteps();
            int numberOfStates = readOut.GetNumberOfNstates_TdType();

            double[,] tmpResult= new double[numberOfSteps, numberOfStates];
            result = new double[numberOfSteps, numberOfStates];

            result = readOut.GetTdaSteepProcess();

        }

        private void WriteWord(string fullName, double[,] result)
        {
            object path = fullName;
            //由于使用的是COM 库，因此有许多变量需要用Missing.Value 代替
            object oMissing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            //创建一个Document类对象
            Microsoft.Office.Interop.Word.Document wordDoc = new Microsoft.Office.Interop.Word.Document();

            wordApp.Visible = true;//使文档可见

            //如果已存在，则删除
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }

            //新建一个word对象
            wordDoc = wordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            //WdSaveDocument为Word2003文档的保存格式(文档后缀.doc)\wdFormatDocumentDefault为Word2007的保存格式(文档后缀.docx)
            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault;

            //移动焦点并换行
            object count = 14;
            object WdLine = Microsoft.Office.Interop.Word.WdUnits.wdLine;                   //换一行;
            wordApp.Selection.MoveDown(ref WdLine, ref count, ref oMissing);                  //移动焦点
            wordApp.Selection.TypeParagraph();//插入段落


            //文档中创建表格
            int Count = result.GetLength(0);
            int col = result.GetLength(1);

            Microsoft.Office.Interop.Word.Table newTable = wordDoc.Tables.Add(wordApp.Selection.Range, Count + 1, col + 1, ref oMissing, ref oMissing);
            //设置表格样式
            newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
            newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            newTable.Columns[1].Width = 100f;
            for (int i = 1; i <= col; i++)
            {
                newTable.Columns[i+1].Width = 100f;
            }
            

            //将数组内容添加到表格
            for (int i = 0; i < Count; i++)
            {
                newTable.Cell(i + 2, 1).Range.Text = i.ToString();
                for (int j = 0; j < col; j++)
                {
                    newTable.Cell(i + 2, j + 2).Range.Text = result[i, j].ToString("0.00000000");
                }
            }

            //将wordDoc 文档对象的内容保存为DOC 文档,并保存到path指定的路径
            wordDoc.SaveAs(ref path, ref format, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            //wordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
            //wordApp.Quit(ref oMissing, ref oMissing, ref oMissing);

            return;
        }









    }
}
