using System;
using System.Threading.Tasks;

namespace BonmasHydraulicErosion
{
    public class HydraulicErosion
    {
        private Random random;

        public HydraulicErosion(int seed)
        {
            random = new Random(seed);
        }

        public float[,] ProcessErosion(float[,] map, int particleCount, int iterations)
        {
            World world = new World(map);

            int width  = world.width;
            int height = world.height;

            Droplet[] particles = new Droplet[particleCount];

            for (int i = 0; i < particleCount; i++)
            {
                particles[i] = new Droplet();
            }

            int tasksCount = Environment.ProcessorCount * 8;

            Task[] particlesTasks = new Task[tasksCount];

            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < particleCount; j++)
                {
                    var particle = particles[j];

                    particlesTasks[j % tasksCount] = Task.Run(() =>
                    {
                        Vector2 position = new Vector2(random.Next(width), random.Next(height));

                        particle.IterateParticle(position, world);
                    });

                    if ((j % tasksCount) == 0 && j != 0)
                    {
                        Task.WaitAll(particlesTasks);

                        WriteDiagnosticData(j, particleCount, i, iterations);
                    }
                }

                Task.WaitAll(particlesTasks);
            }

            WriteDiagnosticData(particleCount, particleCount, iterations, iterations);

            return map;
        }

        private void WriteDiagnosticData(int curPart, int parts, int curIter, int iters)
        {

            Console.Write("p:[{0}/{1}] i:[{2}/{3}]\0", curPart, parts, curIter, iters);

            Console.CursorLeft = 0;
        }
    }
}
