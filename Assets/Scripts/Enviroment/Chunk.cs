using UnityEngine;
using VoxelValley;
using VoxelValley.Enviroment;
using VoxelValley.Engine.Threading;
using VoxelValley.Enviroment.RegionManagement;

public class Chunk : MonoBehaviour
{
    public bool HasGenerated = false;
    public bool IsFinished = false;

    public ushort[,,] voxels;

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
        voxels = RegionManager.GetChunk(positionInWorldSpace.x, positionInWorldSpace.y);
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

    public static bool InChunk(int x, int y, int z)
    {
        return (x > 0 && y > 0 && z > 0 &&
            x < CommonConstants.World.ChunkSizeXZ &&
            y < CommonConstants.World.ChunkSizeY &&
            z < CommonConstants.World.ChunkSizeXZ);
    }
}