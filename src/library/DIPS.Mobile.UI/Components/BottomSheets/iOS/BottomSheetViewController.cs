using DIPS.Mobile.UI.MemoryManagement;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

public class BottomSheetViewController : UIViewController
{
    private readonly BottomSheetContainer m_container;
    private BottomBarView? m_bottomBar;
    private BottomSheetNavigationBarHelper? m_navigationBarHelper;
    private BottomSheetSearchController? m_searchController;

    public BottomSheetViewController(BottomSheet bottomSheet)
    {
        BottomSheet = bottomSheet;
        
        bottomSheet.ViewController = this;

        m_container = new BottomSheetContainer(bottomSheet);
    }
    
    public BottomSheet BottomSheet { get; }
    
    public async void ModifySearchbar(bool add)
    {
        // Delay to make sure the navigation item is available
        await Task.Delay(1);
        
        if (add)
        {
            m_searchController = new BottomSheetSearchController(BottomSheet);
            NavigationItem.SearchController = m_searchController.SearchController;
            NavigationItem.HidesSearchBarWhenScrolling = false;
            DefinesPresentationContext = true;
            
            // Set focus/unfocus actions on the BottomSheet
            BottomSheet.FocusSearchAction = () => m_searchController?.Focus();
            BottomSheet.UnfocusSearchAction = () => m_searchController?.Unfocus();
        }
        else
        {
            NavigationItem.SearchController = null;
            m_searchController?.Cleanup();
            m_searchController = null;
            BottomSheet.FocusSearchAction = null;
            BottomSheet.UnfocusSearchAction = null;
        }
    }
    
    public async void ModifyBottomBar(bool add)
    {
        // Delay to make sure the bottom bar view is created
        await Task.Delay(1);
        
        if (add)
        {
            m_bottomBar?.Show();
        }
        else
        {
            m_bottomBar?.Remove();
        }
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        if(View is null)
            return;
        
        m_container.AddToView(View);
        m_bottomBar = new BottomBarView(View, BottomSheet);
        
        m_navigationBarHelper = new BottomSheetNavigationBarHelper(BottomSheet, NavigationItem, NavigationController);
        m_navigationBarHelper.Configure();
    }

    public override void ViewDidAppear(bool animated)
    {
        base.ViewDidAppear(animated);
        
        if (NavigationController is { NavigationBarHidden: false, NavigationBar: not null })
        {
            UIAccessibility.PostNotification(UIAccessibilityPostNotification.ScreenChanged, NavigationController.NavigationBar);
        }
        else
        {
            UIAccessibility.PostNotification(UIAccessibilityPostNotification.ScreenChanged, View);
        }
    }

    public void Opened()
    {
        BottomSheet.SendOpen();
    }

    internal static Positioning GetCurrentPosition(UISheetPresentationController sheetPresentationController)
    {
        return sheetPresentationController.SelectedDetentIdentifier switch
        {
            UISheetPresentationControllerDetentIdentifier.Unknown => Positioning.Fit,
            UISheetPresentationControllerDetentIdentifier.Medium => Positioning.Medium,
            UISheetPresentationControllerDetentIdentifier.Large => Positioning.Large,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void SetPositioning()
    {
        var controller = NavigationController ?? (UIViewController)this;
        controller.SetPositioning(BottomSheet, m_container);
    }

    public void SetTitle()
    {
        m_navigationBarHelper?.UpdateTitle();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        m_navigationBarHelper?.Dispose();
        m_searchController?.Cleanup();
        m_searchController = null;
        BottomSheet.FocusSearchAction = null;
        BottomSheet.UnfocusSearchAction = null;
        BottomSheet.SendClose();
        BottomSheetService.RemoveFromStack(BottomSheet);

        _ = GCCollectionMonitor.Instance.CheckIfObjectIsAliveAndTryResolveLeaks(BottomSheet.ToCollectionContentTarget());
        BottomSheet.DisconnectHandlers();
        m_container.DisconnectHandlers();
        m_bottomBar?.DisconnectHandlers();
    }
}