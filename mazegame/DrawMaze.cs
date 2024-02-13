using mazegame;
using MazeGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

static class MazeDrawer
{
    public static void drawMaze(GameState gameState, SpriteBatch spriteBatch, Texture2D tile)
    {
        Maze maze = gameState.getMaze();
        for (int y = 0; y < maze.height; y++)
        {
            for (int x = 0; x < maze.width; x++)
            {
                spriteBatch.Draw(tile, new Vector2(x * 40, y * 40), Color.White);
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
                    spriteBatch.Draw(tile, new Vector2(x * 40, y * 40), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
                if (cell.TopNeighbor == null)
                {
                    spriteBatch.Draw(tile, new Vector2(x * 40, y * 40), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
                if (cell.RightNeighbor == null)
                {
                    spriteBatch.Draw(tile, new Vector2(x * 40 + 40, y * 40), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
                if (cell.BottomNeighbor == null)
                {
                    spriteBatch.Draw(tile, new Vector2(x * 40, y * 40 + 40), null, Color.Black, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
                }
            }
        }
    }
}
