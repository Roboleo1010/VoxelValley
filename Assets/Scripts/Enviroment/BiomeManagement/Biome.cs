using System.Collections.Generic;
using UnityEngine;
using VoxelValley.Enviroment.Generation;
using VoxelValley.Enviroment.Structures;

namespace VoxelValley.Enviroment.BiomeManagement
{
    public abstract class Biome
    {
        public abstract string Name { get; }
        public abstract Color Color { get; }
        public abstract byte BiomeId { get; }

        protected List<StructureSpawn> structureSpawns;

        public Biome()
        {
            structureSpawns = StructureManager.GetStructures(Name);
        }

        public abstract ushort GetHeight(int x, int z);
        internal abstract ushort GetVoxel(int x, int y, int z, ushort height);
        internal virtual void GetFinishers(int worldX, int worldZ, ushort chunkX, ushort chunkZ, int height, ref ushort[,,] voxels)
        {
            if (structureSpawns != null)
                foreach (StructureSpawn structureSpawn in structureSpawns)
                    if (GenerationUtilities.White(worldX, worldZ, 1, 1, structureSpawn.NoiseOffset.x, structureSpawn.NoiseOffset.y) < structureSpawn.Chance)
                    {
                        for (ushort x = 0; x < structureSpawn.Structure.Dimension.x; x++)
                            for (ushort y = 0; y < structureSpawn.Structure.Dimension.y; y++)
                                for (ushort z = 0; z < structureSpawn.Structure.Dimension.z; z++)
                                    if (structureSpawn.Structure.Voxels[x, y, z] != 0 && Chunk.InChunk(chunkX + x + structureSpawn.Structure.Origin.x, height + y + structureSpawn.Structure.Origin.y, chunkZ + z + structureSpawn.Structure.Origin.z))
                                        voxels[chunkX + x + structureSpawn.Structure.Origin.x, height + y + structureSpawn.Structure.Origin.y, chunkZ + z + structureSpawn.Structure.Origin.z] = structureSpawn.Structure.Voxels[x, y, z];
                    }
        }
    }
}