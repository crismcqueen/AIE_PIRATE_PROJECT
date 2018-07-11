﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AIE_PIRATE_PROJECT
{
    public class AnimatedTexture
    {
        private int framecount;
        private Texture2D myTexture;
        private float TimePerFrame;
        private int Frame;
        private float TotalElapsed;
        private bool Paused;

        public float Rotation, Scale, Depth;
        public Vector2 Origin;

        public Point FrameSize
        {
            get
            {
                return new Point(myTexture.Width / framecount, myTexture.Height);
            }
        }

        public AnimatedTexture(Vector2 origin, float rotation, float scale, float depth)
        {
            Origin = origin;
            Rotation = rotation;
            Scale = scale;
            Depth = depth;
        }
        public void Load(ContentManager content, string asset, int frameCount, int framesPerSec)
        {
            framecount = frameCount;
            //myTexture = content.Load<Texture2D>(asset);
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }

        // class AnimatedTexture
        public void UpdateFrame(float elapsed)
        {
            if (Paused) return;
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                Frame = Frame % framecount;
                TotalElapsed -= TimePerFrame;
            }
        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch batch, Vector2 screenPos, SpriteEffects effects = SpriteEffects.None)
        {
            DrawFrame(batch, Frame, screenPos, effects);
        }
        public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos, SpriteEffects effects = SpriteEffects.None)
        {
            int FrameWidth = myTexture.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0, FrameWidth, myTexture.Height);
            batch.Draw(myTexture, screenPos, sourcerect, Color.White, Rotation, Origin, Scale, effects, Depth);
        }

        public bool IsPaused
        {
            get { return Paused; }
        }
        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        public void Pause()
        {
            Paused = true;
        }

    }
}