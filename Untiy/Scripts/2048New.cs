using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp8
{
    internal class Class1
    {
        static Random random = new Random();
        static int score = 0;
        /// <summary>
        /// 初始化地图
        /// </summary>
        /// <param name="map">地图</param>
        static void newmap(int[,] map)
        {
            int r;
            for (int i = 0; i < 8; i++)
            {
                r = random.Next(1, 4);
                switch(r)
                {
                    case 1: map[random.Next(0, 4), random.Next(0, 4)] = 2;break;
                    case 2: map[random.Next(0, 4), random.Next(0, 4)] = 4; break;
                    case 3: map[random.Next(0, 4), random.Next(0, 4)] = 8; break;
                }
            }
        }
        /// <summary>
        /// 输出地图
        /// </summary>
        /// <param name="map">地图</param>
        static void inputmap(int[,] map) 
        {
            Console.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    Console.Write("{0}\t", map[i, j]);
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 合成数据
        /// </summary>
        /// <param name="temp">一维数组(一行或一列)</param>
        static void merge(int[] temp)
        {
            for(int i = 0; i < 3; i++)
            {
                if(temp[i] == temp[i+1])
                {
                    score += temp[i];
                    temp[i] = temp[i] * 2;
                    temp[i + 1] = 0;
                }
                else if (temp[i] != temp[i+1] && temp[i] == 0)
                {
                    temp[i] = temp[i + 1];
                    temp[i + 1] = 0;
                }
            }
        }
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="input">输入方向</param>
        static void get(char input, int[,] map)
        {
            int[] temp = new int[4];
            int count = 0;
            if (input == 'w')
            {
                for (int j = 0; j < 4; j++)
                {
                    count = 0;
                    for (int i = 0; i < 4; i++)
                        if (map[i, j] != 0)
                            temp[count++] = map[i, j];
                    merge(temp);
                    for (int i = 0; i < 4; i++)
                    { 
                        map[i, j] = temp[i];
                        temp[i] = 0;
                    }
                }
            }
            else if (input == 's')
            {
                for (int j = 0; j < 4; j++)
                {
                    count = 0;
                    for (int i = 3; i >= 0; i--)
                        if (map[i, j] != 0)
                            temp[count++] = map[i, j];
                    merge(temp);
                    for (int i = 0; i < 4; i++)
                    {
                        map[i, j] = temp[3 - i];
                        temp[3 - i] = 0;
                    }
                }
            }
            if (input == 'a')
            {
                for (int i = 0; i < 4; i++)
                {
                    count = 0;
                    for (int j = 0; j < 4; j++)
                        if (map[i, j] != 0)
                            temp[count++] = map[i, j];
                    merge(temp);
                    for (int j = 0; j < 4; j++)
                    {
                        map[i, j] = temp[j];
                        temp[j] = 0;
                    }
                }
            }
            if (input == 'd')
            {
                for (int i = 0; i < 4; i++)
                {
                    count = 0;
                    for (int j = 3; j >= 0; j--)
                        if (map[i, j] != 0)
                            temp[count++] = map[i, j];
                    merge(temp);
                    for (int j = 3; j >= 0; j--)
                    {
                        map[i, j] = temp[3 - j];
                        temp[3 - j] = 0;
                    }
                }
            }
        }
        /// <summary>
        /// 新增元素并判负
        /// </summary>
        /// <param name="map">地图</param>
        static int addmore(int[,] map)
        {
            int row;
            int col;
            for (int i = 0; i < 10; i++) 
            {
                row = random.Next(0, 4);
                col = random.Next(0, 4);
                if (map[row, col] == 0)
                {
                    switch(random.Next(0, 4))
                    {
                    case 1: map[row, col] = 2; break;
                    case 2: map[row, col] = 4; break;
                    case 3: map[row, col] = 8; break;
                    }
                    return 0;
                }
            }
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i,j] == 0)
                    {
                        switch (random.Next(1, 4))
                        {
                            case 1: map[random.Next(0, 4), random.Next(0, 4)] = 2; break;
                            case 2: map[random.Next(0, 4), random.Next(0, 4)] = 4; break;
                            case 3: map[random.Next(0, 4), random.Next(0, 4)] = 8; break;
                        }
                        return 0;
                    }

            return 1;
        }
        static void Main()
        {
            int[,] arr = new int[4, 4];
            int n = 0;
            char input;
            newmap(arr);
            inputmap(arr);
            do
            {
                Console.WriteLine("score:{0}",score);
                Console.Write("input:");
                char.TryParse(Console.ReadLine(), out input);
                if (input == 'w' || input == 's' || input == 'a' || input == 'd')
                {
                    get(input, arr);
                    n = addmore(arr);
                }
                else
                    Console.Clear();
                inputmap(arr);
            } while (n == 0);
            Console.WriteLine("游戏结束,你的分数是{0}",score);
        }
    }
}
