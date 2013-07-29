#region File introduction
// ButtonCalc.cs by Ben Cortina
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
    public class ButtonCalc : Project3.Button
    {
        #region Fields

        int buttonSize;
        int number;
        string text;
        double timeOld, elapsedTime;
        Rectangle destination;

        #endregion

        #region Properties

        /// <summary>
        /// The text for when the button is in its default state.
        /// In this case, display the multiplication problem
        /// </summary>
        public override string NormalText
        {
            get { return text; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_inFont">font in the button</param>
        /// <param name="p_outFont">font out of the button</param>
        /// <param name="p_rotation">rotation of everything drawn by this button</param>
        public ButtonCalc(Vector2 p_location, int p_buttonSize, String p_text, int p_number, int p_row, int p_col, 
            SpriteFont p_font, Texture2D p_texture, float p_rotation)
        {
            // TODO: Construct any child components here
            buttonSize = p_buttonSize;
            location = p_location;
            text = p_text;
            number = p_number;
            row = p_row;
            col = p_col;
            font = p_font;
            texture = p_texture;
            area = new Rectangle((int)location.X, (int)location.Y, buttonSize, buttonSize);
            rotation = p_rotation;
            elapsedTime = 0;
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
            Vector2 position;
            Vector2 origin = new Vector2(0, 0);
            Color buttonColor, fontColor;
            double time = gameTime.TotalGameTime.TotalSeconds;
            double elapsedRealTime = time - timeOld;
            Random rand = new Random();
            buttonColor = Color.White;

            fontScale = 0.25f;
            if (IsInFocus)
            {
                zBuffer = 0.2f;
                spriteBatch.Draw(texture, area, null, buttonColor, rotation,
                                       Vector2.Zero, SpriteEffects.None, zBuffer);
                fontColor = new Color(220, 228, 255);
                elapsedTime += elapsedRealTime;
                if (elapsedTime > .1)
                {
                    destination = new Rectangle((int)area.X + rand.Next(-2,3), (int)area.Y + rand.Next(-2,3), area.Width, area.Height);
                    elapsedTime = 0;
                }
                if (IsPressed)
                {
                    buttonColor = Color.Wheat;
                    destination = new Rectangle((int)area.X + 6, (int)area.Y + 6, area.Width - 12, area.Height - 12);
                    fontColor = new Color(255, 230, 255);
                    fontScale = 0.20f;
                }
            }
            else
            {
                zBuffer = 0.4f;
                fontScale = 0.25f;
                destination = area;
                fontColor = new Color(230, 255, 230);
            }
            timeOld = gameTime.TotalGameTime.TotalSeconds;
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
