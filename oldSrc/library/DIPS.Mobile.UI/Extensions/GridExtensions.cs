using Xamarin.Forms;

namespace DIPS.Mobile.UI.Extensions
{
    public static class GridExtensions
    {
        /// <summary>
        /// Remove the child  positioned in <see cref="column"/>, <see cref="row"/> from a grid.
        /// </summary>
        /// <param name="grid">The grid to remove the child from</param>
        /// <param name="column">The column position to remove the child at.</param>
        /// <param name="row">The row position to remove the child at.</param>
        public static void RemoveChildAt(this Grid grid, int column, int row)
        {
            View? theViewToRemove = null;
            foreach (var gridChild in grid.Children)
            {
                if (Grid.GetRow(gridChild) == row && Grid.GetColumn(gridChild) == column)
                {
                    theViewToRemove = gridChild;
                }
            }

            if (theViewToRemove != null)
            {
                grid.Children.Remove(theViewToRemove);
            }
        }
    }   
}