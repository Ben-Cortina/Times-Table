#region File Description
// Intro.cs By Ben Cortina
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3.Screens
{

    /// <summary>
    /// The Intro Screen, explains the programs purpose
    /// </summary>
    public class Intro : ScreenManagement.GameScreen
    {
        #region Fields

        List<string> introText;
        List<Vector2> introTextLocation;
        List<Color> introTextColor;

        SpriteFont font;

        float fontScale;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public Intro(Game game)
            : base(game)
        {
            Content = game.Content;
        }

        /// <summary>
        /// Initializes all Lists, thus creating all the text for the intro screen
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            //fontSize
            fontScale = 0.24f;

            //Strings
            introText = new List<string>();
            introText.Add("Multiplication is a fast calculation method for addition. Multiplication of two\n" + //0
                           "numbers is the same as adding the first number several times, which is\n" +
                           "determined by the second number. For Example,\n");
            introText.Add("4   3\n"); //1
            introText.Add("x\n"); //2
            introText.Add("is the same as\n"); //3
            introText.Add("4   4   4\n"); //4
            introText.Add("+\n"); //5
            introText.Add("+\n"); //6
            introText.Add("The result is \n"); //7
            introText.Add("12\n"); //8
            introText.Add("A times table is a table that helps you memorize multiplications of numbers less");//9
            introText.Add("than 10. Select ");//10
            introText.Add("Practice "); //11
            introText.Add("at the menu to learn and memorize the times table."); //12
            introText.Add("After that, you can test your multiplication skills by selecting "); //13
            introText.Add("Test "); //14
            introText.Add("at the menu."); //15

            ///Locations... What a mess...
            introTextLocation = new List<Vector2>();

            introTextLocation.Add(new Vector2(75, 75));   //0
            introTextLocation.Add(new Vector2(75, introTextLocation[0].Y + font.MeasureString(introText[0]).Y * fontScale - fontScale*100));   //1
            introTextLocation.Add(new Vector2(introTextLocation[1].X + font.MeasureString("4 ").X * fontScale, introTextLocation[1].Y));   //2
            introTextLocation.Add(new Vector2(75, introTextLocation[2].Y + font.MeasureString(introText[2]).Y * fontScale - fontScale * 100));   //3
            introTextLocation.Add(new Vector2(75, introTextLocation[3].Y + font.MeasureString(introText[3]).Y * fontScale - fontScale * 100));  //4
            introTextLocation.Add(new Vector2(introTextLocation[4].X + font.MeasureString("4 ").X * fontScale, introTextLocation[4].Y));   //5
            introTextLocation.Add(new Vector2(introTextLocation[5].X + font.MeasureString("+ 4 ").X * fontScale, introTextLocation[5].Y));   //6
            introTextLocation.Add(new Vector2(75, introTextLocation[6].Y + font.MeasureString(introText[6]).Y * fontScale - fontScale * 100));  //7
            introTextLocation.Add(new Vector2(introTextLocation[7].X + font.MeasureString(introText[7]).X * fontScale, introTextLocation[7].Y)); //8
            introTextLocation.Add(new Vector2(75, introTextLocation[8].Y + font.MeasureString(introText[8]).Y * fontScale - fontScale * 100)); //9
            introTextLocation.Add(new Vector2(75, introTextLocation[9].Y + font.MeasureString(introText[9]).Y * fontScale)); //10
            introTextLocation.Add(new Vector2(introTextLocation[10].X + font.MeasureString(introText[10]).X * fontScale, introTextLocation[10].Y)); //11
            introTextLocation.Add(new Vector2(introTextLocation[11].X + font.MeasureString(introText[11]).X * fontScale, introTextLocation[11].Y)); //12
            introTextLocation.Add(new Vector2(75, introTextLocation[12].Y + font.MeasureString(introText[10]).Y * fontScale)); //13
            introTextLocation.Add(new Vector2(introTextLocation[13].X + font.MeasureString(introText[13]).X * fontScale, introTextLocation[13].Y)); //14
            introTextLocation.Add(new Vector2(introTextLocation[14].X + font.MeasureString(introText[14]).X * fontScale, introTextLocation[14].Y)); //15

            //Colors
            introTextColor = new List<Color>();

            introTextColor.Add(Color.White); //0
            introTextColor.Add(new Color(255,210,150)); //1 light orangeish
            introTextColor.Add(new Color(200, 255, 200));//2 light Green
            introTextColor.Add(Color.White); //3
            introTextColor.Add(new Color(255, 210, 150)); //4
            introTextColor.Add(new Color(200, 255, 200)); //5
            introTextColor.Add(new Color(200, 255, 200)); //6
            introTextColor.Add(Color.White); //7
            introTextColor.Add(new Color(255,210,150)); //8
            introTextColor.Add(Color.White); //9
            introTextColor.Add(Color.White); //10
            introTextColor.Add(new Color(200, 220, 255)); //11
            introTextColor.Add(Color.White); //12
            introTextColor.Add(Color.White); //13
            introTextColor.Add(new Color(200, 220, 255)); //14
            introTextColor.Add(Color.White); //15

        }

        /// <summary>
        /// loads content needed for this screen
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            font = Content.Load<SpriteFont>("Fonts/chalk");
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Checks for exit request
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (Input.IsMenuCancel())
                Exiting = true;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the Text.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            for (int i = 0; i < introText.Count; i++)
                SpriteBatch.DrawString(font, introText[i], introTextLocation[i], introTextColor[i],
                                        0, new Vector2(0, 0), fontScale, SpriteEffects.None, 0);
        }


        #endregion
    }
}
