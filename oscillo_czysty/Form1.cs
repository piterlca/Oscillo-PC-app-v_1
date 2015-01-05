using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace oscillo_czysty
{
    public delegate void WyswietlanieDelegate(int t);
    public partial class Form1 : Form
    {
        int podstawa = 150;
        int YMax = 200;
        public Form1()
        {
            InitializeComponent();

            if (chartinit("Channel1") == 0) return;
            if (chartinit("Channel2") == 0) return;


        }


        public int chartinit(string series)
        {
            try                     //adding series with exception handling in case of failure.
            {
                chart1.Series.Add(series);
                chart1.Series[series].ChartArea = "ChartArea1";
                chart1.Series[series].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[series].BorderWidth = 5;
                if (series == "Channel1") chart1.Series[series].Color = Color.Yellow;
                else chart1.Series[series].Color = Color.Blue;
                chart1.ChartAreas["ChartArea1"].AxisX.Maximum = podstawa;
                chart1.ChartAreas["ChartArea1"].AxisY.Maximum = YMax;
            }
            catch (System.ArgumentException)
            {
                chart1.Series.Remove(chart1.Series[series]);
                return 0;
            }
            return 1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(podstawa > 20) podstawa -= 5;
            chart1.Series.Remove(chart1.Series["Channel1"]);
            if (chartinit("Channel1") == 0) return;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            podstawa += 5;
            chart1.Series.Remove(chart1.Series["Channel1"]);
            if (chartinit("Channel1") == 0) return;
        }

        private void Run_Click(object sender, EventArgs e)
        {
            Run.BackColor = Color.Red;
            Thread t = new Thread(new ThreadStart(WatekWyswietlanie));
            t.IsBackground = true;
            t.Start();
        }

        private void WatekWyswietlanie()
        {
            int t = 0;
            while (true)
            {
                Invoke(new WyswietlanieDelegate(NaEkran), new object[] { t++ });
                Thread.Sleep(0);

            }
        }

        private void NaEkran(int t)
        {
            int j = t - podstawa * (int)(t / podstawa);
            chart1.Series["Channel1"].Points.AddXY(j, SigGenerator.sawtooth(t, 5, 20));

            if (j >= podstawa - 2)
            {
                chart1.Series.Remove(chart1.Series["Channel1"]);
                if (chartinit("Channel1") == 0) return;
            }


        }

        private void gainH_Click(object sender, EventArgs e)
        {
            YMax += 10;
            chart1.Series.Remove(chart1.Series["Channel1"]);
            if (chartinit("Channel1") == 0) return;
        }

        private void gainL_Click(object sender, EventArgs e)
        {
            if (YMax > 20) YMax -= 10;
            chart1.Series.Remove(chart1.Series["Channel1"]);
            if (chartinit("Channel1") == 0) return;
        }
    }
}
