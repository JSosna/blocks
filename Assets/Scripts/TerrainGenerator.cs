using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField]
    private float strength = 50;
    [SerializeField]
    private float frequency = 1.5f;
    [SerializeField]
    private int viewDistance = 8;

    public GameObject terrainChunk;
    public Transform player;

    private Vector2Int currentChunkPos;
    private Vector2Int lastChunkPos = new Vector2Int(0, 0);

    private Vector2Int currentChunkHalfPos;
    private Vector2Int lastChunkHalpPos = new Vector2Int(0, 0);

    public static Dictionary<Vector2Int, TerrainChunk> chunks;

    FastNoise fastNoise = new FastNoise();

    private float noise;
    private float noise2;
    private float noise3;

    private List<Vector2Int> chunksToLoad = new List<Vector2Int>();
    private int InitialChunksToLoadCount;
    public static bool InitialMapLoaded { get; set; } = false;

    public GameObject ProgressBar;
    public Slider ProgressBarSlider;
    


    private void Start()
    {
        chunks = new Dictionary<Vector2Int, TerrainChunk>();
        viewDistance = CrossSceneData.ViewDistance;
        frequency = CrossSceneData.Frequency;
        strength = CrossSceneData.Strength;
    }

    void Update()
    {
        if(chunksToLoad.Count != 0)
        {
            CreateChunk(chunksToLoad[chunksToLoad.Count - 1].x, chunksToLoad[chunksToLoad.Count - 1].y);
            chunksToLoad.RemoveAt(chunksToLoad.Count - 1);

            if(!InitialMapLoaded)
            {
                ProgressBarSlider.value = 1 - (chunksToLoad.Count / (float)InitialChunksToLoadCount);


                if (chunksToLoad.Count == 0)
                {
                    ProgressBar.SetActive(false);
                    InitialMapLoaded = true;
                }
            }
        }
        else
        {
            LoadChunks();
        }
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
                    if (GetBlock(chunkX, chunkZ, x, z, y - 15))
                    {
                        // on the bottom of the map create sth like bedrock (not destroyable)
                        if (y == 0)
                            terrainChunk.blocks[x, y, z] = BlockType.WoolBlack;
                        else if (y < Random.Range(20, 25))
                        {
                            terrainChunk.blocks[x, y, z] = BlockType.Stone;

                            if(Random.Range(0, 100) < 1)
                            {
                                terrainChunk.blocks[x, y, z] = BlockType.DiamondOre;
                            }
                        }
                        else if (!GetBlock(chunkX, chunkZ, x, z, y - 14))
                        {
                            if(y < 40)
                            {
                                terrainChunk.blocks[x, y, z] = BlockType.Sand;
                                terrainChunk.blocks[x, y - 1, z] = BlockType.Sand;
                                terrainChunk.blocks[x, y - 2, z] = BlockType.Sand;
                            }
                                
                            else if (y > 60)
                                terrainChunk.blocks[x, y, z] = BlockType.GrassSnow;
                            else
                                terrainChunk.blocks[x, y, z] = BlockType.Grass;

                            if (Random.Range(0, 200) < 1) {
                                if(y >= 40)
                                GenerateTree(terrainChunk, x, y, z);
                            }
                        }

                        else
                            terrainChunk.blocks[x, y, z] = BlockType.Dirt;
                    }
    }

    private void GenerateTree(TerrainChunk terrainChunk, int x, int y, int z)
    {
        if (x + 4 > terrainChunk.blocks.GetLength(0) ||
            x - 4 < 0 ||
            z + 4 > terrainChunk.blocks.GetLength(2) ||
            z - 4 < 0) return;

        int height = Random.Range(5, 8);

        // Base of the tree
        for(int i = 1; i < height; i++)
        {
            terrainChunk.blocks[x, y + i, z] = BlockType.WoodLog;
        }

        // Leaves part
        for(int layer = 0; layer < 4; layer++)
        {
            if (layer == 3)
            {
                for (int a = 0; a < 3; a++)
                    for (int b = 0; b < 3; b++)
                        terrainChunk.blocks[x + a - 1, y + height + layer, z + b - 1] = BlockType.Leaves;
            }
            else
            {
                for (int a = 0; a < 7; a++)
                    for (int b = 0; b < 7; b++)
                        if ((3 - a) * (3 - a) + (3 - b) * (3 - b) < 8)
                            terrainChunk.blocks[x + a - 3, y + height + layer, z + b - 3] = BlockType.Leaves;
            }
        }

        terrainChunk.blocks[x, y + height + 1, z] = BlockType.WoodLog;
        terrainChunk.blocks[x, y + height + 2, z] = BlockType.WoodLog;

    }

    private bool GetBlock(int chunkX, int chunkZ, int x, int z, int y)
    {
        noise = fastNoise.GetPerlin((chunkX + x - 1) * frequency, (chunkZ + z - 1) * frequency) * strength + y;
        noise2 = .5f * fastNoise.GetPerlin((chunkX + x - 1) * frequency * 2, (chunkZ + z - 1) * frequency * 2) * strength + y;
        noise3 = .25f * fastNoise.GetPerlin((chunkX + x - 1) * frequency * 4, (chunkZ + z - 1) * frequency * 4) * strength + y;

        return noise + noise2 + noise3 < TerrainChunk.chunkHeight;
    }

    void LoadChunks()
    {
        // Divided loading and destroying chunks into two parts for better performance. New chunks load when you enter new one, and old chunks are destroyed when you are half way in this new chunk
        currentChunkPos.x = Mathf.FloorToInt(player.position.x / TerrainChunk.chunkWidth) * TerrainChunk.chunkWidth;
        currentChunkPos.y = Mathf.FloorToInt(player.position.z / TerrainChunk.chunkWidth) * TerrainChunk.chunkWidth;

        currentChunkHalfPos.x = Mathf.FloorToInt((player.position.x - 8) / TerrainChunk.chunkWidth) * TerrainChunk.chunkWidth;
        currentChunkHalfPos.y = Mathf.FloorToInt((player.position.z - 8) / TerrainChunk.chunkWidth) * TerrainChunk.chunkWidth;

        
        if (currentChunkPos != lastChunkPos)
        {
            lastChunkPos = currentChunkPos;

            for (int x = currentChunkPos.x - TerrainChunk.chunkWidth * viewDistance; x <= currentChunkPos.x + TerrainChunk.chunkWidth * viewDistance; x += TerrainChunk.chunkWidth)
                for (int z = currentChunkPos.y - TerrainChunk.chunkWidth * viewDistance; z <= currentChunkPos.y + TerrainChunk.chunkWidth * viewDistance; z += TerrainChunk.chunkWidth)
                    if (!chunks.ContainsKey(new Vector2Int(x, z)))
                        // (Mathf.Pow is slower)
                        if ((x - currentChunkPos.x) * (x - currentChunkPos.x) + (z - currentChunkPos.y) * (z - currentChunkPos.y) <= viewDistance * TerrainChunk.chunkWidth * viewDistance * TerrainChunk.chunkWidth)
                            chunksToLoad.Add(new Vector2Int(x, z)); //CreateChunk(x, z);

            if (!InitialMapLoaded)
                InitialChunksToLoadCount = chunksToLoad.Count;
        }


        if (currentChunkHalfPos != lastChunkHalpPos)
        {
            lastChunkHalpPos = currentChunkHalfPos;

            List<Vector2Int> toDestroy = new List<Vector2Int>();

            foreach (KeyValuePair<Vector2Int, TerrainChunk> c in chunks)
            {
                Vector2Int cp = c.Key;
                // Destroy chunks outside of the circle
                if ((cp.x - currentChunkPos.x) * (cp.x - currentChunkPos.x) + (cp.y - currentChunkPos.y) * (cp.y - currentChunkPos.y) >= (viewDistance + 1) * TerrainChunk.chunkWidth * (viewDistance + 1) * TerrainChunk.chunkWidth)
                    toDestroy.Add(c.Key);
            }

            foreach (Vector2Int cp in toDestroy)
            {
                Destroy(chunks[cp].gameObject);
                chunks.Remove(cp);
            }
        }
    }
}