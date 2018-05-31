using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AIE_PIRATE_PROJECT
{
    class Player
    {
        Sprite sprite;

        bool isAlive = true;

        //pos
        Vector2 position;
        float rotation;

        //pos'
        Vector2 velocity;
        float stear;

        //pos''
        const float acceleration = 10;
        const float deceleration = 7;
        const float forcedDeceleration = 12;
        const float stearPower = 3;
        const float maxSpeed = 50;
        const float maxTurn = 10;

        public Player(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void Load(ContentManager content, String assetName)
        {
            sprite = new Sprite(assetName);
            sprite.Load(content);
        }

        public void Update(float deltaTime, KeyboardState state)
        {
            Vector2 force = Vector2.Zero;
            float turn = 0;
            
            //Accleration
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                force.X = (float)Math.Sin(rotation) * acceleration;
                force.Y = (float)Math.Cos(rotation) * acceleration;
            }

            //Turn
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                turn += stearPower;
            }

            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                turn -= stearPower;
            }

            //Deceleration
            if (!(state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                if (velocity.Length() < 0)
                {
                    if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
                    {
                        force.X = -(float)Math.Sin(rotation) * forcedDeceleration;
                        force.Y = -(float)Math.Cos(rotation) * forcedDeceleration;
                    }
                    else
                    {
                        force.X = -(float)Math.Sin(rotation) * deceleration;
                        force.Y = -(float)Math.Cos(rotation) * deceleration;
                    }
                }
                else
                {
                    velocity = Vector2.Zero;
                    force = Vector2.Zero;
                }
            }

            velocity += (force * deltaTime);
            stear += (turn * deltaTime);

            position += (velocity * deltaTime);
            rotation += (stear * deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.draw(spriteBatch, position, rotation);
        }
    }
}
