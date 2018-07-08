using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AIE_PIRATE_PROJECT
{
    class Sprite
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 offset = Vector2.Zero;
        List<AnimatedTexture> animations = new List<AnimatedTexture>();
        List<Vector2> animationOffsets = new List<Vector2>();
        private int currentAnimation = 0;
        SpriteEffects effects = SpriteEffects.None;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(new Point((int)position.X, (int)position.Y), animations[currentAnimation].FrameSize);
            }
        }
        public Sprite()
        {

        }
        public void Add(AnimatedTexture animation, int xOffset = 0, int yOffset = 0)
        {
            animations.Add(animation);
            animationOffsets.Add(new Vector2(xOffset, yOffset));
        }

        public void SpriteLoad(ContentManager content, string asset)
        {
            //texture = content.Load<Texture2D>(asset);
        }
        public void SpriteUpdate(float dt)
        {
            animations[currentAnimation].UpdateFrame(dt);
        }
        public void SpriteDraw(SpriteBatch spriteBatch)
        {
            animations[currentAnimation].DrawFrame(spriteBatch, position + animationOffsets[currentAnimation], effects);

            //spriteBatch.Draw(texture, position + offset, Color.White);

        }
        
        public void Pause()
        {
            animations[currentAnimation].Pause();
        }
        public void Play()
        {
            animations[currentAnimation].Play();
        }

        //public static implicit operator Texture2D(Sprite v)
       // {
            //throw new NotImplementedException();
       // }
    }
}