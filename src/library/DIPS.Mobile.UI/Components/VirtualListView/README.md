This implementation is taken from https://github.com/Redth/Maui.VirtualListView version 0.3.1.

Improvements made:
* (iOS) If `IsRefreshEnabled` is set to false, the `RefreshView` is removed.
* (iOS) The `ScrolledCommand` and `OnScrolled` is now invoked.
* Added `ViewToUnfocusOnScrolled` property so that it unfocuses the assigned view when the list is scrolled.
* Added `container` parameter to `VirtualListViewItemTemplateSelector` so that consumers can do additional logic to display their correct DataTemplates.