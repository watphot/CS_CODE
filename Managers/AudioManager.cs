using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace GameProject
{
    public enum Track
    {
        Track01,
        Track02,
        Track03,
    }

    public enum Fx
    {
        LogoAppear,
        PressStart,
        Shot,
        Enemy,
        Hit,
    }

    public static class AudioManager
    {
        const int MAX_SONGS = 3;
        const int MAX_SOUNDS = 5;

        static Song[] songs;
        static SoundEffect[] sounds;

        public static float MusicVolume
        {
            get { return MediaPlayer.Volume; }
            set { MediaPlayer.Volume = value; }
        }

        public static bool RepeatMusic
        {
            get { return MediaPlayer.IsRepeating; }
            set { MediaPlayer.IsRepeating = value; }
        }

        public static void Initialize(ContentManager content)
        {
            songs = new Song[MAX_SONGS];
            sounds = new SoundEffect[MAX_SOUNDS];

            songs[(int)Track.Track01] = content.Load<Song>("audio/title");
            songs[(int)Track.Track02] = content.Load<Song>("audio/spaceship");
            //songs[(int)Track.Track03] = content.Load<Song>("audio/title");

            sounds[(int)Fx.LogoAppear] = content.Load<SoundEffect>("audio/logo");
            sounds[(int)Fx.Shot] = content.Load<SoundEffect>("audio/laser");
            sounds[(int)Fx.PressStart] = content.Load<SoundEffect>("audio/start");
            sounds[(int)Fx.Hit] = content.Load<SoundEffect>("audio/ping");
            sounds[(int)Fx.Enemy] = content.Load<SoundEffect>("audio/enemy");
        }

        #region Music Playing Methods

        public static void PlayMusic(Track track, float volume)
        {
            MusicVolume = volume;
            MediaPlayer.Stop();
            MediaPlayer.Play(songs[(int)track]);
        }

        public static void PlayMusic(Track track) 
        { 
            PlayMusic(track, 1.0f); 
        }

        public static void StopMusic()
        {
            if (MediaPlayer.State == MediaState.Playing) MediaPlayer.Stop();
        }

        public static void PauseMusic()
        {
            MediaPlayer.Pause();
        }

        public static void ResumeMusic()
        {
            MediaPlayer.Resume();
        }

        #endregion

        #region Sound Playing Methods

        public static void PlaySound(Fx fx, float volume)
        {
            sounds[(int)fx].Play(volume, 0.0f, 0.0f); //Full Volume (1.0f) is relative to SoundEffects.MasterVolume
        }

        public static void PlaySound(Fx fx) 
        { 
            PlaySound(fx, 1.0f); 
        }

        #endregion
    }
}
