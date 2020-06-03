using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelValley.Enviroment;
using VoxelValley.Enviroment.Structures;

public class GameManager : MonoBehaviour
{

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

    }
}
