﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Orujin.Core.Renderer
{
    internal class Renderer
    {
        private RenderTarget2D lightTarget;
        private RenderTarget2D lightBlockerTarget;
        private RenderTarget2D debugTarget;

        private Vector2 RenderTargetPosition = new Vector2(0, 0);
        private SpriteBatch spriteBatch;

        private Texture2D ambientLight;

        public Renderer(ref GraphicsDeviceManager graphics, int frameWidth, int frameHeight)
        {
            graphics.PreferredBackBufferWidth = frameWidth;
            graphics.PreferredBackBufferHeight = frameHeight;
            graphics.ApplyChanges();

            this.spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            this.lightTarget = new RenderTarget2D(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.lightBlockerTarget = new RenderTarget2D(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            this.debugTarget = new RenderTarget2D(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            this.SetAmbientLight(graphics, 1);
        }

        public void SetAmbientLight(GraphicsDeviceManager graphics, float intensity)
        {
            this.ambientLight = new Texture2D(graphics.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = new Color(Color.White, intensity);
            }

            this.ambientLight.SetData(data);
        }

        public void RenderLevel(List<RendererPackage> objects, ref GraphicsDeviceManager graphics)
        {
            //Set the RenderTarget to the backbuffer
            graphics.GraphicsDevice.SetRenderTarget(null);
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.matrix);

            foreach (RendererPackage rp in objects)
            {
                this.RenderPackage(rp);
            }

            this.spriteBatch.End();
        }

        public void RenderLights(List<RendererPackage> lights, ref GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.SetRenderTarget(this.lightTarget);
            graphics.GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Camera.matrix);

            //Render the ambient light to the entire screen no matter camera zoom.
            int x = (int)(-(Camera.screenCenter.X) * ((1 / Camera.scale.X)-1) + Camera.adjustedPosition.X);
            int y = (int)(-(Camera.screenCenter.Y) * ((1 / Camera.scale.Y)-1) + Camera.adjustedPosition.Y);
            int width = (int)((Camera.screenCenter.X * 2) * (1 / Camera.scale.X)) + 10;
            int height = (int)((Camera.screenCenter.Y * 2) * (1 / Camera.scale.Y)) + 10;

            this.spriteBatch.Draw(this.ambientLight, new Rectangle(x, y, width , height), Color.White);

            foreach (RendererPackage rp in lights)
            {
                this.RenderPackage(rp);
            }
            this.spriteBatch.End();
        }

        public void RenderLightBlockers(List<RendererPackage> lightBlockers, ref GraphicsDeviceManager graphics)
        {
           
        }

        public void RenderDebug(List<RendererPackage> debug, ref GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.SetRenderTarget(this.debugTarget);
            graphics.GraphicsDevice.Clear(Color.White);
            this.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.matrix);

            foreach (RendererPackage rp in debug)
            {
                this.RenderPackage(rp);
            }

            this.spriteBatch.End();
        }

        private void RenderPackage(RendererPackage rp)
        {
            //Render the RenderPackage by the right method overload
            switch(rp.overloadIndex)
            {
                case 1:
                {
                    this.spriteBatch.Draw(rp.texture, rp.destination, rp.color);
                    break;
                }
                case 2:
                {
                    this.spriteBatch.Draw(rp.texture, rp.position, rp.color);
                    break;
                }
                case 3:
                {
                    this.spriteBatch.Draw(rp.texture, rp.destination, rp.source, rp.color);
                    break;
                }
                case 4:
                {
                    this.spriteBatch.Draw(rp.texture, rp.position, rp.source, rp.color);
                    break;
                }
                case 5:
                {
                    this.spriteBatch.Draw(rp.texture, rp.destination, rp.source, rp.color, rp.rotation, rp.origin, rp.spriteEffects, 0);
                    break;
                }
                case 6:
                {
                    this.spriteBatch.Draw(rp.texture, rp.position, rp.source, rp.color, rp.rotation, rp.origin, rp.scale.X, rp.spriteEffects, 0);
                    break;
                }
                case 7:
                {
                    this.spriteBatch.Draw(rp.texture, rp.position, rp.source, rp.color, rp.rotation, rp.origin, rp.scale, rp.spriteEffects, 0);
                    break;
                }
            }           
        }

        public void Finish()
        {
            BlendState blendState = new BlendState();
            blendState.AlphaDestinationBlend = Blend.SourceColor;
            blendState.ColorDestinationBlend = Blend.SourceColor;
            blendState.AlphaSourceBlend = Blend.Zero;
            blendState.ColorSourceBlend = Blend.Zero;

            //Draw the additional RenderTarget2D on top of the back buffer.
            spriteBatch.Begin(SpriteSortMode.Deferred, blendState, null, null, null);
            spriteBatch.Draw(this.lightTarget, Vector2.Zero, Color.White);
            //spriteBatch.Draw(this.debugTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }

    public struct RendererPackage
    { 
        public int overloadIndex;
        public Texture2D texture;
        {
            get
            {
                if(this.gray)
                {
                    return this.grayTexture;
                }
                return this.normalTexture;
            }
            set
            {
                this.normalTexture = value;
            }
        }
        
        private bool gray = false;
        private Texture2D normalTexture;
        private Texture2D grayTexture;
        
        public Vector2 scrollOffset;

        private Vector2 privatePosition;
        public Vector2 position
        {
            get
            {
                return this.privatePosition + this.scrollOffset * this.Camera.adjustedPosition;
            }
            set { this.privatePosition = value; }
        }
        public Vector2 positionOffset;
        public Vector2 parentOffset;
        
        private Rectangle privateDestination;
        public Rectangle destination
        {
            get
            {
                Rectangle tempDestination = this.privateDestination;
                tempDestination.Width = (int)(this.scale.X * tempDestination.Width);
                tempDestination.Height = (int)(this.scale.Y * tempDestination.Height);
                return tempDestination;
            }
            set 
            {
                this.privateDestination = value;
            } 
        }

        public Rectangle source;
        public Color color;
        public Color originalColor;
        public float rotation;

        internal Vector2 defaultOrigin;
        public Vector2 origin;
        
        public Vector2 scale;
        public SpriteEffects spriteEffects;
        public int layer;
        
        public void ToggleGrayTexture()
        {
            if(this.grayTexture == null)
            {
                this.CreateGrayscale();
            }
            this.gray = true;
        }
        
        public void ToggleNormalTexture()
        {
            this.gray = false;
        }
        
        public void CreateGrayscale()
        {
            this.grayTexture = new Texture2D(this.texture); 
            Color[] color = new Color[this.grayTexture.Width*grayTexture.Height]; 
            this.grayTexture.GetData<Color>(color); 
            for (int i = 0; i < this.grayTexture.Width * this.grayTexture.Height; i++) 
            { 
                color[i].R = (byte)((float)color[i].R * 0.3f); 
                color[i].G = (byte)((float)color[i].G * 0.59f); 
                color[i].B = (byte)((float)color[i].B * 0.11f); 
            } 
            this.grayTexture.SetData<Color>(color); 
        }
    }
}
