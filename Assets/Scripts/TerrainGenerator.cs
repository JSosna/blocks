using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField]
    private float power = 8;
    [SerializeField]
    private float density = .1f;

    public GameObject terrainChunk;
    public Transform player;

    Dictionary<Vector2Int, TerrainChunk> chunks = new Dictionary<Vector2Int, TerrainChunk>();


    void Start()
    {
        CreateChunk(-5, -5);
    }


    void CreateChunk(int chunkX, int chunkZ)
    {
        GameObject chunk = Instantiate(this.terrainChunk, new Vector3(chunkX, 0, chunkZ), Quaternion.identity);
        TerrainChunk terrainChunk = chunk.GetComponent<TerrainChunk>();

        LoadBlocks(chunkX, chunkZ, terrainChunk);
        terrainChunk.GenerateMesh();

        chunks.Add(new Vector2Int(chunkX, chunkZ), terrainChunk);
    }

    private void LoadBlocks(int chunkX, int chunkZ, TerrainChunk terrainChunk)
    {
        for (int x = 0; x < TerrainChunk.chunkWidth + 2; x++)
            for (int z = 0; z < TerrainChunk.chunkWidth + 2; z++)
                for (int y = 0; y < TerrainChunk.chunkHeight; y++)
                    if (GetBlock(chunkX, chunkZ, x, z, y))
                        // 1 - there isn't any block, 0 - there is a block
                        terrainChunk.blocks[x, y, z] = 1;
    }

    private bool GetBlock(int chunkX, int chunkZ, int x, int z, int y)
    {
        double perlinValue = Mathf.PerlinNoise((chunkX + x - 1) * density, (chunkZ + z - 1) * density) * power + y;

        return perlinValue < TerrainChunk.chunkHeight / 3;
    }
}