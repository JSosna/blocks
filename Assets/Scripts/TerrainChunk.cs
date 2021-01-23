using System.Collections.Generic;
using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    public const int chunkWidth = 16;
    public const int chunkHeight = 40;

    public BlockType[,,] blocks = new BlockType[chunkWidth + 2, chunkHeight, chunkWidth + 2];


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
                        if (y < chunkHeight - 1 && blocks[x, y + 1, z] == BlockType.Air)
                        {
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 0));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].topPos.uvs);
                        }

                        // bottom
                        if (y > 0 && blocks[x, y - 1, z] == BlockType.Air)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].bottomPos.uvs);
                        }

                        // front
                        if (blocks[x, y, z - 1] == BlockType.Air)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].sidePos.uvs);
                        }

                        // back
                        if (blocks[x, y, z + 1] == BlockType.Air)
                        {
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            
                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].sidePos.uvs);
                        }

                        // left
                        if (blocks[x - 1, y, z] == BlockType.Air)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 0, 0));

                            faces++;
                            uvs.AddRange(Block.blocks[blocks[x, y, z]].sidePos.uvs);
                        }

                        // right
                        if (blocks[x + 1, y, z] == BlockType.Air)
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
