using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.Android;

/// <summary>
/// A native Material 3 styled search field for use in BottomSheets.
/// Uses an EditText with rounded background, search icon, and clear button
/// to match the Material 3 search bar design.
/// </summary>
internal class BottomSheetSearchField : Java.Lang.Object, ITextWatcher, AView.IOnFocusChangeListener
{
    private readonly WeakReference<BottomSheet> m_weakBottomSheet;
    private readonly EditText m_editText;
    private readonly FrameLayout m_container;
    private readonly ImageView m_searchIcon;
    private readonly ImageView m_clearButton;
    private string m_previousText = string.Empty;

    public BottomSheetSearchField(Context context, BottomSheet bottomSheet)
    {
        m_weakBottomSheet = new WeakReference<BottomSheet>(bottomSheet);

        var density = context.Resources?.DisplayMetrics?.Density ?? 1f;
        
        // Create the outer container with M3 search bar styling
        m_container = new FrameLayout(context);
        var containerParams = new LinearLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            (int)(56 * density)) // M3 search bar height is 56dp
        {
            LeftMargin = (int)(16 * density),
            RightMargin = (int)(16 * density),
            TopMargin = (int)(8 * density),
            BottomMargin = (int)(8 * density)
        };
        m_container.LayoutParameters = containerParams;

        // Rounded background matching M3 search bar (pill shape)
        var background = new GradientDrawable();
        background.SetShape(ShapeType.Rectangle);
        background.SetCornerRadius(28 * density);
        background.SetColor(Colors.GetColor(ColorName.color_surface_subtle).ToPlatform());
        m_container.Background = background;

        // Search icon
        m_searchIcon = new ImageView(context);
        m_searchIcon.SetImageResource(global::Android.Resource.Drawable.IcMenuSearch);
        m_searchIcon.SetColorFilter(Colors.GetColor(ColorName.color_icon_default).ToPlatform());
        var searchIconParams = new FrameLayout.LayoutParams(
            (int)(24 * density),
            (int)(24 * density),
            GravityFlags.CenterVertical | GravityFlags.Start);
        searchIconParams.LeftMargin = (int)(16 * density);
        m_searchIcon.LayoutParameters = searchIconParams;
        m_container.AddView(m_searchIcon);

        // EditText
        m_editText = new EditText(context);
        m_editText.SetHintTextColor(Colors.GetColor(ColorName.color_text_subtle).ToPlatform());
        m_editText.SetTextColor(Colors.GetColor(ColorName.color_text_default).ToPlatform());
        m_editText.Background = null; // Transparent - container handles background
        m_editText.SetSingleLine(true);
        m_editText.ImeOptions = ImeAction.Search;
        m_editText.InputType = InputTypes.ClassText | InputTypes.TextFlagNoSuggestions;
        var editTextParams = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.MatchParent);
        editTextParams.LeftMargin = (int)(48 * density); // After search icon
        editTextParams.RightMargin = (int)(48 * density); // Before clear button
        m_editText.LayoutParameters = editTextParams;
        m_editText.SetPadding(0, 0, 0, 0);
        m_editText.AddTextChangedListener(this);
        m_editText.OnFocusChangeListener = this;
        m_container.AddView(m_editText);

        // Clear button
        m_clearButton = new ImageView(context);
        m_clearButton.SetImageResource(global::Android.Resource.Drawable.IcMenuCloseClearCancel);
        m_clearButton.SetColorFilter(Colors.GetColor(ColorName.color_icon_default).ToPlatform());
        m_clearButton.Visibility = ViewStates.Gone;
        m_clearButton.Clickable = true;
        m_clearButton.Focusable = true;
        var clearParams = new FrameLayout.LayoutParams(
            (int)(24 * density),
            (int)(24 * density),
            GravityFlags.CenterVertical | GravityFlags.End);
        clearParams.RightMargin = (int)(16 * density);
        m_clearButton.LayoutParameters = clearParams;
        m_clearButton.Click += OnClearButtonClicked;
        m_container.AddView(m_clearButton);
    }

    public AView View => m_container;

    public void Focus()
    {
        m_editText.RequestFocus();
        var imm = (InputMethodManager?)m_editText.Context?.GetSystemService(Context.InputMethodService);
        imm?.ShowSoftInput(m_editText, ShowFlags.Implicit);
    }

    public void Unfocus()
    {
        m_editText.ClearFocus();
        var imm = (InputMethodManager?)m_editText.Context?.GetSystemService(Context.InputMethodService);
        imm?.HideSoftInputFromWindow(m_editText.WindowToken, 0);
    }

    private void OnClearButtonClicked(object? sender, EventArgs e)
    {
        m_editText.Text = string.Empty;
    }

    // ITextWatcher implementation
    public void AfterTextChanged(IEditable? s)
    {
        var newText = s?.ToString() ?? string.Empty;
        m_clearButton.Visibility = string.IsNullOrEmpty(newText) ? ViewStates.Gone : ViewStates.Visible;

        if (!m_weakBottomSheet.TryGetTarget(out var bottomSheet))
            return;

        bottomSheet.OnNativeSearchTextChanged(newText, m_previousText);
        m_previousText = newText;
    }

    public void BeforeTextChanged(Java.Lang.ICharSequence? s, int start, int count, int after)
    {
    }

    public void OnTextChanged(Java.Lang.ICharSequence? s, int start, int before, int count)
    {
    }

    // IOnFocusChangeListener implementation
    public void OnFocusChange(AView? v, bool hasFocus)
    {
        if (!m_weakBottomSheet.TryGetTarget(out var bottomSheet))
            return;

        if (hasFocus)
        {
            bottomSheet.OnSearchFieldFocused();
        }
        else
        {
            bottomSheet.OnSearchFieldUnfocused();
        }
    }

    public void Cleanup()
    {
        m_editText.RemoveTextChangedListener(this);
        m_editText.OnFocusChangeListener = null;
        m_clearButton.Click -= OnClearButtonClicked;
    }
}
