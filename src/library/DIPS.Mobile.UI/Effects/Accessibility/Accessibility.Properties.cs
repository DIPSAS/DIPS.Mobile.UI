namespace DIPS.Mobile.UI.Effects.Accessibility;

public partial class Accessibility
{
    public static readonly BindableProperty ModeProperty = BindableProperty.CreateAttached("Mode",
        typeof(Mode),
        typeof(Accessibility),
        Mode.None,
        propertyChanged: OnModeChanged);
    
    public static readonly BindableProperty TraitProperty = BindableProperty.CreateAttached("Trait",
        typeof(Trait),
        typeof(Accessibility),
        Trait.None,
        propertyChanged: OnTraitChanged);
}

public enum Mode
{
    None = 0,
    /// <summary>
    /// Groups all children of a container into a single accessibility element. <br/>
    /// For more information, see <see href="https://github.com/DIPSAS/DIPS.Mobile.UI/wiki/Accessibility#groupchildren-mode">GroupChildren Mode</see>
    /// </summary>
    /// <remarks>
    /// Note: This does not take into account if the hierarchy changes in runtime, or if any texts changes after the layout has been rendered
    /// <br />
    /// <para>
    /// This effect combines the text content of all descendant elements into the parent container's accessibility description,
    /// allowing screen readers to read all content in a single focus gesture instead of requiring users to navigate each child separately.
    /// </para>
    /// <para><b>When to use:</b></para>
    /// <list type="bullet">
    /// <item>Multiple child labels or text elements that form a single logical piece of information (e.g., address blocks, contact information, product details)</item>
    /// <item>Read-only informational displays where all content should be announced together</item>
    /// <item>Card-like UI patterns where related information should be grouped</item>
    /// <item>When you want to reduce the number of swipe gestures needed for screen reader users</item>
    /// </list>
    /// <para><b>When NOT to use:</b></para>
    /// <list type="bullet">
    /// <item>When any child element is interactive (Button, Entry, Switch, etc.) - interactive elements need individual focus for usability</item>
    /// <item>When child elements represent separate, unrelated pieces of information that users might want to navigate individually</item>
    /// <item>When children have complex accessibility requirements or need custom hints/traits</item>
    /// <item>In lists or collections where each item should be individually navigable</item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;VerticalStackLayout dui:Accessibility.Mode="GroupChildren"&gt;
    ///     &lt;Label Text="John Doe" /&gt;
    ///     &lt;Label Text="Born: 1980-05-15" /&gt;
    ///     &lt;HorizontalStackLayout&gt;
    ///         &lt;Label Text="Phone:" /&gt;
    ///         &lt;Label Text="+47 123 45 678" /&gt;
    ///     &lt;/HorizontalStackLayout&gt;
    ///     &lt;Label Text="john.doe@example.com" /&gt;
    /// &lt;/VerticalStackLayout&gt;
    /// &lt;!-- VoiceOver/TalkBack will read: "John Doe, Born: 1980-05-15, Phone:, +47 123 45 678, john.doe@example.com" --&gt;
    /// </code>
    /// <para>Without this mode, screen readers would require 5 separate swipe gestures to read all information. With GroupChildren, it's read in one focus.</para>
    /// </example>
    GroupChildren = 1
}

[Flags]
public enum Trait
{
    None = 0,
    /// <summary>
    /// Indicates the element behaves as a button.
    /// iOS: Adds UIAccessibilityTrait.Button
    /// Android: Sets class name to "android.widget.Button"
    /// </summary>
    Button = 1 << 0,
    
    /// <summary>
    /// Indicates the element is in a selected state.
    /// iOS: Adds UIAccessibilityTrait.Selected
    /// Android: Sets AccessibilityNodeInfo.Checked = true
    /// Screen readers will announce "Selected" along with the element's description.
    /// </summary>
    Selected = 1 << 1,
    
    /// <summary>
    /// Indicates the element is not in a selected state.
    /// iOS: Does not add Selected trait
    /// Android: Sets AccessibilityNodeInfo.Checked = false
    /// Screen readers will announce "Not Selected" along with the element's description.
    /// </summary>
    NotSelected = 1 << 2
}
