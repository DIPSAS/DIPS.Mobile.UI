namespace DIPS.Mobile.UI.Resources.Colors.Deprecated
{
    public static class ColorPalette
    {

        [Obsolete($"Use dui:Colors {nameof(ColorName.color_system_black)}", true)]
        public static Color Dark = Color.FromHex("#000000");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_90)}", true)]
        public static Color DarkLight = Color.FromHex("#111111");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_80)}", true)]
        public static Color DarkAir = Color.FromHex("#3B3D3D");
        
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_80)}", true)]
        public static Color Tertiary = Color.FromHex("#404040");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_70)}", true)]
        public static Color TertiaryLight = Color.FromHex("#4A4A4A");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_60)}", true)]
        public static Color TertiaryAir = Color.FromHex("#646464");

        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_60)}", true)]
        public static Color Quaternary = Color.FromHex("#76797A");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_60)}", true)]
        public static Color QuaternaryLight = Color.FromHex("#7F7F7F");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_50)}", true)]
        public static Color QuaternaryAir = Color.FromHex("#8C8C8C");

        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_40)}", true)]
        public static Color Quinary = Color.FromHex("#ACACAC");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_40)}", true)]
        public static Color QuinaryLight = Color.FromHex("#B2B2B2");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_30)}", true)]
        public static Color QuinaryAir = Color.FromHex("#D9D9D9");

        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_20)}", true)]
        public static Color Light = Color.FromHex("#EBEBEB");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_05)}", true)]
        public static Color LightLight = Color.FromHex("#F9F9F9");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_system_white)}", true)]
        public static Color LightAir = Color.FromHex("#FFFFFF");

        [Obsolete($"Use dui:Colors {nameof(ColorName.color_obsolete_accent)}", true)]
        public static Color Accent = Color.FromHex("#AB69BF");
        // [Obsolete($"Use dui:Colors {nameof(ColorName.)}", true)]
        public static Color AccentLight = Color.FromHex("#D297E3");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_obsolete_accent_air)}", true)]
        public static Color AccentAir = Color.FromHex("#F4DDFA");
        
        // [Obsolete($"Use dui:Colors {nameof(ColorName.)}", true)]
        public static Color Aqua = Color.FromHex("#129DDB");

        
        public enum Identifier
        {
            [Obsolete($"Use dui:Colors {nameof(ColorName.color_system_black)}", true)]
            Dark,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_90)}", true)]
            DarkLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_80)}", true)]
            DarkAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_80)}", true)]
            Tertiary,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_70)}", true)]
            TertiaryLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_60)}", true)]
            TertiaryAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_60)}", true)]
            Quaternary,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_60)}", true)]
            QuaternaryLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_50)}", true)]
            QuaternaryAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_40)}", true)]
            Quinary,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_40)}", true)]
            QuinaryLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_30)}", true)]
            QuinaryAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_20)}", true)]
            Light,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_05)}", true)]
            LightLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_system_white)}", true)]
            LightAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_obsolete_accent)}", true)]
            Accent,

            // [Obsolete($"Use dui:Colors {nameof(ColorName.)}", true)]
            AccentLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_obsolete_accent_air)}", true)]
            AccentAir,

            // [Obsolete($"Use dui:Colors {nameof(ColorName.)}", true)]
            Aqua
        }
    }
}