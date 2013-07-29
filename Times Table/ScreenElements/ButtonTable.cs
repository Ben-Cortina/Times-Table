#region File introduction
// ButtonTable.cs by Ben Cortina
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
    /// The class Button implements a cell in the time table.
    /// </summary>
    public class ButtonTable
    {
        #region Fields

        int buttonSize;
        List<Button> buttons = new List<Button>();
        Vector2 location;
        int rows, cols;

        #endregion

        #region Properties

        /// <summary>
        /// # of columns in the table
        /// </summary>
        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        /// <summary>
        /// # of rows in the table
        /// </summary>
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        /// <summary>
        /// Location of the button table
        /// </summary>
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        #endregion

        #region Initialization

        public ButtonTable(Texture2D p_texture, SpriteFont p_font,
            Vector2 p_location, int p_rows, int p_cols, string tableType, int p_buttonSize)
        {
            location = p_location;
            rows = p_rows;
            cols = p_cols;

            Button button;
            Vector2 buttonLocation;
            String label;
            Random rand = new Random();
            float p_rotation;
            buttonSize = p_buttonSize;

            #region button creation

            //Create buttons
            if (tableType == "TT")
            {
                String answer;
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                    {
                        label = (i + 1).ToString() + "x" + (j + 1).ToString();
                        answer = ((i + 1) * (j + 1)).ToString();
                        buttonLocation = new Vector2(location.X + j * (buttonSize + 1), location.Y + i * (buttonSize + 1));

                        p_rotation = rand.Next(-6,7) * (float)Math.PI /300;
                        
                        button = new ButtonTT(buttonLocation, 
                            buttonSize - 1,
                            label,
                            answer,
                            i + 1,
                            j + 1,
                            p_font,
                            p_texture,
                            p_rotation);
                        buttons.Add(button);
                    }
            }
            else if (tableType == "Calc")
            {
                int number;
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                    {
                        buttonLocation = new Vector2(location.X + j * (buttonSize + 1),location.Y + i * (buttonSize + 1));
                        number = cols * rows - ((i) * cols + rows - j )-1;

                        if (number < 1)
                        {
                            if (number == 0)
                            {
                                label = "=";
                                number = -1;
                            }
                            else
                            {
                                if (number == -1)
                                {
                                    number = 0;
                                    label = number.ToString();
                                }
                                else
                                    label = "C";
                            }
                        } else
                            label = number.ToString();

                        p_rotation = rand.Next(-6, 7) * (float)Math.PI / 300;

                        button = new ButtonCalc(buttonLocation,
                            buttonSize,
                            label,
                            number,
                            i + 1,
                            j + 1,
                            p_font,
                            p_texture,
                            p_rotation);
                        buttons.Add(button);
                    }
            }
            #endregion
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the button table into the spritebatch supplied
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            foreach (Button b in buttons)
                b.Draw(gameTime, spriteBatch);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks to see if the Mouse is over any buttons
        /// </summary>
        public Vector2 CheckFocus(InputState input)
        {
            Vector2 focus = new Vector2(-1,-1);
            foreach (Button b in buttons)
            {
                b.IsPressed = false;
                if (b.CheckFocus(input.MouseState.X, input.MouseState.Y))
                {
                    focus.X = b.Col;
                    focus.Y = b.Row;
                    if (input.IsMousePressed())
                    {
                        b.IsPressed = true;
                    }
                }
            }
            return focus;
        }

        /// <summary>
        /// Toggles the Pressed state of a button
        /// </summary>
        /// <param name="focus">the button location</param>
        public void TogglePressed(Vector2 focus)
        {
            int location = (int)(focus.Y - 1) * cols + (int)(focus.X);
            buttons[location - 1].IsPressed = !buttons[location - 1].IsPressed;
        }

        /// <summary>
        /// Changes a buttons inFocus Property
        /// </summary>
        /// <param name="focus">the button location</param>
        /// <param name="isFocus">Focus</param>
        public void SetFocus(Vector2 focus, bool isFocus)
        {
            int location = (int)(focus.Y - 1) * cols + (int)(focus.X);
            buttons[location-1].IsInFocus = isFocus;
        }

        /// <summary>
        /// Changes a Buttons pressed Property
        /// </summary>
        /// <param name="focus">the button location</param>
        /// <param name="isPressed">Pressed</param>
        public void SetPressed(Vector2 focus, bool isPressed)
        {
            int location = (int)(focus.Y - 1) * cols + (int)(focus.X);
            buttons[location-1].IsPressed = isPressed;
        }

        /// <summary>
        /// Checks to see if a the button at the supplied location is pressed
        /// </summary>
        /// <param name="focus">Vector2(row, col)</param>
        public bool IsPressed(Vector2 focus)
        {
            int location = (int)(focus.Y - 1) * cols + (int)(focus.X);
            return buttons[location - 1].IsPressed;
        }

        #endregion
    }
}