using System.ComponentModel;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Loading.Skeleton
{
    public class SkeletonSamplePageViewModel : ViewModel
    {
        private bool m_isBusy;

        public SkeletonSamplePageViewModel()
        {
            m_isBusy = false;
        }

        public bool IsBusy
        {
            get => m_isBusy; 
            set => RaiseWhenSet(ref m_isBusy, value);
        }
    }
}