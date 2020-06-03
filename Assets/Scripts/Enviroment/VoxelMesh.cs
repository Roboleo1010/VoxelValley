using System.Collections.Generic;
using UnityEngine;

namespace VoxelValley.Enviroment
{
    public class VoxelMesh
    {
        public List<Vector3> Vertices = new List<Vector3>();
        public List<Color32> Colors = new List<Color32>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<int> Indices = new List<int>();

        ushort[,,] voxels;

        public VoxelMesh(ref ushort[,,] voxels)
        {
            this.voxels = voxels;
        }

        public void Create()
        {
            for (int x = 0; x < voxels.GetLength(0); x++)
                for (int y = 0; y < voxels.GetLength(1); y++)
                    for (int z = 0; z < voxels.GetLength(2); z++)
                        if (voxels[x, y, z] != VoxelManager.AirVoxel)
                            GetMeshData(x, y, z);
        }

        void GetMeshData(int x, int y, int z)
        {
            int addedVertices = 0;
            int indiceOffset = Vertices.Count;

            //Left
            if (IsSolid(x, y, z - 1))
            {
                Vertices.Add(new Vector3(x + 0, y + 0, z + 0));
                Vertices.Add(new Vector3(x + 1, y + 1, z + 0));
                Vertices.Add(new Vector3(x + 1, y + 0, z + 0));
                Vertices.Add(new Vector3(x + 0, y + 1, z + 0));

                Indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 3, indiceOffset + 1 });
                Normals.AddRange(new Vector3[] { -Vector3.forward, -Vector3.forward, -Vector3.forward, -Vector3.forward });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Right
            if (IsSolid(x, y, z + 1))
            {
                Vertices.Add(new Vector3(x + 0, y + 0, z + 1));
                Vertices.Add(new Vector3(x + 1, y + 0, z + 1));
                Vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                Vertices.Add(new Vector3(x + 0, y + 1, z + 1));

                Indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 2, indiceOffset + 3 });
                Normals.AddRange(new Vector3[] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Front
            if (IsSolid(x - 1, y, z))
            {
                Vertices.Add(new Vector3(x + 0, y + 0, z + 0));
                Vertices.Add(new Vector3(x + 0, y + 1, z + 1));
                Vertices.Add(new Vector3(x + 0, y + 1, z + 0));
                Vertices.Add(new Vector3(x + 0, y + 0, z + 1));

                Indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 3, indiceOffset + 1 });
                Normals.AddRange(new Vector3[] { -Vector3.right, -Vector3.right, -Vector3.right, -Vector3.right });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Back
            if (IsSolid(x + 1, y, z))
            {
                Vertices.Add(new Vector3(x + 1, y + 0, z + 0));
                Vertices.Add(new Vector3(x + 1, y + 1, z + 0));
                Vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                Vertices.Add(new Vector3(x + 1, y + 0, z + 1));

                Indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 2, indiceOffset + 3 });
                Normals.AddRange(new Vector3[] { Vector3.right, Vector3.right, Vector3.right, Vector3.right });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Bottom
            if (IsSolid(x, y - 1, z))
            {
                Vertices.Add(new Vector3(x + 0, y + 0, z + 0));
                Vertices.Add(new Vector3(x + 1, y + 0, z + 0));
                Vertices.Add(new Vector3(x + 1, y + 0, z + 1));
                Vertices.Add(new Vector3(x + 0, y + 0, z + 1));

                Indices.AddRange(new int[] { indiceOffset + 0, indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 2, indiceOffset + 3 });
                Normals.AddRange(new Vector3[] { -Vector3.up, -Vector3.up, -Vector3.up, -Vector3.up });

                addedVertices += 4;
                indiceOffset += 4;
            }
            //Top
            if (IsSolid(x, y + 1, z))
            {
                Vertices.Add(new Vector3(x + 1, y + 1, z + 0));
                Vertices.Add(new Vector3(x + 0, y + 1, z + 0));
                Vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                Vertices.Add(new Vector3(x + 0, y + 1, z + 1));

                Indices.AddRange(new int[] { indiceOffset + 1, indiceOffset + 2, indiceOffset + 0, indiceOffset + 1, indiceOffset + 3, indiceOffset + 2 });
                Normals.AddRange(new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up });

                addedVertices += 4;
                indiceOffset += 4;
            }

            if (addedVertices > 0)
            {
                Color32 voxelColor = VoxelManager.GetVoxel(voxels[x, y, z]).Color;
                for (int i = 0; i < addedVertices; i++)
                    Colors.Add(voxelColor);
            }
        }

        bool IsSolid(int x, int y, int z)
        {
            if (x < 0 || x >= voxels.GetLength(0) ||
                y < 0 || y >= voxels.GetLength(1) ||
                z < 0 || z >= voxels.GetLength(2))
                return true;

            return voxels[x, y, z] == VoxelManager.AirVoxel;
        }
    }
}