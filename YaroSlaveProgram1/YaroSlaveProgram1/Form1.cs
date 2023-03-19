using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YaroSlaveProgram1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        double F(double x) {  //f(x) = x^3 - 10x^2 - 3x + 59

            double result = Math.Pow(x, 3) - 10 * Math.Pow(x, 2) - 3 * x + 59;
            return result;
        }

        double F1(double x) { //производная исходной функции f(x)
            double result = 3 * Math.Pow(x, 2) - 20 * x - 3;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x0, xn, xnp1, ex, h = 0.01;
            const int a = 5, b = 7;
            bool isAnswerFound = true;
            this.chart1.Series[0].Points.Clear();
            ex = 0.001; //Эпсилон (допустимая погрешность приближения)
            x0 = 5;     //начальная точка x 
            xn = x0 - F(x0)/F1(x0);
            xnp1 = xn - F(xn)/F1(xn);
            if (xnp1 < 5 || xnp1 > 7) { isAnswerFound = false; }

            //Отрисовываем функцию на графике
            double xk = a - 1;
            while (xk < b + 1)
            {

                this.chart1.Series[0].Points.AddXY(xk, F(xk));
                xk += h;
            }

            this.chart1.Series[1].Points.AddXY(a, 0);
            this.chart1.Series[1].Points.AddXY(a, F(a));

            this.chart1.Series[2].Points.AddXY(b, 0);
            this.chart1.Series[2].Points.AddXY(b, F(b));

            this.chart1.Series[3].Points.AddXY(a - 1, 0);
            this.chart1.Series[3].Points.AddXY(b + 1, 0);


            int cnt = 0;
            while (xn - xnp1 >= ex) //метод Ньютона + подсчет итераций через cnt
            {
                xn = xnp1;
                xnp1 = xn - F(xn)/ F1(xn);
                if (xnp1 < a || xnp1 > b) { isAnswerFound = false; break;  }
                this.chart1.Series[4].Points.AddXY(xn,0);
                cnt++;
            }

            if (isAnswerFound)
            {
                Console.WriteLine("Найденный корень методом Ньютона: x = {0:0.0000}", xnp1);
                Console.WriteLine("Кол-во итераций: {0:0}", cnt);
            }
            else 
            {
                MessageBox.Show("На промежутке [5;7] корней для заданной функции нет");
            }

        }
    }
}
