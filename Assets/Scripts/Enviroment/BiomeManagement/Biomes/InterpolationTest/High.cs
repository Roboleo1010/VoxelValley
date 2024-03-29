using UnityEngine;

namespace VoxelValley.Enviroment.BiomeManagement.Biomes.InterpolationTest
{
    public class High : Biome
    {
        public override string Name => "High";

        public override Color Color => Color.red;

        public override byte BiomeId => 11;

        public override ushort GetHeight(int x, int z)
        {
            return 20;
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            if (y < height)
                return VoxelManager.GetVoxel("debug_red").Id;
            else
                return VoxelManager.AirVoxel;
        }
    }
}