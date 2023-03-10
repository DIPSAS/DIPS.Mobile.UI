using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: InternalsVisibleTo("DIPS.Mobile.UI.Droid")]
[assembly: InternalsVisibleTo("DIPS.Mobile.UI.iOS")]
[assembly: InternalsVisibleTo("DIPS.Mobile.UI.UnitTests")]

[assembly: Preserve]
//Add new namespaces below to make them visible when using Custom Namespace : https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/1
[assembly:XmlnsPrefix("http://dips.com/mobile.ui", "dui")]

[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pages")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Resources.Colors")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Pickers")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Buttons")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.ContextMenus")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Lists")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Labels")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Images")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.CheckBoxes")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.BottomSheets")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Searching")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Progress")]
[assembly: XmlnsDefinition("http://dips.com/mobile.ui", "DIPS.Mobile.UI.Components.Shell")]