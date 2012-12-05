using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orujin.Core.Renderer
{
    public interface IRendererInterface
    {
        RendererPackage GetRendererPackage();
        void Update(float elapsedTime);
        void AdjustBrightness(float newBrightness);
    }
}
