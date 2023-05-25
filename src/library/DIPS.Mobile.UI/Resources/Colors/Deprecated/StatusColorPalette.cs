namespace DIPS.Mobile.UI.Resources.Colors.Deprecated
{
    public static class StatusColorPalette
    {

        public static Color DangerDark = Color.FromHex("#C9524D");
        public static Color Danger = Color.FromHex("#F76D6D");
        public static Color DangerLight = Color.FromHex("#E19D9A");
        public static Color DangerAir = Color.FromHex("#FFDEDE");

        public static Color WarningDark = Color.FromHex("#D29F0F");
        public static Color Warning = Color.FromHex("#FFDC52");
        public static Color WarningLight = Color.FromHex("#EDD5A6");
        public static Color WarningAir = Color.FromHex("#F3E9BC");

        public static Color SuccessDark = Color.FromHex("#006700");
        public static Color Success = Color.FromHex("#5CB85C");
        public static Color SuccessLight = Color.FromHex("#9FD99F");
        public static Color SuccessAir = Color.FromHex("#CCEBCC");

        public static Color InfoDark = Color.FromHex("#266B89");
        public static Color Info = Color.FromHex("#337AB7");
        public static Color InfoLight = Color.FromHex("#7FAFD8");
        public static Color InfoAir = Color.FromHex("#C4DBEF");

        public static Color IdleDark = Color.FromHex("#697577");
        public static Color Idle = Color.FromHex("#92A1A3");
        public static Color IdleLight = Color.FromHex("#B1BEBF");
        public static Color IdleAir = Color.FromHex("#D3DDDE");

        
        public enum Identifier
        {
            [Obsolete($"Use dui:Colors {nameof(ColorName.color_error_dark)}", true)]
            DangerDark,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_obsolete_danger)}", true)]
            Danger,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_obsolete_danger_light)}", true)]
            DangerLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_error_light)}", true)]
            DangerAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_mandatory_dark)}", true)]
            WarningDark,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_attention_dark)}", true)]
            Warning,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_mandatory_light)}", true)]
            WarningLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_attention_light)}", true)]
            WarningAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_success_dark)}", true)]
            SuccessDark,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_obsolete_success)}", true)]
            Success,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_success_light)}", true)]
            SuccessLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_success_light)}", true)]
            SuccessAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_information_dark)}", true)]
            InfoDark,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_information_dark)}", true)]
            Info,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_information_light)}", true)]
            InfoLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_information_light)}", true)]
            InfoAir,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_60)}", true)]
            IdleDark,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_50)}", true)]
            Idle,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_40)}", true)]
            IdleLight,

            [Obsolete($"Use dui:Colors {nameof(ColorName.color_neutral_30)}", true)]
            IdleAir
        }
    }
}