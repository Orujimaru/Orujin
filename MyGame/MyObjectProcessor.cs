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
        public override GameObject ProcessTextureObject(ObjectInformation oi)
        {

           //Tile t = new Tile(item.getTexture(), item.Position, layer.ScrollSpeed, item.Name, layer.Name);
            return null;
        }

        public override GameObject ProcessPrimitiveObject(ObjectInformation oi)
        {
            if (oi.customProperties.Count == 0) //No custom properties means a regular platform
            {
                Platform tempPlatform = new Platform(oi.width, oi.height, oi.position, oi.name);
                return tempPlatform;
            }
            else
            {
                switch (oi.customProperties[0].ToUpper())
                {
                    case "SLOPE":
                        Platform tempSlope = new Platform(oi.width, oi.height, oi.position, oi.name, true);
                        return tempSlope;

                    case "INVERSLOPE":
                        Platform tempInverSlope = new Platform(oi.width, oi.height, oi.position, oi.name, false);
                        return tempInverSlope;
                }
            }

            return null;
        }
    }
}
