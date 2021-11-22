using System;
using System.Threading.Tasks;

namespace BonmasHydraulicErosion
{
    public class HydraulicErosion
    {
        private Random posGen;
        private ILogger logger;

        public HydraulicErosion(int seed, ILogger logger)
        {
            posGen = new Random(seed);

            this.logger = logger;
        }

        /// <summary>
        /// Erosion proccessing on map
        /// </summary>
        /// <param name="map">float height map with values from 0 to 1</param>
        /// <param name="countOfDroplets">count of droplets on iteration</param>
        /// <param name="iterations"></param>
        /// <returns>eroded float heightmap</returns>

        public float[,] ProcessErosion(float[,] map, int countOfDroplets, int iterations)
        {
            MapContainer world = new MapContainer(map);

            int width  = world.width;
            int height = world.height;

            Droplet[] droplets = new Droplet[countOfDroplets];

            for (int i = 0; i < countOfDroplets; i++)
            {
                droplets[i] = new Droplet();
            }

            int tasksCount = Environment.ProcessorCount * 8;

            Task[] tasks = new Task[tasksCount];

            logger.SetLimits(countOfDroplets, iterations);

            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < countOfDroplets; j++)
                {
                    var droplet = droplets[j];

                    tasks[j % tasksCount] = Task.Run(() =>
                    {
                        Vector2 position = new Vector2(posGen.Next(width), posGen.Next(height));

                        droplet.IterateParticle(position, world);
                    });

                    if ((j % tasksCount) == 0 && j != 0)
                    {
                        Task.WaitAll(tasks);

                        logger.Log(j, i);
                    }
                }
                Task.WaitAll(tasks);
            }

            logger.Log(countOfDroplets, iterations);
            Console.WriteLine();
            
            return map;
        }

    }
}
