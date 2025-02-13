using CoreGraphics;
using UIKit;

namespace DIPS.Mobile.UI.Components.TextFields.Entry.iOS;

/// <summary>
/// Based on https://github.com/dotnet/maui/blob/main/src/Core/src/Platform/iOS/MauiDoneAccessoryView.cs#L8
/// </summary>
internal sealed class MauiDoneAccessoryView : UIToolbar
{
    readonly BarButtonItemProxy m_proxy;

    public MauiDoneAccessoryView() : base(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 44))
    {
        m_proxy = new BarButtonItemProxy();
        BarStyle = UIBarStyle.Default;
        Translucent = true;
        var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
        var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, m_proxy.OnDataClicked);

        SetItems([spacer, doneButton], false);
    }

    internal void SetDoneClicked(Action<object>? value) => m_proxy.SetDoneClicked(value);


    internal void SetDataContext(object? dataContext) => m_proxy.SetDataContext(dataContext);

    public MauiDoneAccessoryView(Action doneClicked) : base(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 44))
    {
        m_proxy = new BarButtonItemProxy(doneClicked);
        BarStyle = UIBarStyle.Default;
        Translucent = true;

        var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
        var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, m_proxy.OnClicked);
        SetItems([spacer, doneButton], false);
    }

    class BarButtonItemProxy
    {
        readonly Action? _doneClicked;
        Action<object>? _doneWithDataClicked;
        WeakReference<object>? _data;

        public BarButtonItemProxy() { }

        public BarButtonItemProxy(Action doneClicked)
        {
            _doneClicked = doneClicked;
        }

        public void SetDoneClicked(Action<object>? value) => _doneWithDataClicked = value;

        public void SetDataContext(object? dataContext) => _data = dataContext is null ? null : new(dataContext);

        public void OnDataClicked(object? sender, EventArgs e)
        {
            if (_data is not null && _data.TryGetTarget(out var data))
                _doneWithDataClicked?.Invoke(data);
        }

        public void OnClicked(object? sender, EventArgs e) => _doneClicked?.Invoke();
    }
}