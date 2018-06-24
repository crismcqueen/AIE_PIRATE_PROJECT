﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIE_PIRATE_PROJECT
{
    class Projectile
    {
        Game1 Game = null;
        public Texture2D cannonBall;
        public Vector2 cannonPosition = new Vector2(0, 0);
        private Vector2 cannonVelocity = new Vector2(0, 0);
        public Vector2 cannonOffset = new Vector2(0, 0);
        private float cannonSpeed = 200;
        private float cannonRadius = 5;
        public bool cannonAlive = true;

        //public Projectile(Vector2 newPossition,)
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
