using UnityEngine;
using VoxelValley.Enviroment.BiomeManagement;

namespace VoxelValley.Enviroment.RegionManagement
{
    public abstract class Region
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }

        public abstract Biome GetBiome(int x, int z);
    }
}