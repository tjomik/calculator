using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Result button
        private void equals_Click_1(object sender, EventArgs e)
        {
            label1.Visible = false; // Hide error message

            List<string> rpnExpression = rpnConvertor(textBox1.Text);
            
            // Show error message, if there are some mistakes
            try
            {
                double result = rpnParser(rpnExpression);
                textBox1.Text = result.ToString();
            }
            catch
            {
                label1.Visible = true;
            }
        }


        public List<string> rpnConvertor(string expr)
        {
            string expression = expr;
            List<string> expressionRpn = new List<string>();
            Stack<char> operands = new Stack<char>();
            int numberStartPosition = 0; //Start sign of number
            int i = 0; //Counter

            //Operands with sign
            // Put operand with sign into brackets
            if (expression[0] == '-' || expression[0] == '+')
            {
                expression = expression.Insert(0, "(");
                expressionRpn.Add("0");
                for (int n = 2; n < expression.Length; n++)
                {
                    if (expression[n] == '/' || expression[n] == '*' ||
                     expression[n] == '+' || expression[n] == '-')
                    {
                        expression = expression.Insert(n, ")");
                        break;
                    }
                }
            }

            // RPN 
            for (; i < expression.Length; i++)
            {
                
                if (expression[i] == '/' || expression[i] == '*' ||
                    expression[i] == '+' || expression[i] == '-')
                { 
                    // Everything between operators are numbers
                    if (i - numberStartPosition != 0) expressionRpn.Add(expression.Substring(numberStartPosition, i - numberStartPosition));

                    numberStartPosition = i + 1;

                    if (operands.Count != 0)
                    {
                        if (getPriority(operands.Peek()) >= getPriority(expression[i]))
                        {
                            expressionRpn.Add(operands.Pop().ToString());
                            operands.Push(expression[i]);

                        }
                        else operands.Push(expression[i]);
                    }
                    else operands.Push(expression[i]);         
                }
                else if (expression[i] == '(')
                {
                    // Operands with sign
                    if (expression[i + 1] == '-' || expression[i + 1] == '+')
                    {
                        expression = expression.Insert(i, "(");
                        expressionRpn.Add("0");

                        // Put operand with sign into brackets
                        for (int n = i+3; n < expression.Length; n++)
                        {
                            if (expression[n] == '/' || expression[n] == '*' ||
                             expression[n] == '+' || expression[n] == '-')
                            {
                                expression = expression.Insert(n, ")");
                                // Skip brackets
                                i = i + 1;
                                numberStartPosition = i+1;
                                operands.Push('(');
                                break;
                            }
                        }
                    }
                    operands.Push(expression[i]);
                    numberStartPosition = i + 1;
                }
                else if (expression[i] == ')')
                {
                    
                    if (operands.Contains('('))
                    {
                        // Get last value before bracket
                        if (i - numberStartPosition != 0) expressionRpn.Add(expression.Substring(numberStartPosition, i - numberStartPosition));

                        numberStartPosition = i + 1;

                        // Get all operators before the bracket
                        while (operands.Peek()!='(') expressionRpn.Add(operands.Pop().ToString());
                        operands.Pop();
                    }

                }
            }
            // Get the last value 
            if (i - numberStartPosition != 0) expressionRpn.Add(expression.Substring(numberStartPosition, i - numberStartPosition));
            
            // Get all operands from stack
            while (operands.Count()>0) expressionRpn.Add(operands.Pop().ToString());

            return expressionRpn;
            
        }

        // Comparison of operators priorities 
        public int getPriority (char a)
            {
                switch (a)
                {
                    case '(': return 0;
                    case '-': return 1;
                    case '+': return 1;
                    case '*': return 2;
                    case '/': return 2;
                    case ')': return 3;
                    default: return 4;

                }
            }


        public double rpnParser(List<string> list)
        {
            int i = 0;
            Stack<double> stack = new Stack<double>();

            while (i < list.Count)
            {
                    if (list[i] == "+" || list[i] == "-" || list[i] == "*" || list[i] == "/")
                    {
                        // Get values from stack in reverse order
                        double b = stack.Pop(); 
                        double a = stack.Pop();

                        // Put result on the stack.
                        stack.Push(calculate(a, b, Char.Parse(list[i])));
                    }

                    // Put operands on the stack
                    else stack.Push(Double.Parse(list[i]));

                i++;
            }
            return stack.Pop(); // Last value is the anwser
        }

        // Simple calculations 
        public double calculate(double a, double b, char sign)
        {
            switch (sign)
            {
                case '-': return a - b;
                case '+': return a + b;
                case '/': return a / b;
                case '*': return a * b;
                default: return 0;
            }
        }

       //input buttons click

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text += '1';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += '2';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += '3';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += '4';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += '5';
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += '6';
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += '7';
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += '8';
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += '9';
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text += '0';
        }

        private void division_Click(object sender, EventArgs e)
        {
            textBox1.Text += '/';
        }

        private void minus_Click(object sender, EventArgs e)
        {
            textBox1.Text += '-';
        }

        private void multiplication_Click(object sender, EventArgs e)
        {
            textBox1.Text += '*';
        }

        private void plus_Click(object sender, EventArgs e)
        {
            textBox1.Text += '+';
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Count()>0) textBox1.Text = textBox1.Text.Remove(textBox1.Text.Count()-1);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form info = new Information();
            info.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += ",";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "(";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += ")";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
