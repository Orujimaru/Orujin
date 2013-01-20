using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyGame.MyGameObjects;
using Orujin.Framework;
using Orujin.Pipeline;

namespace MyGame
{
    public class MyObjectProcessor : ObjectProcessor
    {
        public override void ProcessTextureObject(ObjectInformation oi)
        {
            switch (oi.name.ToUpper())
            {
                case "CAMERAOBJECT":
                    CameraObject cameraObject = new CameraObject(2000, 2, oi.position, true, oi.name);
                    break;
            }
           //Tile t = new Tile(item.getTexture(), item.Position, layer.ScrollSpeed, item.Name, layer.Name);
        }

        public override void ProcessPrimitiveObject(ObjectInformation oi)
        {
            if (oi.customProperties.Count == 0) //No custom properties means a regular platform
            {
                Platform tempPlatform = new Platform(oi.width, oi.height, oi.position, oi.name);
            }
            else
            {
                switch (oi.customProperties[0].ToUpper())
                {
                    case "SLOPE":
                        Platform tempSlope = new Platform(oi.width, oi.height, oi.position, oi.name, true);
                        break;

                    case "INVERSLOPE":
                        Platform tempInverSlope = new Platform(oi.width, oi.height, oi.position, oi.name, false);
                        break;
                }
            }
        }
    }
}
