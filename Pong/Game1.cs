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
        private KeyboardState _prevKeys;
        private KeyboardState _currentKeys;
        private Point _oldWindowSize;
        private float _aspectRatio;

        public static Random Random;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.ClientSizeChanged += delegate { Resolution.WasResized = true; };
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            Window.AllowUserResizing = true;
        }

        void Window_ClientSizeChanged(object sender, EventArgs e) {
            if (graphics.IsFullScreen)
                return;

            Window.ClientSizeChanged -= new EventHandler<EventArgs>(Window_ClientSizeChanged);

            if (Window.ClientBounds.Width != _oldWindowSize.X) {
                graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                graphics.PreferredBackBufferHeight = (int)(Window.ClientBounds.Width / _aspectRatio);
            } else if (Window.ClientBounds.Height != _oldWindowSize.Y) {
                graphics.PreferredBackBufferWidth = (int)(Window.ClientBounds.Height * _aspectRatio);
                graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            }

            graphics.ApplyChanges();

            _oldWindowSize = new Point(Window.ClientBounds.Width, Window.ClientBounds.Height);

            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
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

            Resolution.Initialize(graphics, 1280, 720);

            graphics.HardwareModeSwitch = false;
            graphics.IsFullScreen = false;

            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            _aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            _oldWindowSize = new Point(Window.ClientBounds.Width, Window.ClientBounds.Height);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AudioManager.Initialize(Content);

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
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw(gameTime, spriteBatch);

            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Resolution.ScaleMatrix);
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
