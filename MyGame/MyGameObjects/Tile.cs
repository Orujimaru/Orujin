using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orujin.Framework;

namespace MyGame.MyGameObjects
{
    public class Tile : GameObject
    {
        public Tile(Texture2D texture, Vector2 position, Vector2 scrollOffset, String name, String tag)
            : base(position, name, tag)
        {
            this.rendererComponents.AddSprite(texture, new Vector2(0, 0), null, Color.White, name);
            this.scrollOffset = scrollOffset;
            GameManager.game.AddObject(this);
        }
    }
}
