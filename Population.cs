using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace GenAlgo
{
    public class Population
    {
        List<Chromosome> TournamentWinner;
        public List<Chromosome> Chromosomes { get; set; }
        private List<Chromosome> Elites;
        public int Size => Chromosomes.Count;
        private int EliteSize;

        public Population(int size, int eliteSize,int xCount)
        {
            EliteSize = eliteSize;
            Chromosomes = new List<Chromosome>(size);
            Elites = new List<Chromosome>(eliteSize);
            for (int i = 0; i < size; i++)
            {
                Chromosomes.Add(new Chromosome(xCount));
            }
        }

        public void Mutate(float MutateRate)
        {
            Random rand = new Random();
            for (int i = 0; i < Chromosomes.Count; i++)
            {
                if (rand.NextDouble() < MutateRate)
                {
                    Chromosomes[i].Values[rand.Next(0, Chromosomes[i].Values.Count)] = (float)(rand.NextDouble() * (20f) - 10f);
                }
            }
        }
        public void TournamentSelection()
        {
            Random rand = new Random();
            TournamentWinner = new List<Chromosome>();
            for (int i = 0; i < Chromosomes.Count; i++)
            {
                List<Chromosome> tournament= new List<Chromosome>();
                for (int j = 0; j < 2; j++)
                {
                    int randomIndex = rand.Next(0, Chromosomes.Count);
                    tournament.Add(Chromosomes[randomIndex]);
                }
                Chromosome best = tournament.OrderBy(c => c.Fitness()).First();
                TournamentWinner.Add(best);
            }
        }
        public void Crossover(float crossRate)
        {
            List<Chromosome> newGeneration = new List<Chromosome>();
            Random rand = new Random();

            Elites = Chromosomes.OrderBy(c => c.Fitness()).Take(EliteSize).ToList();
            newGeneration.AddRange(Elites);

            while (newGeneration.Count < Chromosomes.Count)
            {
                for (int i = 0; i < Chromosomes.Count - EliteSize; i += 2)
                {
                    if (rand.NextDouble() < crossRate)
                    {
                        Chromosome parent1 = TournamentWinner[rand.Next(TournamentWinner.Count)];
                        TournamentWinner.Remove(parent1);
                        Chromosome parent2 = TournamentWinner[rand.Next(TournamentWinner.Count)];
                        TournamentWinner.Remove(parent2);

                        int crossoverPoint = rand.Next(1, parent1.Values.Count - 1); 

                        List<float> child1Values = new List<float>();
                        List<float> child2Values = new List<float>();

                        for (int j = 0; j < parent1.Values.Count; j++)
                        {
                            if (j < crossoverPoint)
                            {
                                child1Values.Add(parent1.Values[j]);
                                child2Values.Add(parent2.Values[j]);
                            }
                            else
                            {
                                child1Values.Add(parent2.Values[j]);
                                child2Values.Add(parent1.Values[j]);
                            }
                        }

                        Chromosome child1 = new Chromosome(0) { Values = child1Values };
                        Chromosome child2 = new Chromosome(0) { Values = child2Values };
                        newGeneration.Add(child1);
                        newGeneration.Add(child2);
                    }

                    if (newGeneration.Count >= Chromosomes.Count) break;
                }
            }

            Chromosomes = newGeneration;
        }
    }
}
