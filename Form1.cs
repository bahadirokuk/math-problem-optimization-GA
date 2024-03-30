using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenAlgo
{
    public partial class Form1 : Form
    {
        bool isCleanable = false;
        public Form1()
        {
            InitializeComponent();
        }
        void Solve()
        {
            Population population = new Population((int)numericUpDown1.Value, (int)numericUpDown4.Value, (int)numericUpDown6.Value);
            for (int i = 0; i < numericUpDown5.Value; i++)
            {
                population.TournamentSelection();
                population.Crossover((float)(numericUpDown2.Value/100));
                population.Mutate((float)(numericUpDown3.Value/100));

                float min = MinCalculate(population);
                chart1.Series["Algoritma"].Points.AddXY(i, min);
            }
            float max = float.MaxValue;
            List<float> cs = new List<float>();
            foreach (var chromosome in population.Chromosomes)
            {
                if (chromosome.Fitness() < max)
                {
                    cs = chromosome.Values;
                    max = chromosome.Fitness();
                }
            }
            labelResult.Text = "Sonuç = " + max.ToString();
            LabelDoldur(cs);
        }

        float MinCalculate(Population population)
        {
            float min = float.MaxValue;
            foreach (var chromosome in population.Chromosomes)
            {
                if (chromosome.Fitness() < min)
                {
                    min = chromosome.Fitness();
                }
            }
            return min;
        }

        private void start_Click(object sender, EventArgs e)
        { 
            if (!isCleanable)
            {
                isCleanable = true;
                Solve();
                return;
            }
            chart1.Series["Algoritma"].Points.Clear();
            Solve();
        }

        private void LabelDoldur(List<float> sayilar)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Değerler = ");
            for (int i = 0; i < sayilar.Count; i++)
            {
                sb.AppendFormat("X{0} = {1}", i + 1, sayilar[i]);
                if (i < sayilar.Count - 1)
                {
                    sb.Append("   ");
                }
            }
            string labelIcinMetin = sb.ToString().Trim();
            labelx.Text = labelIcinMetin;
        }
    }
}
