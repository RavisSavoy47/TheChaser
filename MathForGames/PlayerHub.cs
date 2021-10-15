using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class PlayerHub : Actor
    {
        private Player _player;
        private UIText _health;
        private UIText _lives;

        public PlayerHub(Player player, UIText health, UIText lives)
        {
            _player = player;
            _health = health;
            _lives = lives;
        }

        public override void Start()
        {
            base.Start();
            _health.Start();
            _lives.Start();
        }

        public override void Update()
        {
            _health.Text = "Health: " + _player.Health.ToString();
            _health.Update();

            _lives.Text = "Live: " + _player.Lives.ToString();
            _lives.Update();
        }

        public override void Draw()
        {
            base.Draw();
            _health.Draw();
            _lives.Draw();
        }
    }
}
