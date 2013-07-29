#region File introduction
// IconTable.cs by Ben Cortina
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3
{
    /// <summary>
    /// The class Icon displays an animal picture for the times table.
    /// </summary>
    public class IconTable
    {
        #region Fields and Property

        const int IconSize = 40;
        Vector2 location;
        List<Texture2D> textures = new List<Texture2D>();
        int currentTexture = 0;
        int rows, cols;
        bool visible;
        Vector2 focus = new Vector2(0, 0);

        /// <summary>
        /// The location of the icon table
        /// </summary>
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public IconTable(List<Texture2D> p_textures, Vector2 p_location, int p_rows, int p_cols)

        {
            // TODO: Construct any child components here
            textures = p_textures;
            location = p_location;
            rows = p_rows;
            cols = p_cols;
            visible = false;
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the icon table into the spritebatch supplied
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                Vector2 iconLocation;
                for (int i = 0; i < focus.X; i++)
                    for (int j = 0; j < focus.Y; j++)
                    {
                        iconLocation = new Vector2(location.X + i * (51), location.Y + j * (51));

                        spriteBatch.Draw(textures[currentTexture], new Rectangle((int)iconLocation.X,
                            (int)iconLocation.Y, IconSize, IconSize), null, Color.White);
                    }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lets the Icon Table know how many icon to display
        /// </summary>
        public void newFocus(Vector2 p_focus)
        {
            if (p_focus.X == -1)
                visible = false;
            else
            {
                visible = true;
                Random rand = new Random();
                currentTexture = rand.Next(textures.Count());

                focus = p_focus;
            }
        }

        #endregion
    }
}