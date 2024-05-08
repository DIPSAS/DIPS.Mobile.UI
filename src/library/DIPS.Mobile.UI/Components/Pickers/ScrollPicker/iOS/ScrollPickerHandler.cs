using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Platforms.iOS;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public partial class ScrollPickerHandler : ViewHandler<ScrollPicker, UIButton>
{
    private Chip m_chip;
    private ScrollPickerViewController? m_scrollPickerViewController;

    protected override UIButton CreatePlatformView()
    {
        m_chip = new Chip { Style = Styles.GetChipStyle(ChipStyle.Input) };
        m_chip.Command = new Command(() => OnTapped(m_chip));

        return (UIButton)m_chip.ToPlatform(DUI.GetCurrentMauiContext!);
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);

        if (VirtualView.Components is {Count:0})
            throw new Exception("The components of ScrollPicker must be set!");

        m_scrollPickerViewModel = new ScrollPickerViewModel(VirtualView.Components);
        m_scrollPickerViewModel.SetDefaultSelectedItemsForAllComponents();
        m_scrollPickerViewModel.OnAnySelectedIndexesChanged += SetChipTitle;
        
        SetChipTitle();
    }

    private void SetChipTitle()
    {
        if (m_scrollPickerViewModel.IsComponentsSelectedItemNull)
        {
            m_chip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            m_chip.Title = DUILocalizedStrings.Choose;
            return;
        }
        
        var componentCount = m_scrollPickerViewModel.GetComponentCount();
        var texts = new string[componentCount];
        for (var i = 0; i < m_scrollPickerViewModel.GetComponentCount(); i++)
        {
            AddTextInRowFromComponent(i, texts);
        }
        
        m_chip.Style = Styles.GetChipStyle(ChipStyle.Input);
        m_chip.Title = texts.Length == 1 ? texts[0] : string.Join(VirtualView.SeparatorText, texts);
        m_scrollPickerViewController?.OnChipTitleChanged();
    }

    private void AddTextInRowFromComponent(int i, IList<string> texts)
    {
        var selectedIndexForComponent = m_scrollPickerViewModel.SelectedIndexForComponent(i);
        texts[i] = m_scrollPickerViewModel.GetTextForRowInComponent(selectedIndexForComponent, i); 
    }

    private void OnTapped(Chip chip)
    {
        if (PlatformView.Window.RootViewController is not {} rootViewController)
            return;
        
        m_scrollPickerViewController = new ScrollPickerViewController();
        m_scrollPickerViewController.Setup(chip, m_scrollPickerViewModel, OnClear);
        
        _ = rootViewController.PresentViewControllerAsync(m_scrollPickerViewController, true);
    }

    private void OnClear()
    {
        m_scrollPickerViewModel.SetToNull();
    }
    
    protected override void DisconnectHandler(UIButton platformView)
    {
        base.DisconnectHandler(platformView);

        m_scrollPickerViewModel.Dispose();
        m_scrollPickerViewModel = null!;
        m_chip.Command = null;
        m_scrollPickerViewController = null;
        m_scrollPickerViewModel.OnAnySelectedIndexesChanged -= SetChipTitle;
    }

}

internal class DUIPickerViewModel : UIPickerViewModel
{
#nullable disable
    public IScrollPickerViewModel ScrollPickerViewModel { get; set; }
#nullable enable
    
    public override nint GetComponentCount(UIPickerView pickerView)
    {
        return ScrollPickerViewModel.GetComponentCount();
    }

    public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
    {
        return ScrollPickerViewModel.GetRowsInComponent((int)component);
    }

    public override string GetTitle(UIPickerView pickerView, IntPtr row, IntPtr component)
    {
        return ScrollPickerViewModel.GetTextForRowInComponent((int)row, (int)component);
    }

    public override void Selected(UIPickerView pickerView, IntPtr row, IntPtr component)
    {
        ScrollPickerViewModel.SelectedRowInComponent((int)row, (int)component);
    }
}

internal class ScrollPickerViewController : UIViewController
{
#nullable disable
    private IScrollPickerViewModel m_scrollPickerViewModel;
    private UIPickerView m_uiPicker;
    private UIButton m_uiButton;
    private UIButton m_clearButton;
#nullable enable

    public void Setup(Chip chip, IScrollPickerViewModel scrollPickerViewModel, Action onClear)
    {
        m_scrollPickerViewModel = scrollPickerViewModel;
        m_uiPicker = new UIPickerView();
        m_uiButton = chip.ToPlatform(DUI.GetCurrentMauiContext!) as UIButton;
        
        var vm = new DUIPickerViewModel { ScrollPickerViewModel = m_scrollPickerViewModel };
        m_uiPicker.Model = vm;

        m_scrollPickerViewModel.OnAnyComponentsDataInvalidated += OnAnyComponentsDataInvalidated;
        m_scrollPickerViewModel.SetDefaultSelectedItemsForAllComponents(true);
        
        for (var i = 0; i < m_scrollPickerViewModel.GetComponentCount(); i++)
        {
            var selectedIndex = m_scrollPickerViewModel.SelectedIndexForComponent(i);
            m_uiPicker.Select(selectedIndex, i, false);
            vm.Selected(m_uiPicker, selectedIndex, i);
        }

        ModalPresentationStyle = UIModalPresentationStyle.Popover;

        if (PopoverPresentationController is null)
            return;
        
        SetPresentationControllerProperties();
        CreateClearButton(onClear);
    }

    private void CreateClearButton(Action onClear)
    {
        if (!m_scrollPickerViewModel.IsNullable)
            return;

        m_clearButton = new UIButtonWithExtraTappableArea();
        m_clearButton.AddGestureRecognizer(new UITapGestureRecognizer(_ =>
        {
            DismissViewControllerAsync(true);
            onClear.Invoke();
        }));
        m_clearButton.SetTitle(DUILocalizedStrings.Remove, UIControlState.Normal);
        m_clearButton.SetTitleColor(Colors.GetColor(ColorName.color_primary_90).ToPlatform(), UIControlState.Normal);
    }

    private void SetPresentationControllerProperties()
    {
        PopoverPresentationController!.PermittedArrowDirections =
            UIPopoverArrowDirection.Up | UIPopoverArrowDirection.Down;
        PopoverPresentationController.SourceView = m_uiButton!;
        PopoverPresentationController.SourceRect = m_uiButton!.Bounds;
        PopoverPresentationController.Delegate = new TipUIPopoverPresentationControllerDelegate();

        if (OperatingSystem.IsIOSVersionAtLeast(16, 0))
        {
            PopoverPresentationController.SourceItem = m_uiButton;
        }
    }

    private void OnAnyComponentsDataInvalidated()
    {
        for (var i = 0; i < m_scrollPickerViewModel.GetComponentCount(); i++)
        {
            var initialSelectedIndexForComponent = m_scrollPickerViewModel.SelectedIndexForComponent(i);
            m_uiPicker.Select(initialSelectedIndexForComponent, i, true);
        }
        
        m_scrollPickerViewModel.SendSelectedIndicesChanged();
    }

    public async void OnChipTitleChanged()
    {
        if (PopoverPresentationController is null || m_uiButton is null)
            return;

        await Task.Delay(1);
        PopoverPresentationController.SourceView = m_uiButton!;
        PopoverPresentationController.SourceRect = m_uiButton!.Bounds;
        PopoverPresentationController.ContainerView.SetNeedsLayout();
        PopoverPresentationController.ContainerView.LayoutIfNeeded();
    }

    public override void ViewWillAppear(bool animated)
    {
        base.ViewWillAppear(animated);
        
        var padding = new Thickness(Sizes.GetSize(SizeName.size_6) * m_scrollPickerViewModel.GetComponentCount());
        if (View is null) 
            return;

        var width = CalculatePopoverWidth(m_uiPicker.Bounds);

        var height = m_uiPicker.IntrinsicContentSize.Height;
        if (m_clearButton is not null)
        {
            height += m_clearButton.IntrinsicContentSize.Height;
        }
        
        PreferredContentSize = new CGSize(width + padding.HorizontalThickness, height);
        
        m_scrollPickerViewModel.SendSelectedIndicesChanged();
    }
    
    public override void ViewDidLoad()
    {
        View = new UIStackView { Axis = UILayoutConstraintAxis.Vertical, Spacing = Sizes.GetSize(SizeName.size_1) };
        View.AddSubview(m_uiPicker);
        if(m_clearButton is not null)
            View.AddSubview(m_clearButton);

        base.ViewDidLoad();
    }

    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();
        
        if(m_uiPicker is null)
            return;
        
        m_uiPicker.TranslatesAutoresizingMaskIntoConstraints = false;
        
        NSLayoutConstraint.ActivateConstraints([
            m_uiPicker.LeadingAnchor.ConstraintEqualTo(View!.LeadingAnchor),
            m_uiPicker.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            m_uiPicker.TopAnchor.ConstraintEqualTo(View.TopAnchor),
            m_uiPicker.BottomAnchor.ConstraintEqualTo(View.BottomAnchor),
        ]);

        if (m_clearButton is null)
            return;

        m_clearButton.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints([
            m_clearButton.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
            m_clearButton.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, -15),
            m_clearButton.LeadingAnchor.ConstraintEqualTo(View!.LeadingAnchor),
        ]);
    }

    private float CalculatePopoverWidth(CGRect bounds)
    {
        var popoverWidth = 0f;
        
        for (var component = 0; component < m_scrollPickerViewModel.GetComponentCount(); component++)
        {
            var rowsInComponent = m_scrollPickerViewModel.GetRowsInComponent(component);
            var textsInComponent = new string[rowsInComponent];
            for (var row = 0; row < rowsInComponent; row++)
            {
                textsInComponent[row] = m_scrollPickerViewModel.GetTextForRowInComponent(row, component);
            }
            
            var longestTextInItemsSource = textsInComponent.OrderByDescending(s => s.Length).First();

            var nssString = new NSString(longestTextInItemsSource);

            var boundingRect = nssString.GetBoundingRect(new CGSize(bounds.Width, nfloat.PositiveInfinity),
                NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes { Font = UIFont.PreferredTitle1 },null);

            popoverWidth += (float)boundingRect.Width;
        }
        
        return popoverWidth;
    }
    
    public override void ViewWillDisappear(bool animated)
    {
        base.ViewWillDisappear(animated);
        
        m_scrollPickerViewModel.OnAnyComponentsDataInvalidated -= OnAnyComponentsDataInvalidated;
        
        m_uiPicker = null!;
        m_uiButton = null!;
        m_clearButton = null!;
        
        if(PopoverPresentationController is not null)
            PopoverPresentationController.Delegate = null!;
    }
    
}