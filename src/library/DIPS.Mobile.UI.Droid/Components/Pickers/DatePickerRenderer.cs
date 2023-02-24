using System;
using System.ComponentModel;
using Android.Content;
using DIPS.Mobile.UI.Components.Pickers;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Google.Android.Material.DatePicker;
using Java.Text;
using Java.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePickerRenderer = DIPS.Mobile.UI.Droid.Components.Pickers.DatePickerRenderer;
using DUIDatePicker = DIPS.Mobile.UI.Components.Pickers.DatePicker;
using Object = Java.Lang.Object;
using TimeZone = Java.Util.TimeZone;

[assembly: ExportRenderer(typeof(DUIDatePicker), typeof(DatePickerRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Pickers
{
    public class DatePickerRenderer : ViewRenderer, IDialogInterfaceOnDismissListener
    {
        private MaterialDatePicker? m_materialDatePicker;
        private DUIDatePicker? m_duiDatePicker;
        private const string DatePickerTag = "DUIMaterialDatePicker";

        public DatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUIDatePicker datePicker)
                {
                    m_duiDatePicker = datePicker;
                    
                }
            }
        }

        private void SetDatePickerTitle(MaterialDatePicker.Builder builder)
        {
            if (m_duiDatePicker != null && !string.IsNullOrEmpty(m_duiDatePicker.Description))
            {
                builder.SetTitleText(m_duiDatePicker.Description);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(DUIDatePicker.IsOpen):
                    ToggleShowing();
                    break;
            }
        }

        private void ToggleShowing()
        {
            if (m_duiDatePicker == null) return;

            if (m_duiDatePicker.IsOpen && m_materialDatePicker == null) //This will only run if the date picker was not previously opened 
            {
                var builder = MaterialDatePicker.Builder.DatePicker();
                SetDatePickerSelection(builder);
                SetDatePickerTitle(builder);
                m_materialDatePicker = builder.Build();
                m_materialDatePicker.Show(Context.GetFragmentManager(), DatePickerTag);
                m_materialDatePicker.AddOnDismissListener(this);
            }
            else
            {
                if (m_materialDatePicker is not {IsVisible: true}) return;

                m_materialDatePicker.Dismiss();
            }
        }

        private void SetDatePickerSelection(MaterialDatePicker.Builder builder)
        {
            if (m_duiDatePicker == null || m_duiDatePicker.SelectedDate == default) return;
            
            //Java uses the unix epoch, so we have find the total milliseconds from the date people have picked and the UnixEpoch start.
            builder.SetSelection((long)(m_duiDatePicker.SelectedDate - DateTime.UnixEpoch).TotalMilliseconds);

        }

        public void OnDismiss(IDialogInterface? dialog)
        {
            if (m_duiDatePicker == null || m_materialDatePicker == null) return;
            
            if (long.TryParse(m_materialDatePicker.Selection?.ToString() , out var milliseconds))
            {
                //Java uses the unix epoch, so we have to create a csharp date time based on the UnixEpoch start with the milliseconds picked by people using the date picker.
                var dateFromJava = DateTime.UnixEpoch.AddMilliseconds(milliseconds);
                m_duiDatePicker.SelectedDate = dateFromJava;
            }
            
            m_materialDatePicker = null;
            m_duiDatePicker.IsOpen = false;
        }
    }
}