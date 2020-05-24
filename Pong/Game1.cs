using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.IO;
using Pong.Sprites;
using Pong.States;
using System;
using System.Collections.Generic;

namespace Pong {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        RenderTarget2D renderTarget;

        private State _currentState;
        private State _nextState;
        KeyboardState _prevKeys;
        KeyboardState _currentKeys;

        public static Random Random;
        public static AudioEngine AudioEngine;
        public static WaveBank WaveBank;
        public static SoundBank SoundBank;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.Window.ClientSizeChanged += delegate { Resolution.WasResized = true; };
        }

        public void ChangeState(State state) {
            _nextState = state;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            Random = new Random();
            Resolution.Initialize(graphics);

            graphics.HardwareModeSwitch = false;
            graphics.IsFullScreen = false;

            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AudioEngine = new AudioEngine("Content/Sound/GameAudio.xgs");
            WaveBank = new WaveBank(AudioEngine, "Content/Sound/Wave Bank.xwb");
            SoundBank = new SoundBank(AudioEngine, "Content/Sound/Sound Bank.xsb");

            _currentState = new MainMenuState(this, graphics.GraphicsDevice, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            Resolution.Update(this, graphics);

            _prevKeys = _currentKeys;
            _currentKeys = Keyboard.GetState();

            if (_nextState != null) {
                _currentState = _nextState;
                _nextState = null;
            }

            if (_currentKeys.IsKeyDown(Keys.F) && !_prevKeys.IsKeyDown(Keys.F)) {
                graphics.ToggleFullScreen();
            }

            _currentState.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            if (graphics.IsFullScreen)
                GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw(gameTime, spriteBatch);

            if (graphics.IsFullScreen) {
                GraphicsDevice.SetRenderTarget(null);
                spriteBatch.Begin();
                spriteBatch.Draw(renderTarget, new Rectangle(0, 0, Resolution.ScreenWidth, Resolution.ScreenHeight), Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
