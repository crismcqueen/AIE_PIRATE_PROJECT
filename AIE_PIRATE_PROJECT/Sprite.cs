using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AIE_PIRATE_PROJECT
{
    class Sprite
    {
        private Texture2D texture;
        private string asset;
        private int? height, width;
        private int margin, border;
        private Vector2 ZeroVector = Vector2.Zero;

        public Sprite(string assetName, int? height = null, int? width = null, int marginSize = 0, int borderSize = 0)
        {
            asset = assetName;

            this.height = height;
            this.width = width;
        }

        public void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset);

            if (height == null)
            {
                height = texture.Height;
            }

            if (width == null)
            {
                width = texture.Width;
            }
        }

        public void draw(SpriteBatch spriteBatch ,Vector2 location, float rotation, int spriteRow = 0, int spriteColumn = 0, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            Rectangle drawArea = new Rectangle((int)((width + border) * spriteColumn) + margin, (int)((height + border) * spriteRow) + margin, (int)width, (int)height);

            spriteBatch.Draw(texture, location, drawArea, Color.White, rotation, (new Vector2((float)width / 2, (float)height / 2)), Vector2.Zero, spriteEffects, 0);
        }
    }
}
