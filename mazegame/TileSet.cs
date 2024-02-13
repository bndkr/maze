using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mazegame
{
    struct TileSet
    {
        public static int TileWidth = 40;
        public static int TileHeight = 40;
        public List<Texture2D> Textures { get; private set; }

    }
}