using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField]
    private int dimensions = 70;

    [SerializeField]
    private float power = 8;

    [SerializeField]
    private float density = 1;

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
        for(float x = 0; x < dimensions/10; x+=.1f)
        {
            for(float z = 0; z < dimensions/10; z+=.1f)
            {
                float y = Mathf.PerlinNoise(x*density, z*density) * power;
                Debug.Log((x) + " " + (z) + " = " +y);
                placeBlock((int)(x * 10) - dimensions / 2, (int)y, (int)(z * 10) - dimensions / 2, 0);
                placeBlock((int)(x * 10) - dimensions / 2, (int)y + 1, (int)(z * 10) - dimensions / 2, 0);
                placeBlock((int)(x * 10) - dimensions / 2, (int)y + 2, (int)(z * 10) - dimensions / 2, 1);   // top layer - "grass"
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
