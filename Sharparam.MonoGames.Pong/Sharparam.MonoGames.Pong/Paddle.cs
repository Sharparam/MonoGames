namespace Sharparam.MonoGames.Pong
{
    using System.IO;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Paddle : IDrawable
    {
        public const int Width = 25;

        public const int Height = 150;

        private Vector2 _position;

        private Texture2D _texture;

        public Paddle(GraphicsDevice graphicsDevice)
        {
            _texture = Texture2D.FromStream(graphicsDevice, File.OpenRead(@"Content\paddle.png"));
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public bool Colliding(Ball ball)
        {
            // Make two rectangles and check intersection
            var thisRect = new Rectangle(X, Y, Width, Height);
            var ballRect = new Rectangle(ball.X, ball.Y, Ball.Width, Ball.Height);

            return thisRect.Intersects(ballRect);
        }
    }
}
