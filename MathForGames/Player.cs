using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
   
        public Player(char icon, float x, float y, float speed, Vector2 velocity, string name = "Player", ConsoleColor color = ConsoleColor.White) 
            : base(icon, x, y, velocity, name, color)
        {
            _speed = speed;
        }

        public override void Update()
        {
            Vector2 moveDirection = new Vector2();

            ConsoleKey keyPressed = Engine.GetNextKey();

            if (keyPressed == ConsoleKey.A)
                moveDirection = new Vector2 { X = - 1 };
            if (keyPressed == ConsoleKey.D)
                moveDirection = new Vector2 { X = 1 };
            if (keyPressed == ConsoleKey.W)
                moveDirection = new Vector2 { Y = -1 };
            if (keyPressed == ConsoleKey.S)
                moveDirection = new Vector2 { Y = 1 };


            Velocity = moveDirection * Speed;

            Position += Velocity;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "Wall")
                Position -= Velocity;

            if (actor.Name == "Goal")
                Engine.CloseApplication();

            if (actor.Name == "Bullet")
                Engine.CloseApplication();
        }
    }
}
