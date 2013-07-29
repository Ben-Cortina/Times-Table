#region File introduction
// AudioLibrary.cs by Ben Cortina
#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

#endregion

namespace Project3.ScreenManagement
{
    /// <summary>
    /// Used for passing loaded sounds to all members of the game
    /// </summary>
    public class AudioLibrary
    {
        #region Fields and Properties

        SoundEffect write;

        /// <summary>
        /// returns the sound for writing an answer
        /// </summary>
        public SoundEffect Write
        {
            get { return write; }
        }

        SoundEffect wrong;

        /// <summary>
        /// returns the sound to indicate an incorrect response
        /// </summary>
        public SoundEffect Wrong
        {
            get { return wrong; }
        }

        SoundEffect right;

        /// <summary>
        /// returns the sound to indicate a correct response
        /// </summary>
        public SoundEffect Right
        {
            get { return right; }
        }

        List<SoundEffect> menuSounds;
        
        /// <summary>
        /// Returns a random menuSound
        /// </summary>
        public SoundEffect MenuSound
        {
            get {
                Random rand = new Random();
                return menuSounds[rand.Next(menuSounds.Count)]; 
            }
        }

        #endregion

        #region LoadContent

        /// <summary>
        /// Load the sounds
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            String[] menuSoundNames
                = new String[]{
                    "Sounds/Menusound1",
                    "Sounds/Menusound2",
                    "Sounds/Menusound3",
                    };
            menuSounds = new List<SoundEffect>();
            foreach (String sound in menuSoundNames)
                menuSounds.Add(Content.Load<SoundEffect>(sound));

            write = Content.Load<SoundEffect>("Sounds/Write");
            wrong = Content.Load<SoundEffect>("Sounds/Wrong");
            right = Content.Load<SoundEffect>("Sounds/Right");
        }

        #endregion
    }
}
