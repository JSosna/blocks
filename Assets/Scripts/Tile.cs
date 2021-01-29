using System.Collections.Generic;
using UnityEngine;


public enum TileType { 
    Dirt,
    DirtD1,
    DirtD2,
    DirtD3,

    Grass,
    GrassD1,
    GrassD2,
    GrassD3,

    GrassSide,
    GrassSideD1,
    GrassSideD2,
    GrassSideD3,
    
    Stone,
    StoneD1,
    StoneD2,
    StoneD3,

    Wood,
    WoodD1,
    WoodD2,
    WoodD3,

    WoolWhite,
    WoolWhiteD1,
    WoolWhiteD2,
    WoolWhiteD3,

    WoolBlack,
    WoolBlackD1,
    WoolBlackD2,
    WoolBlackD3,

    DiamondOre,
    DiamondOreD1,
    DiamondOreD2,
    DiamondOreD3,

    CoalOre,
    CoalOreD1,
    CoalOreD2,
    CoalOreD3,

    IronOre,
    IronOreD1,
    IronOreD2,
    IronOreD3,

    WoodLogTop,
    WoodLogTopD1,
    WoodLogTopD2,
    WoodLogTopD3,

    WoodLogSide,
    WoodLogSideD1,
    WoodLogSideD2,
    WoodLogSideD3,

    Leaves,

    GrassSnow,
    GrassSnowD1,
    GrassSnowD2,
    GrassSnowD3,

    GrassSnowSide,
    GrassSnowSideD1,
    GrassSnowSideD2,
    GrassSnowSideD3,

    Sand,
    SandD1,
    SandD2,
    SandD3,
}


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
        {TileType.GrassD1, new Tile(0, 1)},
        {TileType.GrassD2, new Tile(0, 2)},
        {TileType.GrassD3, new Tile(0, 3)},

        {TileType.GrassSide, new Tile(0, 4)},
        {TileType.GrassSideD1, new Tile(0, 5)},
        {TileType.GrassSideD2, new Tile(0, 6)},
        {TileType.GrassSideD3, new Tile(0, 7)},

        {TileType.Dirt, new Tile(0, 8)},
        {TileType.DirtD1, new Tile(0, 9)},
        {TileType.DirtD2, new Tile(0, 10)},
        {TileType.DirtD3, new Tile(0, 11)},

        {TileType.Stone, new Tile(1, 0)},
        {TileType.StoneD1, new Tile(1, 1)},
        {TileType.StoneD2, new Tile(1, 2)},
        {TileType.StoneD3, new Tile(1, 3)},

        {TileType.Wood, new Tile(2, 0)},
        {TileType.WoodD1, new Tile(2, 1)},
        {TileType.WoodD2, new Tile(2, 2)},
        {TileType.WoodD3, new Tile(2, 3)},

        {TileType.WoolWhite, new Tile(3, 0)},
        
        {TileType.WoolBlack, new Tile(4, 0)},
        
        {TileType.DiamondOre, new Tile(5, 0)},
        {TileType.DiamondOreD1, new Tile(5, 1)},
        {TileType.DiamondOreD2, new Tile(5, 2)},
        {TileType.DiamondOreD3, new Tile(5, 3)},

        {TileType.CoalOre, new Tile(6, 0)},
        {TileType.CoalOreD1, new Tile(6, 1)},
        {TileType.CoalOreD2, new Tile(6, 2)},
        {TileType.CoalOreD3, new Tile(6, 3)},

        {TileType.IronOre, new Tile(7, 0)},
        {TileType.IronOreD1, new Tile(7, 1)},
        {TileType.IronOreD2, new Tile(7, 2)},
        {TileType.IronOreD3, new Tile(7, 3)},

        {TileType.WoodLogTop, new Tile(10, 0)},
        {TileType.WoodLogTopD1, new Tile(10, 1)},
        {TileType.WoodLogTopD2, new Tile(10, 2)},
        {TileType.WoodLogTopD3, new Tile(10, 3)},

        {TileType.WoodLogSide, new Tile(10, 4)},
        {TileType.WoodLogSideD1, new Tile(10, 5)},
        {TileType.WoodLogSideD2, new Tile(10, 6)},
        {TileType.WoodLogSideD3, new Tile(10, 7)},

        {TileType.Leaves, new Tile(11, 0)},
        
        {TileType.GrassSnow, new Tile(0, 12)},
        {TileType.GrassSnowD1, new Tile(0, 13)},
        {TileType.GrassSnowD2, new Tile(0, 14)},
        {TileType.GrassSnowD3, new Tile(0, 15)},

        {TileType.GrassSnowSide, new Tile(1, 12)},
        {TileType.GrassSnowSideD1, new Tile(1, 13)},
        {TileType.GrassSnowSideD2, new Tile(1, 14)},
        {TileType.GrassSnowSideD3, new Tile(1, 15)},

        {TileType.Sand, new Tile(12, 0)},
        {TileType.SandD1, new Tile(12, 1)},
        {TileType.SandD2, new Tile(12, 2)},
        {TileType.SandD3, new Tile(12, 3)},
    };
}