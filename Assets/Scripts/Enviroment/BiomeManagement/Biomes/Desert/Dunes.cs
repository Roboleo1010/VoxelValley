using UnityEngine;
using VoxelValley.Enviroment.Generation;

namespace VoxelValley.Enviroment.BiomeManagement.Biomes.Desert
{
    public class Dunes : Biome
    {
        public override string Name { get => "Dunes"; }
        public override Color Color { get => Color.yellow; }
        public override byte BiomeId { get => 51; }

        public override ushort GetHeight(int x, int z)
        {
            return GenerationUtilities.MapToWorldByte(GenerationUtilities.FBMPerlin(x, z, 5, 0.4f, 0.25f));
        }

        internal override ushort GetVoxel(int x, int y, int z, ushort height)
        {

            if (y > height)
                return VoxelManager.AirVoxel;
            else if (y == height)
                return VoxelManager.GetVoxel("sand").Id;
            else if (y > height - 10)
                return VoxelManager.GetVoxel("sand").Id;
            else
                return VoxelManager.GetVoxel("stone").Id;
        }
    }
}