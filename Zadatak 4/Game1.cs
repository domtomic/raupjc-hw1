using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Zadatak_3;

namespace Zadatak_4
{

    public interface IPhysicalObject2D
    {
        float X { get; set; }
        float Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }

    public class CollisionDetector
    {
        public static bool Overlaps(IPhysicalObject2D a, IPhysicalObject2D b)
        {
            return true;
        }
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 900,
                PreferredBackBufferWidth = 500
            };
            Content.RootDirectory = "Content";
        }

        public GenericList<Wall> Walls { get; set; }
        public GenericList<Wall> Goals { get; set; }

        /// <summary>
        /// Bottom paddle object
        /// </summary>
        public Paddle PaddleBottom { get; private set; }
        /// <summary>
        /// Top paddle object
        /// </summary>
        public Paddle PaddleTop { get; private set; }
        /// <summary>
        /// Ball object
        /// </summary>
        public Ball Ball { get; private set; }
        /// <summary>
        /// Background image
        /// </summary>
        public Background Background { get; private set; }
        /// <summary>
        /// Sound when ball hits an obstacle.
        /// SoundEffect is a type defined in Monogame framework
        /// </summary>
        public SoundEffect HitSound { get; private set; }
        /// <summary>
        /// Background music. Song is a type defined in Monogame framework
        /// </summary>
        public Song Music { get; private set; }
        /// <summary>
        /// Generic list that holds Sprites that should be drawn on screen
        /// </summary>
        private IGenericList<Sprite> SpritesForDrawList = new GenericList<Sprite>();

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            var screenBounds = GraphicsDevice.Viewport.Bounds;

            Walls = new GenericList<Wall>
            {
                // try with 100 for default wall size !
                new Wall (-GameConstants.WallDefaultSize, 0,
                    GameConstants.WallDefaultSize, screenBounds.Height ),
                new Wall (screenBounds.Right, 0, GameConstants.WallDefaultSize,
                    screenBounds.Height)
            };

            Goals = new GenericList<Wall>
            {
                new Wall (0, screenBounds.Height, screenBounds.Width,
                    GameConstants.WallDefaultSize),
                new Wall (screenBounds.Top, -GameConstants.WallDefaultSize,
                    screenBounds.Width, GameConstants.WallDefaultSize)
            };

            // Screen bounds details. Use this information to set up game objects positions.
            
            PaddleBottom = new Paddle(GameConstants.PaddleDefaultWidth,
                GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleBottom.X = screenBounds.Width / 2f - PaddleBottom.Width / 2f;
            PaddleBottom.Y = screenBounds.Bottom - PaddleBottom.Height;
            PaddleTop = new Paddle(GameConstants.PaddleDefaultWidth,
                GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleTop.X = screenBounds.Width / 2f - PaddleBottom.Width / 2f;
            PaddleTop.Y = 0;
            Ball = new Ball(GameConstants.DefaultBallSize, GameConstants.DefaultInitialBallSpeed,
                GameConstants.DefaultBallBumpSpeedIncreaseFactor)
            {
                X = screenBounds.Width / 2f,
                Y = screenBounds.Height / 2f
            };
            Background = new Background(screenBounds.Width, screenBounds.Height);
            // Add our game objects to the sprites that should be drawn collection.
            SpritesForDrawList.Add(Background);
            SpritesForDrawList.Add(PaddleBottom);
            SpritesForDrawList.Add(PaddleTop);
            SpritesForDrawList.Add(Ball);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Initialize new SpriteBatch object which will be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Set textures
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");
            PaddleBottom.Texture = paddleTexture;
            PaddleTop.Texture = paddleTexture;
            Ball.Texture = Content.Load<Texture2D>("ball");
            Background.Texture = Content.Load<Texture2D>("background");
            // Load sounds
            // Start background music
            HitSound = Content.Load<SoundEffect>("hit");
            Music = Content.Load<Song>("music");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Music);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            var touchState = Keyboard.GetState();
            if (touchState.IsKeyDown(Keys.Left))
            {
                PaddleBottom.X = PaddleBottom.X - (float)(PaddleBottom.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            if (touchState.IsKeyDown(Keys.Right))
            {
                PaddleBottom.X = PaddleBottom.X + (float)(PaddleBottom.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            if (touchState.IsKeyDown(Keys.A))
            {
                PaddleTop.X = PaddleTop.X - (float)(PaddleBottom.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            if (touchState.IsKeyDown(Keys.D))
            {
                PaddleTop.X = PaddleTop.X + (float)(PaddleBottom.Speed *
                                                          gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            var screenBounds = GraphicsDevice.Viewport.Bounds;
            PaddleBottom.X = MathHelper.Clamp(PaddleBottom.X, screenBounds.Left, screenBounds.Right -
                                                                           PaddleBottom.Width);
            PaddleTop.X = MathHelper.Clamp(PaddleTop.X, screenBounds.Left, screenBounds.Right -
                                                                                 PaddleTop.Width);

            var ballPositionChange = Ball.Direction *
                                     (float)(gameTime.ElapsedGameTime.TotalMilliseconds * Ball.Speed);
            Ball.X += ballPositionChange.X;
            Ball.Y += ballPositionChange.Y;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);

            // Start drawing .
            spriteBatch.Begin();
            for (int i = 0; i < SpritesForDrawList.Count; i++)
            {
                SpritesForDrawList.GetElement(i).DrawSpriteOnScreen(spriteBatch);
            }
            // End drawing.
            // Send all gathered details to the graphic card in one batch.
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class Wall : IPhysicalObject2D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Wall(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

    public abstract class Sprite : IPhysicalObject2D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// Represents the texture of the Sprite on the screen.
        /// Texture2D is a type defined in Monogame framework.
        /// </summary>
        public Texture2D Texture { get; set; }
        protected Sprite(int width, int height, float x = 0, float y = 0)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }
        /// <summary>
        /// Base draw method
        /// </summary>
        public virtual void DrawSpriteOnScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), new Rectangle(0, 0,
                Width, Height), Color.White);
        }
    }

    /// <summary>
    /// Game background representation
    /// </summary>
    public class Background : Sprite
    {
        public Background(int width, int height) : base(width, height)
        {
        }
    }

    /// <summary>
    /// Game ball object representation
    /// </summary>
    public class Ball : Sprite
    {
        /// <summary>
        /// Defines current ball speed in time.
        /// </summary>
        public float Speed { get; set; }

        public float BumpSpeedIncreaseFactor { get; set; }

        /// <summary>
        /// Defines ball direction.
        /// Valid values ( -1 , -1) , (1 ,1) , (1 , -1) , ( -1 ,1).
        /// Using Vector2 to simplify game calculation . Potentially
        /// dangerous because vector 2 can swallow other values as well.
        /// OPTIONAL TODO : create your own , more suitable type
        /// </summary>
        public Vector2 Direction { get; set; }

        public Ball(int size, float speed, float
            defaultBallBumpSpeedIncreaseFactor) : base(size, size)
        {
            Speed = speed;
            BumpSpeedIncreaseFactor = defaultBallBumpSpeedIncreaseFactor;
            // Initial direction
            Direction = new Vector2(1, 1);
        }
    }

    /// <summary>
    /// Represents player paddle.
    /// </summary>
    public class Paddle : Sprite
    {
        /// <summary>
        /// Current paddle speed in time
        /// </summary>
        public float Speed { get; set; }
        public Paddle(int width, int height, float initialSpeed) : base(width,
            height)
        {
            Speed = initialSpeed;
        }
        /// <summary>
        /// Overriding draw method . Masking paddle texture with black color.
        /// </summary>
        public override void DrawSpriteOnScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), new Rectangle(0, 0,
                Width, Height), Color.GhostWhite);
        }
    }

    public class GameConstants
    {
        public const float PaddleDefaulSpeed = 0.9f;
        public const int PaddleDefaultWidth = 200;
        public const int PaddleDefaulHeight = 20;
        public const float DefaultInitialBallSpeed = 0.4f;
        public const float DefaultBallBumpSpeedIncreaseFactor = 1.05f;
        public const int DefaultBallSize = 40;
        public const int WallDefaultSize = 100;
    }
}