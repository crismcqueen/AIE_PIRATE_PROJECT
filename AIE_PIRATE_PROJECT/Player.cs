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
        private Vector2 position = new Vector2(100, 100);
        private int health = 3;
        public Vector2 playerPosition = new Vector2(0, 0);
        public Vector2 playerOffset = new Vector2(0, 0);
        //private Vector2 playerOffset2 = new Vector2(0, 0);
        private float playerSpeed = 150.0f;
        private float playerWind = 25.0f;
        private float playerDrift = 20.0f;
        private float playerTurnSpeed = 1;
        public float playerRotation = 0;
        //private float playerRadius = 20;
        //private bool playerAlive = true;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public void setX(float newX)
        {
            position.X = newX;
        }
        public void setY(float newY)
        {
            position.Y = newY;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float xSpeed = 0;
            float ySpeed = 0;

            if (state.IsKeyDown(Keys.Up) == true)
            {
                ySpeed += playerSpeed * dt;
            }
            else if (state.IsKeyDown(Keys.Up) == false)
            {
                ySpeed += playerWind * dt;
            }
            if (state.IsKeyDown(Keys.Down) == true)
            {
                ySpeed = 0;
            }
            if (state.IsKeyDown(Keys.Left) == true)
            {
                playerRotation -= playerTurnSpeed * dt;
            }
            if (state.IsKeyDown(Keys.Right) == true)
            {
                playerRotation += playerTurnSpeed * dt;
            }
            if (state.IsKeyDown(Keys.A) == true)
            {
                xSpeed += playerDrift * dt;
            }
            if (state.IsKeyDown(Keys.D) == true)
            {
                xSpeed -= playerDrift * dt;
            }
            double x = (xSpeed * Math.Cos(playerRotation)) - (ySpeed * Math.Sin(playerRotation));
            double y = (xSpeed * Math.Sin(playerRotation)) + (ySpeed * Math.Cos(playerRotation));

            // calculate the player's new position
            position.X += (float)x;
            position.Y += (float)y;


            //Vector2 playerDirection = new Vector2(-(float)Math.Sin(playerRotation), (float)Math.Cos(playerRotation));
            //playerDirection.Normalize();
            // shoot a bullet 
            if (state.IsKeyDown(Keys.Space) == true)
            {

            }
        }
    }
    /*
        {

            Sprite sprite;

            bool isAlive = true;
            // temp. just trying to get camera to work this way if fail will put in game1
            public Vector2 position;
            public Vector2 box;
            //pos

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
            */

}
