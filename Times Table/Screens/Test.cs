#region File introduction
// Test.cs by Ben Cortina
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
    /// This is the Test Screen
    /// a problem is displayed and the user
    /// is asked to answer it
    /// </summary>
    public class Test : ScreenManagement.GameScreen
    {
        #region Fields

        ButtonTable buttonTable;

        Vector2 bTableLocation;
        Vector2 problemLocation;
        Vector2 userAnswerLocation;
        Vector2 correctLocation;
        Vector2 totalLocation;
        Vector2 problemOrigin;
        Vector2 feedbackLocation;
        Vector2 feedbackOrigin;

        Vector2 focus;
        Vector2 oldFocus;

        Texture2D buttonTexture;

        SpriteFont font;

        String problem;
        String userAnswer;
        String rightAnswer;
        String feedback;

        Color feedbackColor;

        double timer;
        double feedbackDuration;

        int correct;
        int total;

        bool answered;
        bool buttonPressed;

        Random rand;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public Test(Game game)
            : base(game)
        {
            SoundPlayed.Add(0);
            SoundPlayed.Add(0);
            SoundPlayed.Add(0);
            SoundPlayed.Add(0);

            answered = false;
            feedbackDuration = 1;
            feedback = "";
        }

        /// <summary>
        /// LoadContent will be load the content for this screen.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            font = Content.Load<SpriteFont>("Fonts/Chalk");
            buttonTexture = Content.Load<Texture2D>("Images/button");


        }

        /// <summary>
        /// Initializes Screen Elements
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            //Initialize random for randomness.
            rand = new Random();

            //Set problem and answer
            NewProblem();

            //set focus
            oldFocus = new Vector2(1, 1);
            focus = new Vector2(1, 1);

            //location of elements
            bTableLocation = new Vector2(200, 250);
            problemLocation = new Vector2(Graphics.PreferredBackBufferWidth / 2, 100);
            userAnswerLocation = new Vector2(Graphics.PreferredBackBufferWidth / 2, 100);
            correctLocation = new Vector2(600, 400);
            totalLocation = new Vector2(600, 450);
            feedbackLocation = new Vector2((Graphics.PreferredBackBufferWidth) *3 /4 -125, 300);

            //User input devive
            buttonTable = new ButtonTable(buttonTexture, font, bTableLocation, 4, 3, "Calc", 70);

            feedbackColor = Color.Green;
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

            // Check NumPad and Number Keys for manual number entering
            String keyString;
            Keys key = new Keys();
            int keyboardInt = -1;
            for (int i = 0; i < 10; i++)
            {
                keyString = "D" + i.ToString();
                key = (Keys)Enum.Parse(typeof(Keys), keyString);
                if (input.IsNewKeyPress(key))
                    keyboardInt = i;
                keyString = "NumPad" + i.ToString();
                key = (Keys)Enum.Parse(typeof(Keys), keyString);
                if (input.IsNewKeyPress(key))
                    keyboardInt = i;
            }

            if (keyboardInt != -1)
                AddToAnswer(keyboardInt.ToString());

            if ( input.IsMouseChanged() ||input.IsMousePressed())
                mouseFocus = buttonTable.CheckFocus(input);

            if (mouseFocus.X != -1)
            {
                focus = mouseFocus;
                if (input.IsNewMouseReleased())
                    HandleButtonInput();
            }
            else
            {
                buttonTable.SetPressed(focus, false);
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
                HandleButtonInput();
                buttonTable.SetPressed(focus, true);
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
            if (!Exiting && !answered)
                HandleInput(Input);

            if (answered)
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timer > feedbackDuration)
            {
                answered = false;
                timer = 0;
                NewProblem();
            }

            if (focus != oldFocus)
            {
                buttonTable.SetPressed(oldFocus, false);
                Audio.MenuSound.Play();
            }

            oldFocus = focus;

            ////play the sound of a person writing the answer
            //if (buttonTable.IsPressed(focus) != pressed && !pressed)
            //    PlayWriteSound(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Vector2 problemOrigin = new Vector2(font.MeasureString(problem).X, 0);
            Vector2 origin = new Vector2(0, 0);

            String correctString = "Correct: " + correct.ToString();
            String totalString = "Total: " + total.ToString();

            buttonTable.Draw(gameTime, SpriteBatch);

            //Draw Problem and user Answer
            SpriteBatch.DrawString(font, problem, problemLocation, Color.White, 0,
                                   problemOrigin, 0.8f, SpriteEffects.None, 0);
            SpriteBatch.DrawString(font, userAnswer, userAnswerLocation, Color.White, 0,
                                   origin, 0.8f, SpriteEffects.None, 0);
            //Draw Score
            SpriteBatch.DrawString(font, correctString, correctLocation, Color.White, 0,
                                   origin, 0.5f, SpriteEffects.None, 0);
            SpriteBatch.DrawString(font, totalString, totalLocation, Color.White, 0,
                                   origin, 0.5f, SpriteEffects.None, 0);

            //Draw Feedback if answered
            if (answered)
            {
                SpriteBatch.DrawString(font, feedback, feedbackLocation, feedbackColor, 0,
                                   feedbackOrigin/2, 1f, SpriteEffects.None, 0);
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Apends new number to the string
        /// </summary>
        /// <param name="p_number"></param>
        public void AddToAnswer(String p_number)
        {
            int stringLength= (int)((font.MeasureString(userAnswer+p_number) + userAnswerLocation).X);
            if(stringLength < Graphics.PreferredBackBufferWidth + 50)
                userAnswer += p_number;

        }

        /// <summary>
        /// Checks the users answer
        /// </summary>
        public void CheckAnswer()
        {
            if (userAnswer == rightAnswer)
            {
                correct++;
                feedback = "Good Job!";
                feedbackColor = Color.Green;
                Audio.Right.Play();
            }
            else
            {
                feedback = "X";
                feedbackColor = Color.Red;
                Audio.Wrong.Play();
            }
            feedbackOrigin = font.MeasureString(feedback);
            answered = true;
            total++;
        }

        /// <summary>
        /// Creates a new problem and answer
        /// </summary>
        public void NewProblem()
        {
            //create new problem
            int a, b;
            a = rand.Next(1,10);
            b = rand.Next(1,10);
            problem = a.ToString() + " x " + b.ToString() + " = ";
            rightAnswer = (a * b).ToString();
            userAnswer = "";
            problemOrigin = new Vector2(font.MeasureString(problem).X, 0);
        }

        /// <summary>
        /// Decides what to do with the selected button
        /// </summary>
        public void HandleButtonInput()
        {
            int focusNumber = (int)(buttonTable.Cols * buttonTable.Rows - ((focus.Y - 1) * buttonTable.Cols + buttonTable.Rows - focus.X) - 2);
            switch (focusNumber)
            {
                case -2:
                    userAnswer = "";
                    break;
                case -1:
                    AddToAnswer("0");
                    break;
                case 0:
                    CheckAnswer();
                    break;
                default:
                    AddToAnswer(focusNumber.ToString());
                    break;
            }
        }


        #endregion
    }
}
