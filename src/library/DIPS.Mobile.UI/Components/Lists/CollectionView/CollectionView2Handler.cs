#if __IOS__
using CollectionViewHandlerImpl = Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2;
#else
using CollectionViewHandlerImpl = Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler;
#endif

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView2Handler : CollectionViewHandlerImpl;
