using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgo
{
    public class Chromosome
    {
        public List<float> Values { get; set; }

        public Chromosome(int xCount)
        {
            Random rand = new Random();
            Values = new List<float>();
            for (int i = 0; i < xCount; i++)
            {
                float value = (float)(rand.NextDouble() * (20f) - 10f);
                this.Values.Add(value);
            }
        }
        public float Fitness()
        {
            float sum = 0;
            foreach (var value in Values)
            {
                sum += value * value;
            }
            return sum;
        }
    }
}
