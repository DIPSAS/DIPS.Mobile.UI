using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Extensions.Android;

namespace DIPS.Mobile.UI.Components.Lists;

internal class KeyboardDismissOnScrollListener : RecyclerView.OnScrollListener
{
    private bool m_hasHiddenKeyboard;
    private bool m_userIsScrolling;

    public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
    {
        base.OnScrollStateChanged(recyclerView, newState);

        switch (newState)
        {
            case RecyclerView.ScrollStateDragging:
                m_userIsScrolling = true;
                break;
            case RecyclerView.ScrollStateIdle:
                m_userIsScrolling = false;
                m_hasHiddenKeyboard = false;
                break;
        }
    }

    public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
    {
        base.OnScrolled(recyclerView, dx, dy);

        if (!m_userIsScrolling || m_hasHiddenKeyboard || dy == 0)
            return;

        m_hasHiddenKeyboard = true;

        KeyboardHelper.HideKeyboardAndClearFocus(recyclerView);
    }
}
