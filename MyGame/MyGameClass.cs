using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyGame.MyGameObjects;
using Orujin.Core.GameHelp;
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
            Platform testPlatform = new Platform(1280.0f, 50.0f, new Vector2(640,640), this.world, "PlatformA");
            this.AddObject(testPlatform);

            Player testPlayer = new Player(new Vector2(400, 200), world, "PlayerOne");
            this.AddObject(testPlayer);

            this.AddInputCommand("PlayerOne", "Jump", DynamicArray.ObjectArray(2.0f), Keys.Up, Buttons.A);
            this.AddInputCommand("PlayerOne", "MovePlayer", DynamicArray.ObjectArray(new Vector2(-10.0f, 0)), Keys.Left, Buttons.LeftThumbstickLeft);
            this.AddInputCommand("PlayerOne", "MovePlayer", DynamicArray.ObjectArray(new Vector2(10.0f, 0)), Keys.Right, Buttons.LeftThumbstickRight);

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
