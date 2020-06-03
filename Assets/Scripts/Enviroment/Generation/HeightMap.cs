using UnityEngine;

namespace VoxelValley.Enviroment.Generation
{
    public static class HeightMap
    {
        public enum HeightType
        {
            Lowest = 0,
            Lower = 1,
            Low = 2,
            High = 3,
            Higher = 4,
            Highest = 5
        };

        internal static HeightType GetHeightType(float x, float z)
        {
            float value = GetHeight(x, z);

            if (value < 0.1f)
                return HeightType.Lowest;
            else if (value < 0.25f)
                return HeightType.Lower;
            else if (value < 0.50f)
                return HeightType.Low;
            else if (value < 0.75f)
                return HeightType.High;
            else if (value < 0.9f)
                return HeightType.Higher;
            else
                return HeightType.Highest;
        }

        static float GetHeight(float x, float z)
        {
            return GenerationUtilities.FBMPerlin(x, z, 3, 0.6f, 1.4f);
        }

        internal static Color GetColor(HeightType type)
        {
            switch (type)
            {
                case HeightType.Lowest:
                    return new Color(255, 0, 0, 128);
                case HeightType.Lower:
                    return new Color(255, 25, 25, 150);
                case HeightType.Low:
                    return new Color(255, 50, 220, 20);
                case HeightType.High:
                    return new Color(255, 16, 160, 0);
                case HeightType.Higher:
                    return new Color(255, 128, 128, 128);
                case HeightType.Highest:
                    return new Color(255, 255, 255, 255);
                default:
                    return Color.magenta;
            }
        }
    }
}