using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TrexGame.Controllers;
using TrexGame.Entities;
using TrexGame.Interfaces;
using TrexGame.Managers;

namespace TrexGame
{
    internal enum GameState
    {
        Initial,
        Starting,
        Started,
        Running,
        GameOver
    }

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
        private IdleTrexBackground _idleTrexBackground;
        private FadeInTexture _fadeInTexture;
        private EntityManager _entityManager;
        private InputController _inputController;
        private GroundManager _groundManager;
        private CloudManager _cloudManager;
        private PteroManager _pteroManager;

        private GameState _gameState = GameState.Initial;
        private float _gameSpeed = 50f;

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
            _entityManager = new();

            _spriteSheet = Content.Load<Texture2D>(GFX_SPRITESHEET);
            _sfxButtonPress = Content.Load<SoundEffect>(SFX_BUTTON_PRESS);
            _sfxHit = Content.Load<SoundEffect>(SFX_HIT);
            _sfxScoreReached = Content.Load<SoundEffect>(SFX_SCORE_REACHED);

            _trex = new(_spriteSheet, new(TREX_INITIAL_X, TREX_INITIAL_Y), _sfxButtonPress, _gameSpeed);
            _idleTrexBackground = new(_spriteSheet, new(TREX_INITIAL_X, TREX_INITIAL_Y));
            _fadeInTexture = new(IdleTrexBackground.SpriteWidth, GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);
            _groundManager = new(_spriteSheet, _gameSpeed);
            _cloudManager = new(_spriteSheet, _gameSpeed, WINDOW_WIDTH, (WINDOW_HEIGHT / 3 * 2) - 14);
            _pteroManager = new(_spriteSheet, _gameSpeed, WINDOW_WIDTH, (WINDOW_HEIGHT / 3 * 2) - 40);

            _entityManager.Add(new List<IGameEntity> { _trex, _idleTrexBackground, _fadeInTexture });

            _inputController = new(_trex);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_trex.State != TrexState.Idle)
            {
                _entityManager.Remove(_idleTrexBackground);
            }

            switch (_gameState)
            {
                case GameState.Initial:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        _entityManager.Add(new List<IGameEntity>() { _cloudManager, _groundManager, _pteroManager });
                        _gameState = GameState.Starting;
                        StartGame();
                    }
                    break;
                case GameState.Starting:
                    if (_fadeInTexture.PositionX >= WINDOW_WIDTH)
                        _gameState = GameState.Started;
                    break;
                case GameState.Started:
                    _trex.SetState(TrexState.Running);
                    _groundManager.IsRunning = true;
                    _cloudManager.IsRunning = true;
                    _pteroManager.IsRunning = true;
                    _entityManager.Remove(_fadeInTexture);
                    _gameState = GameState.Running;
                    break;
                case GameState.Running:

                    _inputController.Process(gameTime);
                    _entityManager.UpdateGameSpeed(_gameSpeed); //TODO, set sensible increase based on score/time spent in game
                    break;
                case GameState.GameOver:
                    break;
                default: break;
            }

            _entityManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _entityManager.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void StartGame()
        {
            _fadeInTexture.StartFade();
        }
    }
}