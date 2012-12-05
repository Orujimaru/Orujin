using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orujin.Core.Renderer
{
    public class Animation : IRendererInterface
    {
        public RendererPackage GetRendererPackage()
        {
            return new RendererPackage();
        }

        public void Update(float elapsedTime)
        {
        }

        public void AdjustBrightness(float newBrightness)
        {
        }
    }
}
