using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ZhangCang.FundamentalConstants;
using bnulkTools.ErrorInfo;

namespace bnulkTools.Gaussian.OniomTools
{
    internal class ActivateHighLevelPeripheralAtoms
    {
        string gjfFilePath;
        List<string> outputList;
        GaussianInputPackage gaussianInputPackage;

        bool isOK = false;
        double range = 0;

        List<string> atomList;
        List<int> freezeList;
        List<double> x;
        List<double> y;
        List<double> z;
        List<string> layerList;


        List<int> highLevelList;
        List<int> interactionsWithHighLevelList;
        List<int> interactionsWithHighLevelAndHList;


        public ActivateHighLevelPeripheralAtoms()
        {
            StringBuilder outputStr = new StringBuilder();
            gjfFilePath = "";

            //获取活化范围
            Form_ActivateHighLevelPeripheralAtoms form_ActivateHighLevelPeripheralAtoms = new Form_ActivateHighLevelPeripheralAtoms();
            form_ActivateHighLevelPeripheralAtoms.ShowDialog();

            isOK = form_ActivateHighLevelPeripheralAtoms.IsOk;
            range = form_ActivateHighLevelPeripheralAtoms.Range;

            if(form_ActivateHighLevelPeripheralAtoms.IsOk==true)
            {
                //获取文件路径
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    gjfFilePath = dialog.FileName;
                }
                outputList = new List<string>();
                gaussianInputPackage = new GaussianInputPackage();

                atomList = new List<string>();
                freezeList = new List<int>();
                x = new List<double>();
                y = new List<double>();
                z = new List<double>();
                layerList = new List<string>();
                highLevelList = new List<int>();
                interactionsWithHighLevelList = new List<int>();
            }
        }

        public void Run()
        {
            if(isOK==true)
            {
                ObtainGaussianInputPackage();
                ObtainList();
                ObtainHighLevelList();
                ObtainInteractionsWithHighLevelList(range);
                ModifyFreezeList();
                CreateOutputList();
                WriteNewInputFile();
            }
            else
            {
                MessageBox.Show("输入的活化原子范围有错误" + "\n" + "The input activation atom range is incorrect");
            }
        }

        private void ObtainGaussianInputPackage()
        {
            try
            {
                if (gjfFilePath != null)
                {
                    ReadGaussianInputFile_App readGaussianInputFile_App = new ReadGaussianInputFile_App(gjfFilePath);
                    readGaussianInputFile_App.Run();
                    gaussianInputPackage = readGaussianInputFile_App.GaussianInputPackage;
                }
                else
                {
                    DisposeError_App app = new DisposeError_App("输入文件为空" + "\r\n" + "The input file is empty");
                    app.Run();
                }
            }
            catch
            {
                DisposeError_App app = new DisposeError_App("输入文件错误" + "\r\n" + "Input file error");
                app.Run();
            }
        }

        private void ObtainList()
        {
            string[] strs = new string[6];
            Atoms atoms = new Atoms();
            try
            {
                if (gaussianInputPackage.molecularSpecification != null)
                {
                    int cycle = gaussianInputPackage.molecularSpecification.Count;
                    for (int i = 0; i < cycle; i++)
                    {
                        strs = Regex.Split(gaussianInputPackage.molecularSpecification[i], "\\s+");
                        if (strs != null)
                        {
                            atomList.Add(strs[0]);
                            freezeList.Add(Convert.ToInt32(strs[1]));
                            x.Add(Convert.ToDouble(strs[2]));
                            y.Add(Convert.ToDouble(strs[3]));
                            z.Add(Convert.ToDouble(strs[4]));
                            layerList.Add(strs[5]);
                        }
                    }
                }
            }
            catch
            {
                DisposeError_App app = new DisposeError_App("oniom输入文件分子说明部分错误" + "\r\n" + "Error in the molecular specification section of the oniom input file");
                app.Run();
            }
        }

        private void ObtainHighLevelList()
        {
            int errorLabel = 0;
            try
            {
                if (gaussianInputPackage.molecularSpecification != null)
                {
                    int cycle = gaussianInputPackage.molecularSpecification.Count;
                    for (int i = 0; i < cycle; i++)
                    {
                        errorLabel = i;
                        if (!string.IsNullOrEmpty(layerList[i]) && layerList[i].Length > 0)
                        {
                            if (layerList[i][0] == 'H')
                            {
                                highLevelList.Add(i);
                            }
                        }
                    }
                }
            }
            catch
            {
                DisposeError_App app = new DisposeError_App("获取高层信息错误，标号=" + errorLabel.ToString() + "\r\n" + "Error in obtaining high-level information. label=" + errorLabel.ToString());
                app.Run();
            }
        }

        private void ObtainInteractionsWithHighLevelList(double range)
        {
            interactionsWithHighLevelList = new List<int>();
            interactionsWithHighLevelAndHList= new List<int>();
            double criteria = range*range;

            try
            {
                if (gaussianInputPackage.molecularSpecification != null && highLevelList != null)
                {
                    int cycle = highLevelList.Count;
                    for (int i = 0; i < cycle; i++)
                    {
                        for(int j=0;j<gaussianInputPackage.molecularSpecification.Count;j++)
                        {
                            if(!highLevelList.Contains(j) && !interactionsWithHighLevelList.Contains(j))
                            {
                                if ((x[i] - x[j])*(x[i] - x[j])+ (y[i] - y[j]) * (y[i] - y[j])+ (z[i] - z[j]) * (z[i] - z[j])<criteria)
                                {
                                    interactionsWithHighLevelList.Add(j);
                                }
                            }
                        }
                    }

                    for (int i = 0; i < interactionsWithHighLevelList.Count; i++)
                    {
                        interactionsWithHighLevelAndHList.Add(interactionsWithHighLevelList[i]);
                    }

                    //加端基H
                    for (int i=0;i<interactionsWithHighLevelList.Count;i++)
                    {
                        for (int j = 0; j < gaussianInputPackage.molecularSpecification.Count; j++)
                        {
                            if (!highLevelList.Contains(j) && !interactionsWithHighLevelAndHList.Contains(j))
                            {
                                if(atomList[j].Length==1)
                                {
                                    if (atomList[j]=="H")
                                    {
                                        if ((x[i] - x[j]) * (x[i] - x[j]) + (y[i] - y[j]) * (y[i] - y[j]) + (z[i] - z[j]) * (z[i] - z[j]) < 1.21)
                                        {
                                            interactionsWithHighLevelAndHList.Add(j);
                                        }
                                    }
                                }

                                if (atomList[j].Length > 1)
                                {
                                    if (atomList[j].Substring(0,2) == "H-")
                                    {
                                        if ((x[i] - x[j]) * (x[i] - x[j]) + (y[i] - y[j]) * (y[i] - y[j]) + (z[i] - z[j]) * (z[i] - z[j]) < 1.21)
                                        {
                                            interactionsWithHighLevelAndHList.Add(j);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch
            {
                DisposeError_App app = new DisposeError_App("oniom输入分层信息错误" + "\r\n" + "Input Hierarchical Information Error");
                app.Run();
            }
        }

        private void ModifyFreezeList()
        {
            for( int i = 0;i< interactionsWithHighLevelList.Count;i++)
            {
                freezeList[interactionsWithHighLevelAndHList[i]] = 0;
            }
        }

        private void CreateOutputList()
        {
            int i = 0;
            outputList = new List<string>();
            for(i=0;i< gaussianInputPackage.firstSection.Count; i++)
            {
                outputList.Add(gaussianInputPackage.firstSection[i]);
            }
            for(i=0;i<gaussianInputPackage.routeSection.Count;i++)
            {
                outputList.Add(gaussianInputPackage.routeSection[i]);
            }
            outputList.Add("");
            for(i=0;i<gaussianInputPackage.titleSection.Count;i++)
            {
                outputList.Add(gaussianInputPackage.titleSection[i]);
            }
            outputList.Add("");
            for(i=0;i<gaussianInputPackage.chargeAndMultiplicity.Count;i++)
            {
                outputList.Add(gaussianInputPackage.chargeAndMultiplicity[i]);
            }
            for(i=0;i<gaussianInputPackage.molecularSpecification.Count;i++)
            {
                outputList.Add((" " + atomList[i]).ToString().PadRight(17)
                    + freezeList[i].ToString().PadLeft(11).PadRight(15)
                    + x[i].ToString("0.000000").PadLeft(11).PadRight(15)
                    + y[i].ToString("0.000000").PadLeft(11).PadRight(15)
                    + z[i].ToString("0.000000").PadLeft(11).PadRight(15)
                    + (" " + layerList[i]).ToString()
                    );
            }
        }


        private void WriteNewInputFile()
        {
            if (gjfFilePath != null && outputList != null)
            {
                string newFilePath = gjfFilePath.Insert(gjfFilePath.Length - 4, "_");

                using (StreamWriter writer = File.CreateText(newFilePath))
                {
                    for (int i = 0; i < outputList.Count; i++)
                    {
                        writer.Write(outputList[i] + "\n");
                    }

                    writer.Flush();
                    writer.Dispose();
                }



            }
        }

    }
}
