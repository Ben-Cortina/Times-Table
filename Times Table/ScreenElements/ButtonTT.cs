#region File introduction
// ButtonTT.cs by Ben Cortina
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3
{
    /// <summary>
    /// A button class to be used in a Times Table
    /// </summary>
    public class ButtonTT : Project3.Button
    {
        #region Fields

        int buttonSize = 50;
        string problem, answer;

        #endregion

        #region Properties

        /// <summary>
        /// The text for when the button is in its default state.
        /// In this case, display the multiplication problem
        /// </summary>
        override public string NormalText
        {
            get { return problem; }
        }

        /// <summary>
        /// The multiplication problem represented by the button
        /// </summary>
        public string Problem
        {
            get { return problem; }
            set { problem = value; }
        }

        /// <summary>
        /// The answer to the problem presented by this button
        /// </summary>
        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_inFont">font in the button</param>
        /// <param name="p_outFont">font out of the button</param>
        /// <param name="p_rotation">rotation of everything drawn by this button</param>
        public ButtonTT(Vector2 p_location,int p_buttonSize, string p_problem, string p_answer, int p_row, int p_col,
            SpriteFont p_font, Texture2D p_texture, float p_rotation)
        {
            // TODO: Construct any child components here
            buttonSize = p_buttonSize;
            location = p_location;
            problem = p_problem;
            answer = p_answer;
            row = p_row;
            col = p_col;
            font = p_font;
            texture = p_texture;
            area = new Rectangle((int)location.X, (int)location.Y, buttonSize, buttonSize);
            rotation = p_rotation;
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the button
        /// </summary>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float fontScale, zBuffer;
            Vector2 textSize;
            Rectangle destination;
            Vector2 position;
            Vector2 origin = new Vector2(0, 0);
            Color buttonColor, fontColor;
            String text = problem;

            if (IsInFocus)
            {
                zBuffer = 0.2f;
                fontScale = 0.32f;
                destination = new Rectangle((int)area.X - 6, (int)area.Y - 6, area.Width + 12, area.Height + 12);
                buttonColor = Color.Wheat;
                fontColor = new Color(220, 228, 255);
                if (IsPressed)
                {
                    fontColor = new Color(255, 230, 255);
                    text = answer;
                    spriteBatch.DrawString(font, problem + " = " + answer,
                        new Vector2(700, 505), Color.White, rotation, origin, .5f, SpriteEffects.None, 0.1f);
                }
            }
            else
            {
                zBuffer = 0.4f;
                fontScale = 0.25f;
                destination = area;
                buttonColor = Color.White;
                fontColor = new Color(230, 255, 230);
            }
            textSize = font.MeasureString(text) * fontScale;
            position = location + new Vector2(area.Width / 2, area.Height / 2) - textSize / 2;

            spriteBatch.Draw(texture, destination, null, buttonColor, rotation,
                                       Vector2.Zero, SpriteEffects.None, zBuffer);

            spriteBatch.DrawString(font, text, position, fontColor, rotation,
                                       origin, fontScale, SpriteEffects.None, 0.1f);
        }

        #endregion
    }
}
