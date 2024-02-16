using System.Numerics;
using mazegame;
using MazeGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;

static class MazeDrawer
{
    static int MazeOffset = 100;
    public static void drawMaze(GameState gameState, SpriteBatch spriteBatch, TileSet tileset, GameTime gameTime, GraphicsDevice device)
    {
        Maze maze = gameState.getMaze();

        // draw the background
        Rectangle windowRect = new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height);
        spriteBatch.Draw(tileset.background, windowRect, Color.White);

        // draw the maze base
        for (int y = 0; y < maze.height; y++)
        {
            for (int x = 0; x < maze.width; x++)
            {
                spriteBatch.Draw(tileset.tile, new Vector2(MazeOffset + x * TileSet.TileSpacing, MazeOffset + y * TileSet.TileSpacing), null, Color.White, 0, new Vector2(0, 0), new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
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
                    spriteBatch.Draw(tileset.v_wall, new Vector2(MazeOffset + x * TileSet.TileSpacing, MazeOffset + y * TileSet.TileSpacing), null, Color.Black, 0, Vector2.Zero, new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
                }
                if (cell.TopNeighbor == null)
                {
                    spriteBatch.Draw(tileset.h_wall, new Vector2(MazeOffset + x * TileSet.TileSpacing, MazeOffset + y * TileSet.TileSpacing), null, Color.Black, 0, Vector2.Zero, new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
                }
                if (cell.RightNeighbor == null)
                {
                    spriteBatch.Draw(tileset.v_wall, new Vector2(MazeOffset + x * TileSet.TileSpacing + TileSet.TileSpacing, MazeOffset + y * TileSet.TileSpacing), null, Color.Black, 0, Vector2.Zero, new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
                }
                if (cell.BottomNeighbor == null)
                {
                    spriteBatch.Draw(tileset.h_wall, new Vector2(MazeOffset + x * TileSet.TileSpacing, MazeOffset + y * TileSet.TileSpacing + TileSet.TileSpacing), null, Color.Black, 0, Vector2.Zero, new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
                }
            }
        }

        // draw the player
        spriteBatch.Draw(tileset.player, new Vector2(MazeOffset + gameState.getPlayerX() * TileSet.TileSpacing, MazeOffset + gameState.getPlayerY() * TileSet.TileSpacing), null, Color.White, 0, new Vector2(0, 0), new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);

        // draw the breadcrumbs
        if (gameState.showBreadcrumbs)
        {
            foreach (Cell cell in gameState.breadcrumbs)
            {
                spriteBatch.Draw(tileset.breadcrumbs, new Vector2(MazeOffset + cell.X * TileSet.TileSpacing, MazeOffset + cell.Y * TileSet.TileSpacing), null, Color.White, 0, new Vector2(0, 0), new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
            }
        }

        // draw the single hint
        if (gameState.showSingleHint)
        {
            if (gameState.shortestPath.Count > 0)
            {
                spriteBatch.Draw(tileset.hint, new Vector2(MazeOffset + gameState.shortestPath.Peek().X * TileSet.TileSpacing, MazeOffset + gameState.shortestPath.Peek().Y * TileSet.TileSpacing), null, Color.White, 0, new Vector2(0, 0), new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
            }
        }

        // draw the shortest path
        if (gameState.showShortestPath)
        {
            foreach (Cell cell in gameState.shortestPath)
            {
                spriteBatch.Draw(tileset.hint, new Vector2(MazeOffset + cell.X * TileSet.TileSpacing, MazeOffset + cell.Y * TileSet.TileSpacing), null, Color.White, 0, new Vector2(0, 0), new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);
            }
        }

        // draw the target
        spriteBatch.Draw(tileset.target, new Vector2(MazeOffset + (maze.width - 1) * TileSet.TileSpacing, MazeOffset + (maze.height - 1) * TileSet.TileSpacing), null, Color.White, 0, new Vector2(0, 0), new Vector2(TileSet.Scale, TileSet.Scale), SpriteEffects.None, 0);

        // draw the leaderboards
        if (gameState.showLeaderboards)
        {
            spriteBatch.DrawString(Game1.font, "Leaderboard:", new Vector2(0, 50), Color.White);

            int y = 0;
            foreach (LeaderboardEntry entry in gameState.leaderboard)
            {
                spriteBatch.DrawString(Game1.font, $"{entry.size}x{entry.size}: {entry.score}", new Vector2(0, y * 20 + 70), Color.White);
                y++;
            }
        }

        // draw the score
        spriteBatch.DrawString(Game1.font, $"Score: {gameState.score}", new Vector2(0, 0), Color.White);

        // draw the game time
        spriteBatch.DrawString(Game1.font, $"Time: {gameTime.TotalGameTime.Minutes:D2}:{gameTime.TotalGameTime.Seconds:D2}", new Vector2(0, 20), Color.White);


        // draw the credits
        if (gameState.showCredits)
        {
            Vector2 creditsPosition = new Vector2((device.Viewport.Width - tileset.credits.Width) / 2, (device.Viewport.Height - tileset.credits.Height) / 2);
            spriteBatch.Draw(tileset.credits, creditsPosition, null, Color.White, 0, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0);
        }

        // draw the controls
        spriteBatch.DrawString(Game1.font, "F1 - New Game 5x5", new Vector2(0, device.Viewport.Height - 120), Color.White);
        spriteBatch.DrawString(Game1.font, "F2 - New Game 10x10", new Vector2(0, device.Viewport.Height - 100), Color.White);
        spriteBatch.DrawString(Game1.font, "F3 - New Game 15x15", new Vector2(0, device.Viewport.Height - 80), Color.White);
        spriteBatch.DrawString(Game1.font, "F4 - New Game 20x20", new Vector2(0, device.Viewport.Height - 60), Color.White);
        spriteBatch.DrawString(Game1.font, "F5 - Display High Scores", new Vector2(0, device.Viewport.Height - 40), Color.White);
        spriteBatch.DrawString(Game1.font, "F6 - Display Credits", new Vector2(0, device.Viewport.Height - 20), Color.White);
        
    }
}
