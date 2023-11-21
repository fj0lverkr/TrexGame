using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TrexGame.Interfaces;

namespace TrexGame.Entities
{
    internal class FadeInTexture : IGameEntity
    {
        private readonly Texture2D _texture;
        private bool _isFading;
        private int _windowWidth;
        private int _windowHeight;

        public int DrawOrder { get; set; }
        public float Speed { get; set; }
        public float PositionX { get; set; }

        public FadeInTexture(int initialMargin, GraphicsDevice g, int windowWidth, int windowHeight)
        {
            PositionX = initialMargin;
            DrawOrder = 0;
            Speed = 850;
            _texture = new(g, 1, 1);
            _texture.SetData(new Color[] { Color.White });
            _isFading = false;
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Math.Round(PositionX), 0, _windowWidth, _windowHeight), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (_isFading)
                PositionX += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
        }

        public void StartFade()
        {
            _isFading = true;
        }
    }
}
