using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrexGame.Interfaces
{
    internal interface IGameEntity
    {
        int DrawOrder { get; set; }
        float Speed { get; set; }

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
