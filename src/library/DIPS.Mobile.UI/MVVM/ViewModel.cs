using System.ComponentModel;
using System.Runtime.CompilerServices;
using DIPS.Mobile.UI.MemoryManagement;

namespace DIPS.Mobile.UI.MVVM;

/// <summary>
/// A view model base class to be used in a model-view-view-model applications,  based on the observable pattern implementing <see cref="INotifyPropertyChanged"/>.
/// </summary>
public abstract class ViewModel : INotifyPropertyChanged
{
    ~ViewModel()
    {
#if DEBUG
        if (ShouldLogWhenGarbageCollected)
        {
            GarbageCollection.Print($"Called finalizer an instance of {GetType().Name}");
        }
#endif
    }

    /// <summary>
    /// Will log to Console when the finalizer has run. This happens when the object was garbage collected.
    /// </summary>
    /// <remarks>This will only run in Debug</remarks>
    public bool ShouldLogWhenGarbageCollected { get; set; }

    /// <summary>
    /// Sets a value to a backing field if it passes a equality check and notifies property changed.
    /// </summary>
    /// <typeparam name="TS">The type of the property</typeparam>
    /// <param name="backingStore">The backing store that will hold the value of the property</param>
    /// <param name="value">The new value that should be set</param>
    /// <param name="propertyName">A nullable property name, if left empty it will pick the caller member name</param>
    /// <remarks>This method does a equality check to see if the value has changed, if you need to notify property changed when the value has not changed, please use <see cref="RaisePropertyChanged(string)"/></remarks>
    /// <returns>A boolean value to indicate that the property changed has been invoked</returns>
    public bool RaiseWhenSet<TS>(ref TS backingStore, TS value, [CallerMemberName] string propertyName = "")
     {
         if (EqualityComparer<TS>.Default.Equals(backingStore, value))
         {
             return false;
         }

         backingStore = value;
         RaisePropertyChanged(propertyName);
         return true;
     }
    

     /// <summary>
    /// Raises property changed for the property named <see cref="propertyName"/>
    /// </summary>
     /// <param name="propertyName">A nullable property name, if left empty it will pick the caller member name</param>
    public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Raises property changed for the properties named <see cref="properties"/>
    /// </summary>
    /// <param name="properties">The properties to notify property changed for.</param>
    public void RaisePropertyChanged(params string[] properties)
    {
        foreach (var property in properties)
        {
            RaisePropertyChanged(property);
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
}