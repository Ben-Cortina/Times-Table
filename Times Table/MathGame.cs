
#region File Description
// MathGame.cs by Ben Cortina
#endregion

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project3.Screens;
using Project3.ScreenManagement;

#endregion

namespace Project3
{


    /// <summary>
    /// A Math Game designed to help teach multiplication
    /// teaching methods include an interactive times table
    /// and a quiz.
    /// </summary>
    public class MathGame : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        GameScreen back, timesTable, menu, help, intro, test;
        InputState input;
        SpriteBatch spriteBatch;
        AudioLibrary audio;


        // By preloading any assets used by UI rendering, we avoid framerate glitches
        // when they suddenly need to be loaded in the middle of a menu transition.
        static readonly string[] preloadAssets =
        {
            "Images/button",
            "Images/BBEdges",
            "Images/BBMiddle",
            "Images/BBCorners",
            "Images/TitleCircle",
            "Images/bird",
            "Images/dog",
            "Images/cat",
            "Images/fish",
        };


        #endregion

        #region Initialization


        /// <summary>
        /// The main game constructor.
        /// </summary>
        public MathGame()
        {
            Content.RootDirectory = "Content";

            // Create a GraphicsDeviceManager for... managing graphics?
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1100;
            graphics.PreferredBackBufferHeight = 640;
        }



        /// <summary>
        /// Loads graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
            audio.LoadContent(Content);
            base.LoadContent();
        }

        /// <summary>
        /// Initializes all game elements
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            // Add InputState
            input = new InputState();
            input.Update();
            Services.AddService(typeof(InputState), input);

            // Create a SpriteBatch for darwing.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            //Create and add audioLibrary
            audio = new AudioLibrary();
            Services.AddService(typeof(AudioLibrary), audio);

            // Create and add the screens
            back = new Background(this); Components.Add(back);
            timesTable = new TimesTable(this); Components.Add(timesTable);
            menu = new Menu(this); Components.Add(menu);
            intro = new Intro(this); Components.Add(intro);
            test = new Test(this); Components.Add(test);
            help = new Help(this); Components.Add(help);
            menu.Show(input);

            base.Initialize();
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the Game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            //update input
            input.Update();

            //screens have their own update methods
            base.Update(gameTime);

            //make sure no screens want to exit
            CheckPulse();
        }

        #endregion

        #region Draw


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            // The real drawing happens inside the screens.
            base.Draw(gameTime);

            spriteBatch.End();
        }


        #endregion

        #region Screen Change Methods

        /// <summary>
        /// Checks if any screens want to exit, then opens the next appropriate screen
        /// </summary>
        public void CheckPulse()
        {
            int newScreen;
            foreach(GameScreen screen in Components)
            {
                newScreen = screen.IsExiting();

                switch (newScreen)
                {
                    case -1:
                        break;
                    case 0:
                        menu.Show(input);
                        break;
                    case 1:
                        intro.Show(input);
                        break;
                    case 2:
                        timesTable.Show(input);
                        break;
                    case 3:
                        test.Show(input);
                        break;
                    case 4:
                        help.Show(input);
                        break;
                    case 5:
                        Exit();
                        break;
                    default:
                        break;
                }
                        
            }
        }

        #endregion
    }
}
