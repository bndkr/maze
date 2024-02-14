using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mazegame
{
    struct TileSet
    {

        public TileSet(Game game)
        {
            tile = game.Content.Load<Texture2D>("tile");
            v_wall = game.Content.Load<Texture2D>("vertical-wall");
            h_wall = game.Content.Load<Texture2D>("horizontal-wall");
            player = game.Content.Load<Texture2D>("player");
            hint = game.Content.Load<Texture2D>("hint");
            breadcrumbs = game.Content.Load<Texture2D>("breadcrumbs");
        }
        public static readonly int TileDimension = 40;
        public Texture2D tile;
        public Texture2D v_wall;
        public Texture2D h_wall;
        public Texture2D player;
        public Texture2D hint;
        public Texture2D breadcrumbs;

    }
}