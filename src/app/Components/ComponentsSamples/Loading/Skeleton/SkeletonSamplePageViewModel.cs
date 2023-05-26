using System.ComponentModel;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.Loading.Skeleton
{
    public class SkeletonSamplePageViewModel : ViewModel
    {
        private bool isLoading;
        private string[] Headers = new[] {"This is a header", "Other headers might be longer", "Trying something new!"};

        public SkeletonSamplePageViewModel()
        {
            isLoading = false;
        }

        public string Title { get; set; } = "Initial header is here";
        public string SubTitle { get; set; } = "Smaller content. Might be a much longer text. Be aware of line shifts";
        public string Initials { get; set; } = "EK";
        public bool IsLoading { get => isLoading; set => RaiseWhenSet(ref isLoading, value); }
    }
}