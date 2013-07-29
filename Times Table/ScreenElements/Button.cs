#region File introduction
// Button.cs by Ben Cortina
#endregion

#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3
{
    /// <summary>
    /// Template for buttons used in the game
    /// </summary>
    public abstract class Button
    {
        #region Fields
        protected Vector2 location;
        protected const int Size = 50;
        protected bool isPressed = false;
        protected bool isInFocus = false;
        protected int row, col;
        protected Texture2D texture;
        protected SpriteFont font;
        protected Rectangle area;
        protected float rotation;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates the column containing the button
        /// </summary>
        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        /// <summary>
        /// Indicates the Row containing the Button
        /// </summary>
        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        /// <summary>
        /// Indicates the Location of the Button on the screen
        /// </summary>
        public virtual Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// The X component of Location
        /// </summary>
        public float LocationX
        {
            get { return location.X; }
            set { location.X = value; }
        }

        /// <summary>
        /// The Y component of Location
        /// </summary>
        public float LocationY
        {
            get { return location.Y; }
            set { location.Y = value; }
        }

        /// <summary>
        /// The text for when the button is in its default state
        /// </summary>
        virtual public string NormalText
        {
            get { return "UDF"; }
            set { }
        }

        /// <summary>
        /// Is the button currently the users focus?
        /// </summary>
        public bool IsInFocus
        {
            get { return isInFocus; }
            set { isInFocus = value; }
        }

        /// <summary>
        /// Is the button being Pressed?
        /// </summary>
        public bool IsPressed
        {
            get { return isPressed; }
            set { isPressed = value; }
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Constructor
        /// </summary>
        public Button() 
        {
            isPressed = false;
            isInFocus = false;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Check if the mouse is over the button.
        /// </summary>
        /// <param name="x"> mouse position X</param>
        /// <param name="y"> mouse position Y</param>
        /// <returns>bool, true if mouse is over button</returns>
        virtual public bool CheckFocus(int x, int y)
        {
            if (area.Left <= x && x <= area.Right && area.Top <= y &&
                y <= area.Bottom)
                isInFocus = true;
            else
                isInFocus = false;
            return isInFocus;
        }
        #endregion

        #region Draw

        /// <summary>
        /// Draw the Button
        /// </summary>
        /// <param name="spritebatch"></param>
        virtual public void Draw(GameTime gameTime, SpriteBatch spritebatch) { }

        #endregion
    }
}
