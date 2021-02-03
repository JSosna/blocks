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

    Plank,
    PlankD1,
    PlankD2,
    PlankD3,

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

    WoodTop,
    WoodTopD1,
    WoodTopD2,
    WoodTopD3,

    WoodSide,
    WoodSideD1,
    WoodSideD2,
    WoodSideD3,

    Leaves,
    Glass,

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

    FurnaceTopBottom,
    FurnaceTopBottomD1,
    FurnaceTopBottomD2,
    FurnaceTopBottomD3,

    FurnaceSide,
    FurnaceSideD1,
    FurnaceSideD2,
    FurnaceSideD3,

    TorchTop,
    TorchSide,
    TorchBottom
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

        {TileType.Plank, new Tile(2, 0)},
        {TileType.PlankD1, new Tile(2, 1)},
        {TileType.PlankD2, new Tile(2, 2)},
        {TileType.PlankD3, new Tile(2, 3)},

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

        {TileType.WoodTop, new Tile(10, 0)},
        {TileType.WoodTopD1, new Tile(10, 1)},
        {TileType.WoodTopD2, new Tile(10, 2)},
        {TileType.WoodTopD3, new Tile(10, 3)},

        {TileType.WoodSide, new Tile(10, 4)},
        {TileType.WoodSideD1, new Tile(10, 5)},
        {TileType.WoodSideD2, new Tile(10, 6)},
        {TileType.WoodSideD3, new Tile(10, 7)},

        {TileType.Leaves, new Tile(11, 0)},
        {TileType.Glass, new Tile(11, 4)},

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

        {TileType.FurnaceTopBottom, new Tile(8, 4)},
        {TileType.FurnaceTopBottomD1, new Tile(8, 5)},
        {TileType.FurnaceTopBottomD2, new Tile(8, 6)},
        {TileType.FurnaceTopBottomD3, new Tile(8, 7)},

        {TileType.FurnaceSide, new Tile(8, 0)},
        {TileType.FurnaceSideD1, new Tile(8, 1)},
        {TileType.FurnaceSideD2, new Tile(8, 2)},
        {TileType.FurnaceSideD3, new Tile(8, 3)},

        {TileType.TorchTop, new Tile(11, 2) },
        {TileType.TorchSide, new Tile(11, 1) },
        {TileType.TorchBottom, new Tile(11, 3) },
    };
}