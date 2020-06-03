using UnityEngine;
using VoxelValley.Enviroment.BiomeManagement;
using VoxelValley.Enviroment.Generation;

namespace VoxelValley.Enviroment.RegionManagement.Regions
{
    public class Greenlands : Region
    {
        public override string Name => "Greenlands";
        public override Color Color => Color.green;

        public override Biome GetBiome(int x, int z)
        {
            HeightMap.HeightType heightType = HeightMap.GetHeightType(x, z);

            switch (heightType)
            {
                case HeightMap.HeightType.Lowest:
                case HeightMap.HeightType.Lower:
                    return BiomeReferences.Grasslands.Plains;
                case HeightMap.HeightType.Low:
                case HeightMap.HeightType.High:
                    return BiomeReferences.Grasslands.Forest;
                case HeightMap.HeightType.Higher:
                    return BiomeReferences.Grasslands.Hills;
                case HeightMap.HeightType.Highest:
                    return BiomeReferences.Grasslands.Mountains;
                default:
                    return BiomeReferences.Empty;
            }
        }
    }
}