using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Orujin.Core.Renderer;
using Orujin.Framework;

namespace MyGame.MyGameObjects
{
    public class Player : GameObject
    {
        private const float MaximumOwnVelocity = 4.0f;
        public bool isOnGround = false;
        private bool decelerate = false;

        public Player(Vector2 position, World world, string name)
            : base(position, name, "Player")
        {
            Body body = BodyFactory.CreateCapsule(world, 100 / Camera.PixelsPerMeter, 20 / Camera.PixelsPerMeter, 20, 20 / Camera.PixelsPerMeter, 20, 1.0f, position / Camera.PixelsPerMeter);
            body.BodyType = BodyType.Dynamic;
            this.AddBody(body);
            this.checkForInput = true;
        }

        public void Jump(float strength)
        {
            if (this.isOnGround)
            {
                this.ApplyLinearImpulse(new Vector2(0, -strength), 1.0f);
            }
        }

        public void MovePlayer(Vector2 direction, float magnitude)
        {
            if (this.body.LinearVelocity.X < MaximumOwnVelocity && direction.X > 0)
            {
                this.body.ApplyForce(direction * magnitude);
            }
            else if (this.body.LinearVelocity.X > -MaximumOwnVelocity && direction.X < 0)
            {
                this.body.ApplyForce(direction * magnitude);
            }
            this.decelerate = false;
        }

        public override void Update(float elapsedTime)
        {
            this.body.FixedRotation = true;
            base.Update(elapsedTime);
            this.HandleDeceleration(elapsedTime);
        }

        public void HandleDeceleration(float elapsedTime)
        {
            //The user did not move the player character last loop, therefore we will make it decelerate a bit faster than the friction
            if (this.decelerate && this.isOnGround)
            {
                this.body.LinearVelocity = new Vector2(this.body.LinearVelocity.X * 0.9f, this.body.LinearVelocity.Y);
            }
            this.decelerate = true;
        }

        public override bool OnCollisionEnter(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            this.isOnGround = true;
            return true;
        }

        public override void OnCollisionExit(Fixture fixtureA, Fixture fixtureB)
        {
            this.isOnGround = false;
        }
    }
}
