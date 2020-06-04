using log4net;
using UnityEngine;
using VoxelValley.Enviroment;
using VoxelValley.Enviroment.Structures;

public class GameManager : MonoBehaviour
{
    private static readonly ILog log = LogManager.GetLogger(typeof(GameManager));

    void Awake()
    {
        log.Info("Starting Voxel Valley!");

        VoxelManager.LoadVoxels();
        StructureManager.LoadStructures();
    }
}
