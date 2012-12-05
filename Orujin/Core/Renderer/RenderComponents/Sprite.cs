using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Orujin.Core.Renderer
{
    public class Sprite : IRendererInterface
    {
        public RendererPackage rendererPackage;
        public SpriteAnimation spriteAnimation { get; private set; }

        public string name { get; private set; }

        private Sprite(Texture2D texture, Vector2 parentOffset, Color color, int layer, string name)
        {
            this.spriteAnimation = null;
            this.rendererPackage.texture = texture;
            this.rendererPackage.destination = new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height);
            this.rendererPackage.parentOffset = parentOffset;
            this.rendererPackage.color = color;
            this.rendererPackage.originalColor = color;
            this.rendererPackage.overloadIndex = 2;
            this.rendererPackage.layer = layer;
            this.name = name;           
        }

        public static Sprite CreateLight(Texture2D texture, Vector2 position, Color color, string name)
        {
            return new Sprite(texture, position, color, 2, name);
        }

        public static Sprite CreateSprite(Texture2D texture, Vector2 position, Color color, string name)
        {
            return new Sprite(texture, position, color, 1, name);
        }

        public void AddAnimation(SpriteAnimation animation)
        {
            this.spriteAnimation = animation;
            this.rendererPackage.overloadIndex = 3;
        }

        public RendererPackage GetRendererPackage()
        {
            if (this.spriteAnimation != null)
            {
                Rectangle src = this.spriteAnimation.GetCurrentFrame();
                this.rendererPackage.source = src;
                this.rendererPackage.destination = src;
            }
            return this.rendererPackage;
        }
    
        public void Update(float elapsedTime)
        {
            if (this.spriteAnimation != null)
            {
                this.spriteAnimation.Update(elapsedTime);
            }
        }

        public void AdjustBrightness(float newBrightness)
        {
            this.rendererPackage.color = Color.Lerp(Color.Black, this.rendererPackage.originalColor, newBrightness);
        }

        public Color[] GetColorData()
        {
            Rectangle source;
            if (this.spriteAnimation == null)
            {
                source = new Rectangle(0, 0, this.rendererPackage.texture.Bounds.Width, this.rendererPackage.texture.Bounds.Height);
            }
            else
            {
                source = this.rendererPackage.source;
            }

            Color[] colorData = new Color[source.Width * source.Height];
            this.rendererPackage.texture.GetData(0, source, colorData, source.X * source.Y, source.Width * source.Height);
            return colorData;
        }
    }
}
