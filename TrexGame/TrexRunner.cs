using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TrexGame.Entities;

namespace TrexGame
{
    public class TrexRunner : Game
    {
        private const string GFX_SPRITESHEET = "spritesheet";
        private const string SFX_BUTTON_PRESS = "button-press";
        private const string SFX_HIT = "hit";
        private const string SFX_SCORE_REACHED = "score-reached";
        private const int TREX_INITIAL_X = 1;
        private const int TREX_INITIAL_Y = 82;

        public const int WINDOW_WIDTH = 600;
        public const int WINDOW_HEIGHT = 150;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _spriteSheet;
        private SoundEffect _sfxButtonPress;
        private SoundEffect _sfxHit;
        private SoundEffect _sfxScoreReached;

        private Trex _trex;

        public TrexRunner()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteSheet = Content.Load<Texture2D>(GFX_SPRITESHEET);
            _sfxButtonPress = Content.Load<SoundEffect>(SFX_BUTTON_PRESS);
            _sfxHit = Content.Load<SoundEffect>(SFX_HIT);
            _sfxScoreReached = Content.Load<SoundEffect>(SFX_SCORE_REACHED);

            _trex = new(_spriteSheet, new(TREX_INITIAL_X, TREX_INITIAL_Y));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _trex.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}