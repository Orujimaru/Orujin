using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Orujin.Core.Renderer
{
    public class CameraManager
    {
        public CameraManager()
        {
        }

        public void Move(Vector2 moveVector)
        {
            Camera.Move(moveVector);
        }
    }
}
