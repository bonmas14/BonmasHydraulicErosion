using System;

namespace BonmasHydraulicErosion
{
    public class ConsoleLogger : ILogger
    {
        private int maxParticles;
        private int maxIterations;



        public void Log(int particles, int iterations)
        {
            float particlesPercent  = (float)Math.Round((particles / maxParticles) * 100f, 1);
            float iterationsPercent = (float)Math.Round((iterations / maxIterations) * 100f, 1);

            Console.Write("P:[{0}/{1}] ", particles, maxParticles, particlesPercent);
            Console.Write(" I:[{0}/{1}]", iterations, maxIterations, iterationsPercent);
            Console.Write("            ");

            Console.CursorLeft = 0;
        }

        public void SetLimits(int particles, int iterations)
        {
            maxParticles = particles;  
            maxIterations = iterations;
        }
    }
}
