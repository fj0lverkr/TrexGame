using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TrexGame.Entities;
using TrexGame.Interfaces;

namespace TrexGame.Managers
{
    internal class PteroManager : IGameEntity
    {
        private const int MIN_PTEROS = 0;
        private const int MAX_PTEROS = 2;
        private const int PTERO_WIDTH = 46;

        private readonly Texture2D _spriteSheet;
        private readonly int _skyWidth;
        private readonly int _skyHeight;

        private List<Pterodactyl> _pteros;
        private float _speed;

        public int DrawOrder { get; set; }
        public bool IsRunning { get; set; }

        public PteroManager(Texture2D spriteSheet, float gameSpeed, int skyWidth, int skyHeight)
        {
            _spriteSheet = spriteSheet;
            _skyWidth = skyWidth;
            _skyHeight = skyHeight;
            _pteros = new();
            _speed = gameSpeed;
            IsRunning = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _pteros.ForEach(p => { p.Draw(gameTime, spriteBatch); });
        }

        public void Update(GameTime gameTime)
        {
            float flySpeed = _speed * 2.5f;
            if (IsRunning)
            {
                if (_pteros.Count > 0)
                {
                    if (_pteros[0].Position.X <= 0)
                        SpawnPteros();
                    if (_pteros[0].Position.Y <= -PTERO_WIDTH)
                        _pteros.RemoveAt(0);
                    _pteros.ForEach(p => { p.PositionX -= flySpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; });
                }
                else
                {
                    SpawnPteros();
                }
            }
            _pteros.ForEach(p => { p.Update(gameTime); });
        }

        private void SpawnPteros()
        {
            Debug.WriteLine("spawnattempt");
            bool doSpawn = new Random().Next(0, 200 / (int)_speed) < 100 && IsRunning && _pteros.Count < MAX_PTEROS;
            if (doSpawn)
            {
                int numInitialPteros = new Random().Next(MIN_PTEROS + 1, MAX_PTEROS);
                for (int i = 0; i < numInitialPteros; i++)
                {
                    DrawOrder = i + 1;
                    int y = new Random().Next(_skyHeight + 5);
                    Vector2 pteroPos = new(_skyWidth + 5, y);
                    _pteros.Add(new(DrawOrder, pteroPos, _spriteSheet, _speed));
                }
            }
        }
    }
}
