

using System.Collections.Generic;
using System.IO;

namespace bnulkTools.Gaussian
{
    internal class ReadGaussianInputFile_App
    {
        string inputFileFullName;
        GaussianInputPackage gaussianInputPackage;
        List<string> inputList;

        public GaussianInputPackage GaussianInputPackage { get => gaussianInputPackage; set => gaussianInputPackage = value; }

        public ReadGaussianInputFile_App(string inputFileFullName)
        {
            this.inputFileFullName = inputFileFullName;
            GaussianInputPackage = new GaussianInputPackage();
            inputList = new List<string>();
        }


        public void Run()
        {
            ObtainInputList();
            InputList2GaussianInputPackage();
        }

        private void ObtainInputList()
        {
            if (File.Exists(inputFileFullName))
            {
                using (StreamReader reader = new StreamReader(new FileStream(inputFileFullName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    string line = "";
                    inputList = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        if (line == null)
                        {
                            inputList.Add("");
                        }
                        else
                        {
                            inputList.Add(line);
                        }

                    }
                    reader.Dispose();
                }
            }
        }


        private void InputList2GaussianInputPackage()
        {
            string str = "";                                                        //读取每行数据
            int iSegment = 0;                                                       //分段的标识
            bool isChargeAndMultiplicity = true;                                    //是否为电荷和自旋多重度的那一行

            //初始化
            gaussianInputPackage.firstSection = new List<string>();
            gaussianInputPackage.routeSection = new List<string>();
            gaussianInputPackage.titleSection = new List<string>();
            gaussianInputPackage.chargeAndMultiplicity = new List<string>();
            gaussianInputPackage.molecularSpecification = new List<string>();
            gaussianInputPackage.addition = new List<string>();
            //填入数据
            for (int i = 0; i < inputList.Count; i++)
            {
                str = inputList[i].Trim();                                                               //去除前后的空格
                if (str == "" && iSegment < 3)
                {
                    if (i++ < inputList.Count && !inputList[i + 1].Contains("="))
                    {
                        iSegment++;
                    }
                }

                switch (iSegment)
                {
                    case 0:
                        if (str.Substring(0, 1) == "%")
                        {
                            gaussianInputPackage.firstSection.Add(str);
                        }
                        else
                        {
                            gaussianInputPackage.routeSection.Add(str);
                        }
                        break;
                    case 1:
                        str = inputList[i].Trim();
                        gaussianInputPackage.titleSection.Add(str);
                        break;
                    case 2:
                        if (isChargeAndMultiplicity == true)
                        {
                            str = inputList[i].Trim();
                            gaussianInputPackage.chargeAndMultiplicity.Add(str);
                            isChargeAndMultiplicity = false;
                        }
                        else
                        {
                            str = str.Trim();
                            gaussianInputPackage.molecularSpecification.Add(str);
                        }
                        break;
                    default:
                        gaussianInputPackage.addition.Add(str);
                        break;
                }
            }
            return;
        }


    }
}
