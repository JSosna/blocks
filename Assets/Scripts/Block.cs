using System.Collections.Generic;


public enum BlockType 
{ 
    Air,

    WoolPink,


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

    WoolBlue,
    WoolBlueD1,
    WoolBlueD2,
    WoolBlueD3,

    WoolGreen,
    WoolGreenD1,
    WoolGreenD2,
    WoolGreenD3,

    WoolRed,
    WoolRedD1,
    WoolRedD2,
    WoolRedD3,

    DiamondOre,
    DiamondOreD1,
    DiamondOreD2,
    DiamondOreD3,

    WoodLog,
    WoodLogD1,
    WoodLogD2,
    WoodLogD3,

    Leaves,
    LeavesD1,
    LeavesD2,
    LeavesD3,

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
        {BlockType.Wood, new Block(TileType.Wood)},
        {BlockType.WoolWhite, new Block(TileType.WoolWhite)},
        {BlockType.WoolRed, new Block(TileType.WoolRed)},
        {BlockType.WoolBlue, new Block(TileType.WoolBlue)},
        {BlockType.WoolGreen, new Block(TileType.WoolGreen)},
        {BlockType.WoolPink, new Block(TileType.WoolPink)}, // Not used
        {BlockType.WoolBlack, new Block(TileType.WoolBlack)},
        {BlockType.DiamondOre, new Block(TileType.DiamondOre)},
        {BlockType.WoodLog, new Block(TileType.WoodLogTop, TileType.WoodLogSide, TileType.WoodLogTop)},
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
