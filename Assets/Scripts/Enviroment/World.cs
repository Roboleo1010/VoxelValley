using System;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using VoxelValley.Entity;

namespace VoxelValley.Enviroment
{
    public class World : MonoBehaviour
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(World));

        //Singleton
        private static World _instance;
        public static World Instance { get { return _instance; } }

        public Material voxelMaterial;

        Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        void Start()
        {
            CreateChunk(new Vector2Int(0, 0));
        }

        private void Update()
        {
            Vector2Int palyerPosInChukSpace = ConvertFromWorldSpaceToVoxelSpace(Player.Instance.gameObject.transform.position).chunk;
            CreateAround(palyerPosInChukSpace);
        }

        Chunk CreateChunk(Vector2Int positionInChunkSpace)
        {
            Chunk chunk = GetChunk(positionInChunkSpace);
            if (chunk == null)
            {
                GameObject chunkGO = new GameObject($"Chunk {positionInChunkSpace.x} {positionInChunkSpace.y}");
                chunkGO.transform.parent = transform;
                chunk = chunkGO.AddComponent<Chunk>();
                chunk.Create(positionInChunkSpace);
                chunks.Add(positionInChunkSpace, chunk);
            }
            return chunk;
        }

        private void CreateAround(Vector2Int position)
        {
            for (int x = -CommonConstants.World.DrawDistance; x < CommonConstants.World.DrawDistance; x++)
                for (int y = -CommonConstants.World.DrawDistance; y < CommonConstants.World.DrawDistance; y++)
                    CreateChunk(new Vector2Int(position.x + x, position.y + y));
        }

        public Chunk GetChunk(Vector2Int positionInChunkSpace)
        {
            if (chunks.TryGetValue(positionInChunkSpace, out Chunk chunk))
                return chunk;
            return null;
        }

        public ushort GetVoxelFromWoldSpace(Vector3 pos)
        {
            (Vector2Int chunk, Vector3Int voxel) convertedPos = ConvertFromWorldSpaceToVoxelSpace(pos);

            Chunk chunk = GetChunk(convertedPos.chunk);

            if (chunk == null || chunk.voxels == null)
                return 0;

            return chunk.voxels[convertedPos.voxel.x, convertedPos.voxel.y, convertedPos.voxel.z];
        }

        public Vector2Int ConvertFromChunkSpaceToWorldSpace(Vector2Int chunkSpacePos)
        {
            return new Vector2Int(
                        chunkSpacePos.x * CommonConstants.World.ChunkSizeXZ,
                        chunkSpacePos.y * CommonConstants.World.ChunkSizeXZ);
        }

        public (Vector2Int chunk, Vector3Int voxel) ConvertFromWorldSpaceToVoxelSpace(Vector3 worldSpacePos)
        {
            Vector2Int chunk = new Vector2Int(0, 0);
            Vector3Int voxel = new Vector3Int(0, (int)worldSpacePos.y, 0);

            if (worldSpacePos.x < 0)
            {
                chunk.x = (int)Math.Floor(worldSpacePos.x / CommonConstants.World.ChunkSizeXZ);
                voxel.x = (CommonConstants.World.ChunkSizeXZ - 1) - (((int)worldSpacePos.x % CommonConstants.World.ChunkSizeXZ) * -1);
            }
            else
            {
                chunk.x = (int)worldSpacePos.x / CommonConstants.World.ChunkSizeXZ;
                voxel.x = ((int)worldSpacePos.x % CommonConstants.World.ChunkSizeXZ);
            }

            if (worldSpacePos.z < 0)
            {
                chunk.y = (int)Math.Floor(worldSpacePos.z / CommonConstants.World.ChunkSizeXZ);
                voxel.z = (CommonConstants.World.ChunkSizeXZ - 1) - ((int)worldSpacePos.z % CommonConstants.World.ChunkSizeXZ * -1);
            }
            else
            {
                chunk.y = (int)worldSpacePos.z / CommonConstants.World.ChunkSizeXZ;
                voxel.z = (int)worldSpacePos.z % CommonConstants.World.ChunkSizeXZ;
            }

            return (chunk, voxel);
        }
    }
}