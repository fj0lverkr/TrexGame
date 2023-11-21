using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrexGame.Interfaces;

namespace TrexGame.Graphics
{
    internal class GroundTile : IGameEntity
    {
        private readonly Sprite _groundSprite;

        public float PositionX { get; set; }
        public int DrawOrder { get; set; }
        public float Speed { get; set; }

        public GroundTile(float positionX, Sprite groundSprite, int drawOrder)
        {
            PositionX = positionX;
            _groundSprite = groundSprite;
            DrawOrder = drawOrder;
        }

        public void Update(GameTime gameTime)
        {
            return;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _groundSprite.Draw(spriteBatch, new(PositionX, 119f));
        }
    }
}
