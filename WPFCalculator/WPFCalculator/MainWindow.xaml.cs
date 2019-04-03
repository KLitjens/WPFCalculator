using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace WPFCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public decimal result;
        public int periodCount;
        public int leftBracketCount;
        public int rightBracketCount;

        public MainWindow()
        {
            InitializeComponent();
            periodCount = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //sender to type Button so we can access the button content
            Button btn = (Button)sender;
            string btnContentString = Convert.ToString(btn.Content);

            switch (btnContentString)
            {
                case "C":                       //reset everything
                    displayTextBox.Clear();
                    periodCount = 1;
                    leftBracketCount = 0;
                    rightBracketCount = 0;
                    break;
                case "=":
                    if (leftBracketCount == rightBracketCount)          //check brackets match up
                    {
                        DataTable dt = new DataTable();
                        result = Convert.ToDecimal(dt.Compute(displayTextBox.Text, ""));        //this is used to convert a string to a maths equation and evaluate it
                        displayTextBox.Text = Convert.ToString(result);
                        periodCount = 1;
                        leftBracketCount = 0;
                        rightBracketCount = 0;
                    }
                    else
                        MessageBox.Show("Brackets need to be equal count");         //Sends error if brackets dont match up
                    break;
                case ".":
                    if (periodCount > 0)                                            //Only allow one point per number
                    {
                        if (displayTextBox.Text == Convert.ToString(result))
                            displayTextBox.Clear();
                        displayTextBox.Text += btnContentString;
                        periodCount = 0;
                    }
                    break;
                case "(":
                case ")":
                        if (btnContentString == "(")
                            leftBracketCount++;
                        if (btnContentString == ")")
                            rightBracketCount++;
                        displayTextBox.Text += btnContentString;
                    break;
                default:                                                            //Everything left is either a number or operator
                    if (!Decimal.TryParse(btnContentString, out decimal input))     //If button isn't number, allow dots to be used again
                        periodCount = 1;
                    displayTextBox.Text += btnContentString;
                    break;
            }
        }
    }
}
