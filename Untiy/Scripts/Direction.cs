using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Csharp11
{
    /// <summary>
    /// 基础方向移动类
    /// </summary>
    internal class Direction
    {
        public int rowIndex { get; set; }
        public int colIndex { get; set; }

        public static Direction Up
        {
            get { return new Direction(-1, 0); }
        }
        public static Direction Down
        {
            get { return new Direction(1, 0); }
        }
        public static Direction Left
        {
            get { return new Direction(0, -1); }
        }
        public static Direction Right
        {
            get { return new Direction(0, 1); }
        }

        public Direction()
        {
        }

        public Direction(int rowIndex, int colIndex)
        {
            this.rowIndex = rowIndex;
            this.colIndex = colIndex;
        }
    }
    /// <summary>
    /// 方向操作类
    /// </summary>
    internal class DoDirection
    { 
        /// <summary>
        /// 根据行列获得方向上的count个数组的值
        /// </summary>
        /// <param name="a">二维数组</param>
        /// <param name="row">行位置</param>
        /// <param name="col">列位置</param>
        /// <param name="dir">方向</param>
        /// <param name="count">获得个数</param>
        /// <returns></returns>
        public static int[] GetElemByDirection(int[,] a,int row,int col,Direction dir,int count)
        {
            List<int> res = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                row += dir.rowIndex;
                col += dir.colIndex;
                if (row >= 0 && row < a.GetLength(0) && col >= 0 && col < a.GetLength(1))
                    res.Add(a[row,col]);
            }
            return res.ToArray();
        }
    }

}
