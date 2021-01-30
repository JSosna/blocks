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

    private List<Vector2Int> chunksToLoad = new List<Vector2Int>();
    private int InitialChunksToLoadCount;
    public static bool InitialMapLoaded { get; set; } = false;

    public GameObject ProgressBar;
    public Slider ProgressBarSlider;

    private bool updateChunks = true;


    private void Start()
    {
        chunks = new Dictionary<Vector2Int, TerrainChunk>();
        viewDistance = CrossSceneData.ViewDistance;
        frequency = CrossSceneData.Frequency;
        strength = CrossSceneData.Strength;
    }

    void Update()
    {
        // If chunks update every second frame game works smoother
        updateChunks = !updateChunks;
        if(updateChunks)
        {
            if (chunksToLoad.Count != 0)
            {
                CreateChunk(chunksToLoad[chunksToLoad.Count - 1].x, chunksToLoad[chunksToLoad.Count - 1].y);
                chunksToLoad.RemoveAt(chunksToLoad.Count - 1);

                if (!InitialMapLoaded)
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
                    SetBlock(terrainChunk, chunkX, chunkZ, x, z, y, 15);
    }


    private void SetBlock(TerrainChunk terrainChunk, int chunkX, int chunkZ, int x, int z, int y, int offsetY)
    {

        float heightMap = GetHeightMap(chunkX, chunkZ, x, z, y - offsetY);
        //return noise + noise2 + noise3 < TerrainChunk.chunkHeight;


        if (heightMap < TerrainChunk.chunkHeight)
        {
            // on the bottom of the map create sth like bedrock (not destroyable)
            if (y == 0)
            {
                terrainChunk.blocks[x, y, z] = BlockType.WoolBlack;
                return;
            }

            /*float caveNoise1 = fastNoise.GetPerlinFractal(x * 5f, y * 10f, z * 5f);
            float caveMask = fastNoise.GetSimplex(x * .3f, z * .3f) + .3f;
            if (caveNoise1 > Mathf.Max(caveMask, .2f))
            {
                terrainChunk.blocks[x, y, z] = BlockType.Air;
                return;
            }*/
            else if (y < Random.Range(25, 30))
            {
                terrainChunk.blocks[x, y, z] = BlockType.Stone;

                if (Random.Range(0, 50) < 1)
                {
                    var random = Random.Range(0, 20);

                    if (random < 1 && y < 18)
                        GenerateOre(terrainChunk, x, y, z, BlockType.DiamondOre, 2);
                    else if (random < 8 && y < 28)
                        GenerateOre(terrainChunk, x, y, z, BlockType.IronOre, 3);
                    else
                        GenerateOre(terrainChunk, x, y, z, BlockType.CoalOre, 3);
                }
                return;
            }
            // if there is no block above current (place grass, sand, trees, snow)
            else if (!(GetHeightMap(chunkX, chunkZ, x, z, y - offsetY + 1) < TerrainChunk.chunkHeight))
            {
                if (y < 40)
                {
                    terrainChunk.blocks[x, y, z] = BlockType.Sand;
                    terrainChunk.blocks[x, y - 1, z] = BlockType.Sand;
                    terrainChunk.blocks[x, y - 2, z] = BlockType.Sand;
                    return;
                }

                else if (y > 60)
                    terrainChunk.blocks[x, y, z] = BlockType.GrassSnow;
                else
                    terrainChunk.blocks[x, y, z] = BlockType.Grass;

                if (Random.Range(0, 200) < 1)
                {
                    if (y >= 40)
                        GenerateTree(terrainChunk, x, y, z);
                }
            }
            else
                terrainChunk.blocks[x, y, z] = BlockType.Dirt;
        }
    }

    private static void GenerateOre(TerrainChunk terrainChunk, int x, int y, int z, BlockType oreType, int size)
    {
        for (int i = 0; i < Random.Range(1, size + 1); i++)
            for (int j = 0; j < Random.Range(1, size + 1); j++)
                for (int k = 0; k < Random.Range(1, size + 1); k++)
                {
                    if(x - i > 0 &&
                       z - k > 0 &&
                       y - j > 0)
                    {
                        terrainChunk.blocks[x - i, y - j, z - k] = oreType;
                    }
                }
    }

    private float GetHeightMap(int chunkX, int chunkZ, int x, int z, int y)
    {
        float noise = fastNoise.GetPerlin((chunkX + x - 1) * frequency, (chunkZ + z - 1) * frequency) * strength + y;
        float noise2 = .5f * fastNoise.GetPerlin((chunkX + x - 1) * frequency * 2, (chunkZ + z - 1) * frequency * 2) * strength + y;
        float noise3 = .25f * fastNoise.GetPerlin((chunkX + x - 1) * frequency * 4, (chunkZ + z - 1) * frequency * 4) * strength + y;

        return noise + noise2 + noise3;
    }



    private void GenerateTree(TerrainChunk terrainChunk, int x, int y, int z)
    {
        if (x + 4 > terrainChunk.blocks.GetLength(0) ||
            x - 4 < 0 ||
            z + 4 > terrainChunk.blocks.GetLength(2) ||
            z - 4 < 0) return;

        int height = Random.Range(5, 8);

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

        // Base of the tree
        for(int i = 1; i <= height; i++)
        {
            terrainChunk.blocks[x, y + i, z] = BlockType.Wood;
        }

        terrainChunk.blocks[x, y + height + 1, z] = BlockType.Wood;
        terrainChunk.blocks[x, y + height + 2, z] = BlockType.Wood;
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