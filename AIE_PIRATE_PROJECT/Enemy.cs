﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;




namespace AIE_PIRATE_PROJECT
{
    class Enemy
    {
       // Sprite sprite = new Sprite(); 
        Game1 game = null;
        private Vector2 enemyPosition;
        
        protected int health;
        protected int speed= 5;
        protected int radius;

        public static List<Enemy> enemies = new List<Enemy>();

        //public Transform2D player;
        //int maxDistance = 10;
        //int minDistance = 5;


        public Vector2 Position
        {
            get
            {
                return enemyPosition;
            }
            set
            {
                enemyPosition = value;
            }
        }
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
        public int Radius
        {
            get
            {
                return radius;
            }
        }
        public Enemy(Vector2 newPos)
        {
            enemyPosition = newPos;
        }

        public void Load(ContentManager content)
        {


        }


        public void Update(GameTime gameTime, Vector2 playerPosition)
        {

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            Vector2 direction = playerPosition - enemyPosition;
            direction.Normalize();
            Vector2 enemyVelocity = direction * speed * Game1.maxVelocity * dt;


            enemyPosition += enemyVelocity*dt;

            

        }

        private void ChasePlayer()
        {
            foreach (Enemy e in enemies)
            {

                /*if (IsColliding() == true)
                {
                    //fire at player
                    //projectiles yet to be implemented

                } 
                else 
                {
                    
                    
                } */
             
                   

               
            }

        } 

        protected bool IsColliding (Vector2 playerPosition, float radius1, Vector2 enemyPosition, float radius2)
        {
            Vector2 direction = enemyPosition - playerPosition;
            float length = direction.Length();

            if (radius1 + radius2 <= length)
            {
                return false;

            }

            return true;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            


        }


    }
    class enemyShip : Enemy
    {
        public enemyShip(Vector2 newPos) : base(newPos)
        {
            speed =4;
        }
    }
    class enemyBoss : Enemy
    {
        public enemyBoss(Vector2 newPos) : base(newPos)
        {
            speed =4;
        }
    }
}
/*
        Sprite sprite;
        Game1 game = null;
        Vector2 enemyOffset = new Vector2(0, 0);
        Vector2 enemyPosition = new Vector2(200, 100);

        public Transform Player;
        int maxDistance = 10;
        int minDistance = 5; 
        

        float enemySpeed = 100;
        float enemyRadius = 60;
        float enemyRotations = 4.5f;

       public Vector2 Position
        {
            get { return sprite.position; }


        } 


        public Enemy(Game1 game)
        {
            this.game = game;

        }

        public void Load(ContentManager content)
        {


        }

        public void Update(float deltaTime)
        {
            transform.LookAt(Player);

            if (Vector3.Distance(transform.position, Player.position) >= MinDist)
            {
                transform.position += transform.forward * enemySpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
                {
                    //shoot at player
                }

            } 
            
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }*/
