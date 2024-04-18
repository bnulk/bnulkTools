using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace bnulkTools.Gaussian.OniomTools
{
    internal class FreezeNonHydrogenAtom
    {
        string gjfFilePath;
        List<string> inputList;
        List<string> outputList;

        public FreezeNonHydrogenAtom() 
        {
            StringBuilder outputStr = new StringBuilder();
            //获取文件路径
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                gjfFilePath = dialog.FileName;
            }
            else
            {
                return;
            }

            inputList = new List<string>();
            outputList = new List<string>();
        }

        public void Run()
        {
            ObtainInputList();
            ChangeFreeze();
            WriteNewInputFile();
        }

        private void ObtainInputList()
        {
            if (File.Exists(gjfFilePath))
            {
                using (StreamReader reader = new StreamReader(new FileStream(gjfFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    string line = "";
                    inputList = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        inputList.Add(line);
                    }
                    reader.Dispose();
                }
            }
        }

        private void ChangeFreeze()
        {
            int sectionLabel = 0;
            outputList = new List<string>();
            string[] tmpStrs;
            StringBuilder sb = new StringBuilder();
            bool chargeAndMultiplicity = true;

            if(inputList!=null)
            {
                int cycle=inputList.Count;
                for(int i = 0; i < cycle; i++)
                {
                    if (inputList[i].Trim() == "")
                    {
                        sectionLabel++;
                        outputList.Add(inputList[i]);
                    }
                    else
                    {
                        if (sectionLabel != 2)
                        {
                            outputList.Add(inputList[i]);
                        }
                        if (sectionLabel == 2)
                        {
                            if (chargeAndMultiplicity == true)
                            {
                                outputList.Add(inputList[i]);
                                chargeAndMultiplicity = false;
                            }
                            else
                            {
                                tmpStrs = Regex.Split(inputList[i].Trim(), "\\s+");
                                if (tmpStrs.Length < 6)
                                {

                                }
                                else
                                {
                                    if (tmpStrs[0] == "H" || tmpStrs[0] == "1")
                                    {
                                        tmpStrs[1] = "0";
                                    }
                                    else
                                    {
                                        tmpStrs[1] = "-1";
                                    }
                                }

                                sb = new StringBuilder();
                                sb.Append(" " + tmpStrs[0].PadRight(9));
                                sb.Append(tmpStrs[1].PadLeft(5).PadRight(8));
                                sb.Append(tmpStrs[2].PadRight(14));
                                sb.Append(tmpStrs[3].PadRight(14));
                                sb.Append(tmpStrs[4].PadRight(14));
                                for (int j = 5; j < tmpStrs.Length; j++)
                                {
                                    sb.Append(" " + tmpStrs[j] + " ");
                                }
                                outputList.Add(sb.ToString());
                            }
                        }
                    }
                }                    
            }
        }

        private void WriteNewInputFile()
        {
            string newFilePath = gjfFilePath.Insert(gjfFilePath.Length - 4, "_");
            StreamWriter newGjf= File.CreateText(newFilePath);

            for(int i = 0; i < outputList.Count; i++)
            {
                newGjf.Write(outputList[i] + "\n");
            }

            newGjf.Flush();
            newGjf.Dispose();
        }






    }
}
