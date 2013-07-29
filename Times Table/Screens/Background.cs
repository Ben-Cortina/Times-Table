#region File Description
// Background.cs By Ben Cortina
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3.Screens
{
    #region Enums

    /// <summary>
    /// Finally found a use for these...
    /// Easier to read than 0,1,2,3
    /// </summary>
    public enum Side
    {
        Left = 0,
        Top,
        Right,
        Bottom
    }

    /// <summary>
    /// Easier to read than 0,1,2,3
    /// </summary>
    public enum Corner
    {
        TopL = 0,
        TopR,
        BotR,
        BotL
    }
    #endregion

    /// <summary>
    /// The Background images and layout for the entire game
    /// </summary>
    public class Background : ScreenManagement.GameScreen
    {
        #region Fields

        Texture2D corners;
        Texture2D middle;
        Texture2D edges;

        List<Rectangle> cornerTextures;
        List<Rectangle> cornerRects;
        Rectangle middleRect;
        List<Rectangle> edgeTextures;
        List<Rectangle> edgeRects;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public Background(Game game)
            : base(game)
        {
            Content = game.Content;
            Visible = true;
        }

        public override void Initialize()
        {
            base.Initialize();

            cornerTextures = new List<Rectangle>();
            cornerRects = new List<Rectangle>();

            edgeTextures = new List<Rectangle>();
            edgeRects = new List<Rectangle>();

            //TopL
            cornerTextures.Add( new Rectangle(0, 0, 111, 114) );
            cornerRects.Add( new Rectangle(0, 0, 50, 50) );
            //TopR
            cornerTextures.Add( new Rectangle(112, 0, 115, 114) );
            cornerRects.Add( new Rectangle(Graphics.PreferredBackBufferWidth - 50, 0, 50, 50) );
            //BotR
            cornerTextures.Add( new Rectangle(112, 115, 115, 118) );
            cornerRects.Add( new Rectangle(Graphics.PreferredBackBufferWidth - 50, Graphics.PreferredBackBufferHeight - 50, 50, 50) );
            //BotL
            cornerTextures.Add( new Rectangle(0, 115, 111, 118) );
            cornerRects.Add( new Rectangle(0, Graphics.PreferredBackBufferHeight - 50, 50, 50) );

            //Left
            edgeTextures.Add( new Rectangle(0, 0, 111, 2184) );
            edgeRects.Add( new Rectangle(0, 50, 50, Graphics.PreferredBackBufferHeight - 100) );
            //Top
            edgeTextures.Add( new Rectangle(228, 0, 114, 3495) );
            edgeRects.Add( new Rectangle(50, 50, 50, Graphics.PreferredBackBufferWidth - 100) );
            //Right
            edgeTextures.Add( new Rectangle(112, 0, 115, 2184) );
            edgeRects.Add( new Rectangle(Graphics.PreferredBackBufferWidth - 50, 50, 50, Graphics.PreferredBackBufferHeight - 100) );
            //Bottom
            edgeTextures.Add( new Rectangle(344, 0, 118, 3495) );
            edgeRects.Add(new Rectangle(50, Graphics.PreferredBackBufferHeight, 50, Graphics.PreferredBackBufferWidth - 100));

            middleRect = new Rectangle(50, 50, Graphics.PreferredBackBufferWidth - 100, Graphics.PreferredBackBufferHeight - 100);


        }

        protected override void LoadContent()
        {
            base.LoadContent();

            corners= Content.Load<Texture2D>("Images/BBCorners");
            middle = Content.Load<Texture2D>("Images/BBMiddle");
            edges = Content.Load<Texture2D>("Images/BBEdges");
        }

        #endregion

        #region Draw

        /// <summary>
        /// Draws the Background.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //middle
            SpriteBatch.Draw(middle, middleRect, null,
                            Color.White, 0f,
                             Vector2.Zero, SpriteEffects.None, 1);
            //corners
            SpriteBatch.Draw(corners, cornerRects[(int)Corner.TopL], cornerTextures[(int)Corner.TopL],
                            Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            SpriteBatch.Draw(corners, cornerRects[(int)Corner.TopR], cornerTextures[(int)Corner.TopR],
                            Color.White, 0f,
                             Vector2.Zero, SpriteEffects.None, 0.9f);
            SpriteBatch.Draw(corners, cornerRects[(int)Corner.BotL], cornerTextures[(int)Corner.BotL],
                            Color.White, 0f,
                             Vector2.Zero, SpriteEffects.None, 0.9f);
            SpriteBatch.Draw(corners, cornerRects[(int)Corner.BotR], cornerTextures[(int)Corner.BotR],
                            Color.White, 0f,
                             Vector2.Zero, SpriteEffects.None, 0.9f);
            //edges
            SpriteBatch.Draw(edges, edgeRects[(int)Side.Left], edgeTextures[(int)Side.Left],
                            Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            SpriteBatch.Draw(edges, edgeRects[(int)Side.Right], edgeTextures[(int)Side.Right],
                            Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            SpriteBatch.Draw(edges, edgeRects[(int)Side.Top], edgeTextures[(int)Side.Top],
                            Color.White, -(float)Math.PI/2, Vector2.Zero, SpriteEffects.None, 0.9f);
            SpriteBatch.Draw(edges, edgeRects[(int)Side.Bottom], edgeTextures[(int)Side.Bottom],
                            Color.White, -(float)Math.PI / 2, Vector2.Zero, SpriteEffects.None, 0.9f);

        }


        #endregion
    }
}
