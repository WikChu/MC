using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 界面开发
{
    public class PeriodEiTable
    {
        //创建一个Dictionary存放元素周期表数据
        public Dictionary<string, Element> PeriodElTable = new Dictionary<string, Element>();
        public List<Element> listelements = new List<Element>();

        public Element this[string i]
        {
            get { return PeriodElTable[i]; }
        }
        //从元素周期表.csv表格中读取数据
        public void ReadCsv()
        {
            
            //读取每一行
            var lines = File.ReadLines("元素周期表.csv");
            //遍历每一行的数据
            foreach (var str in lines)
            {
                //按逗号分隔数据，并返回一个字符串数组
                string[] ElementArray = str.Split(',');
                //读取质量信息
                double atomMass = 0;
                if (ElementArray[3].IndexOf('(') > 0)
                    atomMass = float.Parse(ElementArray[3].Substring(0, ElementArray[3].IndexOf('(')));
                //实例化Elemnet调用Elemet传入数据
                Element writeElemnt = new Element(ElementArray[2].Trim(), ElementArray[1].Trim(), int.Parse(ElementArray[0]), atomMass); //Math.Round
                //放入Dictionary
                PeriodElTable.Add(writeElemnt.Symbol, writeElemnt);
                listelements.Add(writeElemnt);
            }
            
        }
    }
}
