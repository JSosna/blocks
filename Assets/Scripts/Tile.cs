using System.Collections.Generic;
using UnityEngine;


public enum TileType { Dirt, Grass, GrassSide, Stone, Wood, WoolWhite, WoolRed, WoolBlue, WoolGreen, WoolPink, WoolBlack }


public class Tile
{
    public Vector2[] uvs { get; }

    public Tile(int x, int y)
    {
        uvs = new Vector2[4];
        uvs[0] = new Vector2(x / 16f + .001f, y / 16f + .001f);
        uvs[1] = new Vector2(x / 16f + .001f, (y + 1) / 16f - .001f);
        uvs[2] = new Vector2((x + 1) / 16f - .001f, (y + 1) / 16f - .001f);
        uvs[3] = new Vector2((x + 1) / 16f - .001f, y / 16f + .001f);
    }

    public static Dictionary<TileType, Tile> tiles = new Dictionary<TileType, Tile>()
    {
        {TileType.Grass, new Tile(0, 0)},
        {TileType.GrassSide, new Tile(0, 1)},
        {TileType.Dirt, new Tile(0, 2)},
        {TileType.Stone, new Tile(1, 0)},
        {TileType.Wood, new Tile(2, 0)},
        {TileType.WoolWhite, new Tile(3, 0)},
        {TileType.WoolRed, new Tile(4, 0)},
        {TileType.WoolBlue, new Tile(5, 0)},
        {TileType.WoolGreen, new Tile(6, 0)},
        {TileType.WoolPink, new Tile(7, 0)},
        {TileType.WoolBlack, new Tile(8, 0)},
    };
}