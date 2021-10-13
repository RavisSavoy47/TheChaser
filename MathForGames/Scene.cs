﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// Array that contain all actors in the scene
        /// </summary>
        private Actor[] _actors;

        public Scene()
        {
            _actors = new Actor[0];
        }

        /// <summary>
        /// Calls start for all actors in the actors array
        /// </summary>
        public virtual void Start()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Start();
            }
        }

        /// <summary>
        /// Calls update for every actor in the scene. Calls start for the actor if it hasn't been called.
        /// </summary>
        public virtual void Update()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if(!_actors[i].Started)
                _actors[i].Start();

                _actors[i].Update();

                //Check for collision
                for (int j = 0; j < _actors.Length; j++)
                {
                    if (_actors[i].Position == _actors[j].Position && j != i)
                        _actors[i].OnCollision(_actors[j]);
                }
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Draw();
            }
        }

        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].End();
            }
        }

        /// <summary>
        /// Adds an actor the scenes list of actors.
        /// </summary>
        /// <param name="actor">The actor to add to the scene</param>
        public virtual void AddActor(Actor actor)
        {
            //Create a new temp arary larger than the current one
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all values from old array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
            }

            //Add the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Set the old array to be the new array
            _actors = tempArray;
        }

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">Teh actor to remove</param>
        /// <returns>False if the actor was not in the scene array</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a new temp arary smaller than the current one
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Copy all values except the actor we dont want into the new array
            int j = 0;
            for(int i = 0; i < tempArray.Length; i++)
            {
                //If the actor that the loop is on is not the temp array counter
                if (_actors[i] != actor)
                {
                    //...adds the actor into the new array and increments the tmep array counter
                    tempArray[j] = _actors[i];
                    j++;
                }
                //Otherwise if the actor is the one to remove...
                else
                {
                    //...set actorRemove to true
                    actorRemoved = true;
                }
            }

            //If the actorRemove was successful them
            if(actorRemoved)
                //Add the new array to the old array
                _actors = tempArray;
            
            return actorRemoved;
        }
    }
}
