using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemOptimizer
{
    [Serializable]
    public struct MoleculeSpecification
    {
        /// <summary>
        /// 原子个数
        /// </summary>
        public int numberOfAtoms;
        /// <summary>
        /// 原子序号（核电荷数）数组
        /// </summary>
        public int[] atomicNumbers;
        /// <summary>
        /// 原子量数组
        /// </summary>
        public double[] realAtomicWeights;
        /// <summary>
        /// 电荷
        /// </summary>
        public List<int[]> charge;
        /// <summary>
        /// 自旋多重度
        /// </summary>
        public List<int[]> multiplicity;
        /// <summary>
        /// 输入的ZMatrix
        /// </summary>
        public ZMatrix inputZmatrix;
        /// <summary>
        /// 输入的ZMatrix
        /// </summary>
        public Cartesian inputCartesian;
        /// <summary>
        /// 冻结的原子标号
        /// </summary>
        public int[] freeze;
        /// <summary>
        /// 原子类型
        /// </summary>
        public string[] atomType;
        /// <summary>
        /// 分层标识
        /// </summary>
        public List<string[]> markOfLayer;
    }

    // <summary>
    /// ZMatrix信息
    /// </summary>
    [Serializable]
    public struct ZMatrix
    {
        /// <summary>
        /// 是否存在ZMatrix信息
        /// </summary>
        public bool isExist;
        /// <summary>
        /// 原子序号（核电荷数）数组
        /// </summary>
        public int[] atomicNumbers;
        /// <summary>
        /// 内坐标的连接信息
        /// </summary>
        public int[,] connectionInfo;
        /// <summary>
        /// 内坐标的连接信息的值
        /// </summary>
        public double[,] connectionInfoValue;
        /// <summary>
        /// 内坐标的参数数组
        /// </summary>
        public string[] paraName;
        /// <summary>
        /// 内坐标的坐标数组
        /// </summary>
        public double[] paraValue;
    }

    public struct Cartesian
    {
        /// <summary>
        /// 是否存在Cartesian信息
        /// </summary>
        public bool isExist;
        /// <summary>
        /// 原子序号（核电荷数）数组
        /// </summary>
        public int[] atomicNumbers;
        /// <summary>
        /// 笛卡尔坐标数组
        /// </summary>
        public double[,] cartesian3;
    }





}
