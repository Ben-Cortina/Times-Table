#region File Description
// Menu.cs By Ben Cortina
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Project3.Screens
{
    /// <summary>
    /// The Menu Screen, containing buttons that will activate other screens
    /// </summary>
    public class Menu : ScreenManagement.GameScreen
    {
        #region Fields

        List<ButtonMenu> menuEntries = new List<ButtonMenu>();
        int selectedEntry = 0;
        SpriteFont font, titleFont;
        String menuTitle;
        Texture2D titleCircle;
        Vector2 titlePosition;
        Vector2 titleOrigin;
        Rectangle circleDestination;
        float titleScale = 0.74f;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public Menu(Game game)
            :base(game)
        {
            menuTitle = "Times Table";
            SoundPlayed.Add(0);
            SoundPlayed.Add(0);
        }

        /// <summary>
        /// Loads Content for the menu screen
        /// </summary>
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Fonts/Chalk");
            titleCircle = Content.Load<Texture2D>("Images/TitleCircle");

        }

        /// <summary>
        /// Initializes elements used in the game
        /// </summary>
        public override void Initialize()
        {
            
            base.Initialize();

            menuEntries.Add(new ButtonMenu(new Vector2(Graphics.PreferredBackBufferWidth / 2, 270),
                "Intro",
                0,
                font
                ));
            menuEntries.Add(new ButtonMenu(new Vector2(Graphics.PreferredBackBufferWidth / 2, 335),
               "Practice",
                1,
                font
                ));
            menuEntries.Add(new ButtonMenu(new Vector2(Graphics.PreferredBackBufferWidth / 2, 400),
                "Test",
                2,
                font
                ));
            menuEntries.Add(new ButtonMenu(new Vector2(Graphics.PreferredBackBufferWidth / 2, 465),
                "Help",
                3,
                font
                ));
            menuEntries.Add(new ButtonMenu(new Vector2(Graphics.PreferredBackBufferWidth / 2, 530),
                "Exit",
                4,
                font
                ));
            // element positions
            titleScale = 0.74f;
            titlePosition = new Vector2(Graphics.PreferredBackBufferWidth / 2, 150);
            titleOrigin = (font.MeasureString(menuTitle)) / 2;
            circleDestination = new Rectangle((int)(titlePosition.X - titleCircle.Width / 2), (int)(titlePosition.Y - titleOrigin.Y +20), titleCircle.Width, titleCircle.Height);

        }
        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public void HandleInput(InputState input)
        {
            Vector2 mouseFocus = new Vector2(-1,-1);

            if (Input.IsMouseChanged())
                    mouseFocus = CheckFocus(menuEntries, Input.MouseState);

            if (mouseFocus.Y != -1)
                selectedEntry = (int)mouseFocus.Y;
            else
            {
                menuEntries[selectedEntry].IsPressed = false;
                menuEntries[selectedEntry].IsInFocus = false;
                // Move to the previous menu entry?
                if (input.IsMenuUp())
                {
                    selectedEntry--;

                    if (selectedEntry < 0)
                        selectedEntry = menuEntries.Count - 1;
                }

                // Move to the next menu entry?
                if (input.IsMenuDown())
                {
                    selectedEntry++;

                    if (selectedEntry >= menuEntries.Count)
                        selectedEntry = 0;
                }
                menuEntries[selectedEntry].IsInFocus = true;
            }

            // Check if the inFocus menu entry has been seletcted or if the player wants to cancel the menu

            if (input.IsMenuSelect())
            {
                menuEntries[selectedEntry].IsPressed = true;
                Exiting = true;
            }
            else if (input.IsMenuCancel())
            {
                Game.Exit();
            }
        }

        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            int selectedOld = selectedEntry;
            if(!Exiting)
            { 
               HandleInput(Input);
            }
            if (selectedOld !=selectedEntry)
                PlayMenuSound(gameTime);

            base.Update(gameTime);
        }


        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                menuEntries[i].Draw(gameTime, SpriteBatch);
            }

            //Draw Menu Title
            Color titleColor = new Color(255, 255, 255);

            SpriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            SpriteBatch.Draw(titleCircle, circleDestination,null,titleColor);

        }


        #endregion

        #region Methods

        /// <summary>
        /// Exits the game
        /// </summary>
        public void ExitGame()
        {
            Game.Exit();
        }

        /// <summary>
        /// Checks whether or not the mouse is over a button
        /// </summary>
        /// <returns>returns the row and column of the focused button</returns>
        public Vector2 CheckFocus(List<ButtonMenu> buttons, MouseState mouseState)
        {
            Vector2 focus = new Vector2(-1,-1);
            foreach (Button b in buttons)
            {
                b.IsPressed = false;
                if (b.CheckFocus(mouseState.X, mouseState.Y))
                {
                    focus.Y = b.Row;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        b.IsPressed = true;
                        Exiting = true;
                    }
                }
            }
            return focus;
        }

        /// <summary>
        /// Checks whether or not the Screen wants to exit
        /// </summary>
        public override int IsExiting()
        {
            if (Exiting)
            {
                Audio.Write.Play();
                ExitScreen();
                Exiting = false;
                return selectedEntry+1;
            }
            else
                return -1;
        }

        #endregion
    }
}
