namespace BonmasHydraulicErosion
{
    internal struct Droplet
    {
        const float density = 1.0f;
        const float friction = 0.05f;

        const float minVolume = 0.01f;
        const float depositionRate = 0.01f;
        const float evapRate = 0.001f;

        const float stepSize = 1.6f;

        public void IterateParticle(Vector2 pos, World world)
        {
            Vector2 accel = new Vector2(0, 0);

            float sediment = 0.0f;
            float volume = 1f;

            while (volume > minVolume)
            {
                Vector2 worldPos = pos;
                Vector2 norm;

                int wpX = (int)worldPos.X;
                int wpY = (int)worldPos.Y;

                norm = world.GetSurfaceNormal(wpX, wpY);

                accel += norm / (volume * density) * stepSize;
                pos += accel * stepSize;
                accel *= 1.0f - stepSize * friction;

                if (pos.X < 0 || pos.X > (world.width - 1)) break;
                if (pos.Y < 0 || pos.Y > (world.height - 1)) break;

                float worldHeight = world.Map[wpX, wpY];
                float particleHeight = world.Map[(int)pos.X, (int)pos.Y];

                float maxSediment = volume * accel.Magnitude * (worldHeight - particleHeight);

                if (maxSediment < 0.0f) maxSediment = 0.0f;

                float cdiff = maxSediment - sediment;

                sediment += stepSize * depositionRate * cdiff;
                worldHeight -= stepSize * volume * depositionRate * cdiff;

                world.Map[wpX, wpY] = worldHeight;

                volume *= (float)(1.0 - stepSize * evapRate);
            }
        }
    }
}
