using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary1;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private static Icon[,] _buffer;



        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            //loop until the application is told to close
            while(!_applicationShouldClose)
            {
                Update();
                Draw();

                Thread.Sleep(50);
            }

            //Call end for the entire application
            End();
        }

        /// <summary>
        /// Called when th eapplication starts 
        /// </summary>
        private void Start()
        {
            Scene scene = new Scene();

            //top walls
            for (int i = 1; i < 59; i++)
            {
                Actor wall = new Actor('_', i, 1, "Wall", ConsoleColor.Yellow);
                scene.AddActor(wall);
            }

            //left walls
            for (int i = 2; i < 20; i++)
            {
                Actor wall1 = new Actor('|', 0, i, "Wall", ConsoleColor.Yellow);
                scene.AddActor(wall1);
            }
            
            //bottom walls
            for (int i = 1; i < 60; i++)
            {
                Actor wall2 = new Actor('_', i, 19, "Wall", ConsoleColor.Yellow);
                scene.AddActor(wall2);
            }

            //right wall
            for (int i = 2; i < 20; i++)
            {
                Actor wall3 = new Actor('|', 59, i, "Wall", ConsoleColor.Yellow);
                scene.AddActor(wall3);
            }

            Enemy rightbullet = new Enemy('*', 3, 8, 1, "Bullet", ConsoleColor.Red);
            Enemy downbullet = new Enemy('*', 17, 12, 1, "Bullet", ConsoleColor.Red);
            Enemy leftbullet = new Enemy('*', 5, 14, 1, "Bullet", ConsoleColor.Red);
            Enemy upbullet = new Enemy('*', 4, 4, 1, "Bullet", ConsoleColor.Red);

            for (int i = 2; i < 20; i++)
            {
                Enemy bullet1 = new Enemy('*', 15, 4, 1, "Bullet", ConsoleColor.Red);
                scene.AddActor(bullet1);
            }

            Actor goal = new Actor('O', 55,17, "Goal", ConsoleColor.Green);

            Player player = new Player('@', 1, 3, 1, "Player", ConsoleColor.Blue);

            UIText healtText = new UIText(70, 3, "Health", ConsoleColor.Cyan, 25, 20, "Get to the O to survie!");

            scene.AddUIElement(healtText);
            scene.AddActor(player);
            scene.AddActor(goal);
            scene.AddActor(rightbullet);
            scene.AddActor(downbullet);
            scene.AddActor(leftbullet);
            scene.AddActor(upbullet);


            _currentSceneIndex = AddScene(scene);
            _scenes[_currentSceneIndex].Start();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// Called everytime the game loops.
        /// </summary>
        private void Update()
        {
            _scenes[_currentSceneIndex].Update();

            while (Console.KeyAvailable)
                Console.ReadKey(true);

            
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            //Clear the stuff that was on the screen in the last frame
            _buffer = new Icon[Console.WindowWidth, Console.WindowHeight - 1];

            //Reset the cursor position to the top so the previous screen is drawn over
            Console.SetCursorPosition(0, 0);

            //Adds all actors icons to buffer
            _scenes[_currentSceneIndex].Draw();
            _scenes[_currentSceneIndex].DrawUI();

            //Iterate through buffer
            for (int y = 0; y < _buffer.GetLength(1); y++)
            {
                for(int x = 0; x < _buffer.GetLength(0); x++)
                {
                    if (_buffer[x, y].Symbol == '\0')
                        _buffer[x, y].Symbol = ' ';

                    //Set console texxt color to be color of item at buffer
                    Console.ForegroundColor = _buffer[x, y].Color;
                    //Print the symbol of the item in the buffer
                    Console.Write(_buffer[x, y].Symbol);
                }

                //Skip a line once the end of the row has been reached
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
        }

        /// <summary>
        /// Adds a scene to the engine's scene array 
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where th enew scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Set the last item to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length -1;
        }

        /// <summary>
        /// Gets the key in the input stream
        /// </summary>
        /// <returns>teh key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {
            //If there is no key being pressed
            if (!Console.KeyAvailable)
                //..return
                return 0;

            //return teh current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Adds the icon to the buffer to print to the screen in the next draw call.
        /// Prints the icon at the given position in th ebuffer.
        /// </summary>
        /// <param name="icon">The icon to draw</param>
        /// <param name="positison">The position of the icon in the buffer</param>
        /// <returns>Falseif teh position is outside the bounds of the buffer</returns>returns>
        public static bool Render(Icon icon, Vector2 positison)
        {
            //If the position is out of bounds..
            if (positison.X < 0 || positison.X >= _buffer.GetLength(0) || positison.Y < 0 || positison.Y >= _buffer.GetLength(1))
                //..return false
                return false;

            //Set th ebuffer at the index of th egiven position to be teh icon
            _buffer[(int)positison.X, (int)positison.Y] = icon;
            return true;
        }

        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
