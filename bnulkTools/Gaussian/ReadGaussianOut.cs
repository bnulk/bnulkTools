using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;              //使用正则表达式


namespace bnulkTools.Gaussian
{
    internal class ReadGaussianOut
    {
        private string filePath;            //Out文件的物理地址
        private int nAtoms = 0;             //分子中包含原子个数
        private string errorInfo = "";      //出错信息

        public int NAtoms { get => nAtoms; set => nAtoms = value; }
        public string ErrorInfo { get => errorInfo; set => errorInfo = value; }


        /// <summary>
        /// 构造函数；完成两项初始化工作。１、把GaussianOut文件的物理路径赋值给FilePath。
        /// ２、把分子中原子的个数赋值给N；
        /// </summary>
        /// <param name="filepath">GaussianOut文件的物理路径</param>
        public ReadGaussianOut(string filepath)
        {
            filePath = filepath;      //全局变量FilePath，文件的物理路径，有初始化参量获得
            nAtoms = ObtainN();       //分子中原子个数
        }

        /// <summary>
        /// 获取原子个数
        /// </summary>
        /// <returns></returns>
        public int ObtainN()    //获取Ｎ值
        {
            int N = -1;
            int indexMark = -1;
            StreamReader OutFileStreamReader = null;
            OutFileStreamReader = File.OpenText(@filePath);
            string str = null;
            while (OutFileStreamReader.Peek() > -1)                 //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();
                indexMark = str.IndexOf("NAtoms=");
                if (indexMark != -1)
                {
                    str = str.Remove(0, indexMark + 7);
                    str = str.Trim();
                    indexMark = str.IndexOf(" ");
                    str = str.Substring(0, indexMark).Trim();
                    try
                    {
                        N = Convert.ToInt32(str);
                    }
                    catch
                    {
                        errorInfo = "ReadGaussianOut.ObtainN() Error";
                    }
                    return N;
                }
            }
            return N;
        }

        /// <summary>
        /// 获取HF类型的能量
        /// </summary>
        /// <returns>HF类型的能量，双精度型</returns>
        public string GetArchive()
        {
            string str = null;
            string archive = "";
            StringBuilder archiveStr = new StringBuilder();
            StreamReader OutFileStreamReader = null;

            OutFileStreamReader = File.OpenText(@filePath);
            while (OutFileStreamReader.Peek() > -1)
            {
                str = OutFileStreamReader.ReadLine();
                if (str == " Test job not archived.")
                {
                    for (; str != "";)
                    {
                        str = OutFileStreamReader.ReadLine();
                        archiveStr.Append(str.Trim());
                    }
                }
            }
            archive = archiveStr.ToString();
            if (archive == "")
            {
                errorInfo="ReadGaussianOut.GetArchive() Error";
            }
            return archive;
        }


        /// <summary>
        /// 获取HF类型的能量
        /// </summary>
        /// <returns>HF类型的能量，双精度型</returns>
        public double GetHFEnergy()
        {
            double Energy = -1.0;
            int indexMark = -1;
            string str = null;

            str = GetArchive();
            indexMark = str.IndexOf("HF=");
            //Linux系统，“\”分割；Windows系统“|”分割。
            str = str.Replace("\\", "|");
            str = str.Remove(0, indexMark + 3);
            indexMark = str.IndexOf('|');
            str = str.Substring(0, indexMark);
            Energy = Convert.ToDouble(str);
            return Energy;
        }

        /// <summary>
        /// 获取CIS类型的能量
        /// </summary>
        /// <returns>CIS类型的能量，双精度型</returns>
        public double GetCISEnergy()
        {
            double Energy = -1.0;
            int indexMark = -1;
            string str = null;
            bool isReadFile = true;

            StreamReader OutFileStreamReader = null;
            OutFileStreamReader = File.OpenText(@filePath);

            while (OutFileStreamReader.Peek() > -1 && isReadFile == true)                //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();              //读文件的一行

                //获取力
                if (str == " This state for optimization and/or second-order correction.")           //读梯度的标志
                {
                    isReadFile = false;
                    str = OutFileStreamReader.ReadLine();              //读CIS一行
                    indexMark = str.IndexOf(" Total Energy, E(CIS/TDA) =  ");
                    str = str.Remove(0, indexMark + 28);
                    Energy = Convert.ToDouble(str);
                }
            }
            return Energy;
        }

        /// <summary>
        /// 获取TD类型的能量
        /// </summary>
        /// <returns>TD类型的能量，双精度型</returns>
        public double GetTDEnergy()
        {
            double Energy = -1.0;
            int indexMark = -1;
            string str = null;
            bool isReadFile = true;

            StreamReader OutFileStreamReader = null;
            OutFileStreamReader = File.OpenText(@filePath);

            while (OutFileStreamReader.Peek() > -1 && isReadFile == true)                //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();              //读文件的一行

                //获取力
                if (str == " This state for optimization and/or second-order correction.")           //读梯度的标志
                {
                    isReadFile = false;
                    str = OutFileStreamReader.ReadLine();              //读TD一行
                    indexMark = str.IndexOf(" Total Energy, E(TD-HF/TD-DFT) =");
                    str = str.Remove(0, indexMark + 33);
                    Energy = Convert.ToDouble(str);
                }
            }
            return Energy;
        }

        /// <summary>
        /// 获取TDA类型的能量
        /// </summary>
        /// <returns>TDA类型的能量，双精度型</returns>
        public double GetTDAEnergy()
        {
            double Energy = -1.0;
            int indexMark = -1;
            string str = null;
            bool isReadFile = true;

            StreamReader OutFileStreamReader = null;
            OutFileStreamReader = File.OpenText(@filePath);

            while (OutFileStreamReader.Peek() > -1 && isReadFile == true)                //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();              //读文件的一行

                //获取力
                if (str == " This state for optimization and/or second-order correction.")           //读梯度的标志
                {
                    isReadFile = false;
                    str = OutFileStreamReader.ReadLine();              //读TD一行
                    indexMark = str.IndexOf(" Total Energy, E(CIS/TDA) =");
                    str = str.Remove(0, indexMark + 28);
                    Energy = Convert.ToDouble(str);
                }
            }
            return Energy;
        }

        /// <summary>
        /// 获取吉布斯自由能
        /// </summary>
        /// <returns></returns>
        public double GetFreeEnergy()
        {
            double freeEnergy = -1.0;

            bool finishTask = false;
            bool cycleFinish = false;
            int indexMark = -1;
            string str = null;
            StreamReader OutFileStreamReader = null;
            OutFileStreamReader = File.OpenText(@filePath);

            while (OutFileStreamReader.Peek() > -1 && finishTask == false)
            {
                str = OutFileStreamReader.ReadLine();
                if (str == " - Thermochemistry -")
                {
                    for (; cycleFinish == false;)
                    {
                        if (OutFileStreamReader.Peek() <= -1)
                        {
                            cycleFinish = true;
                        }
                        else
                        {
                            str = OutFileStreamReader.ReadLine();
                            indexMark = str.IndexOf("Sum of electronic and thermal Free Energies=");
                            if (indexMark != -1)
                            {
                                str = str.Substring(45);
                                freeEnergy = Convert.ToDouble(str);
                                cycleFinish = true;
                                finishTask = true;
                            }
                        }
                    }
                }
            }

            return freeEnergy;
        }

        /// <summary>
        /// 得到一行和参数对应的梯度值。 注意：本程序中所有梯度，都是-DE/DX，这个一定要当心。
        /// </summary>
        /// <returns>3*N-6个梯度值</returns>
        public string[] GetForce_Zmatrix()
        {
            string[] Force = new string[3 * nAtoms - 6];
            string[,] ForceParams = new string[3 * nAtoms - 6, 3];
            ForceParams = GetForceParams_Zmatrix();
            for (int i = 0; i < 3 * nAtoms - 6; i++)
            {
                Force[i] = ForceParams[i, 2];
            }
            return Force;
        }

        /// <summary>
        /// string［M,3］。其中M=3*N-6是变量个数，3分别是“变量名”“变量值”“梯度”
        /// </summary>
        /// <returns>string[M,3]二维数组</returns>
		public string[,] GetForceParams_Zmatrix()
        {
            string str = null;
            string[] tmpStr = new string[1000];
            string[] tmpParams = new string[7];
            string[,] ForceParams = new string[3 * nAtoms - 6, 3];
            string[] tmp3 = new string[3];                                           //
            StreamReader OutFileStreamReader = null;
            OutFileStreamReader = File.OpenText(@filePath);

            for (int i = 0; i < 3 * nAtoms - 6; i++)           //初始化ForceParams数组，如果读不到梯度的标志，则数组不变。
            {
                for (int j = 0; j < 3; j++)
                {
                    ForceParams[i, j] = "bnulk";
                }
            }

            while (OutFileStreamReader.Peek() > -1)                //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();              //读文件的一行

                //获取力
                if (str == " From PutF, contents of force:")           //读梯度的标志
                {
                    for (int i = 0; i < (3 * nAtoms - 6); i++)
                    {
                        str = OutFileStreamReader.ReadLine();
                        //TianjinData.m_Result.Append(str.ToString() + "\n");
                        //WriteOut.Write();
                        tmpStr = str.Split(' ');
                        int k = 0;
                        foreach (string temp in tmpStr)
                        {
                            if (temp != " " && temp != "" && temp != "\r")
                            {
                                tmpParams[k] = temp;
                                k++;
                            }
                        }
                        ForceParams[i, 2] = tmpParams[1];
                    }
                }

                //获取参数名称和当前参数值
                if (str == " Variable       Old X    -DE/DX   Delta X   Delta X   Delta X     New X")           //读梯度的标志
                {
                    str = OutFileStreamReader.ReadLine();     //跳过(Linear)    (Quad)   (Total)那一行
                    for (int i = 0; i < (3 * nAtoms - 6); i++)
                    {
                        str = OutFileStreamReader.ReadLine();
                        tmp3[0] = str.Substring(0, 11);
                        tmp3[1] = str.Substring(11, 10);
                        tmp3[2] = str.Substring(21, 10);
                        ForceParams[i, 0] = tmp3[0];
                        ForceParams[i, 1] = tmp3[1];
                        //ForceParams[i,2] = tmp3[2];
                    }
                }
            }

            //把Out文件力矩阵矩阵元中的D改为E
            for (int m = 0; m < 3 * nAtoms - 6; m++)
            {
                ForceParams[m, 2] = ForceParams[m, 2].Replace('D', 'E');
            }
            //返回值
            return ForceParams;
        }

        /// <summary>
        /// 得到一行和参数对应的梯度值。 注意：本程序中所有梯度，都是-DE/DX，这个一定要当心。
        /// </summary>
        /// <returns>3*N个梯度值</returns>
        public string[] GetForce_Cartesian()
        {
            string[] Force = new string[3 * nAtoms];
            string[,] ForceParams = new string[3 * nAtoms, 3];
            ForceParams = GetForceParams_Cartesian();
            for (int i = 0; i < 3 * nAtoms; i++)
            {
                Force[i] = ForceParams[i, 2];
            }
            return Force;
        }

        /// <summary>
        /// string［M,3］。其中M=3*N是变量个数，3分别是“变量名”“变量值”“梯度”
        /// </summary>
        /// <returns>string[M,3]二维数组</returns>
		public string[,] GetForceParams_Cartesian()
        {
            string str = null;
            string[] tmpStr = new string[1000];
            string[] tmpParams = new string[7];
            string[,] ForceParams = new string[3 * nAtoms, 3];
            string[] tmp3 = new string[3];                      //
            StreamReader OutFileStreamReader = null;
            OutFileStreamReader = File.OpenText(@filePath);

            for (int i = 0; i < 3 * nAtoms; i++)                    //初始化ForceParams数组，如果读不到梯度的标志，则数组不变。
            {
                for (int j = 0; j < 3; j++)
                {
                    ForceParams[i, j] = "bnulk";
                }
            }

            while (OutFileStreamReader.Peek() > -1)                //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();              //读文件的一行

                //获取力
                if (str == " ***** Axes restored to original set *****")           //读梯度的标志
                {
                    str = OutFileStreamReader.ReadLine();
                    str = OutFileStreamReader.ReadLine();
                    str = OutFileStreamReader.ReadLine();
                    str = OutFileStreamReader.ReadLine();
                    str = OutFileStreamReader.ReadLine();                          //跳过5行
                    for (int i = 0; i < nAtoms; i++)
                    {
                        str = OutFileStreamReader.ReadLine();
                        tmp3[0] = str.Substring(26, 12);
                        tmp3[1] = str.Substring(41, 12);
                        tmp3[2] = str.Substring(56, 12);
                        ForceParams[3 * i, 2] = tmp3[0];
                        ForceParams[3 * i + 1, 2] = tmp3[1];
                        ForceParams[3 * i + 2, 2] = tmp3[2];
                    }
                }

                //获取参数名称和当前参数值
                if (str == " Variable       Old X    -DE/DX   Delta X   Delta X   Delta X     New X")           //读梯度的标志
                {
                    str = OutFileStreamReader.ReadLine();     //跳过(Linear)    (Quad)   (Total)那一行
                    for (int i = 0; i < (3 * nAtoms); i++)
                    {
                        str = OutFileStreamReader.ReadLine();
                        tmp3[0] = str.Substring(0, 11);
                        tmp3[1] = str.Substring(11, 10);
                        tmp3[2] = str.Substring(21, 10);
                        ForceParams[i, 0] = tmp3[0];
                        ForceParams[i, 1] = tmp3[1];
                        //ForceParams[i,2] = tmp3[2];
                    }
                }
            }
            //把Out文件力矩阵矩阵元中的D改为E
            for (int m = 0; m < 3 * nAtoms; m++)
            {
                ForceParams[m, 2] = ForceParams[m, 2].Replace('D', 'E');
            }
            //返回值
            return ForceParams;
        }

        /// <summary>
        /// 得到力常数矩阵，参数的标号按Out文件中的顺序
        /// </summary>
        /// <returns>二维数组，力常数矩阵string[3*N-6,3*N-6]</returns>
        //public string[,] GetForceConstant()
        public string[,] GetForceConstant_Zmatrix()
        {
            //数据准备
            string str = "";         //临时存放每一行数据
            string[] temp_Data = new string[1000];   //临时存放数据行被分割的部分
            string[] Data = new string[6];           //把每一行数据按空格分成六份,每一份为一个力常数数据
            //初始化每行数据Data
            for (int i = 0; i < 6; i++)
            {
                Data[i] = "ForceConstant";
            }
            int Block = 0;                            //力常数矩阵在Out文件中被分成的块数

            Block = Convert.ToInt32(Math.Floor((3 * Convert.ToDouble(nAtoms) - 6) / 5) + 1);         //力常数矩阵的块数
            string[,] ForceConstant = new string[3 * nAtoms - 6, 3 * nAtoms - 6];        //定义力常数矩阵数组
            //初始化力常数矩阵
            for (int i = 0; i < 3 * nAtoms - 6; i++)
            {
                for (int j = 0; j < 3 * nAtoms - 6; j++)
                {
                    ForceConstant[i, j] = "bnulk";
                }
            }

            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法

            while (OutFileStreamReader.Peek() > -1)               //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();             //读文件的一行
                if (str == " Force constants in internal coordinates: ")           //读梯度的标志
                {
                    //跳过没有数据的一行
                    str = OutFileStreamReader.ReadLine();

                    for (int i = 0; i < Block; i++)              //按块读数据
                    {
                        for (int m = 5 * i; m < 3 * nAtoms - 6; m++)      //读行
                        {
                            //读文本中每一行数据
                            str = OutFileStreamReader.ReadLine();
                            temp_Data = str.Split(' ');
                            for (int j = 0, k = 0; j < temp_Data.Length; j++)
                            {
                                if (temp_Data[j] != "")
                                {
                                    Data[k] = temp_Data[j];
                                    k++;
                                }
                            }
                            if (i * 5 + 5 <= 3 * nAtoms - 6)                          //判断是否为最后一个力常数块
                            //不是最后一个力常数块
                            {
                                for (int n = 5 * i; n < 5 + 5 * i; n++) //读列
                                {
                                    if (n <= m)
                                    {
                                        ForceConstant[m, n] = Data[n - 5 * i + 1];
                                    }
                                }
                            }
                            else
                            //最后一个力常数块
                            {
                                for (int n = 5 * i; n < 3 * nAtoms - 6; n++) //读列
                                {
                                    if (n <= m)
                                    {
                                        ForceConstant[m, n] = Data[n - 5 * i + 1];
                                    }
                                }
                            }
                        }

                        str = OutFileStreamReader.ReadLine();     //跳过Block之间的构型参数
                    }
                    for (int m = 0; m < 3 * nAtoms - 6; m++)
                    {
                        for (int n = 0; n < 3 * nAtoms - 6; n++)
                        {
                            if (m < n)
                            {
                                ForceConstant[m, n] = ForceConstant[n, m];
                            }
                        }
                    }
                    //把Out文件力常数矩阵矩阵元中的D改为E
                    for (int m = 0; m < 3 * nAtoms - 6; m++)
                    {
                        for (int n = 0; n < 3 * nAtoms - 6; n++)
                        {
                            ForceConstant[m, n] = ForceConstant[m, n].Replace('D', 'E');
                        }
                    }
                }
            }
            return ForceConstant;
        }

        /// <summary>
        /// 得到力常数矩阵，参数的标号按Out文件中的顺序
        /// </summary>
        /// <returns>二维数组，力常数矩阵string[3*N,3*N]</returns>
        public string[,] GetForceConstant_Cartesian()
        {
            //数据准备
            string str = "";         //临时存放每一行数据
            string[] temp_Data = new string[1000];   //临时存放数据行被分割的部分
            string[] Data = new string[6];           //把每一行数据按空格分成六份,每一份为一个力常数数据
            //初始化每行数据Data
            for (int i = 0; i < 6; i++)
            {
                Data[i] = "ForceConstant";
            }
            int Block = 0;                            //力常数矩阵在Out文件中被分成的块数
            Block = Convert.ToInt32(Math.Floor((3 * Convert.ToDouble(nAtoms)) / 5) + 1);         //力常数矩阵的块数
            string[,] ForceConstant = new string[3 * nAtoms, 3 * nAtoms];        //定义力常数矩阵数组
            //初始化力常数矩阵
            for (int i = 0; i < 3 * nAtoms; i++)
            {
                for (int j = 0; j < 3 * nAtoms; j++)
                {
                    ForceConstant[i, j] = "bnulk";
                }
            }

            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法

            while (OutFileStreamReader.Peek() > -1)               //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();             //读文件的一行
                if (str == " Force constants in Cartesian coordinates: ")           //读梯度的标志
                {
                    //跳过没有数据的一行
                    str = OutFileStreamReader.ReadLine();

                    for (int i = 0; i < Block; i++)              //按块读数据
                    {
                        for (int m = 5 * i; m < 3 * nAtoms; m++)      //读行
                        {
                            //读文本中每一行数据
                            str = OutFileStreamReader.ReadLine();
                            temp_Data = str.Split(' ');
                            for (int j = 0, k = 0; j < temp_Data.Length; j++)
                            {
                                if (temp_Data[j] != "")
                                {
                                    Data[k] = temp_Data[j];
                                    k++;
                                }
                            }
                            if (i * 5 + 5 <= 3 * nAtoms)                          //判断是否为最后一个力常数块
                            //不是最后一个力常数块
                            {
                                for (int n = 5 * i; n < 5 + 5 * i; n++) //读列
                                {
                                    if (n <= m)
                                    {
                                        ForceConstant[m, n] = Data[n - 5 * i + 1];
                                    }
                                }
                            }
                            else
                            //最后一个力常数块
                            {
                                for (int n = 5 * i; n < 3 * nAtoms; n++) //读列
                                {
                                    if (n <= m)
                                    {
                                        ForceConstant[m, n] = Data[n - 5 * i + 1];
                                    }
                                }
                            }
                        }

                        str = OutFileStreamReader.ReadLine();     //跳过Block之间的构型参数
                    }
                    for (int m = 0; m < 3 * nAtoms; m++)
                    {
                        for (int n = 0; n < 3 * nAtoms; n++)
                        {
                            if (m < n)
                            {
                                ForceConstant[m, n] = ForceConstant[n, m];
                            }
                        }
                    }
                    //把Out文件力常数矩阵矩阵元中的D改为E
                    for (int m = 0; m < 3 * nAtoms; m++)
                    {
                        for (int n = 0; n < 3 * nAtoms; n++)
                        {
                            ForceConstant[m, n] = ForceConstant[m, n].Replace('D', 'E');
                        }
                    }
                }
            }
            return ForceConstant;
        }

        /// <summary>
        /// 读输入取向坐标
        /// </summary>
        /// <param name="atomicNumber">原子序数</param>
        /// <param name="atomicType">原子类型</param>
        /// <param name="coordinates_Angstroms">坐标</param>
        public void ReadInputOrientation(out int[] atomicNumber, out int[] atomicType, out double[,] coordinates_Angstroms)
        {
            int dim = 3 * nAtoms;
            atomicNumber = new int[nAtoms];
            atomicType = new int[nAtoms];
            coordinates_Angstroms = new double[dim, 3];
            string[] tmpWords = new string[6];

            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法
            string str;

            //定位
            str = OutFileStreamReader.ReadLine();             //读文件的一行
            while (str.Trim() != "Input orientation:" && OutFileStreamReader.Peek() > -1)               //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();
            }
            for (int i = 0; i < 4; i++)
            {
                str = OutFileStreamReader.ReadLine();
            }

            //读取数据
            for (int i = 0; i < nAtoms; i++)
            {
                str = OutFileStreamReader.ReadLine();
                tmpWords = Tools_GetStringSeparatedbySpaces(str);
                atomicNumber[i] = Convert.ToInt32(tmpWords[1]);
                atomicType[i] = Convert.ToInt32(tmpWords[2]);
                coordinates_Angstroms[i, 0] = Convert.ToDouble(tmpWords[3]);
                coordinates_Angstroms[i, 1] = Convert.ToDouble(tmpWords[4]);
                coordinates_Angstroms[i, 2] = Convert.ToDouble(tmpWords[5]);
            }

            return;
        }

        public void ReadInputOrientationForce(out double[] force)
        {
            int dim = 3 * nAtoms;
            force = new double[dim];
            string[] tmpWords = new string[5];

            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法
            string str;

            //定位
            str = OutFileStreamReader.ReadLine();             //读文件的一行
            while (str.Trim() != "***** Axes restored to original set *****" && OutFileStreamReader.Peek() > -1)               //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();
            }
            for (int i = 0; i < 5; i++)
            {
                str = OutFileStreamReader.ReadLine();
            }

            //读取数据
            for (int i = 0; i < nAtoms; i++)
            {
                str = OutFileStreamReader.ReadLine();
                tmpWords = Tools_GetStringSeparatedbySpaces(str);
                force[3 * i] = Convert.ToDouble(tmpWords[2]);
                force[3 * i + 1] = Convert.ToDouble(tmpWords[3]);
                force[3 * i + 2] = Convert.ToDouble(tmpWords[4]);
            }

            return;
        }

        /// <summary>
        /// 读L703后的力常数矩阵，即输入坐标下的力常数矩阵
        /// </summary>
        /// <param name="forceConstant">力常数矩阵</param>
        public void ReadL703Hessian(out double[,] forceConstant)
        {
            //数据准备
            string str = "";         //临时存放每一行数据
            string[] temp_Data = new string[1000];   //临时存放数据行被分割的部分
            string[] Data = new string[6];           //把每一行数据按空格分成六份,每一份为一个力常数数据
            //初始化每行数据Data
            for (int i = 0; i < 6; i++)
            {
                Data[i] = "ForceConstant";
            }
            int Block = 0;                            //力常数矩阵在Out文件中被分成的块数
            int N = ObtainN();                        //获取原子个数N值
            int dim = 3 * N;
            Block = Convert.ToInt32(Math.Floor((3 * Convert.ToDouble(N)) / 5) + 1);         //力常数矩阵的块数
            string[,] strForceConstant = new string[dim, dim];        //定义力常数矩阵数组
            forceConstant = new double[dim, dim];
            //初始化力常数矩阵
            for (int i = 0; i < 3 * N; i++)
            {
                for (int j = 0; j < 3 * N; j++)
                {
                    strForceConstant[i, j] = "bnulk";
                }
            }

            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法

            //定位
            while (str.Trim() != "Hessian after L703:" && OutFileStreamReader.Peek() > -1)
            {
                str = OutFileStreamReader.ReadLine();             //读文件的一行
            }

            //读取数据
            str = OutFileStreamReader.ReadLine();                        //跳过没有数据的一行
            for (int i = 0; i < Block; i++)              //按块读数据
            {
                for (int m = 5 * i; m < dim; m++)      //读行
                {
                    //读文本中每一行数据
                    str = OutFileStreamReader.ReadLine();
                    temp_Data = str.Split(' ');
                    for (int j = 0, k = 0; j < temp_Data.Length; j++)
                    {
                        if (temp_Data[j] != "")
                        {
                            Data[k] = temp_Data[j];
                            k++;
                        }
                    }
                    if (i * 5 + 5 <= dim)                          //判断是否为最后一个力常数块
                                                                   //不是最后一个力常数块
                    {
                        for (int n = 5 * i; n < 5 + 5 * i; n++) //读列
                        {
                            if (n <= m)
                            {
                                strForceConstant[m, n] = Data[n - 5 * i + 1];
                            }
                        }
                    }
                    else
                    //最后一个力常数块
                    {
                        for (int n = 5 * i; n < dim; n++) //读列
                        {
                            if (n <= m)
                            {
                                strForceConstant[m, n] = Data[n - 5 * i + 1];
                            }
                        }
                    }
                }
                str = OutFileStreamReader.ReadLine();     //跳过Block之间的构型参数
            }

            //补齐力常数矩阵
            for (int m = 0; m < dim; m++)
            {
                for (int n = 0; n < dim; n++)
                {
                    if (m < n)
                    {
                        strForceConstant[m, n] = strForceConstant[n, m];
                    }
                }
            }
            //把Out文件力常数矩阵矩阵元中的D改为E，然后把字符串转为双精度的数
            for (int m = 0; m < dim; m++)
            {
                for (int n = 0; n < dim; n++)
                {
                    strForceConstant[m, n] = strForceConstant[m, n].Replace('D', 'E');
                    forceConstant[m, n] = Convert.ToDouble(strForceConstant[m, n]);
                }
            }

            return;
        }

        /// <summary>
        /// 获取优化的步数
        /// </summary>
        /// <returns>优化步数</returns>
        public int GetNumberOfSteps()
        {
            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法
            string str;
            string[] tmpWords = new string[5];
            int result = -1;

            //定位
            str = OutFileStreamReader.ReadLine();             //读文件的一行
            while (str.Trim() != "Optimization stopped." && OutFileStreamReader.Peek() > -1)               //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();
            }

            if(OutFileStreamReader.Peek()<0)
            {
                errorInfo = "ReadGaussianOut.GetNumberOfSteps(). The specified content was not found.";
            }

            //读取数据
            try
            {
                str = OutFileStreamReader.ReadLine();
                tmpWords = str.Split('=');
                result = Convert.ToInt32(tmpWords[1]);
            }
            catch
            {
                errorInfo = "ReadGaussianOut.GetNumberOfSteps(). The specified content was not found.";
            }

            return result;
        }

        /// <summary>
        /// 获取RouteSection
        /// </summary>
        /// <returns>RouteSection</returns>
        public string GetRouteSection()
        {
            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法
            string str;
            string[] tmpWords = new string[5];
            StringBuilder result = new StringBuilder();

            //定位
            str = OutFileStreamReader.ReadLine();             //读文件的一行
            while (str.Trim() != "----------------------------------------------------------------------" && OutFileStreamReader.Peek() > -1)               //应用一个while循环，条件是文件不结束
            {
                str = OutFileStreamReader.ReadLine();
            }

            if (OutFileStreamReader.Peek() < 0)
            {
                errorInfo = "ReadGaussianOut.GetRouteSection(). The specified content was not found.";
            }

            //读取数据
            str = OutFileStreamReader.ReadLine();
            try
            {
                while (str.Trim() != "----------------------------------------------------------------------" && OutFileStreamReader.Peek() > -1)
                {
                    if (str.Trim() != "----------------------------------------------------------------------")
                    {
                        result.Append(str.Remove(0,1));
                    }
                    str = OutFileStreamReader.ReadLine();                                       
                }
                
            }
            catch
            {
                errorInfo = "ReadGaussianOut.GetRouteSection(). The specified content was not found.";
            }

            return result.ToString();
        }

        /// <summary>
        /// 获取TD类型的态数目
        /// </summary>
        /// <returns>TD类型的态数目</returns>
        public int GetNumberOfNstates_TdType()
        {
            int indexMark = -1;

            string routeSection = GetRouteSection();
            routeSection = routeSection.ToLower();
            indexMark = routeSection.IndexOf("nstates");
            if (indexMark == -1)
            {
                return 3;
            }

            routeSection = routeSection.Remove(0, indexMark + 8);
            indexMark = -1;

            indexMark = routeSection.IndexOf(")");
            if(indexMark!=-1)
            {
                routeSection = routeSection.Remove(indexMark);
                return Convert.ToInt32(routeSection);
            }

            indexMark = routeSection.IndexOf(" ");
            if (indexMark != -1)
            {
                routeSection = routeSection.Remove(indexMark);
                return Convert.ToInt32(routeSection);
            }

            return Convert.ToInt32(routeSection);
        }

        public double[,] GetTdaSteepProcess()
        {
            int numberOfSteps = GetNumberOfSteps();
            int numberOfStates = GetNumberOfNstates_TdType();
            double[,] result = new double[numberOfSteps, numberOfStates];

            //正式开始操作
            StreamReader OutFileStreamReader = null;            //生成一个StreamReader的实例OutFileStreamReader；用读方式打开GaussianOut文件
            OutFileStreamReader = File.OpenText(@filePath);     //用OutFileStreamReader打开指定的GaussianOut文件FilePath，使用了System.IO.File名称空间的OpenText方法
            string str;
            double[] energy = new double[numberOfStates];
            int count = 0;
            int indexMark = -1;

            //读取数据
            str = OutFileStreamReader.ReadLine();                 //读文件的一行
            while (OutFileStreamReader.Peek() > -1)               //应用一个while循环，条件是文件不结束
            {
                if(str.Trim() == "Excitation energies and oscillator strengths:")
                {
                    for (int i = 0; i < numberOfStates;)
                    {
                        str = OutFileStreamReader.ReadLine();
                        indexMark = str.IndexOf("Excited State");

                        if (indexMark != -1)
                        {
                            str = str.Substring(39, 8);
                            result[count, i] = Convert.ToDouble(str);
                            i++;
                        }
                    }
                    count++;
                }

                str = OutFileStreamReader.ReadLine();
            }

            return result;
        }













        /// <summary>
        /// 以字符串数组形式，返回被空格分隔的字符串。
        /// </summary>
        /// <param name="original">字符串</param>
        /// <returns>被空格分离的字符串构成的数组</returns>
        private string[] Tools_GetStringSeparatedbySpaces(string original)
        {
            string[] result;
            original = original.Trim();
            result = Regex.Split(original, "\\s+", RegexOptions.IgnoreCase);
            return result;
        }

    }
}
