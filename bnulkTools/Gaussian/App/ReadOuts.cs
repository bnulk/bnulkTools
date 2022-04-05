using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace bnulkTools.Gaussian.App
{
    class ReadOuts
    {
        private string path = "";                 //目录

        public void Run()
        {
            string[,] result;
            StringBuilder outputStr = new StringBuilder();
            //获取目录
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择Out或者Log所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    path = dialog.SelectedPath;
                }
            }

            //读一系列out文件
            try
            {
                Read(path, out result);
                int cycle = result.GetLength(0);
                for (int i = 0; i < cycle; i++)
                {
                    outputStr.Append(result[i, 0].PadLeft(50) + Convert.ToDouble(result[i, 1]).ToString("0.0000000").PadLeft(20) + Convert.ToDouble(result[i, 2]).ToString("0.000000").PadLeft(20) + "\r\n");
                }
            }
            catch
            {
                MessageBox.Show("读Out文件出错", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }

            //写入文本
            try
            { 
                Output.WriteOutput.Write(path + "\\result.txt");
                Output.WriteOutput.WriteStr("FileNames".PadLeft(50) + "Total Energies".PadLeft(20) + "Free Energies".PadLeft(20) + "\r\n" + "\r\n");
                Output.WriteOutput.WriteStr(outputStr);
            }
            catch
            {
                MessageBox.Show("写result.txt出错", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            //写入Word
            try
            {
                WriteWord(path + "\\result.docx", result);
            }
            catch
            {
                MessageBox.Show("写result.docx出错", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            return;
        }

        private void Read(string path, out string[,] result)
        {
            //获取out文件的个数
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            List<FileInfo> fileOuts = new List<FileInfo>();
            foreach(FileInfo tmp in fileInfos)
            {
                if(tmp.Extension.Equals(".out") || tmp.Extension.Equals(".log"))
                {
                    fileOuts.Add(tmp);
                }
            }


            //初始化结果数组。三列分别是文件名、能量、吉布斯自由能
            int n = fileOuts.Count;
            string fullPath = directoryInfo.FullName;
            result = new string[n, 3];
            double tmpEnergy;
            
            for (int i = 0; i < n; i++)
            {
                result[i, 0] = fileOuts[i].Name;
                try
                {
                    ReadGaussianOut readOut = new ReadGaussianOut(fileOuts[i].FullName);
                    try
                    {
                        tmpEnergy = readOut.GetHFEnergy();
                        if(tmpEnergy == -1)
                        {
                            result[i, 1] = "0.0";
                        }
                        else
                        {
                            result[i, 1] = tmpEnergy.ToString();
                        }
                        
                    }
                    catch
                    {
                        result[i, 1] = "0.0";
                    }
                    try
                    {
                        tmpEnergy = readOut.GetFreeEnergy();
                        if (tmpEnergy == -1)
                        {
                            result[i, 2] = "0.0";
                        }
                        else
                        {
                            result[i, 2] = tmpEnergy.ToString();
                        }
                        
                    }
                    catch
                    {
                        result[i, 2] = "0.0";
                    }
                }
                catch
                {
                    result[i, 1] = "0.0";
                    result[i, 2] = "0.0";
                }
            }
            /*
            //产生结果文件
            StreamWriter textResult;
            textResult = File.CreateText(@Path + "IRCpointsResult" + ".txt");
            for (int i = 0; i <= numberOfIrcPoints; i++)
            {
                textResult.WriteLine("     " + i.ToString().PadRight(20, ' ') + IRCpointsResult[i].ToString().PadRight(20, ' '));
                textResult.Flush();
            }
            textResult.Close();

    */
            return;
        }

        private void WriteWord(string fullName, string[,] result)
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
            Microsoft.Office.Interop.Word.Table newTable = wordDoc.Tables.Add(wordApp.Selection.Range, Count + 1, 3, ref oMissing, ref oMissing);
            //设置表格样式
            newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
            newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            newTable.Columns[1].Width = 220f;
            newTable.Columns[2].Width = 100f;
            newTable.Columns[3].Width = 100f;

            //将数组内容添加到表格
            newTable.Cell(1, 1).Range.Text = "Name";
            newTable.Cell(1, 2).Range.Text = "Total Energy";
            newTable.Cell(1, 3).Range.Text = "Free Energy";
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    newTable.Cell(i + 2, j + 1).Range.Text = result[i, j];
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
