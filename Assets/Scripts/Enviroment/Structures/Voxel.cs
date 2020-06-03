using Newtonsoft.Json;
using UnityEngine;

namespace VoxelValley.Enviroment.Structures
{
    /// <summary>
    /// JSON Helper Class
    /// </summary>
    public class Voxel
    {
        public ushort voxel { get; private set; }
        public Vector3Int position { get; private set; }

        [JsonConstructor]
        public Voxel(string voxel, int[] position)
        {
            this.voxel = VoxelManager.GetVoxel(voxel).Id;
            this.position = new Vector3Int(position[0], position[1], position[2]);
        }
    }
}