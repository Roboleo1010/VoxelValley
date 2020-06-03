using UnityEngine;

namespace VoxelValley.Enviroment
{
    public class Voxel
    {
        public ushort Id;
        public string Name;
        public Color Color;

        public Voxel(string name, int[] color)
        {
            Name = name;
            Color = new Color((float)color[0] / 255f, (float)color[1] / 255f, (float)color[2] / 255f);
        }
    }
}