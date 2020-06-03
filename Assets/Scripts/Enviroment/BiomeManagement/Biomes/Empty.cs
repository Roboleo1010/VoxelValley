using UnityEngine;

namespace VoxelValley.Enviroment.BiomeManagement.Biomes
{
    public class Empty : Biome
    {
        public override string Name => "Empty";

        public override Color Color => Color.white;

        public override byte BiomeId => 0;

        public override ushort GetHeight(int x, int z)
        {
            return 0;
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {
            return VoxelManager.AirVoxel;
        }
    }
}