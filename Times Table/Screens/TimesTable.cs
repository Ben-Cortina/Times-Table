#region File introduction
// TimesTable.cs by Ben Cortina
#endregion

#region Using Statements

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3.Screens
{
    /// <summary>
    /// This is the Practice screen
    /// It displays an interactive timestable
    /// </summary>
    public class TimesTable : ScreenManagement.GameScreen
    {
        #region Fields

        ButtonTable buttonTable;
        IconTable iconTable;

        Vector2 bTableLocation;
        Vector2 iTableLocation;

        Vector2 focus;
        Vector2 oldFocus;

        Texture2D buttonTexture;
        List<Texture2D> iconTextures;

        SpriteFont font;
        SpriteFont fontBold;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public TimesTable(Game game) 
            : base(game)
        {
            SoundPlayed.Add(0);
            SoundPlayed.Add(0);
        }

        /// <summary>
        /// LoadContent will be load the content for this screen.
        /// </summary>
        protected override void LoadContent()
        {           
            // Create a new SpriteBatch, which can be used to draw textures.
            font = Content.Load<SpriteFont>("Fonts/Chalk");
            buttonTexture = Content.Load<Texture2D>("Images/button");
            iconTextures = new List<Texture2D>(new Texture2D[]{
                Content.Load<Texture2D>("Images/bird"),
                Content.Load<Texture2D>("Images/dog"),
                Content.Load<Texture2D>("Images/cat"),
                Content.Load<Texture2D>("Images/fish"),
            });


        }

        /// <summary>
        /// Initializes Screen Elements
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            //set focus
            oldFocus = new Vector2(1, 1);
            focus = new Vector2(1, 1);

            //set locations of elements
            bTableLocation = new Vector2(60, 60);
            iTableLocation = new Vector2(590, 60);

            //9x9 table
            buttonTable = new ButtonTable(buttonTexture, font, bTableLocation, 9, 9, "TT", 57);

            iconTable = new IconTable(iconTextures, iTableLocation, 9, 9);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public void HandleInput(InputState input)
        {
            Vector2 mouseFocus = new Vector2(-1, -1);

            if (input.IsMouseChanged())
                    mouseFocus = buttonTable.CheckFocus(input);
            if (mouseFocus.X != -1)
                focus = mouseFocus;
            else
            {
                buttonTable.SetFocus(focus, false);
                // Move up?
                if (input.IsMenuUp())
                {
                    focus = new Vector2(focus.X, focus.Y - 1);

                    if (focus.Y < 1)
                        focus.Y = buttonTable.Rows;
                }

                // Move down?
                if (input.IsMenuDown())
                {
                    focus = new Vector2(focus.X, focus.Y + 1);

                    if (focus.Y > buttonTable.Rows)
                        focus.Y = 1;
                }

                // Move left?
                if (input.IsMenuLeft())
                {
                    focus = new Vector2(focus.X - 1, focus.Y);

                    if (focus.X < 1)
                        focus.X = buttonTable.Cols;
                }

                // Move right?
                if (input.IsMenuRight())
                {
                    focus = new Vector2(focus.X + 1, focus.Y);

                    if (focus.X > buttonTable.Cols)
                        focus.X = 1;
                }
                buttonTable.SetFocus(focus, true);
            }
                

            // Check if the button is being pressed or if the user
            // is asking to exit the screen.

            if (input.IsMenuSelect())
            {
                buttonTable.TogglePressed(focus);
            }
            else if (input.IsMenuCancel())
            {
                Exiting = true;
            }
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            bool pressed = buttonTable.IsPressed(focus);

            //Check Input
            if (!Exiting)
            {
                HandleInput(Input);
            }

            if (focus != oldFocus)
            {
                buttonTable.SetPressed(oldFocus, false);
                iconTable.newFocus(focus);
                Audio.MenuSound.Play();
            }

            oldFocus = focus;

            //play the sound of a person writing the answer
            if (buttonTable.IsPressed(focus) != pressed && !pressed)
              PlayWriteSound(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            buttonTable.Draw(gameTime, SpriteBatch);
            iconTable.Draw(SpriteBatch);

        }

        #endregion
    }
}
