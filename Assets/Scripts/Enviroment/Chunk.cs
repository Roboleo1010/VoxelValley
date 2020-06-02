using UnityEngine;
using VoxelValley;
using VoxelValley.Enviroment;
using VoxelValley.Engine.Threading;

public class Chunk : MonoBehaviour
{
    public bool HasGenerated = false;
    public bool IsFinished = false;

    public ushort[,,] voxels = new ushort[CommonConstants.World.ChunkSizeXZ, CommonConstants.World.ChunkSizeY, CommonConstants.World.ChunkSizeXZ];

    Vector2Int positionInWorldSpace;
    Vector2Int positionInChunkSpace;

    VoxelMesh voxelMesh;

    public void Create(Vector2Int posInChunkSpace)
    {
        positionInChunkSpace = posInChunkSpace;
        positionInWorldSpace = World.Instance.ConvertFromChunkSpaceToWorldSpace(posInChunkSpace);

        transform.position = new Vector3(positionInWorldSpace.x, 0, positionInWorldSpace.y);

        ThreadManager.Instance.CreateThread(() => { Generate(); PrepareMesh(); }, () => { HasGenerated = true; }, $"Chunk_{positionInChunkSpace}", System.Threading.ThreadPriority.AboveNormal);
    }

    private void Update()
    {
        if (HasGenerated && !IsFinished)
            SetRenderer();
    }

    void Generate()
    {
        // voxels = RegionManager.GetChunk(positionInWorldSpace.X, positionInWorldSpace.Z);

        for (int x = 0; x < voxels.GetLength(0); x++)
            for (int y = 0; y < voxels.GetLength(1); y++)
                for (int z = 0; z < voxels.GetLength(2); z++)
                    voxels[x, y, z] = 2;
    }

    void PrepareMesh()
    {
        voxelMesh = new VoxelMesh(ref voxels);
        voxelMesh.Create();
    }

    void SetRenderer()
    {
        Mesh mesh = new Mesh();

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = voxelMesh.Vertices.ToArray();
        mesh.triangles = voxelMesh.Indices.ToArray();
        mesh.colors32 = voxelMesh.Colors.ToArray();
        mesh.normals = voxelMesh.Normals.ToArray();
        mesh.RecalculateBounds();

        voxelMesh = null;

        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>();

        IsFinished = true;
    }
}