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
        
        //Game1 Game = null;
        
        
        private Vector2 cannonPosition = new Vector2(0, 0);
        private Vector2 cannonVelocity = new Vector2(0, 0);
        private Vector2 cannonOffset = new Vector2(0, 0);
        private int cannonSpeed = 200;
        private int cannonRadius = 5;
        private bool cannonCollided = false;
        private bool cannonRange = true;
        public bool isAlive = false;
        public Projectile(Vector2 position, Vector2 direction)
        {
            cannonVelocity = direction * cannonSpeed;
            cannonPosition = position;
        }
        public bool Collided
        {
            get { return cannonCollided; }
            set { cannonCollided = value; }
        }
        public bool Missed
        {
            get { return cannonRange; }
            set { cannonRange = value; }
        }
        public Vector2 CannonPosition
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
        
        
        public void UpdateBullet(float deltaTime)
        {
            cannonPosition += cannonVelocity * deltaTime;

           
            
        }
    }
}
