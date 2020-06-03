using VoxelValley.Enviroment.Generation;
using VoxelValley.Enviroment.RegionManagement.Regions;

namespace VoxelValley.Enviroment.RegionManagement
{
    public static class RegionManager
    {
        public static ushort[,,] GetChunk(int worldBaseX, int worldBaseZ)
        {
            ushort[,,] voxels = new ushort[CommonConstants.World.ChunkSizeXZ, CommonConstants.World.ChunkSizeY, CommonConstants.World.ChunkSizeXZ];

            GenerationPipeline.Generate(worldBaseX, worldBaseZ, ref voxels);

            return voxels;
        }

        public static Region GetRegion(int worldX, int worldZ)
        {
            if (GenerationUtilities.Cellular(worldX, worldZ, 0.6f, 1.4f) > 0.5f)
                return RegionReferences.Greenlands;
            else
                return RegionReferences.Desert;
        }
    }
}