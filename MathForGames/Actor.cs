using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary1;

namespace MathForGames
{
    struct Icon
    {
        public char Symbol;
        public ConsoleColor Color;
    }

    class Actor
    {
        private Icon _icon;
        private string _name;
        private Vector2 _position;
        private bool _started;
        private float _speed;
        private Vector2 _velocity;

        /// <summary>
        /// True if th estart fuction has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public string Name
        {
            get { return _name; }
        }

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

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Icon Icon
        {
            get { return _icon; }
        }

        public Actor(char icon, float x, float y, float velocityx, float velocityy, string name = "Actor", ConsoleColor color = ConsoleColor.White) :
         this(icon, new Vector2 { X = x, Y = y }, new Vector2 { X = velocityx, Y = velocityy }, name, color) {}       

        public Actor(char icon, Vector2 position, Vector2 velocity, string name = "Actor", ConsoleColor color = ConsoleColor.White)
        {
            _velocity = velocity;
            _icon = new Icon { Symbol = icon, Color = color };
            _position = position;
            _name = name;
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update()
        {
            Position += Velocity;
        }

        public virtual void Draw()
        {
            Engine.Render(_icon, Position);

        }

        public virtual void End()
        {

        }

        public virtual void OnCollision(Actor actor)
        {
            if (actor.Name == "Wall")
            {
                Position -= Velocity;
                Velocity *= -1;
            }
        }

    }
}
