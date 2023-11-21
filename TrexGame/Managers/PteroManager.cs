using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TrexGame.Entities;
using TrexGame.Interfaces;

namespace TrexGame.Managers
{
    internal class PteroManager : IGameEntity
    {
        private const int MAX_PTEROS = 2;
        private const int PTERO_WIDTH = 46;

        private readonly Texture2D _spriteSheet;
        private readonly int _skyWidth;
        private readonly int _skyHeight;
        private readonly List<Pterodactyl> _pteros;

        private double _lastSpawnTimeStamp;

        public int DrawOrder { get; set; }
        public float Speed { get; set; }
        public bool IsRunning { get; set; }

        public PteroManager(Texture2D spriteSheet, float gameSpeed, int skyWidth, int skyHeight)
        {
            _spriteSheet = spriteSheet;
            _skyWidth = skyWidth;
            _skyHeight = skyHeight;
            _pteros = new();
            Speed = gameSpeed;
            IsRunning = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _pteros.ForEach(p => { p.Draw(gameTime, spriteBatch); });
        }

        public void Update(GameTime gameTime)
        {
            float flySpeed = Speed * 2.5f;
            if (IsRunning)
            {
                if (_pteros.Count > 0)
                {
                    if (_pteros[0].Position.X <= 0)
                        SpawnPteros(gameTime);
                    if (_pteros[0].Position.X <= -PTERO_WIDTH)
                        _pteros.RemoveAt(0);
                    _pteros.ForEach(p => { p.PositionX -= flySpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; });
                }
                else
                {
                    SpawnPteros(gameTime);
                }
            }
            _pteros.ForEach(p => { p.Update(gameTime); });
        }

        private void SpawnPteros(GameTime gameTime)
        {
            double delta = gameTime.TotalGameTime.TotalSeconds - _lastSpawnTimeStamp;
            double minDelta = (new Random().NextDouble() + 1) * 1000 / Speed;
            bool doSpawn = delta >= minDelta && IsRunning && _pteros.Count < MAX_PTEROS;
            if (doSpawn)
            {
                int numPteros = new Random().Next(MAX_PTEROS);
                for (int i = 0; i < numPteros; i++)
                {
                    DrawOrder = i + 1;
                    int y = new Random().Next(_skyHeight);
                    Vector2 pteroPos = new(_skyWidth + 15 * (i + 1), y);
                    _pteros.Add(new(DrawOrder, pteroPos, _spriteSheet, Speed));
                    _lastSpawnTimeStamp = gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }
    }
}
