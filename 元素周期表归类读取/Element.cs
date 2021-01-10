using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 元素周期表归类读取
{
    public class Element
    {
        //提取元素名字
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
        public float AtomMass
        {
            private set;get;
        }
        //初始化成分
        public Element(string name, string symbol, int number, float atomMass)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Number = number;
            this.AtomMass = atomMass;
        }
    }
}
