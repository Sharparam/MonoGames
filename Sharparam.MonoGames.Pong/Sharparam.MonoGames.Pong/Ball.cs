namespace Sharparam.MonoGames.Pong
{
    using System.IO;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Ball : IUpdatable, IDrawable
    {
        public const int Width = 25;

        public const int Height = 25;

        private Vector2 _position;

        private Vector2 _direction;

        private Texture2D _texture;

        public Ball(GraphicsDevice graphicsDevice)
        {
            _texture = Texture2D.FromStream(graphicsDevice, File.OpenRead(@"Content\ball.png"));
        }

        public int X
        {
            get { return (int)_position.X; }
            set { _position.X = value; }
        }

        public int Y
        {
            get { return (int)_position.Y; }
            set { _position.Y = value; }
        }

        public int XSpeed
        {
            get { return (int)_direction.X; }
            set { _direction.X = value; }
        }

        public int YSpeed
        {
            get { return (int)_direction.Y; }
            set { _direction.Y = value; }
        }

        public void Update(GameTime gameTime)
        {
            X += XSpeed;
            Y += YSpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
