using log4net;
using UnityEngine;
using VoxelValley.Enviroment;
using VoxelValley.Enviroment.Structures;

public class GameManager : MonoBehaviour
{
    private static readonly ILog log = LogManager.GetLogger(typeof(GameManager));

    void Awake()
    {


        VoxelManager.LoadVoxels();
        StructureManager.LoadStructures();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        log.Info("Starting Voxel Valley!");
    }
}
