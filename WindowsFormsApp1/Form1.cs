using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }
    /// <summary>
    /// Левая граница графика
    /// </summary>
    private double XMin = -0.9;
    /// <summary>
    /// Правая граница графика
    /// </summary>
    private double XMax = 3.1;
    /// <summary>
    /// Шаг графика
    /// </summary>
    private double Step = 0.1;
    // Массив значений X - общий для обоих графиков
    private double[] x;
    // Два массива Y - по одному для каждого графика
    private double[] y1;
    private double[] y2;
    Chart chart;
    /// <summary>
    /// Расчѐт значений графика
    /// </summary>
    private void CalcFunction()
    {
      // Количество точек графика
      int count = (int)Math.Ceiling((XMax - XMin) / Step)
      + 1;
      // Создаѐм массивы нужных размеров
      x = new double[count];
      y1 = new double[count];
      y2 = new double[count];
      // Расчитываем точки для графиков функции
      for (int i = 0; i < count; i++)
      {
        // Вычисляем значение X
        x[i] = XMin + Step * i;
        // Вычисляем значение функций в точке X
        y1[i] = Math.Abs(1 + Math.Cos(Math.Sqrt(x[i])) - (Math.Pow(Math.Log(1), 2) * 2 * x[i])) / ((Math.Sqrt(3 * x[i] + 4)) - (Math.Pow(2, 0.5 * x[i] - 1)));
      }
    }
    /// <summary>
    /// Создаѐм элемент управления Chart и настраиваем его
    /// </summary>
    private void CreateChart()
    {
      // Создаѐм новый элемент управления Chart
      chart = new Chart();
      // Помещаем его на форму
      chart.Parent = this;
      // Задаѐм размеры элемента
      chart.SetBounds(10, 10, ClientSize.Width - 20,
      ClientSize.Height - 20);
      // Создаѐм новую область для построения графика
      ChartArea area = new ChartArea();
      // Даѐм ей имя (чтобы потом добавлять графики)
      area.Name = "myGraph";
      // Задаѐм левую и правую границы оси X
      area.AxisX.Minimum = XMin;
      area.AxisX.Maximum = XMax;
      // Определяем шаг сетки
      area.AxisX.MajorGrid.Interval = Step;
      // Добавляем область в диаграмму
      chart.ChartAreas.Add(area);
      // Создаѐм объект для первого графика
      Series series1 = new Series();
      // Ссылаемся на область для построения графика
      series1.ChartArea = "myGraph";
      // Задаѐм тип графика - сплайны
      series1.ChartType = SeriesChartType.Spline;
      // Указываем ширину линии графика
      series1.BorderWidth = 3;
      // Название графика для отображения в легенде
      series1.LegendText = "sin(x)";
      // Добавляем в список графиков диаграммы
      chart.Series.Add(series1);
      // Аналогичные действия для второго графика
      Series series2 = new Series();
      series2.ChartArea = "myGraph";
      series2.ChartType = SeriesChartType.Spline;
      series2.BorderWidth = 3;
      series2.LegendText = "cos(x)";
      chart.Series.Add(series2);
      // Создаѐм легенду, которая будет показывать названия
      Legend legend = new Legend();
      chart.Legends.Add(legend);
    }
    private void Form1_Load(object sender, EventArgs e)
        {
      // Создаѐм элемент управления
      CreateChart();
      // Расчитываем значения точек графиков функций
      CalcFunction();
      // Добавляем вычисленные значения в графики
      chart.Series[0].Points.DataBindXY(x, y1);
      chart.Series[1].Points.DataBindXY(x, y2);
    }
    }
}
