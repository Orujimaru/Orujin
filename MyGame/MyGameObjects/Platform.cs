using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Orujin.Core.Renderer;
using Orujin.Framework;

namespace MyGame.MyGameObjects
{
    public class Platform : GameObject
    {
        public Platform(float width, float height, Vector2 position, World world, string name)
            : base(position, name, "Platform")
        {
            Body body = BodyFactory.CreateRectangle(world, width / Camera.PixelsPerMeter, height / Camera.PixelsPerMeter, 1f, position / Camera.PixelsPerMeter);
            body.IsStatic = true;
            body.Restitution = 0f;
            body.Friction = 0.5f;
            this.AddBody(body);
        }
    }
}
