using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    public const int chunkWidth = 16;
    public const int chunkHeight = 100;

    public BlockType[,,] blocks = new BlockType[chunkWidth + 2, chunkHeight, chunkWidth + 2];
    private int[,,] damageLevel = new int[chunkWidth + 2, chunkHeight, chunkWidth + 2];

    private Dictionary<Tuple<int, int, int>, float> blocksToObserve = new Dictionary<Tuple<int, int, int>, float>();
    

    private float timeToRestore = 4f;


    private void Update()
    {
        if (blocksToObserve.Count == 0) return;

        try
        {
            foreach (var block in blocksToObserve)
            {
            
                    blocksToObserve[block.Key] -= 1f * Time.deltaTime;

                    // Reduce block damage
                    if (block.Value <= 0)
                    {
                        damageLevel[block.Key.Item1, block.Key.Item2, block.Key.Item3]--;
                        blocks[block.Key.Item1, block.Key.Item2, block.Key.Item3]--;
                        blocksToObserve[block.Key] = .2f;

                        // Remove block from list of observing blocks if damage is 0
                        if (damageLevel[block.Key.Item1, block.Key.Item2, block.Key.Item3] == 0)
                            blocksToObserve.Remove(block.Key);

                        GenerateMesh();
                    }
            
            }
        } catch { }
    }

    public BlockType? IncreaseBLockDestroyLevel(int x, int y, int z)
    {
        // if block don't have damage effect
        if(blocks[x, y, z] == BlockType.Leaves) // add glass
        {
            blocks[x, y, z] = BlockType.Air;
            return null;
        }

        var key = new Tuple<int, int, int>(x, y, z);

        // if block is already being observed
        if (blocksToObserve.ContainsKey(key))
        {
            damageLevel[x, y, z]++;
            blocks[x, y, z]++;
            blocksToObserve[key] = 2f;

            // Destroy block and add block to inventory
            if (damageLevel[x, y, z] == 4)
            {
                BlockType blockType = blocks[x, y, z] - 4;
                blocks[x, y, z] = BlockType.Air;
                damageLevel[x, y, z] = 0;
                blocksToObserve.Remove(key);

                return blockType;
            }
        }
        else // Add block to the list
        {
            blocksToObserve.Add(key, timeToRestore);
            damageLevel[x, y, z]++;
            blocks[x, y, z]++;
        }

        return null;
    }

    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int x = 1; x < chunkWidth+1; x++)
            for (int z = 1; z < chunkWidth+1; z++)
                for (int y = 0; y < chunkHeight; y++)
                    if (blocks[x, y, z] != BlockType.Air)
                    {
                        Vector3 blockPos = new Vector3(x - 1, y, z - 1);
                        int faces = 0;

                        // top
                        if (y < chunkHeight - 1 && blocks[x, y + 1, z] == BlockType.Air || blocks[x, y + 1, z] == BlockType.Leaves)
                        {
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 0));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].topPos.uvs);
                        }

                        // bottom
                        if (y > 0 && (blocks[x, y - 1, z] == BlockType.Air || blocks[x, y - 1, z] == BlockType.Leaves))
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].bottomPos.uvs);
                        }

                        // front
                        if (blocks[x, y, z - 1] == BlockType.Air || z - 1 == 0 || blocks[x, y, z - 1] == BlockType.Leaves)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].sidePos.uvs);
                        }

                        // back
                        if (blocks[x, y, z + 1] == BlockType.Air || z == chunkWidth || blocks[x, y, z + 1] == BlockType.Leaves)
                        {
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            
                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].sidePos.uvs);
                        }

                        // left
                        if (blocks[x - 1, y, z] == BlockType.Air || x - 1 == 0 || blocks[x - 1, y, z] == BlockType.Leaves)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 0, 0));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].sidePos.uvs);
                        }

                        // right
                        if (blocks[x + 1, y, z] == BlockType.Air || x == chunkWidth || blocks[x + 1, y, z] == BlockType.Leaves)
                        {
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 0, 1));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].sidePos.uvs);
                        }


                        int tl = verts.Count - 4 * faces;

                        for (int i = 0; i < faces; i++)
                            tris.AddRange(new int[] { tl + i * 4, tl + i * 4 + 1, tl + i * 4 + 2, tl + i * 4, tl + i * 4 + 2, tl + i * 4 + 3 });
                    }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
