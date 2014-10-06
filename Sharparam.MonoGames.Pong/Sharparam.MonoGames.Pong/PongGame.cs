using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sharparam.MonoGames.Pong
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PongGame : Game
    {
        public static readonly Random Rng = new Random();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Ball _ball;

        private Paddle _left;

        private Paddle _right;

        private int _width;

        private int _height;

        public PongGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _width = GraphicsDevice.Viewport.Width;
            _height = GraphicsDevice.Viewport.Height;

            _ball = new Ball(GraphicsDevice);

            _ball.XSpeed = 5; //Rng.Next(25, 30);
            _ball.YSpeed = 5; //Rng.Next(25, 30);

            _left = new Paddle(GraphicsDevice);
            _left.X = 25;
            _left.Y = _height / 2 - Paddle.Height / 2;

            _right = new Paddle(GraphicsDevice);
            _right.X = _width - 25 - Paddle.Width;
            _right.Y = _height / 2 - Paddle.Height / 2;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kbState = Keyboard.GetState();
            var padState = GamePad.GetState(PlayerIndex.One);

            if (kbState.IsKeyDown(Keys.W) || padState.ThumbSticks.Left.Y > 0)
                _left.Y -= 5;

            if (kbState.IsKeyDown(Keys.S) || padState.ThumbSticks.Left.Y < 0)
                _left.Y += 5;

            if (kbState.IsKeyDown(Keys.Up) || padState.ThumbSticks.Right.Y > 0)
                _right.Y -= 5;

            if (kbState.IsKeyDown(Keys.Down) || padState.ThumbSticks.Right.Y < 0)
                _right.Y += 5;

            _ball.Update(gameTime);

            if (_ball.X <= 0 || (_ball.X + Ball.Width) >= _width)
                _ball.XSpeed = -_ball.XSpeed; // * 0.8);
            if (_ball.Y <= 0 || (_ball.Y + Ball.Height) >= _height)
                _ball.YSpeed = -_ball.YSpeed; // * 0.8);

            if (_left.Colliding(_ball) || _right.Colliding(_ball))
                _ball.XSpeed = -_ball.XSpeed;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _left.Draw(_spriteBatch);

            _right.Draw(_spriteBatch);

            _ball.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
