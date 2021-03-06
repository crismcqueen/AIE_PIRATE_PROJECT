﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;


namespace AIE_PIRATE_PROJECT
{
    public class Player
    {
        Game1 game = null;
        public List<Projectile> projectiles = new List<Projectile>();

     

        private Vector2 position = new Vector2(100, 100);
       
        private int health = 3;
        //public Vector2 playerPosition = new Vector2(0, 0);
        public Vector2 playerOffset = new Vector2(0, 0);
        //private Vector2 playerOffset2 = new Vector2(0, 0);
        private float playerSpeed = 150.0f;
        private float playerWind = 25.0f;
        private float playerDrift = 20.0f;
        private float playerTurnSpeed = 1;
        public float playerRotation = 1.57f;
        private float detectRadius = 300;
        private float playerRadius = 20;
        //private bool playerAlive = true;
        float attackDelay = 0.5f;
        float timerDelay = 0.5f;

        public int PlayerHealth
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
        public float PlayerDetection
        {
            get
            {
                return detectRadius;
            }
            set
            {
                detectRadius = value;
            }
        }
        public Vector2 PlayerPosition
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
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

        public Player(Game1 game)
        {
            this.game = game;
        }


        public void Update(GameTime gameTime, SoundEffectInstance cannon)
        {
            KeyboardState state = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float xSpeed = 0;
            float ySpeed = 0;
            timerDelay -= dt;
            if (state.IsKeyDown(Keys.Up))
            {
                ySpeed -= playerSpeed * dt;
            }
            else if (!state.IsKeyDown(Keys.Up))
            {
                ySpeed -= playerWind * dt;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                ySpeed = 0;
            }
            if (state.IsKeyDown(Keys.Left))
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

            position.X = Math.Max(64 + playerRadius, Math.Min(64 * 24 - playerRadius, position.X));
            position.Y = Math.Max(64 + playerRadius, Math.Min(64 * 24 - playerRadius, position.Y));

            //Vector2 playerDirection = new Vector2(-(float)Math.Sin(playerRotation), (float)Math.Cos(playerRotation));
            //playerDirection.Normalize();
            // shoot a bullet 
            if (state.IsKeyDown(Keys.A) == true && timerDelay <= 0)
            {
              // cannonProjectile.cannonProjectile(position, ForwardDirection(playerRotation +MathHelper.ToRadians (180) ));
                Projectile projectile = new Projectile(position, ForwardDirection(playerRotation + MathHelper.ToRadians(180)));
                projectiles.Add(projectile);
                timerDelay = attackDelay;

                cannon.Stop();
                cannon.Play();
            }
            
            if (state.IsKeyDown(Keys.D) == true && timerDelay <= 0)
            {
                Projectile projectile = new Projectile(position, ForwardDirection(playerRotation));
                projectiles.Add(projectile);
                timerDelay = attackDelay;

                cannon.Stop();
                cannon.Play();
            }
            Vector2 ForwardDirection(float rotationDirection)
            {
                Vector2 forwardDirection = Vector2.Zero;
                float rotation = rotationDirection;
                Vector2 direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                direction.Normalize();
                forwardDirection = direction;


                return forwardDirection;
            }

            List<Projectile> pDeaths = new List<Projectile>();
            foreach (Projectile can in projectiles)
            {
                can.UpdateBullet(dt, game);

                if (!can.isAlive)
                {
                    pDeaths.Add(can);
                }
            }
            foreach (Projectile p in pDeaths)
            {
                projectiles.Remove(p);
            }
            pDeaths.Clear();

            List<Enemy> eDeaths = new List<Enemy>();
            foreach (Enemy e in game.enemies)
            {
                if (e.health <= 0)
                {
                    eDeaths.Add(e);
                    game.score++;
                    break;
                }

                if ((PlayerPosition - e.Position).Length() < playerRadius + e.radius)
                {
                    PlayerHealth--;
                    eDeaths.Add(e);
                }
            }
            foreach (Enemy e in eDeaths)
            {
                game.enemies.Remove(e);
            }
            eDeaths.Clear();
        }
    }
}