using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects
{
  public abstract  class RectGameObject: GameObject
    {
      public abstract Vector2 Position { get; set; }
      public abstract int Width { get; set; }
      public abstract int Height { get; set; }
      public abstract float Speed { get; set; }
    
    }
}
