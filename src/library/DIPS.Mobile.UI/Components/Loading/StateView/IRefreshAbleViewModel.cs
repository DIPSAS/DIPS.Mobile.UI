namespace DIPS.Mobile.UI.Components.Loading.StateView;

internal interface IRefreshAbleViewModel
{
    /// <summary>
    /// Determines whether the view should have a <see cref="RefreshView"/>
    /// </summary>
    public bool HasRefreshView { get; set; }
}