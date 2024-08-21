using Android.App;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Lifecycle;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Buttons.Android;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Google.Android.Material.Button;
using Google.Android.Material.DatePicker;
using Google.Android.Material.Internal;
using Java.Interop;
using Microsoft.Maui.Platform;
using Application = Android.App.Application;
using Button = Android.Widget.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Object = Java.Lang.Object;
using TimePickerService = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePickerService;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;

public class MaterialDatePickerFragment : Object, IMaterialDateTimePickerFragment, IMaterialPickerOnPositiveButtonClickListener, ILifecycleObserver
{
    private readonly DatePicker m_datePicker;
    private readonly MaterialDatePicker m_materialDatePicker;
    private Button? m_extraButton;

    public MaterialDatePickerFragment(DatePicker datePicker)
    {
        m_datePicker = datePicker;
        
        var builder = MaterialDatePicker.Builder.DatePicker();
        SetDatePickerSelection(builder);

        //Set min and max time
        var calendarConstraints = new CalendarConstraints.Builder();
        var listValidators = new List<CalendarConstraints.IDateValidator>();
        if (datePicker.MinimumDate != null) //TODO: Support only setting one of these as well
        {
            listValidators.Add(DateValidatorPointForward.From(datePicker.MinimumDate.Value.ToLong()));
        }
        if (datePicker.MaximumDate != null)
        {
            listValidators.Add(DateValidatorPointBackward.Before(datePicker.MaximumDate.Value.ToLong()));
        }

        if (listValidators.Any())
        {
            calendarConstraints.SetValidator(CompositeDateValidator.AllOf(listValidators));    
        }
        
        m_materialDatePicker = builder.SetCalendarConstraints(calendarConstraints.Build()).Build();
        
        m_materialDatePicker.AddOnPositiveButtonClickListener(this);
        
        // If the date picker is nullable or the today button should be displayed, we need to add an observer to the lifecycle to add custom buttons.
        if(m_datePicker.IsNullable() || m_datePicker.DisplayTodayButton)
            m_materialDatePicker.Lifecycle.AddObserver(this);

        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        m_materialDatePicker.Show(fragmentManager!, TimePickerService.TimePickerTag);
    }
    
    [Export, Lifecycle.Event.OnStart]
    public void OnDatePickerDialogStart()
    {
        var rootView = m_materialDatePicker.View;

        AddTodayOrClearButton(rootView);
    }

    [Export, Lifecycle.Event.OnDestroy]
    public void OnDatePickerDialogDestroy()
    {
        m_extraButton?.SetOnClickListener(null);
        
        m_materialDatePicker.Lifecycle.RemoveObserver(this);
    }

    
    /// <summary>
    /// Found the views that are needed to add the clear button by using material design github:
    /// https://github.com/material-components/material-components-android/blob/31f80151a99cebb7ad37a4965d479b461eadbfcb/lib/java/com/google/android/material/datepicker/res/layout/mtrl_picker_actions.xml#L19
    /// <br />
    /// <br />
    /// The root view here is a LinearLayout with gravity set to the end. To place custom buttons to the left side
    /// we first have to make sure the LinearLayout fill out the whole width, and then also set the ok/cancel buttons to the end.
    /// Then we can add our cancel button and set it to the start.
    /// </summary>  
    private void AddTodayOrClearButton(View? rootView)
    {
        DUI.TryGetResourceId("date_picker_actions", out var id, "id");
        var linearLayoutThatContainsButtons = rootView?.FindViewById<LinearLayout>(id);
        if(linearLayoutThatContainsButtons is null)
            return;
        
        linearLayoutThatContainsButtons.SetGravity(GravityFlags.Fill);

        var cancelButton = linearLayoutThatContainsButtons.GetChildAt(0);
        var okButton = linearLayoutThatContainsButtons.GetChildAt(1);

        if(cancelButton is null || okButton is null)
            return;
        
        SetLayoutParameterToView(cancelButton, GravityFlags.End);
        SetLayoutParameterToView(okButton, GravityFlags.End);

        var wrapperView = new LinearLayout(Application.Context);

        if (m_datePicker.IsNullable())
        {
            m_extraButton = CreateButton(DUILocalizedStrings.Clear, () =>
            {
                m_datePicker.SetSelectedDateTime(null);
            });
        }
        else if (m_datePicker.DisplayTodayButton)
        {
            m_extraButton = CreateButton(DUILocalizedStrings.Today, () =>
            {
                m_datePicker.SetSelectedDateTime(DateTime.Now);
            });
        }

        if (m_extraButton is not null)
        {
            wrapperView.AddView(m_extraButton);
            SetLayoutParameterToView(m_extraButton, GravityFlags.Start);
        }
        
        SetLayoutParameterToView(wrapperView, GravityFlags.Start, 1);
        
        linearLayoutThatContainsButtons.AddView(wrapperView, 0);
    }

    private Button CreateButton(string text, Action onExecute)
    {
        var button = new Button(Application.Context, null, global::Android.Resource.Attribute.ButtonBarButtonStyle)
        {
            Text = text.ToUpper(),
            LetterSpacing = .06f,
        };
        
        button.SetTextColor(Colors.GetColor(ColorName.color_primary_90).ToPlatform());
        button.SetOnClickListener(new GenericButtonMenuClickListener(() =>
        {
            onExecute.Invoke();
            Close();
        }));

        return button;
    }
    
    private static void SetLayoutParameterToView(View view, GravityFlags? gravityFlags = null, float? weight = null, int width = ViewGroup.LayoutParams.WrapContent, int height = ViewGroup.LayoutParams.WrapContent)
    {
        var layoutParameters = new LinearLayout.LayoutParams(width, height);

        if (gravityFlags is not null)
        {
            layoutParameters.Gravity = gravityFlags.Value;
        }

        if (weight is not null)
        {
            layoutParameters.Weight = weight.Value;
        }

        view.LayoutParameters = layoutParameters;
    }

    private void SetDatePickerSelection(MaterialDatePicker.Builder builder)
    {
        var date = m_datePicker.IgnoreLocalTime ? m_datePicker.SelectedDate : m_datePicker.SelectedDate.ToLocalTime();

        //Java uses the unix epoch, so we have find the total milliseconds from the date people have picked and the UnixEpoch start.
        builder.SetSelection(date.ToLong());
    }

    public bool IsOpen()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(TimePickerService.TimePickerTag);
        return fragment is MaterialDatePicker;
    }

    public void Close()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(TimePickerService.TimePickerTag);
        if (fragment is MaterialDatePicker datePickerFragment)
        {
            datePickerFragment.Dismiss();
        }
    }

    public void OnPositiveButtonClick(Object? p0)
    {
        if (long.TryParse(m_materialDatePicker.Selection?.ToString(), out var milliseconds))
        {
            //Java uses the unix epoch, so we have to create a csharp date time based on the UnixEpoch start with the milliseconds picked by people using the date picker.
            var dateFromJava = DateTime.UnixEpoch.AddMilliseconds(milliseconds);

            var dateTime = m_datePicker.IgnoreLocalTime ? dateFromJava.ToUniversalTime() : dateFromJava.ToLocalTime();
            m_datePicker.SetSelectedDateTime(dateTime);
        }
    }
}