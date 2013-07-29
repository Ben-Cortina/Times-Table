#region File Description
// Help.cs By Ben Cortina
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
    #region enums

    /// <summary>
    /// Keeps track of which Help I am currently displaying
    /// </summary>
    public enum HelpSection
    {
        General,
        Practice,
        Test,
    }

    #endregion

    /// <summary>
    /// The Help Screen, explains the program and its controls
    /// </summary>
    public class Help : ScreenManagement.GameScreen
    {
        #region Fields

        List<List<string>> helpText;
        List<List<Vector2>> helpTextLocation;
        List<List<Color>> helpTextColor;
        List<string> helpTitle;

        List<ButtonMenu> arrows;

        HelpSection current;

        SpriteFont font;

        Vector2 titlePosition;
        Vector2 titleOrigin;
        float titleScale = 0.74f;

        float fontScale;


        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public Help(Game game)
            : base(game)
        {
            Content = game.Content;
            current = HelpSection.General;
        }

        /// <summary>
        /// Initializes all Lists, thus creating all the text for the help screens
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            //fontSize
            fontScale = 0.24f;

            helpTitle = new List<string>();
            helpText = new List<List<string>>();
            helpTextLocation = new List<List<Vector2>>();
            helpTextColor = new List<List<Color>>();
            
            //Strings
            List<string> tempText; 
            List<Vector2> tempLocation;
            List<Color> tempColor;
            
            //create the text, text location, and text color for each help section
            #region General Help

            tempText = new List<string>();
            tempLocation = new List<Vector2>();
            tempColor = new List<Color>();

            helpTitle.Add("Help: General");

            tempText.Add("All sections of this game share the same controls. They are as follows: "); //0
            tempText.Add("Select button or menu option:"); //1
            tempText.Add("     Keyboad and Mouse: "); //2
            tempText.Add("Left Mouse Button, Space, or Enter"); //3
            tempText.Add("     Gamepad: "); //4
            tempText.Add("A or Start"); //5
            tempText.Add("Navigate buttons or menu options:"); //6
            tempText.Add("     Keyboad and Mouse: "); //7
            tempText.Add("Arrow Keys or Mouse"); //8
            tempText.Add("     Gamepad: "); //9
            tempText.Add("Left Thumbstick or D-Pad"); //10
            tempText.Add("Exit Section:"); //11
            tempText.Add("     Keyboad and Mouse: "); //12
            tempText.Add("Esc or Q"); //13
            tempText.Add("     Gamepad: "); //14
            tempText.Add("B or Back"); //15
            
            helpText.Add(tempText);

            tempLocation.Add(new Vector2(75, 150)); //0
            tempLocation.Add(new Vector2(75, tempLocation[0].Y + font.MeasureString(tempText[0]).Y * fontScale)); //1
            tempLocation.Add(new Vector2(75, tempLocation[1].Y + font.MeasureString(tempText[1]).Y * fontScale)); //2
            tempLocation.Add(new Vector2(tempLocation[2].X + font.MeasureString(tempText[2]).X * fontScale, tempLocation[2].Y)); //3
            tempLocation.Add(new Vector2(75, tempLocation[3].Y + font.MeasureString(tempText[3]).Y * fontScale)); //4
            tempLocation.Add(new Vector2(tempLocation[4].X + font.MeasureString(tempText[4]).X * fontScale, tempLocation[4].Y)); //5
            tempLocation.Add(new Vector2(75, tempLocation[5].Y + font.MeasureString(tempText[5]).Y * fontScale)); //6
            tempLocation.Add(new Vector2(75, tempLocation[6].Y + font.MeasureString(tempText[6]).Y * fontScale)); //7
            tempLocation.Add(new Vector2(tempLocation[7].X + font.MeasureString(tempText[7]).X * fontScale, tempLocation[7].Y)); //8
            tempLocation.Add(new Vector2(75, tempLocation[8].Y + font.MeasureString(tempText[8]).Y * fontScale)); //9
            tempLocation.Add(new Vector2(tempLocation[9].X + font.MeasureString(tempText[9]).X * fontScale, tempLocation[9].Y)); //10
            tempLocation.Add(new Vector2(75, tempLocation[10].Y + font.MeasureString(tempText[10]).Y * fontScale)); //11
            tempLocation.Add(new Vector2(75, tempLocation[11].Y + font.MeasureString(tempText[11]).Y * fontScale)); //12
            tempLocation.Add(new Vector2(tempLocation[12].X + font.MeasureString(tempText[12]).X * fontScale, tempLocation[12].Y)); //13
            tempLocation.Add(new Vector2(75, tempLocation[13].Y + font.MeasureString(tempText[13]).Y * fontScale)); //14
            tempLocation.Add(new Vector2(tempLocation[14].X + font.MeasureString(tempText[14]).X * fontScale, tempLocation[14].Y)); //15

            helpTextLocation.Add(tempLocation);

            tempColor.Add(Color.Honeydew); //0
            tempColor.Add(Color.LightGoldenrodYellow); //1
            tempColor.Add(Color.PeachPuff); //2
            tempColor.Add(Color.Salmon); //3
            tempColor.Add(Color.PeachPuff); //4
            tempColor.Add(Color.Salmon); //5
            tempColor.Add(Color.LightGoldenrodYellow); //6
            tempColor.Add(Color.PeachPuff); //7
            tempColor.Add(Color.Salmon); //8
            tempColor.Add(Color.PeachPuff); //9
            tempColor.Add(Color.Salmon);//10
            tempColor.Add(Color.LightGoldenrodYellow); //11
            tempColor.Add(Color.PeachPuff); //12
            tempColor.Add(Color.Salmon); //13
            tempColor.Add(Color.PeachPuff); //14
            tempColor.Add(Color.Salmon); //15

            helpTextColor.Add(tempColor);


            #endregion

            #region Practice Help

            tempText = new List<string>();
            tempLocation = new List<Vector2>();
            tempColor = new List<Color>();

            helpTitle.Add("Help: Practice");

            tempText.Add("The practice session provides an interactive times table which can help teach\n" + //0
                          "multiplication.");
            tempText.Add("Highlighting one of the multiplication problems will display a number of objects\n" + //1
                          "equal to the answer.");
            tempText.Add("Clicking on a problem will show the answer inside the selected button and display\n" + //2
                          "the full equation in the bottom right");

            helpText.Add(tempText);

            tempLocation.Add(new Vector2(75, 150)); //0
            tempLocation.Add(new Vector2(75, tempLocation[0].Y + font.MeasureString(tempText[0]).Y * fontScale)); //1
            tempLocation.Add(new Vector2(75, tempLocation[1].Y + font.MeasureString(tempText[1]).Y * fontScale)); //2

            helpTextLocation.Add(tempLocation);

            tempColor.Add(Color.LightGoldenrodYellow); // 0
            tempColor.Add(Color.Honeydew); //1
            tempColor.Add(Color.Honeydew); //2

            helpTextColor.Add(tempColor);

            #endregion

            #region Test Help

            tempText = new List<string>();
            tempLocation = new List<Vector2>();
            tempColor = new List<Color>();

            helpTitle.Add("Help: Test");

            tempText.Add("The test section allows you to test your multiplication skills through a quiz.\n"); //0
            tempText.Add("A multiplication problem will be presented and you are asked to answer it. You"); //1
            tempText.Add("may use the the supplied input buttons, or the ");//2
            tempText.Add("0 - 9"); //3
            tempText.Add(" keys on your keyboard."); //4

            helpText.Add(tempText);

            tempLocation.Add(new Vector2(75, 150)); //0
            tempLocation.Add(new Vector2(75, tempLocation[0].Y + font.MeasureString(tempText[0]).Y * fontScale)); //1
            tempLocation.Add(new Vector2(75, tempLocation[1].Y + font.MeasureString(tempText[1]).Y * fontScale)); //2
            tempLocation.Add(new Vector2(tempLocation[2].X + font.MeasureString(tempText[2]).X * fontScale, tempLocation[2].Y)); //3
            tempLocation.Add(new Vector2(tempLocation[3].X + font.MeasureString(tempText[3]).X * fontScale, tempLocation[3].Y)); //4

            helpTextLocation.Add(tempLocation);

            tempColor.Add(Color.LightGoldenrodYellow); //0
            tempColor.Add(Color.Honeydew); //1
            tempColor.Add(Color.Honeydew); //2
            tempColor.Add(Color.Salmon); //3
            tempColor.Add(Color.Honeydew); //4

            helpTextColor.Add(tempColor);

            #endregion

            //title Elements
            titleScale = 0.74f;
            titlePosition = new Vector2(Graphics.PreferredBackBufferWidth / 2, 100);
            titleOrigin = (font.MeasureString(helpTitle[(int)current])) / 2; 

            //Help Screen Navigation Buttons
            ButtonMenu button;
            arrows = new List<ButtonMenu>();
            button = new ButtonMenu(new Vector2(0,0),
                "",
                0,
                font
                );
            button.fontSize = 0.2f;
            button.TextColor = Color.Aquamarine;
            arrows.Add(button);
            button = new ButtonMenu(new Vector2(0,0),
                helpTitle[(int)current+1],
                1,
                font
                );
            button.fontSize = 0.2f;
            button.TextColor = Color.Aquamarine;
            arrows.Add(button);
            arrows[1].Location = new Vector2(Graphics.PreferredBackBufferWidth - (font.MeasureString(helpTitle[(int)current + 1]).X * 0.2f) / 2 - 75,
                                  Graphics.PreferredBackBufferHeight - (font.MeasureString(helpTitle[(int)current + 1]).Y * 0.2f) / 3 - 75);
            arrows[1].NormalText = helpTitle[(int)current + 1];


        }

        protected override void LoadContent()
        {
            base.LoadContent();

            font = Content.Load<SpriteFont>("Fonts/chalk");
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

            if (Input.IsMouseChanged())
                mouseFocus = CheckFocus(arrows, input);
            if (mouseFocus.Y != -1)
            {
                if (arrows[(int)mouseFocus.Y].IsPressed)
                {
                    if (mouseFocus.Y == 0)
                        PreviousSection();
                    else
                        NextSection();
                }
            }
            else
            {
                // Move to the next section?
                if (input.IsMenuRight())
                {
                    NextSection();
                }

                // Move to the previous section?
                if (input.IsMenuLeft())
                {
                    PreviousSection();
                }
            }

            // Check if the player wants to cancel the menu
            if (input.IsMenuCancel())
            {
                Exiting = true;
            }
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            HandleInput(Input);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the Text.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.DrawString(font, helpTitle[(int)current], titlePosition , Color.White, 0,
                                        titleOrigin, titleScale, SpriteEffects.None, 0);

            foreach (ButtonMenu b in arrows)
                b.Draw(gameTime, SpriteBatch);

            for (int i = 0; i < helpText[(int)current].Count; i++)
                SpriteBatch.DrawString(font, helpText[(int)current][i], helpTextLocation[(int)current][i], helpTextColor[(int)current][i],
                                        0, new Vector2(0, 0), fontScale, SpriteEffects.None, 0);
        }


        #endregion

        #region Methods

        /// <summary>
        /// Goes to next help section
        /// </summary>
        public void NextSection()
        {
            if (current != HelpSection.Test)
                current++;
            if (current == HelpSection.Test)
            {
                arrows[1].NormalText = "";
            }
            else
            {

                titlePosition = new Vector2(Graphics.PreferredBackBufferWidth / 2, 100);
                titleOrigin = (font.MeasureString(helpTitle[(int)current])) / 2;

                arrows[1].Location = new Vector2(Graphics.PreferredBackBufferWidth - (font.MeasureString(helpTitle[(int)current + 1]).X * 0.2f) / 2 - 75,
                                  Graphics.PreferredBackBufferHeight - (font.MeasureString(helpTitle[(int)current + 1]).Y * 0.2f / 3) - 75);
                arrows[1].NormalText = helpTitle[(int)current + 1];

            }
            arrows[0].Location = new Vector2(75 + font.MeasureString(helpTitle[(int)current - 1]).X * 0.2f / 2,
                                      Graphics.PreferredBackBufferHeight - (font.MeasureString(helpTitle[(int)current - 1]).Y * 0.2f / 3) - 75);
            arrows[0].NormalText = helpTitle[(int)current - 1];

        }

        /// <summary>
        /// Goes to previous help section
        /// </summary>
        public void PreviousSection()
        {
            if (current != HelpSection.General)
                current--;
            if (current == HelpSection.General)
                arrows[0].NormalText = "";
            else
            {
                titlePosition = new Vector2(Graphics.PreferredBackBufferWidth / 2, 100);
                titleOrigin = (font.MeasureString(helpTitle[(int)current])) / 2;

                arrows[0].Location = new Vector2(75 + font.MeasureString(helpTitle[(int)current - 1]).X * 0.2f / 2,
                                                      Graphics.PreferredBackBufferHeight - (font.MeasureString(helpTitle[(int)current - 1]).Y * 0.2f) / 3 - 75);
                arrows[0].NormalText = helpTitle[(int)current - 1];

            }
            arrows[1].Location = new Vector2(Graphics.PreferredBackBufferWidth - (font.MeasureString(helpTitle[(int)current + 1]).X * 0.2f) / 2 - 75,
                                              Graphics.PreferredBackBufferHeight - (font.MeasureString(helpTitle[(int)current + 1]).Y * 0.2f) / 3 - 75);
            arrows[1].NormalText = helpTitle[(int)current + 1];
        }

        /// <summary>
        /// Checks whether or not the mouse is over a button
        /// </summary>
        /// <returns>returns the row and column of the focused button</returns>
        public Vector2 CheckFocus(List<ButtonMenu> buttons, InputState input)
        {
            Vector2 focus = new Vector2(-1, -1);
            foreach (Button b in buttons)
            {
                b.IsPressed = false;
                if (b.CheckFocus(input.MouseState.X, input.MouseState.Y))
                {
                    focus.Y = b.Row;
                    focus.X = b.Col;
                    if (input.IsNewMouseReleased())
                    {
                        b.IsPressed = true;
                    }
                }
            }
            return focus;
        }

        #endregion
    }
}
