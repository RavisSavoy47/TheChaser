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

            Enemy bullet = new Enemy('*', 3, 8, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet1 = new Enemy('*', 48, 12, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet2 = new Enemy('*', 5, 14, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet3 = new Enemy('*', 18, 4, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet4 = new Enemy('*', 28, 8, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet5 = new Enemy('*', 35, 12, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet6 = new Enemy('*', 10, 14, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet7 = new Enemy('*', 20, 4, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet8 = new Enemy('*', 35, 2, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet9 = new Enemy('*', 49, 14, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet10 = new Enemy('*', 40, 4, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet11 = new Enemy('*', 37, 8, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet12 = new Enemy('*', 50, 12, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet13 = new Enemy('*', 55, 14, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet14 = new Enemy('*', 23, 4, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet15 = new Enemy('*', 31, 8, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet16 = new Enemy('*', 19, 12, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet17 = new Enemy('*', 32, 14, 1, "Bullet", ConsoleColor.Red);
            Enemy bullet18 = new Enemy('*', 15, 4, 1, "Bullet", ConsoleColor.Red);

            //Creates the goal and player
            Actor goal = new Actor('O', 55,17, "Goal", ConsoleColor.Green);
            Player player = new Player('@', 1, 3, 1, "Player", ConsoleColor.Blue);
            scene.AddActor(player);
            scene.AddActor(goal);

            //Create and add UI for scene
            UIText HubText = new UIText(70, 0, "Hub", ConsoleColor.Cyan, 25, 1, "Get to the O to survie!");
            UIText healthText = new UIText(70, 2, "Health", ConsoleColor.Cyan, 25, 1);
            UIText LivesText = new UIText(70, 4, "Lives", ConsoleColor.Cyan, 25, 1);
            PlayerHub playerHub = new PlayerHub(player, healthText, LivesText);
            scene.AddUIElement(HubText);
            scene.AddUIElement(playerHub);
            

            scene.AddActor(bullet);
            scene.AddActor(bullet);
            scene.AddActor(bullet1);
            scene.AddActor(bullet2);
            scene.AddActor(bullet3);
            scene.AddActor(bullet4);
            scene.AddActor(bullet5);
            scene.AddActor(bullet6);
            scene.AddActor(bullet7);
            scene.AddActor(bullet8);
            scene.AddActor(bullet9);
            scene.AddActor(bullet10);
            scene.AddActor(bullet11);
            scene.AddActor(bullet12);
            scene.AddActor(bullet13);
            scene.AddActor(bullet14);
            scene.AddActor(bullet15);
            scene.AddActor(bullet16);
            scene.AddActor(bullet17);
            scene.AddActor(bullet18);

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

            _scenes[_currentSceneIndex].UpdateUI();
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
