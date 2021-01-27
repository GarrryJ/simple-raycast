using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace simple_raycast {
    public class Engine : Game {
        private const int WINDOW_HEIGHT = 512;
        private const int WINDOW_WIDTH = 1024;
        private const int WINDOW_X_STEP = 8;
        private GraphicsDeviceManager _graphics;
        private Texture2D rectangleBlock;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Map map;
        private KeyboardState currentKeyboardState;
        private double rayAngle;
        private double step;
        private double distanceToWall;
        private bool hitWall;
        private double eyeX;
        private double eyeY;
        private int wallId;
        private int tX;
        private int tY;
        private int ceiling;
        private int floor;

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
            int[] mapArr = new int[160]
            {
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 3, 1, 0, 0, 0, 1, 1, 1, 1,
                1, 1, 2, 0, 3, 0, 1, 1, 1, 1,
                1, 0, 0, 0, 3, 0, 0, 3, 2, 3,
                3, 0, 2, 1, 1, 1, 0, 0, 0, 2,
                3, 0, 2, 4, 4, 4, 1, 3, 0, 3,
                3, 0, 2, 4, 0, 0, 4, 1, 0, 2,
                3, 0, 2, 4, 0, 0, 0, 0, 0, 3,
                3, 0, 2, 4, 0, 0, 4, 2, 1, 3,
                3, 0, 2, 1, 4, 4, 1, 1, 1, 1,
                1, 0, 0, 1, 1, 1, 1, 0, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 0, 0, 0, 0, 0, 0, -1, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            };

            int playerCoor = 0;
            while (mapArr[playerCoor] != -1)
                playerCoor++;
            mapArr[playerCoor] = 0;
            map = new Map(10, 16, mapArr);
            player = new Player(playerCoor % map.getMapWidth(), playerCoor / map.getMapWidth(), 3);
            
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
                if ( !map.getMap()[ (int)player.getY() * map.getMapWidth() + (int)player.getX() ].Equals(0) )
                    player.goBack();
            }
            if (currentKeyboardState.IsKeyDown(Keys.S)) {
                player.goBack();
                if ( !map.getMap()[ (int)player.getY() * map.getMapWidth() + (int)player.getX() ].Equals(0) )
                    player.goForward();
            }
            
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(rectangleBlock, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT/2), Color.Black);
            _spriteBatch.Draw(rectangleBlock, new Rectangle(0, WINDOW_HEIGHT/2, WINDOW_WIDTH, WINDOW_HEIGHT/2), Color.Black);

            Color color;

            for (int x = 0; x < WINDOW_WIDTH; x += WINDOW_X_STEP) {
                rayAngle = (player.getDirection() - player.getFOV()/2) + (((double)x / WINDOW_WIDTH) * player.getFOV());
                step = 0.05;
                distanceToWall = 0.0;
                hitWall = false;

                eyeX = Math.Sin(rayAngle);
                eyeY = Math.Cos(rayAngle);

                wallId = 0;

                while (!hitWall && (distanceToWall < player.getDepth())) {
                    distanceToWall += step;

                    tX = (int)(player.getX() + eyeX * distanceToWall);
                    tY = (int)(player.getY() + eyeY * distanceToWall);

                    if (tX < 0 || tX >= map.getMapWidth() || tY < 0 || tY >= map.getMapHeigth()) {
                        hitWall = true;
                        distanceToWall = player.getDepth();
                    } else if (!map.getMap()[tY*map.getMapWidth() + tX].Equals(0)) {
                        hitWall = true;
                        wallId = map.getMap()[tY*map.getMapWidth() + tX];
                    }
                }

                ceiling = (int)((WINDOW_HEIGHT/2) - WINDOW_HEIGHT/distanceToWall);
                floor = WINDOW_HEIGHT - ceiling;

                color = Wall.getColorById(wallId, (int)(((float)255 * (distanceToWall/player.getDepth()) )));

                _spriteBatch.Draw(rectangleBlock, new Rectangle(x, ceiling, WINDOW_X_STEP, floor - ceiling), color);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}