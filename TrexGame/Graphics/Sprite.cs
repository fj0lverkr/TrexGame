using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrexGame.Graphics
{
    public class Sprite
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
            spriteBatch.Draw(_texture, position, new(X, Y, Width, Height), TintColor);
        }
    }
}
