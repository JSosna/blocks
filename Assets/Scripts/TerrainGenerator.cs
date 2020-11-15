using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    private int dimensions = 70;

    [SerializeField]
    private GameObject blockPrefab;

    private BlockSystem blockSystem;

    void Start()
    {
        blockSystem = GetComponent<BlockSystem>();
        generatePlane();
    }

    void generatePlane()
    {
        for(int x = 0; x < dimensions; x++)
        {
            for(int z = 0; z < dimensions; z++)
            {
                int y = Random.Range(0, 3);
                placeBlock(x - dimensions / 2, y, z - dimensions / 2, 0);
                placeBlock(x - dimensions / 2, y + 1, z - dimensions / 2, 0);
                placeBlock(x - dimensions / 2, y + 1, z - dimensions / 2, 1);   // top layer - "grass"
            }
        }
    }

    void placeBlock(int x, int y, int z, int type)
    {
        GameObject newBlock = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity);
        Block tempBlock = blockSystem.allBlocks[type];
        newBlock.name = tempBlock.blockName + "-Block";
        newBlock.GetComponent<MeshRenderer>().material = tempBlock.blockMaterial;
    }
}
