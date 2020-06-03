using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VoxelValley.Enviroment.Structures
{
    public class Structure
    {
        public string Name { get; private set; }
        public Vector3Int Origin { get; private set; }
        public Vector3Int Dimension { get; private set; }
        public ushort[,,] Voxels { get; private set; }
        public Spawn[] Spawns { get; set; }


        [JsonConstructor]
        public Structure(string name, Voxel[] voxels, Spawn[] spawns)
        {
            Name = name;

            int minX = int.MaxValue;
            int maxX = int.MinValue;

            int minY = int.MaxValue;
            int maxY = int.MinValue;

            int minZ = int.MaxValue;
            int maxZ = int.MinValue;

            foreach (Voxel voxel in voxels)
            {
                if (voxel.position.x > maxX)
                    maxX = voxel.position.x;
                if (voxel.position.x < minX)
                    minX = voxel.position.x;

                if (voxel.position.y > maxY)
                    maxY = voxel.position.y;
                if (voxel.position.y < minY)
                    minY = voxel.position.y;

                if (voxel.position.z > maxZ)
                    maxZ = voxel.position.z;
                if (voxel.position.z < minZ)
                    minZ = voxel.position.z;
            }

            minX = Math.Abs(minX);
            maxX = Math.Abs(maxX);

            minY = Math.Abs(minY);
            maxY = Math.Abs(maxY);

            minZ = Math.Abs(minZ);
            maxZ = Math.Abs(maxZ);

            Dimension = new Vector3Int(minX + maxX + 1, minY + maxY + 1, minZ + maxZ + 1);

            Voxels = new ushort[Dimension.x, Dimension.y, Dimension.z];

            Origin = new Vector3Int(minX, minY, minZ);

            foreach (Voxel voxel in voxels)
                Voxels[voxel.position.x + minX, voxel.position.y + minY, voxel.position.z + minZ] = voxel.voxel;

            Spawns = spawns;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}