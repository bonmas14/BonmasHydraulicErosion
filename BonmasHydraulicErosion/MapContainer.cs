using System;

namespace BonmasHydraulicErosion
{
    internal class MapContainer
    {
        public float[,] Map { get; private set; }

        public readonly int width;
        public readonly int height;

        public MapContainer(float[,] map)
        {
            Map = map;

            width = Map.GetLength(0);
            height = Map.GetLength(1);
        }

        public Vector2 GetSurfaceNormal(int xCoord, int yCoord)
        {
            try
            {
                float pointHeight = Map[xCoord, yCoord];

                Vector2[] vectors = new Vector2[8];

                int i = 0;

                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        int curPosX = xCoord + x;
                        int curPosY = yCoord + y;

                        if (xCoord == curPosX && yCoord == curPosY)
                            continue;

                        float delta = -1;

                        if (curPosX >= 0 && curPosX < width && curPosY >= 0 && curPosY < height)
                            delta = pointHeight - Map[xCoord + x, yCoord + y];

                        vectors[i++] = new Vector2(x * delta, y * delta);
                    }
                }

                Vector2 sum = new Vector2(0, 0);

                for (int j = 0; j < vectors.Length; j++)
                    sum += vectors[j];

                if (sum.Magnitude < 1f)
                    return sum;
                else
                    return sum.GetNormalized();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }
    }
}
