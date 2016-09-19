using Microsoft.Win32;
using System;
using System.Windows.Media;

namespace WebSiteExtractor
{
    public class MusicPlayer
    {
        private Uri defaultMusicUri;
        MediaPlayer player;
        public bool IsPlaying { get; private set; }
        public string ActualMusicFileName { get; private set; }

        public double Volume
        {
            get { return player.Volume; }
            set { player.Volume = value; }
        }

        public const double DefaultVolumeLevel = 0.5;

        public MusicPlayer()
        {
            player = new MediaPlayer();
        }

        public void PlayMusic()
        {
            player.Play();
            IsPlaying = true;
        }

        public void StopMusic()
        {
            player.Stop();
            IsPlaying = false;
        }

        public void PauseMusic()
        {
            player.Pause();
            IsPlaying = false;
        }

        public void LoadMusic()
        {
            var result = OpenFileDialog();
            if (result != null)
                player.Open(result);
        }

        private Uri OpenFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Music Files (.mp3)|*.mp3|All Files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            bool? userClickedOK = dialog.ShowDialog();

            if (userClickedOK == true)
            {
                ActualMusicFileName = dialog.SafeFileName;
                return new Uri(dialog.FileName);
            }
            return null;
        }
    }
}
