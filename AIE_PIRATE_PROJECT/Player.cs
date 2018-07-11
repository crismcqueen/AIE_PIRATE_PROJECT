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
        }

        public void setX(float newX)
        {
            position.X = newX;
        }
        public void setY(float newY)
        {
            position.Y = newY;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
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


            foreach (Projectile can in projectiles)
            {
                if (Vector2.Distance(can.spawnPosition, PlayerPosition) > can.CannonRange)
                {
                    
                    projectiles.Remove(can);
                    break;
                }
                
                //can.isAlive = false;
                can.UpdateBullet(dt);
            }                       


            //Vector2 playerDirection = new Vector2(-(float)Math.Sin(playerRotation), (float)Math.Cos(playerRotation));
            //playerDirection.Normalize();
            // shoot a bullet 
            if (state.IsKeyDown(Keys.A) == true && timerDelay <= 0)
            {
              // cannonProjectile.cannonProjectile(position, ForwardDirection(playerRotation +MathHelper.ToRadians (180) ));
                Projectile projectile = new Projectile(position, ForwardDirection(playerRotation + MathHelper.ToRadians(180)));
                projectiles.Add(projectile);
                timerDelay = attackDelay;

            }
            
            if (state.IsKeyDown(Keys.D) == true && timerDelay <= 0)
            {
                Projectile projectile = new Projectile(position, ForwardDirection(playerRotation));
                projectiles.Add(projectile);
                timerDelay = attackDelay;

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
            
            foreach (Enemy e in enemies)
            {
                if ((PlayerPosition - e.Position).Length() < playerRadius + e.radius)
                {
                    //hit
                }
            }
        }
        
        

    }
   

}
