﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrexGame.Graphics
{
    internal class Sprite
    {
        private readonly Texture2D _texture;
        private int _width;
        private int _height;
        private int _x;
        private int _y;
        private Color _tintColor;

        public Texture2D Texture
        {
            get => _texture;
        }

        public int Width
        {
            get => _width;
            set => _width = value;
        }
        public int Height
        {
            get => _height;
            set => _height = value;
        }
        public int X
        {
            get => _x;
            set => _x = value;
        }
        public int Y
        {
            get => _y;
            set => _y = value;
        }
        public Color TintColor
        {
            get => _tintColor;
            set => _tintColor = value;
        }

        public Sprite(Texture2D texture, int width, int height, int x, int y)
        {
            _texture = texture;
            Width = width;
            Height = height;
            X = x;
            Y = y;
            TintColor = Color.White;
        }

        public Sprite(Texture2D texture, Rectangle dimensions)
        {
            _texture = texture;
            Width = dimensions.Width;
            Height = dimensions.Height;
            X = dimensions.X;
            Y = dimensions.Y;
            TintColor = Color.White;
        }

        public void UpdateDimensions(Rectangle dimensions)
        {
            Width = dimensions.Width;
            Height = dimensions.Height;
            X = dimensions.X;
            Y = dimensions.Y;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle source = new(X, Y, Width, Height);
            spriteBatch.Draw(_texture, position, source, TintColor);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, bool flipX = false, bool flipY = false)
        {
            Rectangle destination = new((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y);
            Rectangle source = new(X, Y, Width, Height);
            SpriteEffects effect = flipX ? SpriteEffects.FlipHorizontally : flipY ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(_texture, destination, source, TintColor, 0f, new(0), effect, 0f);
        }
    }
}
