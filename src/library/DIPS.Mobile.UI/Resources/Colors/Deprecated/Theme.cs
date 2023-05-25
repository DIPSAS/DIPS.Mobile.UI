namespace DIPS.Mobile.UI.Resources.Colors.Deprecated
{
    public static class Theme
    {
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_primary_90)}", true)]
        public static Color TealPrimary = Color.FromHex("#047F89");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_primary_80)}", true)]
        public static Color TealPrimaryLight = Color.FromHex("#65868F");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_primary_70)}", true)]
        public static Color TealPrimaryAir = Color.FromHex("#98B2AE");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_secondary_90)}", true)]
        public static Color TealSecondary = Color.FromHex("#97C8CD");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_secondary_30)}", true)]
        public static Color TealSecondaryLight = Color.FromHex("#ECF3F4");
        [Obsolete($"Use dui:Colors {nameof(ColorName.color_secondary_20)}", true)]
        public static Color TealSecondaryAir = Color.FromHex("#F0F5F7");

        public enum Identifier
        {
            [Obsolete($"Use dui:Colors {nameof(ColorName.color_primary_90)}", true)]
            TealPrimary,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_primary_80)}", true)]
            TealPrimaryLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_primary_70)}", true)]
            TealPrimaryAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_secondary_90)}", true)]
            TealSecondary,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_secondary_30)}", true)]
            TealSecondaryLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_secondary_20)}", true)]
            TealSecondaryAir,
        }
    }
}