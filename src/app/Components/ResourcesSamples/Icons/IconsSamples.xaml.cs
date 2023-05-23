using DIPS.Mobile.UI.Resources.Icons;

namespace Components.ResourcesSamples.Icons
{
    public partial class IconsSamples
    {
        private Dictionary<string, ImageSource> m_icons;
        private Dictionary<string, ImageSource> m_allIcons;

        public IconsSamples()
        {
            InitializeComponent();
            Icons = GetIcons();
            m_allIcons = Icons;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Icons = GetIcons();
            m_allIcons = Icons;
        }

        private static Dictionary<string, ImageSource> GetIcons()
        {
            var theIcons = new DIPS.Mobile.UI.Resources.Icons.Icons();
            var icons = new Dictionary<string, ImageSource>();
            foreach (var colorPair in theIcons)
            {
                if (colorPair.Value is string theIconFile)
                {
                    var imageSource = ImageSource.FromFile(theIconFile);
                    icons.Add(colorPair.Key, imageSource);
                    var image = new Image();
                    image.Source = imageSource;
                }
            }

            return icons;
        }

        public Dictionary<string, ImageSource> Icons
        {
            get => m_icons;
            private set
            {
                m_icons = value;
                OnPropertyChanged();
            }
        }


        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                Icons = m_allIcons;
            }
            else
            {
                var matchingIcons = m_allIcons.Where(c => c.Key.ToLower().Contains(e.NewTextValue.ToLower()));
                Icons = matchingIcons.ToDictionary(icon => icon.Key,
                    matchingIcon => matchingIcon.Value);
            }
        }
    }
}