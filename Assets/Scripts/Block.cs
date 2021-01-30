using System.Collections.Generic;


public enum BlockType 
{ 
    Air,

    Grass,
    GrassD1,
    GrassD2,
    GrassD3,

    Dirt,
    DirtD1,
    DirtD2,
    DirtD3,

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

    Wood,
    WoodD1,
    WoodD2,
    WoodD3,

    Leaves,

    GrassSnow,
    GrassSnowD1,
    GrassSnowD2,
    GrassSnowD3,

    Sand,
    SandD1,
    SandD2,
    SandD3,
}


public class Block
{
    public TileType top, side, bottom;
    public Tile topPos, sidePos, bottomPos;

    public Block(TileType tile)
    {
        top = side = bottom = tile;
        GetPositions();
    }

    public Block(TileType top, TileType side, TileType bottom)
    {
        this.top = top;
        this.side = side;
        this.bottom = bottom;
        GetPositions();
    }

    void GetPositions()
    {
        topPos = Tile.tiles[top];
        sidePos = Tile.tiles[side];
        bottomPos = Tile.tiles[bottom];
    }

    public static Dictionary<BlockType, Block> blocks = new Dictionary<BlockType, Block>(){
        {BlockType.Grass, new Block(TileType.Grass, TileType.GrassSide, TileType.Dirt)},
        {BlockType.GrassD1, new Block(TileType.GrassD1, TileType.GrassSideD1, TileType.DirtD1)},
        {BlockType.GrassD2, new Block(TileType.GrassD2, TileType.GrassSideD2, TileType.DirtD2)},
        {BlockType.GrassD3, new Block(TileType.GrassD3, TileType.GrassSideD3, TileType.DirtD3)},

        {BlockType.Dirt, new Block(TileType.Dirt)},
        {BlockType.DirtD1, new Block(TileType.DirtD1)},
        {BlockType.DirtD2, new Block(TileType.DirtD2)},
        {BlockType.DirtD3, new Block(TileType.DirtD3)},

        {BlockType.Stone, new Block(TileType.Stone)},
        {BlockType.StoneD1, new Block(TileType.StoneD1)},
        {BlockType.StoneD2, new Block(TileType.StoneD2)},
        {BlockType.StoneD3, new Block(TileType.StoneD3)},

        {BlockType.Plank, new Block(TileType.Plank)},
        {BlockType.PlankD1, new Block(TileType.PlankD1)},
        {BlockType.PlankD2, new Block(TileType.PlankD2)},
        {BlockType.PlankD3, new Block(TileType.PlankD3)},

        {BlockType.WoolWhite, new Block(TileType.WoolWhite)},
        {BlockType.WoolBlack, new Block(TileType.WoolBlack)},

        {BlockType.DiamondOre, new Block(TileType.DiamondOre)},
        {BlockType.DiamondOreD1, new Block(TileType.DiamondOreD1)},
        {BlockType.DiamondOreD2, new Block(TileType.DiamondOreD2)},
        {BlockType.DiamondOreD3, new Block(TileType.DiamondOreD3)},

        {BlockType.CoalOre, new Block(TileType.CoalOre)},
        {BlockType.CoalOreD1, new Block(TileType.CoalOreD1)},
        {BlockType.CoalOreD2, new Block(TileType.CoalOreD2)},
        {BlockType.CoalOreD3, new Block(TileType.CoalOreD3)},

        {BlockType.IronOre, new Block(TileType.IronOre)},
        {BlockType.IronOreD1, new Block(TileType.IronOreD1)},
        {BlockType.IronOreD2, new Block(TileType.IronOreD2)},
        {BlockType.IronOreD3, new Block(TileType.IronOreD3)},

        {BlockType.Wood, new Block(TileType.WoodTop, TileType.WoodSide, TileType.WoodTop)},
        {BlockType.WoodD1, new Block(TileType.WoodTopD1, TileType.WoodSideD1, TileType.WoodTopD1)},
        {BlockType.WoodD2, new Block(TileType.WoodTopD2, TileType.WoodSideD2, TileType.WoodTopD2)},
        {BlockType.WoodD3, new Block(TileType.WoodTopD3, TileType.WoodSideD3, TileType.WoodTopD3)},

        {BlockType.Leaves, new Block(TileType.Leaves)},

        {BlockType.GrassSnow, new Block(TileType.GrassSnow, TileType.GrassSnowSide, TileType.Dirt)},
        {BlockType.GrassSnowD1, new Block(TileType.GrassSnowD1, TileType.GrassSnowSideD1, TileType.DirtD1)},
        {BlockType.GrassSnowD2, new Block(TileType.GrassSnowD2, TileType.GrassSnowSideD2, TileType.DirtD2)},
        {BlockType.GrassSnowD3, new Block(TileType.GrassSnowD3, TileType.GrassSnowSideD3, TileType.DirtD3)},

        {BlockType.Sand, new Block(TileType.Sand)},
        {BlockType.SandD1, new Block(TileType.SandD1)},
        {BlockType.SandD2, new Block(TileType.SandD2)},
        {BlockType.SandD3, new Block(TileType.SandD3)},
    };
}
