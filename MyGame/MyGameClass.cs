using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyGame.MyGameObjects;
using Orujin.Core.GameHelp;
using Orujin.Core.Renderer;
using Orujin.Framework;

namespace MyGame
{
    public class MyGameClass : OrujinGame
    {
        public MyGameClass()
            : base("MyGame", new Vector2(0, 9.82f))
        {
        }

        /***Add objects here***/
        public override void Start()
        {

            Tile tile1 = new Tile(OrujinGame.GetTexture2DByName("Textures/Level1"), new Vector2(1280/2, 720/2), new Vector2(-0.5f,0.5f), "Tile1", "Tile");
            base.AddObject(tile1);

            Platform testPlatform = new Platform(1280.0f, 50.0f, new Vector2(640,640), this.world, "PlatformA");
            base.AddObject(testPlatform);

            Player testPlayer = new Player(new Vector2(400, 200), world, "PlayerOne");
            base.AddObject(testPlayer);

            SpriteSkeleton spriteSkeleton = new SpriteSkeleton(new Vector2(650, 515));
            ModularSkeleton modularSkeleton= new ModularSkeleton(new Vector2(800, 515));

            base.AddObject(spriteSkeleton);
            base.AddObject(modularSkeleton);

            base.AddInputCommand("PlayerOne", "Jump", DynamicArray.ObjectArray(2.0f), Keys.Up, Buttons.A);
            base.AddInputCommand("PlayerOne", "MovePlayer", DynamicArray.ObjectArray(new Vector2(-10.0f, 0)), Keys.Left, Buttons.LeftThumbstickLeft);
            base.AddInputCommand("PlayerOne", "MovePlayer", DynamicArray.ObjectArray(new Vector2(10.0f, 0)), Keys.Right, Buttons.LeftThumbstickRight);

            base.AddInputCommand("Camera", "Move", DynamicArray.ObjectArray(new Vector2(2.0f,0)), Keys.A, Buttons.X);
            base.AddInputCommand("Camera", "Move", DynamicArray.ObjectArray(new Vector2(-2.0f, 0)), Keys.D, Buttons.X);

            base.AddInputCommand("Camera", "Scale", DynamicArray.ObjectArray(new Vector2(0.1f, 0.1f)), Keys.W, Buttons.X);
            base.AddInputCommand("Camera", "Scale", DynamicArray.ObjectArray(new Vector2(-0.1f, -0.1f)), Keys.S, Buttons.X);
            base.Start();
        }

        /***Add game logic here***/
        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
        }

        public static void Initialize()
        {
            GameManager.game = new MyGameClass();
        }
    }

}
