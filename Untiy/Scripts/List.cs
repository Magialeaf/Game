namespace C_1._0线性表
{
    public class List
    {
        //属性
        /// <summary>
        /// 数据数组
        /// </summary>
        private int[] data { get; set; }
        /// <summary>
        /// 线性表长度
        /// </summary>
        private int length { get; set; }
        /// <summary>
        /// 线性表最大长度
        /// </summary>
        private int max_length { get; set; } 

        //构造函数
        /// <summary>
        /// 未输入参数默认为3
        /// </summary>
        public List() : this(3)
        { }
        /// <summary>
        /// 输入参数
        /// </summary>
        /// <param name="len">线性表最大长度</param>
        public List(int len)
        {
            data = new int[len];
            this.length = 0;
            this.max_length = len;
        }

        //方法
        /// <summary>
        /// 增加线性表元素
        /// </summary>
        /// <param name="L">插入类</param>
        /// <param name="k">添加元素</param>
        public void AddList(int k) 
        {
            checklen();
            this.data[length++] = k;
        }
        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="L">插入类</param>
        /// <param name="i">插入位置</param>
        /// <param name="k">插入元素</param>
        public void InsertList(int i,int k)
        {
            checklen();
            for (int j = length; j >= i; j--)
                this.data[j] = this.data[j - 1];
            this.data[i - 1] = k;
            this.length++;
        }
        /// <summary>
        /// 删除元素
        /// </summary>
        /// <param name="i">删除位置</param>
        public void DeleteList(int i)
        {
            if (i <= 0 || i > this.length) 
            {
                Console.WriteLine("Index Out Of Range!");
                return;
                //throw new IndexOutOfRangeException();
            }
            else
            {
                for (int j = i - 1; j < this.length; j++)
                    this.data[j] = this.data[j + 1];
                this.length--;
            }

        }
        /// <summary>
        /// 遍历线性表
        /// </summary>
        /// <param name="L">插入类</param>
        public void VisitList() 
        {
            for (int i = 0; i < this.length; i++)
            {
                Console.WriteLine(this.data[i]);
            }
        }
        /// <summary>
        /// 检查线性表长度
        /// </summary>
        /// <param name="L">线性表</param>
        private void checklen()
        {
            if(this.length >= this.max_length)
            {
                int[] NewData = new int[max_length * 2];
                this.data.CopyTo(NewData,0);
                this.data = NewData;
                this.max_length = max_length * 2;
            }
        }
        /// <summary>
        /// 通过索引找到数据值
        /// </summary>
        /// <param name="index">索引</param>
        public int getElement(int index)
        {
            return data[index];
        }

    }
    public class User
    {
        static void Main()
        {
            List x = new List();
            for (int i = 0; i < 5; i++)
                x.AddList(i*2);
            x.DeleteList(0);
            x.VisitList();
        }
    }
}
