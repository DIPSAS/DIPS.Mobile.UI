using System.Windows.Input;
using DIPS.Mobile.UI.Components.Saving.SaveView;

namespace DIPS.Mobile.UI.Components.Pages.ContentSavePage;

public partial class ContentSavePage
{
    /// <summary>
    /// Determines whether the <see cref="SaveView"/> is currently saving or not
    /// </summary>
    /// <remarks>The content of this page will be overriden by <see cref="SaveView"/></remarks>
    public bool IsSaving
    {
        get => (bool)GetValue(IsSavingProperty);
        set => SetValue(IsSavingProperty, value);
    }

    /// <summary>
    /// The text to be displayed inside <see cref="SaveView"/> when <see cref="IsSaving"/> is true
    /// </summary>
    public string SavingText
    {
        get => (string)GetValue(SavingTextProperty);
        set => SetValue(SavingTextProperty, value);
    }

    /// <summary>
    /// Determines whether the <see cref="SaveView"/> has completed saving or not
    /// </summary>
    public bool IsSavingCompleted
    {
        get => (bool)GetValue(IsSavingCompletedProperty);
        set => SetValue(IsSavingCompletedProperty, value);
    }

    /// <summary>
    /// The text to be displayed inside <see cref="SaveView"/>  when <see cref="IsSaving"/> is true
    /// </summary>
    public string SavingCompletedText
    {
        get => (string)GetValue(SavingCompletedTextProperty);
        set => SetValue(SavingCompletedTextProperty, value);
    }

    /// <summary>
    /// The command to be executed when <see cref="IsSavingCompleted"/> is true and all animations are finished
    /// </summary>
    public ICommand SavingCompletedCommand
    {
        get => (ICommand)GetValue(SavingCompletedCommandProperty);
        set => SetValue(SavingCompletedCommandProperty, value);
    }
    
    public View OriginalContent
    {
        get => (View)GetValue(OriginalContentProperty);
        set => SetValue(OriginalContentProperty, value);
    }
    
    public static readonly BindableProperty OriginalContentProperty = BindableProperty.Create(
        nameof(OriginalContent),
        typeof(View),
        typeof(ContentSavePage),
        propertyChanged: (bindable, _, _) =>  ((ContentSavePage)bindable).OnOriginalContentChanged());
    
    public static readonly BindableProperty IsSavingProperty = BindableProperty.Create(
        nameof(IsSaving),
        typeof(bool),
        typeof(ContentPage),
        false,
        propertyChanged:IsSavingChanged);

    public static readonly BindableProperty SavingTextProperty = BindableProperty.Create(
        nameof(SavingText),
        typeof(string),
        typeof(ContentPage));

    public static readonly BindableProperty IsSavingCompletedProperty = BindableProperty.Create(
        nameof(IsSavingCompleted),
        typeof(bool),
        typeof(ContentPage),
        false);

    public static readonly BindableProperty SavingCompletedTextProperty = BindableProperty.Create(
        nameof(SavingCompletedText),
        typeof(string),
        typeof(ContentPage));

    public static readonly BindableProperty SavingCompletedCommandProperty = BindableProperty.Create(
        nameof(SavingCompletedCommand),
        typeof(ICommand), 
        typeof(ContentPage));
}