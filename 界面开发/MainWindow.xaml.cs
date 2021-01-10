using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Data;

namespace 界面开发
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, int> inputElements = new Dictionary<string, int>();
        PeriodEiTable FullTable = new PeriodEiTable();
        
        /// <summary>
        /// 将周期表展示在表格中
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            FullTable.ReadCsv();
            dgPlay.ItemsSource = FullTable.listelements;
        }
        //接受输入 注册改变事件
        private void receiveInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputElements = CountElements(receiveInput.Text);
            if (inputElements.Count > 0)
            {
                var allInputElements = from element in inputElements
                                       join tableElement in FullTable.PeriodElTable on element.Key equals tableElement.Key
                                       select new
                                       {
                                           Symbol = tableElement.Value.Symbol,
                                           Count = element.Value,
                                           Mass = Convert.ToSingle(tableElement.Value.AtomMass),
                                           TotalMass = Convert.ToSingle(tableElement.Value.AtomMass * element.Value),
                                           
                                       };
                dgPlay.Visibility = Visibility.Hidden;
                dgPlay_2.Visibility = Visibility.Visible;
                
                dgPlay_2.ItemsSource = null;
                dgPlay_2.ItemsSource = allInputElements;
                var molarMass = (from element in allInputElements select element.TotalMass).Sum();
               
                textBox1.Text = molarMass.ToString() + " g/mol";
            }
            else
            {
                dgPlay.Visibility = Visibility.Visible;
                dgPlay_2.Visibility = Visibility.Hidden;
                
                dgPlay.ItemsSource = null;
                dgPlay.ItemsSource = FullTable.listelements;
                textBox1.Clear();
            }
        }
        //对输入部分进行匹配
        private Dictionary<string, int> CountElements(string strChemForm)
        {
            Dictionary<string, int> dElements = new Dictionary<string, int>();

            string pattern = @"([A-Z])([a-z]+)?(([1-9])(\d+)?)?"; //new and better pattern
            Regex rgx = new Regex(pattern);

            string matchingSymbolStr = "";

            foreach (Match match in rgx.Matches(strChemForm))
            {
                string symbol;
                int count;
                if (Regex.IsMatch(match.Value, @"([a-zA-Z]+)(\d+)"))
                {
                    Regex rgx2 = new Regex(@"([a-zA-Z]+)(\d+)");
                    Match match2 = rgx2.Match(match.Value);
                    symbol = match2.Groups[1].Value;

                    Int32.TryParse(match2.Groups[2].Value, out count);
                    if (match2.Groups[2].Length <= 9)
                    {
                        Int32.TryParse(match2.Groups[2].Value, out count);
                    }
                    else
                    {
                        MessageBox.Show("Too many elements after " + symbol + ". This element will not be calculated.");
                    }
                }
                else
                {
                    symbol = match.Value;
                    count = 1;
                }

                if (FullTable.PeriodElTable.ContainsKey(symbol))
                {
                    matchingSymbolStr += match.Value;

                    if (dElements.ContainsKey(symbol))
                    {
                        dElements[symbol] += count;
                    }
                    else
                    {
                        dElements.Add(symbol, count);
                    }
                }
            }
            if (matchingSymbolStr.Length == 0 && receiveInput.Text != "")
                MessageBox.Show("不存在", "系统提醒", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (matchingSymbolStr.Length < receiveInput.Text.Length)
                textBox1.Background = Brushes.Yellow;
            else
                textBox1.Background = Brushes.White;
            return dElements;
        }

        private void butClear_Click(object sender, RoutedEventArgs e)
        {
            receiveInput.Clear();
            textBox1.Clear();
        }
    }
} 


    

