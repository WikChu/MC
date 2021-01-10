using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 界面开发
{
    public class Element
    {
        //提取重量
        public string Name
        {
            private
            set;
            get;
        }
        //元素符号
        public string Symbol
        {
            private
            set;
            get;
        }
        //元素周期表元素序号
        public int Number
        {
            private
            set;
            get;
        }
        //原子质量
        public double AtomMass
        {
            private set; get;
        }
        //初始化成分
        public Element(string name, string symbol, int number, double atomMass)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Number = number;
            this.AtomMass = atomMass;
        }
    }
}
