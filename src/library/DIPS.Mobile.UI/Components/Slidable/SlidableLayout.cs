using DIPS.Mobile.UI.Components.Slidable.Util;

namespace DIPS.Mobile.UI.Components.Slidable
{
    /// <summary>
    /// Layout used to scroll through indexes smoothly. This has enabled acceleration.
    /// </summary>
    public abstract partial class SlidableLayout : ContentView
    {
        private readonly PanGestureRecognizer m_panGestureRecognizer;
        private readonly AccelerationService m_accelerator = new(true);
        private int m_lastId = -2; // Different than default of SlideProperties
        private double m_startSlideLocation;
        private int m_lastIndex = int.MinValue;
        private bool m_disableTouchScroll;
        private bool m_hasInitialized;

        private DisplayOrientation m_currentOrientation;

        

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public SlidableLayout()
        {
            Padding = 0;
            Margin = 0;
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Fill;
            Config = new SliderConfig(int.MinValue, int.MaxValue);
            
            m_panGestureRecognizer = new PanGestureRecognizer();


            GestureRecognizers.Add(m_panGestureRecognizer);
            
            m_panGestureRecognizer.PanUpdated += PanGestureRecognizerPanUpdated;

            var tapGestureRecognizer = new TapGestureRecognizer();
            GestureRecognizers.Add(tapGestureRecognizer);
            tapGestureRecognizer.Tapped += OnEntireLayoutTapped;

            m_currentOrientation = DeviceDisplay.MainDisplayInfo.Orientation;
        }

        private void OnEntireLayoutTapped(object? sender, Microsoft.Maui.Controls.TappedEventArgs eventArgs)
        {
            var point = eventArgs.GetPosition((Element)sender);
            if (point.HasValue)
            {
                var nonRoundedIndex = CalculateIndex(point.Value.X);
                var relativeIndex = GetIndexFromValue(nonRoundedIndex);
                var currentIndex = Math.Clamp((int)SlideProperties.Position + relativeIndex, Config.MinValue, Config.MaxValue);
                Tapped?.Invoke(this, new TappedEventArgs(currentIndex));
                OnTapped(currentIndex);
            }
        }

        private static void OnChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var me = (SlidableLayout)bindable;
            if (me.m_lastId == me.SlideProperties.HoldId)
            {
                return;
            }

            me.OnScrolledInternal();
        }
        
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > 0 && !m_hasInitialized)
            {
                OnScrolledInternal(true);
                m_hasInitialized = true;
                return;
            }
            
            if (m_currentOrientation != DeviceDisplay.MainDisplayInfo.Orientation)
            {
                m_currentOrientation = DeviceDisplay.MainDisplayInfo.Orientation;
                Redraw();
                OnScrolledInternal(true);
            }
        }

        public abstract void Redraw();

        /// <summary>
        /// Scrolls to the index
        /// </summary>
        /// <param name="index">Index to scroll to</param>
        /// <param name="length">Time used on the scrolling</param>
        public void ScrollTo(int index, int length = 250)
        {
            SlidableProperties.ScrollTo(s => SlideProperties = s, () => SlideProperties, index, length);
        }

        private void PanGestureRecognizerPanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            if (DisableTouchScroll)
            {
                return;
            }

            if (Config == null)
            {
                return;
            }

            var currentId = e.GestureId;
            if (!(m_lastId == SlideProperties.HoldId || !SlideProperties.IsHeld || currentId == m_lastId))
            {
                return;
            }

            if (e.StatusType == GestureStatus.Started)
            {
                // Start tracking time
                m_startSlideLocation = CalculateDist(SlideProperties.Position);
                PanStarted?.Invoke(this, new PanEventArgs((int)Math.Round(CalculateIndex(m_startSlideLocation))));
            }

            var currentPos = m_startSlideLocation - e.TotalX;
            m_lastId = currentId;
            var index = Math.Max(Config.MinValue - 0.45, Math.Min(Config.MaxValue + 0.45, CalculateIndex(currentPos)));

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                index = SlideProperties.Position;
                m_accelerator.OnDrag(index);
                SlideProperties = new SlidableProperties(index, m_lastId,
                    e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
            }
            else
            {
                SlideProperties = new SlidableProperties(index, m_lastId,
                    e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
                OnScrolledInternal();
            }

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                m_accelerator.EndDrag();

                if (StopOnGestureEnded)
                {
                    PanEnded?.Invoke(this, new PanEventArgs((int)Math.Round(index)));
                    return;
                }

                Dispatcher.StartTimer(TimeSpan.FromMilliseconds(10), () => // ~80 fps
                {
                    if (currentId != SlideProperties.HoldId) return false;
                    m_accelerator.Min = Config.MinValue - 0.45;
                    m_accelerator.Max = Config.MaxValue + 0.45;
                    var next = m_accelerator.GetValue(out bool isDone);
                    index = next;
                    if (SlideProperties.IsHeld) return false;
                    SlideProperties = new SlidableProperties(index, m_lastId, false);
                    OnScrolledInternal();

                    if (isDone)
                    {
                        PanEnded?.Invoke(this, new PanEventArgs((int)Math.Round(index)));
                    }

                    return !isDone;
                });
            }
            else if (e.StatusType == GestureStatus.Started)
            {
                m_accelerator.StartDrag(index);
            }
            else
            {
                m_accelerator.OnDrag(index);
            }
        }

        private double CalculateIndex(double dist)
        {
            var itemWidth = GetItemWidth();
            return (dist - Width / 2) / itemWidth;
        }

        /// <summary>
        /// Gets the index of a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected int GetIndexFromValue(double value) => (int)Math.Round(value);

        /// <summary>
        /// Center of the screen
        /// </summary>
        protected double Center => Width / 2;

        /// <summary>
        /// Gets the center
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double GetCenterPosition(double value, double index) =>
            Center + GetItemWidth() * (GetIndexFromValue(value) - index);

        /// <summary>
        /// Gets the left position of the item, used in drawing a bigger item
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double GetLeftPosition(double value, double index) =>
            Center + GetItemWidth() * (GetIndexFromValue(value) - 0.5 - index);

        /// <summary>
        /// Calculates the distance from the center
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double CalculateDist(double index)
        {
            var width = GetItemWidth();
            return index * width + Width / 2;
        }

        /// <summary>
        /// Gets the width of the item, by extracting the ElementWidth and Proportional settings.
        /// </summary>
        /// <returns></returns>
        protected double GetItemWidth()
        {
            if (!WidthIsProportional) return ElementWidth;
            return Width * ElementWidth;
        }

        protected void OnScrolledInternal(bool force = false)
        {
            OnScrolled(SlideProperties.Position);
            
            var index = (int)Math.Round(SlideProperties.Position);
            
            if (!force && index == m_lastIndex)
                return;

            SelectedItemChangedCommand?.Execute(index);
            m_lastIndex = index;
        }

        /// <summary>
        /// Override this to handle the scrolling of this layout
        /// </summary>
        /// <param name="index"></param>
        protected virtual void OnScrolled(double index)
        {
        }
        
        /// <summary>
        /// Override this to handle the tapping of the view
        /// </summary>
        /// <param name="index"></param>
        protected virtual void OnTapped(int index)
        {
        }

        /// <summary>
        /// Disables the scrolling on this Layout. Use this if you layout has to be inside a ScrollView on Android.
        /// </summary>
        public bool DisableTouchScroll
        {
            get => m_disableTouchScroll;
            set
            {
                m_disableTouchScroll = value;
                GestureRecognizers.Remove(m_panGestureRecognizer);
                if (!value)
                {
                    GestureRecognizers.Add(m_panGestureRecognizer);
                }
            }
        }

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);
            
            if (args.NewHandler is null)
            {
                m_panGestureRecognizer.PanUpdated -= PanGestureRecognizerPanUpdated;
            }
        }
    }
}