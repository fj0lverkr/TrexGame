using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using TrexGame.Interfaces;

namespace TrexGame.Managers
{
    public class EntityManager
    {
        private readonly List<IGameEntity> _entities;

        public List<IGameEntity> GameEntities
        {
            get => _entities;
        }

        public EntityManager()
        {
            _entities = new();
        }

        public void Add(IGameEntity entity)
        {
            _entities.Add(entity);
        }

        public void Add(List<IGameEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(IGameEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public void Update(GameTime gameTime)
        {
            _entities.ForEach(x => x.Update(gameTime));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw entities in sequence by DrawOrder descending (i.e. 0 gets drawn last so it's at the top)
            List<IGameEntity> drawOrderedEntities = _entities.OrderByDescending(e => e.DrawOrder).ToList();
            drawOrderedEntities.ForEach(x => x.Draw(gameTime, spriteBatch));
        }
    }
}
