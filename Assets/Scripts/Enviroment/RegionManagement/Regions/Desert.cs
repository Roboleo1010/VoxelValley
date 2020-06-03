using UnityEngine;
using VoxelValley.Enviroment.BiomeManagement;
using VoxelValley.Enviroment.Generation;

namespace VoxelValley.Enviroment.RegionManagement.Regions
{
    public class Desert : Region
    {
        public override string Name => "Desert";
        public override Color Color => Color.yellow;

        public override Biome GetBiome(int x, int z)
        {
            HeightMap.HeightType heightType = HeightMap.GetHeightType(x, z);

            switch (heightType)
            {
                case HeightMap.HeightType.Lowest:
                    return BiomeReferences.Desert.Oasis;
                case HeightMap.HeightType.Lower:
                case HeightMap.HeightType.Low:
                case HeightMap.HeightType.High:
                    return BiomeReferences.Desert.FlatDesert;
                case HeightMap.HeightType.Higher:
                case HeightMap.HeightType.Highest:
                    return BiomeReferences.Desert.Dunes;
                default:
                    return BiomeReferences.Empty;
            }
        }
    }
}