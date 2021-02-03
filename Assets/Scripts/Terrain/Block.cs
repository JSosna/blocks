using System.Collections.Generic;
using UnityEngine;

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
    Glass,

    GrassSnow,
    GrassSnowD1,
    GrassSnowD2,
    GrassSnowD3,

    Sand,
    SandD1,
    SandD2,
    SandD3,

    Furnace,
    FurnaceD1,
    FurnaceD2,
    FurnaceD3,

    Torch,
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
        {BlockType.WoolWhiteD1, new Block(TileType.WoolWhiteD1)},
        {BlockType.WoolWhiteD2, new Block(TileType.WoolWhiteD2)},
        {BlockType.WoolWhiteD3, new Block(TileType.WoolWhiteD3)},
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
        {BlockType.Glass, new Block(TileType.Glass)},

        {BlockType.GrassSnow, new Block(TileType.GrassSnow, TileType.GrassSnowSide, TileType.Dirt)},
        {BlockType.GrassSnowD1, new Block(TileType.GrassSnowD1, TileType.GrassSnowSideD1, TileType.DirtD1)},
        {BlockType.GrassSnowD2, new Block(TileType.GrassSnowD2, TileType.GrassSnowSideD2, TileType.DirtD2)},
        {BlockType.GrassSnowD3, new Block(TileType.GrassSnowD3, TileType.GrassSnowSideD3, TileType.DirtD3)},

        {BlockType.Sand, new Block(TileType.Sand)},
        {BlockType.SandD1, new Block(TileType.SandD1)},
        {BlockType.SandD2, new Block(TileType.SandD2)},
        {BlockType.SandD3, new Block(TileType.SandD3)},

        {BlockType.Furnace, new Block(TileType.FurnaceTopBottom, TileType.FurnaceSide, TileType.FurnaceTopBottom)},
        {BlockType.FurnaceD1, new Block(TileType.FurnaceTopBottomD1, TileType.FurnaceSideD1, TileType.FurnaceTopBottomD1)},
        {BlockType.FurnaceD2, new Block(TileType.FurnaceTopBottomD2, TileType.FurnaceSideD2, TileType.FurnaceTopBottomD2)},
        {BlockType.FurnaceD3, new Block(TileType.FurnaceTopBottomD3, TileType.FurnaceSideD3, TileType.FurnaceTopBottomD3)},

        {BlockType.Torch, new Block(TileType.TorchTop, TileType.TorchSide, TileType.TorchBottom) }
    };

    public static ParticleSystem.MinMaxGradient GetBlockTypeParticlesGradient(BlockType blockType) {
        switch (blockType) {
            default:
                return new ParticleSystem.MinMaxGradient(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0));
            case BlockType.Dirt:
            case BlockType.DirtD1:
            case BlockType.DirtD2:
            case BlockType.DirtD3:
                return new ParticleSystem.MinMaxGradient(new Color(.45f, .35f, .25f), new Color(.35f, .25f, .15f));
            case BlockType.Grass:
            case BlockType.GrassD1:
            case BlockType.GrassD2:
            case BlockType.GrassD3:
                return new ParticleSystem.MinMaxGradient(new Color(.45f, .35f, .25f), new Color(.20f, .65f, .40f));
            case BlockType.GrassSnow:
            case BlockType.GrassSnowD1:
            case BlockType.GrassSnowD2:
            case BlockType.GrassSnowD3:
                return new ParticleSystem.MinMaxGradient(new Color(1, 1, 1), new Color(.45f, .35f, .25f));
            case BlockType.Leaves:
                return new ParticleSystem.MinMaxGradient(new Color(.16f, .45f, .17f), new Color(.16f, .27f, .17f));
            case BlockType.Torch:
            case BlockType.Wood:
            case BlockType.WoodD1:
            case BlockType.WoodD2:
            case BlockType.WoodD3:
                return new ParticleSystem.MinMaxGradient(new Color(.27f, .15f, .11f), new Color(.15f, .08f, .04f));
            case BlockType.Stone:
            case BlockType.StoneD1:
            case BlockType.StoneD2:
            case BlockType.StoneD3:
                return new ParticleSystem.MinMaxGradient(new Color(.3f, .3f, .3f), new Color(.5f, .5f, .5f));
            case BlockType.CoalOre:
            case BlockType.CoalOreD1:
            case BlockType.CoalOreD2:
            case BlockType.CoalOreD3:
                return new ParticleSystem.MinMaxGradient(new Color(.03f, .03f, .03f), new Color(.5f, .5f, .5f));
            case BlockType.IronOre:
            case BlockType.IronOreD1:
            case BlockType.IronOreD2:
            case BlockType.IronOreD3:
                return new ParticleSystem.MinMaxGradient(new Color(.87f, .7f, .5f), new Color(.5f, .5f, .5f));
            case BlockType.DiamondOre:
            case BlockType.DiamondOreD1:
            case BlockType.DiamondOreD2:
            case BlockType.DiamondOreD3:
                return new ParticleSystem.MinMaxGradient(new Color(.3f, .7f, 1), new Color(.5f, .5f, .5f));
            case BlockType.Sand:
            case BlockType.SandD1:
            case BlockType.SandD2:
            case BlockType.SandD3:
                return new ParticleSystem.MinMaxGradient(new Color(1, .9f, .6f), new Color(.9f, .7f, .4f));
            case BlockType.WoolWhite:
            case BlockType.WoolWhiteD1:
            case BlockType.WoolWhiteD2:
            case BlockType.WoolWhiteD3:
                return new ParticleSystem.MinMaxGradient(new Color(.7f, .7f, .7f), new Color(.9f, .9f, .9f));
            case BlockType.Plank:
            case BlockType.PlankD1:
            case BlockType.PlankD2:
            case BlockType.PlankD3:
                return new ParticleSystem.MinMaxGradient(new Color(.7f, .5f, .3f), new Color(.5f, .3f, .2f));
            case BlockType.Furnace:
            case BlockType.FurnaceD1:
            case BlockType.FurnaceD2:
            case BlockType.FurnaceD3:
                return new ParticleSystem.MinMaxGradient(new Color(.7f, .7f, .7f), new Color(.278f, .278f, .278f));
            case BlockType.Glass:
                return new ParticleSystem.MinMaxGradient(new Color(.77f, .929f, .956f), new Color(.737f, .898f, .925f));
        }
    }

    public static Tools.ToolType GetMostEffectiveToolType(BlockType blockType) {
        switch (blockType) {
            default:
                return Tools.ToolType.Pickaxe;

            case BlockType.Wood:
            case BlockType.WoodD1:
            case BlockType.WoodD2:
            case BlockType.WoodD3:
            case BlockType.Plank:
            case BlockType.PlankD1:
            case BlockType.PlankD2:
            case BlockType.PlankD3:
                return Tools.ToolType.Axe;

            case BlockType.Dirt:
            case BlockType.DirtD1:
            case BlockType.DirtD2:
            case BlockType.DirtD3:
            case BlockType.Grass:
            case BlockType.GrassD1:
            case BlockType.GrassD2:
            case BlockType.GrassD3:
            case BlockType.GrassSnow:
            case BlockType.GrassSnowD1:
            case BlockType.GrassSnowD2:
            case BlockType.GrassSnowD3:
            case BlockType.Sand:
            case BlockType.SandD1:
            case BlockType.SandD2:
            case BlockType.SandD3:
            case BlockType.WoolWhite:
            case BlockType.WoolWhiteD1:
            case BlockType.WoolWhiteD2:
            case BlockType.WoolWhiteD3:
                return Tools.ToolType.Shovel;
        }
    }

    public static BlockType GetNotDestroyedBlock(BlockType blockType) {
        switch (blockType) {
            
            case BlockType.Air:
                return BlockType.Air;
            case BlockType.Leaves:
                return BlockType.Leaves;
            default:
            case BlockType.Torch:
                return BlockType.Torch;
            case BlockType.Dirt:
            case BlockType.DirtD1:
            case BlockType.DirtD2:
            case BlockType.DirtD3:
                return BlockType.Dirt;
            case BlockType.Grass:
            case BlockType.GrassD1:
            case BlockType.GrassD2:
            case BlockType.GrassD3:
                return BlockType.Grass;
            case BlockType.GrassSnow:
            case BlockType.GrassSnowD1:
            case BlockType.GrassSnowD2:
            case BlockType.GrassSnowD3:
                return BlockType.GrassSnow;
            case BlockType.Wood:
            case BlockType.WoodD1:
            case BlockType.WoodD2:
            case BlockType.WoodD3:
                return BlockType.Wood;
            case BlockType.Stone:
            case BlockType.StoneD1:
            case BlockType.StoneD2:
            case BlockType.StoneD3:
                return BlockType.Stone;
            case BlockType.CoalOre:
            case BlockType.CoalOreD1:
            case BlockType.CoalOreD2:
            case BlockType.CoalOreD3:
                return BlockType.CoalOre;
            case BlockType.IronOre:
            case BlockType.IronOreD1:
            case BlockType.IronOreD2:
            case BlockType.IronOreD3:
                return BlockType.IronOre;
            case BlockType.DiamondOre:
            case BlockType.DiamondOreD1:
            case BlockType.DiamondOreD2:
            case BlockType.DiamondOreD3:
                return BlockType.DiamondOre;
            case BlockType.Sand:
            case BlockType.SandD1:
            case BlockType.SandD2:
            case BlockType.SandD3:
                return BlockType.Sand;
            case BlockType.Plank:
            case BlockType.PlankD1:
            case BlockType.PlankD2:
            case BlockType.PlankD3:
                return BlockType.Plank;
            case BlockType.Furnace:
            case BlockType.FurnaceD1:
            case BlockType.FurnaceD2:
            case BlockType.FurnaceD3:
                return BlockType.Furnace;
            case BlockType.WoolWhite:
            case BlockType.WoolWhiteD1:
            case BlockType.WoolWhiteD2:
            case BlockType.WoolWhiteD3:
                return BlockType.WoolWhite;
            case BlockType.WoolBlack:
            case BlockType.WoolBlackD1:
            case BlockType.WoolBlackD2:
            case BlockType.WoolBlackD3:
                return BlockType.WoolBlack;
        }
    }
}
