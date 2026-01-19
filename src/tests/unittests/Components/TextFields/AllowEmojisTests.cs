using DIPS.Mobile.UI.Formatters;
using Editor = DIPS.Mobile.UI.Components.TextFields.Editor.Editor;
using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;

namespace DIPS.Mobile.UI.UnitTests.Components.TextFields;

public class AllowEmojisTests
{
    [Fact]
    public void Entry_Should_Remove_Emojis_When_AllowEmojis_False()
    {
        var entry = new Entry();
        entry.Text = "Hello 😄 World";
        entry.AllowEmojis = false;

        // Simulate unfocus by invoking private handler
        var mi = typeof(Entry).GetMethod("OnUnfocusedReplaceEmojis", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        mi!.Invoke(entry, new object?[] { null, null });

        entry.Text.Should().Be("Hello [Emoji] World");
    }

    [Fact]
    public void Entry_Should_Preserve_Emojis_When_AllowEmojis_True()
    {
        var entry = new Entry();
        entry.Text = "Hi 🎉";
        entry.AllowEmojis = true;

        var mi = typeof(Entry).GetMethod("OnUnfocusedReplaceEmojis", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        mi!.Invoke(entry, new object?[] { null, null });

        entry.Text.Should().Be("Hi 🎉");
    }

    [Fact]
    public void Editor_Should_Remove_Emojis_When_AllowEmojis_False()
    {
        var editor = new Editor();
        editor.Text = "Line1 😄\nLine2";
        editor.AllowEmojis = false;

        var mi = typeof(Editor).GetMethod("OnUnfocusedReplaceEmojis", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        mi!.Invoke(editor, new object?[] { null, null });

        editor.Text.Should().Be("Line1 [Emoji]\nLine2");
    }

    [Fact]
    public void Editor_Should_Preserve_Emojis_When_AllowEmojis_True()
    {
        var editor = new Editor();
        editor.Text = "Line 😄";
        editor.AllowEmojis = true;

        var mi = typeof(Editor).GetMethod("OnUnfocusedReplaceEmojis", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        mi!.Invoke(editor, new object?[] { null, null });

        editor.Text.Should().Be("Line 😄");
    }

    [Fact]
    public void ReplaceAllEmojisWithPlaceholder_Should_Use_Custom_Placeholder()
    {
        var input = "A 😄 B";
        var result = input.ReplaceAllEmojisWithPlaceholder("<E>");
        result.Should().Be("A <E> B");
    }
}
