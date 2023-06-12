using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Slidable;

public partial class SlidableLayout
{
     /// <summary>
        /// <see cref="Config"/>
        /// </summary>
        public static readonly BindableProperty ConfigProperty = BindableProperty.Create(
            nameof(Config),
            typeof(SliderConfig),
            typeof(SlidableLayout));

        /// <summary>
        /// Configuration indicating max and min values of this layout. 
        /// </summary>
        public SliderConfig Config
        {
            get => (SliderConfig)GetValue(ConfigProperty);
            set => SetValue(ConfigProperty, value);
        }

        /// <summary>
        /// <see cref="SlideProperties"/>
        /// </summary>
        public static readonly BindableProperty SlidePropertiesProperty = BindableProperty.Create(
            nameof(SlideProperties),
            typeof(SlidableProperties),
            typeof(SlidableLayout),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: new SlidableProperties(0),
            propertyChanged: OnChanged);

        /// <summary>
        /// Properties used to define where the slider is at the moment, in terms of index and some internal properties used for the scrolling.
        /// </summary>
        public SlidableProperties SlideProperties
        {
            get => (SlidableProperties)GetValue(SlidePropertiesProperty);
            set => SetValue(SlidePropertiesProperty, value);
        }

        /// <summary>
        /// <see cref="SelectedItemChangedCommand"/>
        /// </summary>
        public static readonly BindableProperty SelectedItemChangedCommandProperty = BindableProperty.Create(
            nameof(SelectedItemChangedCommand),
            typeof(ICommand),
            typeof(SlidableLayout));

        /// <summary>
        /// Command invoked every time the selection of an index changes.
        /// </summary>
        public ICommand SelectedItemChangedCommand
        {
            get => (ICommand)GetValue(SelectedItemChangedCommandProperty);
            set => SetValue(SelectedItemChangedCommandProperty, value);
        }

        /// <summary>
        /// <see cref="ElementWidth"/>
        /// </summary>
        public static readonly BindableProperty ElementWidthProperty = BindableProperty.Create(
            nameof(ElementWidth),
            typeof(double),
            typeof(SlidableLayout),
            0.2);

        /// <summary>
        /// Width of an Element, either proportional or exact.
        /// </summary>
        public double ElementWidth
        {
            get => (double)GetValue(ElementWidthProperty);
            set => SetValue(ElementWidthProperty, value);
        }

        /// <summary>
        /// <see cref="WidthIsProportional"/>
        /// </summary>
        public static readonly BindableProperty WidthIsProportionalProperty = BindableProperty.Create(
            nameof(WidthIsProportional),
            typeof(bool),
            typeof(SlidableLayout),
            true);
    


        /// <summary>
        /// Default true and defines if the ElementWidth is proportional to the width of the parent or exact pixel values.
        /// </summary>
        public bool WidthIsProportional
        {
            get => (bool)GetValue(WidthIsProportionalProperty);
            set => SetValue(WidthIsProportionalProperty, value);
        }
    /// <summary>
    /// Toggles drag effect after pan gesture is completed. Set to true if scroll should stop immediately after finger is lifted. 
    /// </summary>
    public bool StopOnGestureEnded { get; set; }

    /// <summary>
    /// Invoked on start of a Pan gesture
    /// </summary>
    public event EventHandler<PanEventArgs>? PanStarted;

    /// <summary>
    /// Invoked on end of Pan gesture.
    /// </summary>
    public event EventHandler<PanEventArgs>? PanEnded;
        
    /// <summary>
    /// Invoked when people tap the slidable layout
    /// </summary>
    public event EventHandler<TappedEventArgs>? Tapped;
    
}