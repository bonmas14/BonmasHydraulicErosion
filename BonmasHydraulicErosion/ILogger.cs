namespace BonmasHydraulicErosion
{
    public interface ILogger
    {
        void Log(int particles, int iterations);

        void SetLimits(int particles, int iterations);
    }
}
