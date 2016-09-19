using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace WebSiteExtractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Extractor extractor;
        private MusicPlayer musicPlayer;
        private Website actualWebsite;
        private BrowserType actualBrowserType;
        private const BrowserType defaultBrowserType = BrowserType.Internal;

        private const string InputDefaultText = "Paste Text Here ...";
        private const string OutputDefaultText = "BBCode Result For TS3 Description";
        private const string DefaultWebsiteUrl = "https://www.google.com/";
        private Website defaultWebsite;
        public string DefaultMusicPlayerLabel = "Music Player";


        public double VolumeLevel
        {
            get { return musicPlayer.Volume; }
            set
            {
                if (value != musicPlayer.Volume)
                {
                    musicPlayer.Volume = value;
                    OnPropertyChanged("VolumeLevel");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
            InitializeFields();
        }

        private void Initialize()
        {
            DataContext = this;
            extractor = new Extractor();
            musicPlayer = new MusicPlayer();
            VolumeLevel = MusicPlayer.DefaultVolumeLevel;

            actualBrowserType = defaultBrowserType;
            defaultWebsite = new Website(null, DefaultWebsiteUrl, WebsiteType.google);
        }

        private void InitializeFields()
        {
            InputText.Document.Blocks.Clear();
            InputText.Document.Blocks.Add(new Paragraph(new Run(InputDefaultText)));
            InputText.FontSize = 12;
            InputText.Document.LineHeight = 1;
            InputText.Document.TextAlignment = TextAlignment.Center;
            InputText.SelectAll();

            OutputText.Document.Blocks.Clear();
            OutputText.Document.Blocks.Add(new Paragraph(new Run(OutputDefaultText)));
            OutputText.FontSize = 12;
            OutputText.Document.LineHeight = 1;
            OutputText.Document.TextAlignment = TextAlignment.Center;

            Browser.Navigated += new NavigatedEventHandler(Browser_Navigated);
            SetupWindow();
        }

        private void SetupWindow()
        {
            if (actualWebsite != defaultWebsite)
            {
                actualWebsite = defaultWebsite;
                Browser.Navigate(defaultWebsite.Url);
            }
            if (WebsitesViewer.SelectedItems != null)
                WebsitesViewer.SelectedItem = null;
        }

        private void ExtractWebsitesButton_Click(object sender, RoutedEventArgs e)
        {
            var text = new TextRange(InputText.Document.ContentStart, InputText.Document.ContentEnd).Text;
            var resultWebSites = extractor.FindWebsiteAddresses(text);
            WebsitesViewer.ItemsSource = resultWebSites;

            OutputText.Document.Blocks.Clear();
            if (resultWebSites.Count > 0)
                OutputText.Document.Blocks.Add(new Paragraph(new Run(extractor.GenerateBBCode())));
            else
                OutputText.Document.Blocks.Add(new Paragraph(new Run(OutputDefaultText)));

            SetupWindow();
        }

        private void PlayMusicButton_Click(object sender, RoutedEventArgs e)
        {
            if (musicPlayer.ActualMusicFileName != null && musicPlayer.ActualMusicFileName != "")
                MusicPlayerLabel.Content = musicPlayer.ActualMusicFileName;
            if (!musicPlayer.IsPlaying)
            {
                musicPlayer.PlayMusic();
                PlayMusicButton.Content = "Pause";
            }
            else
            {
                musicPlayer.PauseMusic();
                PlayMusicButton.Content = "Play";
            }
        }

        private void StopMusicButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayerLabel.Content = DefaultMusicPlayerLabel;
            musicPlayer.StopMusic();
            PlayMusicButton.Content = "Play";
        }

        private void LoadMusicButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayerLabel.Content = DefaultMusicPlayerLabel;
            musicPlayer.StopMusic();
            PlayMusicButton.Content = "Play";
            musicPlayer.LoadMusic();
            if(musicPlayer.ActualMusicFileName != null && musicPlayer.ActualMusicFileName != "")
            {
                PlayMusicButton.IsEnabled = true;
                StopMusicButton.IsEnabled = true;
            }
        }

        private void WebsitesViewer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1)
                return;
            actualWebsite = e.AddedItems[0] as Website;

            switch (actualBrowserType)
            {
                case BrowserType.External:
                    ExternalBrowserController.Navigate(actualWebsite.Url);
                    break;
                case BrowserType.Internal:
                    Browser.Navigate(actualWebsite.Url);
                    break;
                default:
                    break;
            }
        }

        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            mshtml.HTMLDocument htmlDoc = Browser.Document as mshtml.HTMLDocument;
            if (htmlDoc != null)
                switch (actualWebsite.Type)
                {
                    case WebsiteType.jbzd:
                        htmlDoc.parentWindow.scrollBy(0, 230);
                        break;
                    case WebsiteType.gag:
                        htmlDoc.parentWindow.scrollBy(30, 200);
                        break;
                    case WebsiteType.google:
                        htmlDoc.parentWindow.scrollBy(90, 80);
                        break;
                    default:
                        break;
                }
            InputText.Focus();
        }

        void Browser_Navigated(object sender, NavigationEventArgs e)
        {
            InternalBrowserController.SetSilent(Browser, true);
        }

        private void ExternalBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            actualBrowserType = BrowserType.External;
            ShowAllButton.IsEnabled = true;
            SetupWindow();
        }

        private void InternalBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            actualBrowserType = BrowserType.Internal;
            ShowAllButton.IsEnabled = false;
            SetupWindow();
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in WebsitesViewer.Items)
            {
                var website = item as Website;
                ExternalBrowserController.Navigate(website.Url);
            }
        }
    }
}
