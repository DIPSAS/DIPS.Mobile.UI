Making your app accessible ensures that all users, including those with disabilities, can effectively use your application. Accessibility features work with platform screen readers like **VoiceOver** (iOS) and **TalkBack** (Android).

## Table of Contents
- [MAUI SemanticProperties](#maui-semanticproperties)
  - [Description](#description)
  - [Hint](#hint)
  - [HeadingLevel](#headinglevel)
- [MAUI AutomationProperties](#maui-automationproperties)
  - [ExcludedWithChildren](#excludedwithchildren)
  - [IsInAccessibleTree](#isinaccessibletree)
- [DIPS.Mobile.UI Accessibility Helpers](#dipsmobileui-accessibility-helpers)
  - [Accessibility.Mode - GroupChildren](#accessibilitymode---groupchildren)
  - [Accessibility.Trait - Button and Selection States](#accessibilitytrait---button-and-selection-states)
- [Touch Effect Accessibility](#touch-effect-accessibility)
- [ListItem with Interactive Content](#listitem-with-interactive-content)
- [Best Practices](#best-practices)
- [Enabling Screen Readers](#enabling-screen-readers)

---

## MAUI SemanticProperties

.NET MAUI provides `SemanticProperties` which are attached properties that define information about which controls should receive accessibility focus and which text should be read aloud to the user. These properties set platform accessibility values so that a screen reader can speak about the element.

### Description

The `SemanticProperties.Description` attached property represents a short, descriptive text that a screen reader uses to announce an element. This property should be set for elements that have a meaning that's important for understanding the content or interacting with the user interface.

#### Usage:

```xml
<Image Source="dotnet_bot.png"
       SemanticProperties.Description="Cute dot net bot waving hi to you!" />
```

Or in C#:

```csharp
Image image = new Image { Source = "dotnet_bot.png" };
SemanticProperties.SetDescription(image, "Cute dot net bot waving hi to you!");
```

#### ⚠️ Critical behavior: Description excludes children - with platform differences!

**When you set `SemanticProperties.Description` on a parent element that has children, the behavior differs between platforms:**

**iOS (VoiceOver):**
- **All child elements are excluded** from the accessibility tree
- Screen readers only announce the parent's custom description
- Users cannot navigate to any children, including interactive elements

**Android (TalkBack):**
- **Non-interactive child elements (Label, Image, etc.) are excluded** from the accessibility tree
- **Interactive child elements (Button, Entry, Switch, etc.) remain accessible**
- Users can still navigate to buttons and other interactive controls within the parent

This is native platform behavior and developers need to be aware of these differences!

##### Example showing this behavior:

```xml
<!-- Without Description: Each element is individually accessible -->
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    
    <Label Text="John Doe" />
    <Label Text="Born: 1980-05-15" Grid.Row="1" />
    <Button Text="Call" Grid.Row="2" Command="{Binding CallCommand}" />
</Grid>
<!-- Result iOS: 3 swipe gestures (2 labels + 1 button) -->
<!-- Result Android: 3 swipe gestures (2 labels + 1 button) -->

<!-- With Description: Platform differences appear -->
<Grid SemanticProperties.Description="John Doe, Born: 1980-05-15">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    
    <Label Text="John Doe" />
    <Label Text="Born: 1980-05-15" Grid.Row="1" />
    <Button Text="Call" Grid.Row="2" Command="{Binding CallCommand}" />
</Grid>
<!-- Result iOS: 1 swipe gesture (parent description only, button excluded) -->
<!-- Result Android: 2 swipe gestures (parent description + button, labels excluded) -->
```

##### Platform differences with Touch effect:

When combining `SemanticProperties.Description` with `dui:Touch.Command`:

```xml
<Grid dui:Touch.Command="{Binding AddItemCommand}"
      SemanticProperties.Description="Add new item card">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    
    <Label Text="Title" />
    <Label Text="Subtitle" Grid.Row="1" />
    <Button Text="Details" Grid.Row="2" Command="{Binding DetailsCommand}" />
</Grid>
<!-- Result iOS: 1 swipe gesture (parent as button, all children excluded) -->
<!-- Result Android: 2 swipe gestures (parent as button + Details button, labels excluded) -->
```

**Key insight:** On iOS, setting `Description` on a parent excludes ALL children. On Android, it excludes non-interactive children (Labels, Images) but interactive elements (Buttons, Entries, Switches) remain accessible.

#### Important notes:
- **Avoid** setting `Description` on a `Label`. This will stop the `Text` property being spoken by the screen reader. The visual text should ideally match the text read aloud.
- **Avoid** setting `Description` on an `Entry` or `Editor` on Android. This will stop TalkBack actions from functioning. Instead, use the `Placeholder` property or the `Hint` attached property.
- **Be aware** of platform differences: On iOS, children are completely excluded when you set Description on a parent. On Android, non-interactive children (Labels, Images) are excluded but interactive elements (Buttons, Entries) remain accessible.
- **If you have interactive children on iOS** and need them accessible, either don't set Description on the parent, or use a different approach for grouping information.
- **For consistent cross-platform behavior** where you want to exclude all children on both platforms, use `AutomationProperties.ExcludedWithChildren="true"` instead.

---

### Hint

The `SemanticProperties.Hint` attached property provides additional context to the `Description`, such as the purpose of a control.

#### Usage:

```xml
<Image Source="like.png"
       SemanticProperties.Description="Like"
       SemanticProperties.Hint="Like this post." />
```

Or in C#:

```csharp
Image image = new Image { Source = "like.png" };
SemanticProperties.SetDescription(image, "Like");
SemanticProperties.SetHint(image, "Like this post.");
```

---

### HeadingLevel

The `SemanticProperties.HeadingLevel` attached property enables an element to be marked as a heading to organize the UI and make it easier to navigate.

#### Usage:

```xml
<Label Text="Main Heading"
       SemanticProperties.HeadingLevel="Level1" />
```

Or in C#:

```csharp
Label label = new Label { Text = "Main Heading" };
SemanticProperties.SetHeadingLevel(label, SemanticHeadingLevel.Level1);
```

Available heading levels: `None`, `Level1`, `Level2`, `Level3`, `Level4`, `Level5`, `Level6`, `Level7`, `Level8`, `Level9`

**Note:** Not all platforms support all heading levels. Check the specific platform documentation for details.

---

## MAUI AutomationProperties

In addition to `SemanticProperties`, MAUI provides `AutomationProperties` for more advanced accessibility control. These are attached properties that can be added to any element to indicate how the element is reported to the underlying platform's accessibility framework.

### ExcludedWithChildren

The `AutomationProperties.ExcludedWithChildren` property determines if an element and its children should be excluded from the accessibility tree.

```xml
<StackLayout AutomationProperties.ExcludedWithChildren="true">
    <!-- Children will not be visible to screen readers -->
</StackLayout>
```

### IsInAccessibleTree

The `AutomationProperties.IsInAccessibleTree` property indicates whether the element is available in the accessibility tree. Must be set to `true` to use other automation properties.

```xml
<Entry AutomationProperties.IsInAccessibleTree="true" />
```

**Warning:** On iOS, if `IsInAccessibleTree` is `true` on any control that has children, the screen reader will be unable to reach the children.

---

## DIPS.Mobile.UI Accessibility Helpers

DIPS.Mobile.UI provides helpful attached properties that simplify common accessibility scenarios for screen readers. These helpers make it easier to implement accessibility best practices without writing platform-specific code.

### Accessibility.Mode - GroupChildren

The `Accessibility.Mode` attached property with the `GroupChildren` value simplifies grouping related information for screen readers. **This implementation uses MAUI's `SemanticProperties.Description` under the hood** to automatically group child elements - leveraging the native behavior where setting a description excludes children from the accessibility tree.

**Platform behavior:** On iOS, all children are excluded. On Android, non-interactive children (Labels) are excluded but interactive elements (Buttons, Entries) remain accessible. See [Description behavior](#critical-behavior-description-excludes-children---with-platform-differences) for details.

#### When to use:
- Multiple child labels or text elements that form a single logical piece of information (e.g., address blocks, contact information, patient cards, product details)
- Read-only informational displays where all content should be announced together
- Card-like UI patterns where related information should be grouped
- When you want to reduce the number of swipe gestures needed for screen reader users

#### When NOT to use:
- When any child element is interactive (Button, Entry, Switch, etc.) - interactive elements need individual focus for usability
- When child elements represent separate, unrelated pieces of information that users might want to navigate individually
- When children have complex accessibility requirements or need custom hints/traits
- In lists or collections where each item should be individually navigable

#### Example:

```xml
<dui:VerticalStackLayout Spacing="{dui:Sizes size_1}"
                         Padding="{dui:Sizes size_3}"
                         BackgroundColor="{dui:Colors color_surface_subtle}"
                         dui:Accessibility.Mode="GroupChildren">
    <dui:Label Text="John Doe"
               Style="{dui:Styles Label=Body400}" />
    <dui:Label Text="Born: 1980-05-15"
               Style="{dui:Styles Label=UI200}" />
    <dui:HorizontalStackLayout Spacing="{dui:Sizes size_1}">
        <dui:Label Text="Phone:"
                   Style="{dui:Styles Label=UI200}" />
        <dui:Label Text="+47 123 45 678"
                   Style="{dui:Styles Label=UI200}" />
    </dui:HorizontalStackLayout>
    <dui:Label Text="john.doe@example.com"
               Style="{dui:Styles Label=UI200}" />
</dui:VerticalStackLayout>
```

**Result:** VoiceOver/TalkBack will read: *"John Doe, Born: 1980-05-15, Phone:, +47 123 45 678, john.doe@example.com"*

Without this mode, screen readers would require **5 separate swipe gestures** to read all information. With `GroupChildren`, it's read in **one focus**.

#### How it works:
1. Automatically collects text from all descendant `Label` elements
2. Collects any existing `SemanticProperties.Description` values from descendants
3. Combines all text with commas: `"Text1, Text2, Text3"`
4. Sets the combined text as `SemanticProperties.Description` on the parent
5. Because `Description` is set, **non-interactive children are excluded** by the native platform behavior (all children on iOS, non-interactive children on Android)

#### Technical notes:
- **This mode does not track runtime changes** - it collects text once when the layout is rendered
- If text in child labels changes after rendering, the accessibility description won't update
- If you add/remove children dynamically, the description won't reflect those changes
- The implementation relies on the native platform behavior where `SemanticProperties.Description` excludes children (platform-specific: all children on iOS, non-interactive children on Android)

### Accessibility.Trait - Button and Selection States

The `Accessibility.Trait` attached property allows you to set accessibility traits on any MAUI view, informing screen readers about the element's interactive nature and state. This is particularly useful for custom controls or when making non-standard elements interactive.

#### Available Traits

The `Trait` enum supports the `[Flags]` attribute, allowing multiple traits to be combined:

- **`Trait.Button`**: Announces the element as a button, indicating it's tappable/interactive
- **`Trait.Selected`**: Announces the element as selected (e.g., in a radio button group)
- **`Trait.NotSelected`**: Announces the element as not selected

#### Platform Implementation

- **iOS**: Maps to native `UIAccessibilityTrait` flags
  - `Trait.Button` → `UIAccessibilityTrait.Button`
  - `Trait.Selected` → `UIAccessibilityTrait.Selected`
  
- **Android**: Uses a custom `AccessibilityDelegate` with `AccessibilityNodeInfo`
  - `Trait.Button` → Sets `ClassName = "android.widget.Button"`
  - `Trait.Selected` → Sets `Checked = true` and `Checkable = true`
  - `Trait.NotSelected` → Sets `Checked = false` and `Checkable = true`

#### When to Use

Use `Accessibility.Trait` when:
- Creating custom interactive controls that aren't using the `Touch` effect
- Building selection UI (like radio button groups, toggle cards, or selectable items)
- You need to explicitly mark an element as interactive without using `Touch.Command`
- You want to announce selection state changes to screen reader users

**Note:** When using `dui:Touch.Command` with `SemanticProperties.Description`, the Touch effect already adds the Button trait automatically. Use `Accessibility.Trait="Button"` only when you're not using the Touch effect.

#### Basic Button Example

Making a custom view interactive without using the Touch effect:

```xml
<!-- Without Touch effect - need to add Button trait manually -->
<Grid Gesture.TapGesture="{Binding SelectCommand}"
      dui:Accessibility.Trait="Button"
      SemanticProperties.Description="Select patient">
    <Label Text="Patient Card" />
</Grid>

<!-- With Touch effect - Button trait is automatic, don't need to set it -->
<Grid dui:Touch.Command="{Binding SelectCommand}"
      SemanticProperties.Description="Select patient">
    <Label Text="Patient Card" />
</Grid>
```

**Result:** Both announce as *"Select patient, Button"* to screen readers.

#### Selection State Example

Indicating selection state in a radio button group or selectable list:

```xml
<!-- Selected item - using Touch effect -->
<Grid dui:Touch.Command="{Binding SelectOptionACommand}"
      dui:Accessibility.Trait="Selected"
      SemanticProperties.Description="Option A">
    <Label Text="Option A" />
    <Image Source="checkmark.png" IsVisible="True" />
</Grid>

<!-- Not selected item - using Touch effect -->
<Grid dui:Touch.Command="{Binding SelectOptionBCommand}"
      dui:Accessibility.Trait="NotSelected"
      SemanticProperties.Description="Option B">
    <Label Text="Option B" />
    <Image Source="checkmark.png" IsVisible="False" />
</Grid>
```

**Note:** Since we're using `dui:Touch.Command` with `SemanticProperties.Description`, the Button trait is automatically added by the Touch effect. We only need to set the selection state (`Selected` or `NotSelected`).

**Result:** 
- VoiceOver: *"Option A, Selected, Button"* and *"Option B, Not Selected, Button"*
- TalkBack: *"Option A, Checked, Button"* and *"Option B, Not checked, Button"*

#### Dynamic Trait Binding

Traits can be bound dynamically based on view model state:

```xml
<!-- Using Touch effect - Button trait is automatic -->
<Grid dui:Touch.Command="{Binding ToggleSelectionCommand}"
      SemanticProperties.Description="{Binding OptionName}">
    <Grid.Triggers>
        <DataTrigger TargetType="Grid" 
                     Binding="{Binding IsSelected}" 
                     Value="True">
            <Setter Property="dui:Accessibility.Trait" Value="Selected" />
        </DataTrigger>
        <DataTrigger TargetType="Grid" 
                     Binding="{Binding IsSelected}" 
                     Value="False">
            <Setter Property="dui:Accessibility.Trait" Value="NotSelected" />
        </DataTrigger>
    </Grid.Triggers>
    
    <Label Text="{Binding OptionName}" />
</Grid>
```

Or in C#:

```csharp
// Without Touch effect - need to add Button trait
Accessibility.SetTrait(myView, Trait.Button);

// Combine multiple traits (without Touch effect)
Accessibility.SetTrait(myView, Trait.Button | Trait.Selected);

// With Touch effect - only set selection state
// (Button trait is already added by Touch effect)
if (isSelected)
    Accessibility.SetTrait(myView, Trait.Selected);
else
    Accessibility.SetTrait(myView, Trait.NotSelected);
```

#### Combining with Other Accessibility Properties

`Accessibility.Trait` works seamlessly with other accessibility properties:

```xml
<!-- Using Touch effect - Button trait is automatic, only set selection state -->
<Grid dui:Touch.Command="{Binding SelectCommand}"
      dui:Accessibility.Trait="Selected"
      SemanticProperties.Description="Dark mode theme"
      SemanticProperties.Hint="Double tap to select this theme">
    <Label Text="Dark Mode" />
    <Image Source="checkmark.png" />
</Grid>
```

**Result:** Screen readers provide full context: description, trait, and hint together.

#### Best Practices

1. **Don't add Button trait when using Touch effect:** The `dui:Touch.Command` with `SemanticProperties.Description` already adds Button trait automatically. Only add Button trait manually when not using Touch:
   ```xml
   <!-- ✅ Good: Using Touch effect, only set selection state -->
   <Grid dui:Touch.Command="{Binding Command}"
         dui:Accessibility.Trait="Selected"
         SemanticProperties.Description="Option A" />
   
   <!-- ✅ Good: Not using Touch, add Button trait manually -->
   <Grid dui:Accessibility.Trait="Button, Selected"
         SemanticProperties.Description="Option A" />
   
   <!-- ❌ Redundant: Button trait added twice -->
   <Grid dui:Touch.Command="{Binding Command}"
         dui:Accessibility.Trait="Button, Selected"
         SemanticProperties.Description="Option A" />
   ```

2. **Toggle between Selected and NotSelected:** Don't use both at once - they're mutually exclusive states:
   ```xml
   <!-- ✅ Good: One or the other -->
   dui:Accessibility.Trait="Selected"
   dui:Accessibility.Trait="NotSelected"
   
   <!-- ❌ Bad: Both together -->
   dui:Accessibility.Trait="Selected, NotSelected"
   ```

3. **Always provide Description with Trait:** Traits describe *what* an element is, but `SemanticProperties.Description` describes *what it does* or *what it represents*:
   ```xml
   <Grid dui:Accessibility.Trait="Selected"
         SemanticProperties.Description="Large font size">
   ```

4. **Use NotSelected explicitly:** Even though "not selected" is the default, explicitly setting `NotSelected` helps screen readers announce the state clearly to users.

5. **Test with actual screen readers:** Trait announcements vary slightly between VoiceOver and TalkBack - always test on both platforms.

#### Technical Implementation

The `Accessibility.Trait` property is implemented using the MAUI `PlatformEffect` pattern:
- Creates a platform effect that applies native accessibility traits
- Automatically updates when the trait value changes
- Works with any MAUI `View` element
- Properly handles multiple traits through the `[Flags]` enum

---

## Touch Effect Accessibility

When using the `Touch` effect (from [Touch](Touch.md)) to make elements interactive, proper accessibility support is critical. The Touch effect automatically enhances accessibility when `SemanticProperties.Description` is set.

### How it works

When you set `SemanticProperties.Description` on an element with `Touch.Command`, the Touch effect automatically configures the native platform accessibility traits:

- **iOS:** Appends `UIAccessibilityTrait.Button` to the element's accessibility traits
- **Android:** Sets the accessibility class name to `"android.widget.Button"`

This ensures screen readers (VoiceOver/TalkBack) announce the element as a "Button", providing clear feedback about its interactive nature.

Note: If you do not want this behavior, it can be disabled by setting the `dui:Touch.IsButtonTraitEnabled` property to false.

### Example without accessibility (❌ Bad):

```xml
<Grid dui:Touch.Command="{Binding SelectPatientCommand}">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />
        <ColumnDefinition Width="Auto" />
    </Grid.ColumnDefinitions>
    
    <VerticalStackLayout>
        <Label Text="John Doe" />
        <Label Text="Born: 1980-05-15" />
    </VerticalStackLayout>
    
    <Label Grid.Column="1" Text="→" />
</Grid>
```

**Problem:** Screen reader users won't know this element is tappable. They'll hear each label separately without any indication it's an interactive button.

### Example with accessibility (✅ Good):

```xml
<Grid dui:Touch.Command="{Binding SelectPatientCommand}"
      SemanticProperties.Description="Select patient John Doe, Born 1980-05-15">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />
        <ColumnDefinition Width="Auto" />
    </Grid.ColumnDefinitions>
    
    <VerticalStackLayout>
        <Label Text="John Doe" />
        <Label Text="Born: 1980-05-15" />
    </VerticalStackLayout>
    
    <Label Grid.Column="1" Text="→" />
</Grid>
```

**Result:** Screen readers will announce: *"Select patient John Doe, Born 1980-05-15, Button"* - users know it's interactive!

**Note:** Remember that setting `SemanticProperties.Description` on the Grid excludes the child Labels from accessibility (see [Description behavior](#critical-behavior-description-automatically-excludes-children)).

### Alternative using GroupChildren mode (✅ Also Good):

If you want to automatically combine the text from children without manually writing the description:

```xml
<Grid dui:Touch.Command="{Binding SelectPatientCommand}"
      dui:Accessibility.Mode="GroupChildren">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />
        <ColumnDefinition Width="Auto" />
    </Grid.ColumnDefinitions>
    
    <VerticalStackLayout>
        <Label Text="John Doe" />
        <Label Text="Born: 1980-05-15" />
    </VerticalStackLayout>
    
    <Label Grid.Column="1" Text="→" />
</Grid>
```

**Result:** The `GroupChildren` mode automatically collects text from all Labels and sets `SemanticProperties.Description` to *"John Doe, Born: 1980-05-15, →"*. The Touch effect then adds the Button trait, so screen readers announce: *"John Doe, Born: 1980-05-15, →, Button"*

This approach is convenient when you have dynamic content or don't want to manually maintain the description text. See [GroupChildren Mode](#groupchildren-mode) for more details.

### Important notes:

- ⚠️ **Always set `SemanticProperties.Description`** when using `Touch.Command` on non-button elements
- The Touch effect monitors for changes to `SemanticProperties.Description` and updates accessibility traits automatically
- Remember that setting `Description` on a parent has platform-specific behavior: on iOS it excludes all children, on Android it excludes non-interactive children but interactive elements remain accessible (see [Description behavior](#critical-behavior-description-excludes-children---with-platform-differences))

---

## ListItem with Interactive Content

When a `ListItem` contains interactive controls (like `Switch`, `Button`, `Entry`, or other tappable elements), screen readers on iOS (VoiceOver) and Android (TalkBack) will focus on the ListItem's title and subtitle first before reaching the interactive element. This creates unnecessary navigation steps and a poor accessibility experience for users with screen readers.

### The Problem

Consider this example with a switch:

```xml
<dui:ListItem Title="Enable notifications"
              Subtitle="Receive alerts when new messages arrive">
    <dui:Switch IsToggled="{Binding NotificationsEnabled}" />
</dui:ListItem>
```

**Screen reader behavior:**
1. **First focus:** "Enable notifications"
2. **Second focus:** "Receive alerts when new messages arrive"  
3. **Third focus:** "Switch" (the actual interactive element)

Screen reader users must perform 3 separate swipe gestures to reach the switch, hearing redundant information along the way.

### The Solution: DisableInternalAccessibility

The `DisableInternalAccessibility` property excludes the ListItem's internal elements (title, subtitle, and icon) from the accessibility tree, allowing screen readers to focus directly on the interactive control.

```xml
<dui:ListItem Title="Enable notifications"
              Subtitle="Receive alerts when new messages arrive"
              DisableInternalAccessibility="True">
    <dui:Switch IsToggled="{Binding NotificationsEnabled}"
                SemanticProperties.Description="Enable notifications. Receive alerts when new messages arrive" />
</dui:ListItem>
```

**Screen reader behavior:**
1. **Only focus:** "Enable notifications. Receive alerts when new messages arrive, Switch"

Now screen reader users reach the interactive control in just 1 swipe gesture with full context!

### How It Works

When `DisableInternalAccessibility="True"`:
- The title and subtitle container is excluded from the accessibility tree using `AutomationProperties.ExcludedWithChildren="true"`
- The icon (if present) is also excluded from the accessibility tree
- Screen readers skip directly to any interactive content inside the ListItem
- Works consistently on both iOS (VoiceOver) and Android (TalkBack)

### Best Practices

#### ✅ Always Set SemanticProperties.Description

When using `DisableInternalAccessibility="True"`, **always** set `SemanticProperties.Description` on the interactive element with descriptive text that includes the context from the title/subtitle. This ensures screen reader users get the complete information.

```xml
<!-- ✅ Good: Full context provided -->
<dui:ListItem Title="Dark mode"
              Subtitle="Use dark theme throughout the app"
              DisableInternalAccessibility="True">
    <dui:Switch SemanticProperties.Description="Dark mode. Use dark theme throughout the app"
                IsToggled="{Binding DarkModeEnabled}" />
</dui:ListItem>

<!-- ❌ Bad: Missing context -->
<dui:ListItem Title="Dark mode"
              Subtitle="Use dark theme throughout the app"
              DisableInternalAccessibility="True">
    <dui:Switch IsToggled="{Binding DarkModeEnabled}" />
    <!-- Screen reader will just say "Switch" without any context! -->
</dui:ListItem>
```

#### When to Use DisableInternalAccessibility

Use `DisableInternalAccessibility="True"` when:
- The ListItem contains a **single** interactive control (Switch, Button, Entry, etc.)
- The title and subtitle are purely descriptive labels for that control
- You want to reduce navigation steps for screen reader users
- The interactive element can meaningfully incorporate the title/subtitle context in its description

#### When NOT to Use DisableInternalAccessibility

Don't use `DisableInternalAccessibility="True"` when:
- The ListItem contains **multiple** interactive elements (users need to navigate between them)
- The title/subtitle contain information separate from the interactive control's purpose
- The ListItem is purely informational without interactive content
- You're using the ListItem's built-in tap functionality (via `Command` property)

### Examples

#### Example 1: Switch Control

```xml
<dui:ListItem Title="Show archived items"
              Subtitle="Include archived items in the list"
              DisableInternalAccessibility="True">
    <dui:Switch SemanticProperties.Description="Show archived items. Include archived items in the list"
                IsToggled="{Binding ShowArchivedItems}" />
</dui:ListItem>
```

#### Example 2: Button

```xml
<dui:ListItem Title="Export data"
              Subtitle="Download your data as a CSV file"
              DisableInternalAccessibility="True">
    <dui:Button Text="Export"
                Command="{Binding ExportCommand}"
                SemanticProperties.Description="Export data. Download your data as a CSV file" />
</dui:ListItem>
```

#### Example 3: Entry Field

```xml
<dui:ListItem Title="Email address"
              Subtitle="Your contact email"
              DisableInternalAccessibility="True">
    <dui:Entry Placeholder="email@example.com"
               Text="{Binding Email}"
               SemanticProperties.Description="Email address. Your contact email"
               Keyboard="Email" />
</dui:ListItem>
```

#### Example 4: Multiple Interactive Elements (DON'T use DisableInternalAccessibility)

```xml
<!-- ❌ Bad: Don't disable internal accessibility with multiple interactive elements -->
<dui:ListItem Title="Notification settings"
              Subtitle="Configure your notification preferences">
    <!-- Users need to access both buttons independently -->
    <HorizontalStackLayout Spacing="8">
        <dui:Button Text="Email" Command="{Binding ConfigureEmailCommand}" />
        <dui:Button Text="Push" Command="{Binding ConfigurePushCommand}" />
    </HorizontalStackLayout>
</dui:ListItem>
```

---

## Best Practices

1. **Make your UI self-describing:** Test that all elements are screen reader accessible. Add descriptive text and hints when necessary.

2. **Provide alternate text for images and icons:** Always use `SemanticProperties.Description` for meaningful images.

3. **Group related information:** Use `Accessibility.Mode="GroupChildren"` for card-like patterns with multiple labels representing a single piece of information. Note: On iOS all children are excluded, on Android interactive children remain accessible.

4. **Exclude decorative elements:** Use `AutomationProperties.ExcludedWithChildren="true"` for purely decorative elements, or set a custom `SemanticProperties.Description` on the parent (which has platform-specific child exclusion behavior).

5. **Don't mix visual and semantic text:** If you set a custom `SemanticProperties.Description`, ensure it matches the visual content users can see.

6. **Test with actual screen readers:** Enable VoiceOver (iOS) or TalkBack (Android) and navigate through your app to ensure a smooth experience.

7. **Support large fonts and high contrast:** Use dynamic layouts that can accommodate larger text sizes.

8. **Localize accessibility descriptions:** When your app supports multiple languages, ensure all accessibility text is also localized.

9. **Keep interactive elements separate on iOS:** Don't use `GroupChildren` or set `Description` on parents containing interactive controls if you need them accessible on iOS. On Android, interactive elements remain accessible even with parent descriptions.

10. **Follow WCAG guidelines:** Ensure your app is perceivable, operable, understandable, and robust for all users. See [Web Content Accessibility Guidelines (WCAG)](https://www.w3.org/WAI/standards-guidelines/wcag/).

11. **Choose the right approach:** 
    - For custom descriptions: Use `SemanticProperties.Description` directly (remember platform differences in child exclusion!)
    - For automatic grouping: Use DIPS.Mobile.UI's `Accessibility.Mode="GroupChildren"` which automatically collects and combines text
    - For excluding decorative elements: Use `AutomationProperties.ExcludedWithChildren="true"`

12. **Understand the Description behavior:** Always remember that `SemanticProperties.Description` has platform-specific behavior regarding children. On iOS, all children are excluded. On Android, non-interactive children (Labels, Images) are excluded but interactive elements (Buttons, Entries, Switches) remain accessible. See [Description behavior](#critical-behavior-description-excludes-children---with-platform-differences) for details.

13. **Always set Description with Touch.Command:** When using the `Touch` effect to make elements interactive, always set `SemanticProperties.Description` so screen readers announce the element as a "Button". See details in [Touch Effect Accessibility](#touch-effect-accessibility).

14. **Use DisableInternalAccessibility for ListItems with interactive content:** When a ListItem contains a single interactive control (Switch, Button, Entry, etc.), use `DisableInternalAccessibility="True"` to skip the title/subtitle and focus directly on the control. Always set `SemanticProperties.Description` on the interactive element with the full context. See details in [ListItem with Interactive Content](#listitem-with-interactive-content).

15. **Use Accessibility.Trait for custom interactive controls and selection states:** When building custom controls or selection UI, use `Accessibility.Trait` to announce button behavior and selection states to screen readers. Always combine with `SemanticProperties.Description` for complete context. See details in [Accessibility.Trait](#accessibilitytrait---button-and-selection-states).

---

## Enabling Screen Readers

### iOS - VoiceOver
1. Open the **Settings** app
2. Select **Accessibility** > **VoiceOver**
3. Turn **VoiceOver** on

For more information: [Apple VoiceOver Guide](https://support.apple.com/guide/iphone/iph3e2e415f/ios)

### Android - TalkBack
1. Open the **Settings** app
2. Select **Accessibility** > **TalkBack**
3. Turn **Use TalkBack** on

For more information: [Google TalkBack Guide](https://support.google.com/accessibility/android/answer/6007100)

---

## Additional Resources

- [Microsoft MAUI Accessibility Documentation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/accessibility)
- [Web Content Accessibility Guidelines (WCAG)](https://www.w3.org/WAI/standards-guidelines/wcag/)
- [Apple Accessibility Guidelines](https://developer.apple.com/accessibility/)
- [Android Accessibility Guidelines](https://developer.android.com/guide/topics/ui/accessibility)
