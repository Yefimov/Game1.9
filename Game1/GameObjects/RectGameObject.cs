using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameObjects
{
  public abstract  class RectGameObject: GameObject
    {
      public Vector2 Position { get; set; }
      public float Width { get; private set; }
      public float Height { get; private set; }

      private float Scale;
      private Vector2 Origin;

      public SpriteInfo Sprite { get; private set; }

      private RectGameObject() { }
      protected RectGameObject(Vector2 Position, SpriteInfo Sprite, float Scale)
      {
          this.Position = Position;
          this.Width = Scale * Sprite.Texture.Bounds.Width;
          this.Height = Scale * Sprite.Texture.Bounds.Height;
          this.Sprite = Sprite;
          this.Scale = Scale;
          Origin = new Vector2(Scale * Sprite.Texture.Bounds.Width / 2f, Scale * Sprite.Texture.Bounds.Height / 2f);
      }

      public bool Intersect(RectGameObject that)
      {
          Rectangle a = new Rectangle((int) this.Position.X, (int) this.Position.Y, (int) this.Width, (int) this.Height);
          Rectangle b = new Rectangle((int) that.Position.X, (int) that.Position.Y, (int) that.Width, (int) that.Height);
          return a.Intersects(b);       
      }
      public override void Draw(SpriteBatch spriteBatch)
      {
          var sourceRect = new Rectangle(0, 0, (int) Width, (int) Height);//Rect Танка
          spriteBatch.Draw(Sprite.Texture, Position, sourceRect, Color.White, 0.0f, Origin, Scale, SpriteEffects.None, 0f);
      }
    }
}
