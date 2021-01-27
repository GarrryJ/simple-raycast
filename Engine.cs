using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace simple_raycast {
    public class Engine : Game {
        private const int WINDOW_HEIGHT = 500;
        private const int WINDOW_WIDTH = 1000;
        private GraphicsDeviceManager _graphics;
        private Texture2D rectangleBlock;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Map map;
        private KeyboardState currentKeyboardState;

        public Engine() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize() {
            Window.Title = "Simple Raycast Engine";
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent() {
            player = new Player(1, 4, 1.5);
            int[] mapArr = new int[100]
            {
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 0, 0, 1, 0, 0, 1,
                1, 0, 0, 0, 1, 0, 0, 0, 1, 1,
                1, 0, 0, 0, 0, 0, 1, 0, 0, 1,
                1, 0, 0, 0, 1, 0, 0, 0, 1, 1,
                1, 0, 0, 0, 0, 0, 1, 0, 0, 1,
                1, 0, 0, 0, 1, 0, 0, 0, 1, 1,
                1, 0, 0, 0, 0, 0, 1, 0, 0, 1,
                1, 0, 0, 0, 1, 0, 0, 0, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            };
            map = new Map(10, 10, mapArr);

            rectangleBlock = new Texture2D(GraphicsDevice, 1, 1);
            Color xnaColorBorder = new Color(255, 255, 255);
            rectangleBlock.SetData(new[] {xnaColorBorder});

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime) {
            currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
                Exit();
            if (currentKeyboardState.IsKeyDown(Keys.D))
                player.turnRight();
            if (currentKeyboardState.IsKeyDown(Keys.A))
                player.turnLeft();
            if (currentKeyboardState.IsKeyDown(Keys.W)) {
                player.goForward();
                if ( map.getMap()[ (int)player.getY() * map.getMapWidth() + (int)player.getX() ].Equals(1) )
                    player.goBack();
            }
            if (currentKeyboardState.IsKeyDown(Keys.S)) {
                player.goBack();
                if ( map.getMap()[ (int)player.getY() * map.getMapWidth() + (int)player.getX() ].Equals(1) )
                    player.goForward();
            }
            
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            for (int x = 0; x < WINDOW_WIDTH; x += 5) {
                double rayAngle = (player.getDirection() - player.getFOV()/2) + (((double)x / WINDOW_WIDTH) * player.getFOV());
                double step = 0.05;
                double distanceToWall = 0.0;
                bool hitWall = false;

                double eyeX = Math.Sin(rayAngle);
                double eyeY = Math.Cos(rayAngle);

                while (!hitWall && (distanceToWall < player.getDepth())) {
                    distanceToWall += step;

                    int tX = (int)(player.getX() + eyeX * distanceToWall);
                    int tY = (int)(player.getY() + eyeY * distanceToWall);

                    if (tX < 0 || tX >= map.getMapWidth() || tY < 0 || tY >= map.getMapHeigth()) {
                        hitWall = true;
                        distanceToWall = player.getDepth();
                    } else if (map.getMap()[tY*map.getMapWidth() + tX].Equals(1)) {
                        hitWall = true;
                    }
                }

                int ceiling = (int)((WINDOW_HEIGHT/2) - WINDOW_HEIGHT/distanceToWall);
                int floor = WINDOW_HEIGHT - ceiling;

                Color color = new Color((int)((float)255 -  ((float)255 * (distanceToWall/player.getDepth()) )), 0, 0);

                _spriteBatch.Draw(rectangleBlock, new Rectangle(x, 0, 5, ceiling), Color.Black);
                _spriteBatch.Draw(rectangleBlock, new Rectangle(x, ceiling, 5, floor - ceiling), color);
                _spriteBatch.Draw(rectangleBlock, new Rectangle(x, floor, 5, WINDOW_HEIGHT - floor), Color.Gray);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}