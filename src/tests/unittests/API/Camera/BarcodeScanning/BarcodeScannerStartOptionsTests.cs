using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace DIPS.Mobile.UI.UnitTests.API.Camera.BarcodeScanning;

public class BarcodeScannerStartOptionsTests
{
    [Fact]
    public void Defaults_Preserve_Legacy_Duplicate_Suppression_Options()
    {
        var options = new BarcodeScannerStartOptions();

        options.AllowRepeatedBarcodes.Should().BeFalse();
        options.DuplicateScanCooldown.Should().Be(TimeSpan.FromMilliseconds(800));
        options.Strategy.Should().BeOfType<TimerBarcodeScanStrategy>()
            .Which.DetectionTime.Should().Be(TimeSpan.FromMilliseconds(500));
    }

    [Fact]
    public void DuplicateScanCooldown_Preserves_Public_NonNullable_TimeSpan_Accessors()
    {
        var property = typeof(BarcodeScannerStartOptions)
            .GetProperty(nameof(BarcodeScannerStartOptions.DuplicateScanCooldown))!;

        property.PropertyType.Should().Be(typeof(TimeSpan));
        property.GetMethod!.IsPublic.Should().BeTrue();
        property.GetMethod.ReturnType.Should().Be(typeof(TimeSpan));
        property.SetMethod!.IsPublic.Should().BeTrue();
        property.SetMethod.GetParameters()[0].ParameterType.Should().Be(typeof(TimeSpan));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(125)]
    [InlineData(1600)]
    public void Legacy_Caller_Can_Still_Configure_DuplicateScanCooldown(int milliseconds)
    {
        var cooldown = TimeSpan.FromMilliseconds(milliseconds);
        var options = new BarcodeScannerStartOptions { DuplicateScanCooldown = cooldown };

        options.DuplicateScanCooldown.Should().Be(cooldown);
        options.AllowRepeatedBarcodes.Should().BeFalse();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Repeated_Barcodes_Are_Explicit_OptIn_For_Either_Strategy(bool useScanRectangle)
    {
        var options = new BarcodeScannerStartOptions
        {
            AllowRepeatedBarcodes = true,
            Strategy = useScanRectangle
                ? new ScanRectangleBarcodeScanStrategy()
                : new TimerBarcodeScanStrategy()
        };

        options.AllowRepeatedBarcodes.Should().BeTrue();
        options.DuplicateScanCooldown.Should().Be(TimeSpan.FromMilliseconds(800));
        options.Hint.ShowAutomaticHint.Should().BeFalse();
    }

    [Fact]
    public void Disabling_OptIn_Preserves_Configured_Legacy_Cooldown()
    {
        var cooldown = TimeSpan.FromSeconds(2);
        var options = new BarcodeScannerStartOptions
        {
            DuplicateScanCooldown = cooldown,
            AllowRepeatedBarcodes = true
        };

        options.AllowRepeatedBarcodes = false;

        options.AllowRepeatedBarcodes.Should().BeFalse();
        options.DuplicateScanCooldown.Should().Be(cooldown);
    }

    [Fact]
    public void Automatic_Hint_Is_Disabled_By_Default_With_Fifteen_Second_Delay()
    {
        var hint = new BarcodeScannerStartOptions().Hint;

        hint.Should().NotBeNull();
        hint.ShowAutomaticHint.Should().BeFalse();
        hint.Delay.Should().Be(TimeSpan.FromSeconds(15));
        hint.HintText.Should().BeNull();
        hint.CanShowHintAsync.Should().BeNull();
        hint.OnHintShownAsync.Should().BeNull();
    }

    [Fact]
    public void Hint_And_Repeated_Barcode_Options_Are_Not_Shared_Between_Sessions()
    {
        var first = new BarcodeScannerStartOptions { AllowRepeatedBarcodes = true };
        var second = new BarcodeScannerStartOptions();

        first.Hint.ShowAutomaticHint = true;
        first.Hint.HintText = "Move the label farther away.";
        first.Hint.Delay = TimeSpan.FromSeconds(3);

        second.AllowRepeatedBarcodes.Should().BeFalse();
        second.Hint.Should().NotBeSameAs(first.Hint);
        second.Hint.ShowAutomaticHint.Should().BeFalse();
        second.Hint.HintText.Should().BeNull();
        second.Hint.Delay.Should().Be(TimeSpan.FromSeconds(15));
    }

    [Fact]
    public void Enabling_Automatic_Hint_Does_Not_Enable_Repeated_Barcodes()
    {
        var hint = new BarcodeScannerHintOptions
        {
            ShowAutomaticHint = true,
            HintText = "Move the label farther away."
        };
        var options = new BarcodeScannerStartOptions { Hint = hint };

        options.Hint.Should().BeSameAs(hint);
        options.AllowRepeatedBarcodes.Should().BeFalse();
        options.DuplicateScanCooldown.Should().Be(TimeSpan.FromMilliseconds(800));
    }
}