using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Vector2 _moveDirection = new Vector2{Y = -1};

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Enemy(char icon, float x, float y, float speed, string name = "Bullet", ConsoleColor color = ConsoleColor.White)
            : base(icon, x, y, name, color)
        {
            _speed = speed;
        }

        public override void Update()
        {
            Velocity = _moveDirection * Speed;

            Position += Velocity;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "Wall")
                _moveDirection *= -1;


        }
    }
}
