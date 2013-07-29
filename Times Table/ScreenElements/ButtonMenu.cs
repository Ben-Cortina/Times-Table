#region File introduction
// ButtonMenu.cs by Ben Cortina
#endregion

#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3
{
    /// <summary>
    /// Button for the Menu
    /// </summary>
    public class ButtonMenu : Project3.Button
    {
        #region Fields

        Vector2 textSize;
        float focusEffect;
        string text;
        double timeOld, elapsedTime;
        float fontScale;
        Vector2 origin;
        Color textColor;

        /// <summary>
        /// ...Color of text
        /// </summary>
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        /// <summary>
        /// ...Size of text, causes button to resize itself as well
        /// </summary>
        public float fontSize
        {
            get { return fontScale; }
            set { fontScale = value; UpdateSize();
            }
        }

        /// <summary>
        /// Text displayed on button
        /// </summary>
        public override string NormalText
        {
            get { return text; }
            set { text = value; UpdateSize(); }
        }

        /// <summary>
        /// Indicates the Location of the Button on the screen
        /// </summary>
        public override Vector2 Location
        {
            get { return base.Location; }
            set { base.Location = value; UpdateSize(); }
        }

        #endregion

        #region  Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_location">location of button</param>
        /// <param name="p_text">string to be displayed on button</param>
        /// <param name="p_row">row the button is in</param>
        /// <param name="p_inFont">font the text will display in</param>
        public ButtonMenu(Vector2 p_location, String p_text, int p_row, SpriteFont p_font)
        {
            // TODO: Construct any child components here
            fontScale = 0.5f;
            text = p_text;
            location = p_location;
            row = p_row;
            font = p_font;
            textSize = font.MeasureString(text) * fontScale;
            focusEffect = 0;
            UpdateSize();
            timeOld = 0;
            elapsedTime = 0;
            rotation = 0;
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draw the button
        /// </summary>
        /// <param name="spritebatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update/Get Times
            double time = gameTime.TotalGameTime.TotalSeconds;
            double elapsedRealTime = time - timeOld;
            Random rand = new Random();
            Vector2 loc = new Vector2(location.X - textSize.X / 2, location.Y - textSize.Y / 2);

            //Allow for smooth fade in and out
            if (IsInFocus)
            {
                spriteBatch.DrawString(font, text, loc, new Color(220, 255, 255), 0,
                                   new Vector2(0, 0), fontScale, SpriteEffects.None, 0.1f);
                elapsedTime += elapsedRealTime;
                if (elapsedTime > .1)
                {
                    origin = new Vector2(rand.Next(-2, 3), rand.Next(-2, 3));
                    elapsedTime = 0;
                }
            }
            else
            {
                origin = new Vector2(0,0);
                rotation = 0;
            }
            timeOld = gameTime.TotalGameTime.TotalSeconds;
            focusEffect = Math.Abs((float)Math.Sin((elapsedTime) * 3));


            // Pulsate the size of the selected menu entry.
            int blue = (int)(focusEffect * 255) - 50;
            
            spriteBatch.DrawString(font, text, loc, new Color(220, 255, 255), rotation,
                                   origin, fontScale, SpriteEffects.None, 0);
        }

        #endregion

        #region UpdateSize

        /// <summary>
        /// Updates the size of the button, this needs to be called everytime the text is changed
        /// </summary>
        public void UpdateSize()
        {
            Vector2 loc;
            textSize = font.MeasureString(text) * fontScale;
            loc = new Vector2(location.X - textSize.X / 2, location.Y - textSize.Y / 3);
            area = new Rectangle(
            (int)(loc.X),
            (int)(loc.Y),
            (int)(textSize.X),
            (int)(textSize.Y/2));
            origin = new Vector2(0,0);
        }

        #endregion
    }
}
