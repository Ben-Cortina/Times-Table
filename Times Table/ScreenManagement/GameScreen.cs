#region File introduction
// GameScreen.cs by Ben Cortina
#endregion


#region Using Statements

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Project3.ScreenManagement
{

    /// <summary>
    /// Parent Class for all screens in the game
    /// </summary>
    public abstract class GameScreen : DrawableGameComponent
    {
        #region Feilds

        /// <summary>
        /// Content manager
        /// </summary>
        public ContentManager Content
        {
            get { return content; }
            protected set { content = value; }
        }
        

        ContentManager content;

        /// <summary>
        /// Tells the Screen wether or not to update.
        /// </summary>
        public bool Active
        {
            get { return Enabled; }
            protected set { Enabled = value; Visible = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_input"></param>
        public void Show(InputState p_input)
        {
          Enabled = true; 
          Visible = true;
          input = p_input;
        }

        bool exiting;

        /// <summary>
        /// Marks the screen as needing to be deactivated
        /// </summary>
        public bool Exiting
        {
            get { return exiting; }
            set { exiting = value; }
        }

        SpriteBatch spriteBatch;

        /// <summary>
        /// The SpriteBatch used by the game
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            protected set { spriteBatch = value; }
        }

        GraphicsDeviceManager graphics;

        /// <summary>
        /// The SpriteBatch used by the game
        /// </summary>
        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
            protected set { graphics = value; }
        }

        AudioLibrary audio;

        /// <summary>
        /// The AudioLibrary Used by the game
        /// </summary>
        public AudioLibrary Audio
        {
            get { return audio; }
        }

        List<double> soundPlayed;

        /// <summary>
        /// Stores if a sound is playing and when it was started.
        /// </summary>
        public List<double> SoundPlayed
        {
            get { return soundPlayed; }
            set { soundPlayed = value; }
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameScreen(Game game)
            : base(game)
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(
                                typeof(SpriteBatch));
            graphics = (GraphicsDeviceManager)Game.Services.GetService(
                                typeof(IGraphicsDeviceManager));
            input = (InputState)Game.Services.GetService(
                                typeof(InputState));
            audio = (AudioLibrary)Game.Services.GetService(
                                typeof(AudioLibrary));
            content = game.Content;
            Visible = false;
            Enabled = false;
            exiting = false;
            soundPlayed = new List<double>();
        }

        /// <summary>
        /// User Input
        /// </summary>
        public InputState Input
        {
            get { return input; }
            set { input = value; }
        }

        InputState input;

        #endregion

        #region Methods

        /// <summary>
        /// plays menu sound if it is not already playing
        /// </summary>
        public virtual void PlayMenuSound(GameTime gameTime)
        {
            if (SoundPlayed[0] + Audio.MenuSound.Duration.TotalSeconds < gameTime.TotalGameTime.TotalSeconds)
            {
                Audio.MenuSound.Play();
                SoundPlayed[0] = gameTime.TotalGameTime.TotalSeconds;
            }
        }

        /// <summary>
        /// Plays Write sound if it is not already playing
        /// </summary>
        public virtual void PlayWriteSound(GameTime gameTime)
        {
            if (SoundPlayed[1] + Audio.Write.Duration.TotalSeconds < gameTime.TotalGameTime.TotalSeconds)
            {
                Audio.Write.Play();
                SoundPlayed[1] = gameTime.TotalGameTime.TotalSeconds;
            }
        }

        /// <summary>
        /// Allows the Game to know whether the screen wants to exit.
        /// </summary>
        public virtual int IsExiting() 
        {
            if (Exiting)
            {
                Audio.Write.Play();
                ExitScreen();
                Exiting = false;
                return 0;
            }
            else
                return -1;
        }

        /// <summary>
        /// Disables the screen
        /// </summary>
        public virtual void ExitScreen()
        {
            Active = false;
        }

        #endregion
    }
}
