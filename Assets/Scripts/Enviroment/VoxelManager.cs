using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;
using Newtonsoft.Json;
using UnityEngine;
using VoxelValley.Helper;

namespace VoxelValley.Enviroment
{
    public static class VoxelManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VoxelManager));

        static Type type = typeof(VoxelManager);
        static Dictionary<ushort, Voxel> voxels = new Dictionary<ushort, Voxel>();
        static Dictionary<string, Voxel> voxelLookUp = new Dictionary<string, Voxel>();

        static ushort voxelIndex;
        public static ushort AirVoxel;

        public static void LoadVoxels()
        {
            //Add Air Voxel
            Voxel airVoxel = new Voxel("air", new int[] { 0, 0, 0 });

            voxels.Add(0, airVoxel);
            voxelLookUp.Add("air", airVoxel);
            voxelIndex = 1;

            string[] paths = FileHelper.GetAllFilesOfType($"{Application.streamingAssetsPath}/VoxelTypes/", "*.json");

            foreach (string path in paths)
                LoadVoxelJson(path);

            log.Info($"Loaded {voxels.Count} voxels");
        }

        static void LoadVoxelJson(string path)
        {
            List<Voxel> voxels = new List<Voxel>();

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                voxels = JsonConvert.DeserializeObject<List<Voxel>>(reader.ReadToEnd());
            }

            foreach (Voxel voxel in voxels)
            {
                voxel.Id = voxelIndex;

                VoxelManager.voxels.Add(voxel.Id, voxel);
                VoxelManager.voxelLookUp.Add(voxel.Name, voxel);

                voxelIndex++;
            }
        }

        public static Voxel GetVoxel(string name)
        {
            if (voxelLookUp.TryGetValue(name, out Voxel voxelType))
                return voxelType;

            return GetVoxel("missing_voxel");
        }

        public static Voxel GetVoxel(ushort id)
        {
            if (voxels.TryGetValue(id, out Voxel voxelType))
                return voxelType;

            return GetVoxel("missing_voxel");
        }
    }
}