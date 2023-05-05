using System.Collections;
using System.Collections.ObjectModel;

namespace DIPS.Mobile.UI.Extensions;

public static class ObservableCollectionExtensions
{
    public static void AddWithRespectOf<T>(this ObservableCollection<T> observableCollection,  T item, IEnumerable originalItems, Func<T, bool> predicate)
    {
       
    } 
}