using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelValley.Enviroment
{
    public class World : MonoBehaviour
    {
        //Singleton
        private static World _instance;
        public static World Instance { get { return _instance; } }

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

        // Start is called before the first frame update
        void Start()
        {
            CreateChunk(new Vector2Int(0, 0));
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

        public Chunk GetChunk(Vector2Int positionInChunkSpace)
        {
            if (chunks.TryGetValue(positionInChunkSpace, out Chunk chunk))
                return chunk;
            return null;
        }

        public Vector2Int ConvertFromChunkSpaceToWorldSpace(Vector2Int chunkSpacePos)
        {
            return new Vector2Int(
                        chunkSpacePos.x * CommonConstants.World.ChunkSizeXZ,
                        chunkSpacePos.y * CommonConstants.World.ChunkSizeY);
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

            if (worldSpacePos.y < 0)
            {
                chunk.y = (int)Math.Floor(worldSpacePos.y / CommonConstants.World.ChunkSizeY);
                voxel.y = (CommonConstants.World.ChunkSizeY - 1) - ((int)worldSpacePos.y % CommonConstants.World.ChunkSizeY * -1);
            }
            else
            {
                chunk.y = (int)worldSpacePos.y / CommonConstants.World.ChunkSizeY;
                voxel.y = (int)worldSpacePos.y % CommonConstants.World.ChunkSizeY;
            }

            return (chunk, voxel);
        }
    }
}