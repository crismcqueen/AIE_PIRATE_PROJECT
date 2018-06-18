using System;
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
        private Vector2 position;
        protected int health;
        protected int speed;
        protected int radius;

        public static List<Enemy> enemies = new List<Enemy>();

        public Transform2D player;
        int maxDistance = 10;
        int minDistance = 5;


        public Vector2 Position
        {
            get
            {
                return position;
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
            position = newPos;
        }

        public void Load(ContentManager content)
        {




        }


        public void Update(float deltaTime)
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {



        }


    }
    class enemyShip : Enemy
    {
        public enemyShip(Vector2 newPos) : base(newPos) { }
    }
    class enemyBoss : Enemy
    {
        public enemyBoss(Vector2 newPos) : base(newPos) { }
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
