using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrexGame.Interfaces
{
    public interface IGameEntity
    {
        int DrawOrder { get; set; }

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
