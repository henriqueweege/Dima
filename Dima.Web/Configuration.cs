﻿using MudBlazor;

namespace Dima.Web
{
    public static class Configuration
    {
        public const string HttpClientName = "dima";
        public const string BackendUrl = "http://localhost:5096";

        public static MudTheme Theme = new()
        {
            Typography = new Typography()
            {
                Default = new Default()
                {
                    FontFamily = new string[] { "Raleway", "sans-serif" }
                }
            },
            Palette = new PaletteLight
            {
                Primary = new MudBlazor.Utilities.MudColor("#1EFA2D"),
                Secondary = Colors.LightGreen.Darken3,
                Background = Colors.Grey.Lighten4,
                AppbarBackground = new MudBlazor.Utilities.MudColor("#1EFA2D"),
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.Black,
                PrimaryContrastText = Colors.Shades.Black,
                DrawerText = Colors.Shades.Black,
                DrawerBackground = Colors.LightGreen.Lighten4,
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.LightGreen.Accent3,
                Secondary = Colors.LightGreen.Darken3,
                AppbarBackground = Colors.LightGreen.Accent3,
                AppbarText = Colors.Shades.Black
            }
        };
    }
}
