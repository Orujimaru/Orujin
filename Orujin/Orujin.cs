#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Orujin.Core.Renderer;
using Orujin.Framework;
using Orujin.Core.Logic;
using Orujin.Core.Input;
using System.Reflection;
using Orujin.Core.GameHelp;
using Orujin.Debug;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Orujin.Pipeline;
#endregion

namespace Orujin
{
    public class Orujin : Game
    {
        private GraphicsDeviceManager graphics;
        internal RendererManager rendererManager { get; private set; }
        internal GameObjectManager gameObjectManager { get; private set; }
        internal InputManager inputManager { get; private set; }
        internal CameraManager cameraManager { get; private set; }
        internal DebugManager debugManager { get; private set; }

        public Orujin()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);      
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.rendererManager = new RendererManager(ref this.graphics, 1280, 720);
            this.rendererManager.SetAmbientLightIntensity(this.graphics, 1f);
            this.gameObjectManager = new GameObjectManager();
            this.inputManager = new InputManager();
            this.cameraManager = new CameraManager();
            this.debugManager = new DebugManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {     
            this.debugManager.InitiateFarseerDebugView(this.graphics.GraphicsDevice, this.Content, GameManager.game.world);
            GameManager.game.Initialize(this);
            GameManager.game.Start();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
                
            List<InputCommand> input = this.inputManager.Update();
            
            this.gameObjectManager.Update(elapsedTime, input);

            this.HandleAdditionalInput(input);
            GameManager.game.world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
            
            base.Update(gameTime);
        }

        private void HandleAdditionalInput(List<InputCommand> input)
        {
            foreach (InputCommand ic in input)
            {
                if (ic.objectName.Equals("Camera"))
                {
                    if (ic.isDown)
                    {
                        MethodInfo method = new Camera().GetType().GetMethod(ic.methodName);
                        method.Invoke(this.cameraManager, ic.parameters);
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            this.rendererManager.Begin();

            this.rendererManager.Render(this.gameObjectManager);

            this.rendererManager.End(ref this.graphics);

            this.debugManager.RenderFarseerDebugView(this.graphics);

            base.Draw(gameTime);
        }

        public Texture2D GetTexture2DByName(String name)
        {
            return Content.Load<Texture2D>(name);
        }

        public SpriteAnimation GetSpriteAnimationByName(String name)
        {
            return SpriteAnimationLoader.Load(name);
        }

        public ModularAnimation GetModularAnimationByName(String name)
        {
            return ModularAnimationLoader.Load(name);
        }
    }
}
