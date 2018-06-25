using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIE_PIRATE_PROJECT
{
    class Projectile
    {
        public static List<Projectile> projectiles = new List<Projectile>();
        Game1 Game = null;
        public Texture2D cannonBall;
        private Direction direction;
        private Vector2 cannonPosition = new Vector2(0, 0);
        private Vector2 cannonVelocity = new Vector2(0, 0);
        private Vector2 cannonOffset = new Vector2(0, 0);
        private int cannonSpeed = 200;
        private int cannonRadius = 5;
        public bool cannonAlive = true;
        public Projectile(Vector2 newPos, Direction newDir)
        {
            cannonPosition = newPos;
            direction = newDir;
        }

        public Vector2 Position
        {
            get
            {
                return cannonPosition;
            }
        } 
        public int Radius
        {
            get
            {
                return cannonRadius;
            }
        }
        public void cannonProjectile(Vector2 position, float rotation)
        {
            if (cannonAlive == false)
                return;

            Vector2 direction = new Vector2((float)-Math.Sin(rotation), (float)Math.Cos(rotation));
            direction.Normalize();
            cannonVelocity = direction * cannonSpeed;
            cannonPosition = position;
            cannonAlive = false;
        }
        public void Update (GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (direction)
            {
                case Direction.Port:
                    cannonPosition.X += cannonSpeed * dt;
                    break;
                case Direction.Starboard:
                    cannonPosition.Y -= cannonSpeed * dt;
                    break;
            }
        }
        public void UpdateBullet(float deltaTime)
        {
            cannonPosition += cannonVelocity * deltaTime;

            if (cannonPosition.X < 0 || cannonPosition.X > Game.ScreenX || cannonPosition.Y < 0 || cannonPosition.Y > Game.ScreenY)
            {
                cannonAlive = true;
            }
        }
    }
}
