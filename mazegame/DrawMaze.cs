using mazegame;
using MazeGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

static class MazeDrawer
{
    public static void drawMaze(GameState gameState, SpriteBatch spriteBatch, TileSet tileset)
    {
        Maze maze = gameState.getMaze();
        for (int y = 0; y < maze.height; y++)
        {
            for (int x = 0; x < maze.width; x++)
            {
                spriteBatch.Draw(tileset.tile, new Vector2(x * TileSet.TileDimension, y * TileSet.TileDimension), Color.White);
            }
        }

        // draw the walls
        for (int y = 0; y < maze.height; y++)
        {
            for (int x = 0; x < maze.width; x++)
            {
                Cell cell = maze.GetCell(x, y);
                if (cell.LeftNeighbor == null)
                {
                    spriteBatch.Draw(tileset.v_wall, new Vector2(x * TileSet.TileDimension, y * TileSet.TileDimension), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
                if (cell.TopNeighbor == null)
                {
                    spriteBatch.Draw(tileset.h_wall, new Vector2(x * TileSet.TileDimension, y * TileSet.TileDimension), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
                if (cell.RightNeighbor == null)
                {
                    spriteBatch.Draw(tileset.v_wall, new Vector2(x * TileSet.TileDimension + TileSet.TileDimension, y * TileSet.TileDimension), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
                if (cell.BottomNeighbor == null)
                {
                    spriteBatch.Draw(tileset.h_wall, new Vector2(x * TileSet.TileDimension, y * TileSet.TileDimension + TileSet.TileDimension), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
            }
        }

        // draw the player
        spriteBatch.Draw(tileset.player, new Vector2(gameState.getPlayerX() * TileSet.TileDimension, gameState.getPlayerY() * TileSet.TileDimension), Color.White);

        // draw the breadcrumbs
        if (gameState.showBreadcrumbs)
        {
            foreach (Cell cell in gameState.breadcrumbs)
            {
                spriteBatch.Draw(tileset.breadcrumbs, new Vector2(cell.X * TileSet.TileDimension, cell.Y * TileSet.TileDimension), Color.White);
            }
        }
        
        // draw the single hint
        if (gameState.showSingleHint)
        {
            if (gameState.shortestPath.Count > 0)
            {
                spriteBatch.Draw(tileset.hint, new Vector2(gameState.shortestPath.Peek().X * TileSet.TileDimension, gameState.shortestPath.Peek().Y * TileSet.TileDimension), Color.White);
            }
        }

        // draw the shortest path
        if (gameState.showShortestPath)
        {
            foreach (Cell cell in gameState.shortestPath)
            {
                spriteBatch.Draw(tileset.hint, new Vector2(cell.X * TileSet.TileDimension, cell.Y * TileSet.TileDimension), Color.White);
            }
        }
    }
}
