namespace DIPS.Mobile.UI.Components.TextFields.Editor;

public partial class EditorHandler : Microsoft.Maui.Handlers.EditorHandler
{
    public EditorHandler() : base(PropertyMapper)
    {
    }
    
    public static IPropertyMapper<Editor, EditorHandler> PropertyMapper = new PropertyMapper<Editor, EditorHandler>(Mapper)
    {
        [nameof(Editor.HasBorder)] = MapHasBorder,
        [nameof(Editor.ShouldSelectAllTextOnFocused)] = MapShouldSelectTextOnTapped,
        [nameof(Editor.ShouldUseDefaultPadding)] = MapShouldUseDefaultPadding
    };

    private static partial void MapShouldUseDefaultPadding(EditorHandler handler, Editor editor);
    private static partial void MapShouldSelectTextOnTapped(EditorHandler handler, Editor entry);
    private static partial void MapHasBorder(EditorHandler handler, Editor entry);
}