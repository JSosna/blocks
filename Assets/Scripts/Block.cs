using System.Collections.Generic;


public enum BlockType { Air, WoolPink, Grass, Dirt, Stone, Wood, WoolWhite, WoolBlack, WoolBlue, WoolGreen, WoolRed, DiamondOre, WoodLog, Leaves }


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
        {BlockType.Dirt, new Block(TileType.Dirt)},
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
    };
}
