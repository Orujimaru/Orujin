using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Orujin.Core.Renderer
{
    public class Camera
    {
        public const float PixelsPerMeter = 64.0f;
        public static Vector2 position { get; private set; }
        public static Vector2 screenCenter { get; private set; }
        public static Vector2 scale { get; private set; }
        public static Matrix matrix { 
            get 
            {
                return Matrix.CreateTranslation(new Vector3(position - screenCenter, 0f))
                    * Matrix.CreateScale(new Vector3(scale, 1)) 
                    * Matrix.CreateTranslation(new Vector3(screenCenter, 0f)) ;
            }
            private set { return; }
        }
        public static Matrix farseerMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3((position - screenCenter) / PixelsPerMeter, 0f))
                    * Matrix.CreateScale(new Vector3(scale, 1))
                    * Matrix.CreateTranslation(new Vector3(screenCenter / PixelsPerMeter, 0f));
            }
            private set { return; }
        }

        public static void Initialize(float frameWidth, float frameHeight)
        {
            screenCenter = new Vector2(frameWidth / 2, frameHeight / 2);
            position = Vector2.Zero;
            scale = new Vector2(1, 1);       
        }

        public static void Move(Vector2 moveVector)
        {
            position += moveVector;
        }

        public static void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public static void Scale(Vector2 scaleVector)
        {
            scale += scaleVector;
            if (scale.X < 0.01f)
            {
                scale = new Vector2(0.01f, scale.Y);
            }
            if (scale.Y < 0.01f)
            {
                scale = new Vector2(scale.X, 0.01f);
            }
        }
    }
}
