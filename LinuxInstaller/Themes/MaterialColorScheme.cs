using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace LinuxInstaller.Themes;

// --- Simplified HSL/HSV Structure (unchanged) ---
public struct HSL
{
    public float H; // Hue (0-360)
    public float S; // Saturation (0-1) - Approximates Chroma
    public float L; // Lightness (0-1) - Approximates Tone
    
    public HSL(float h, float s, float l)
    {
        H = h;
        S = s;
        L = l;
    }

    public Color ToRgb()
    {
        float r, g, b;
        // Simplified HSL to RGB conversion logic...
        if (S == 0)
        {
            r = g = b = L;
        }
        else
        {
            float q = L < 0.5f ? L * (1 + S) : L + S - L * S;
            float p = 2 * L - q;
            r = HueToRgb(p, q, H / 360f + 1f / 3f);
            g = HueToRgb(p, q, H / 360f);
            b = HueToRgb(p, q, H / 360f - 1f / 3f);
        }

        return Color.FromArgb(
            (int)Math.Round(r * 255),
            (int)Math.Round(g * 255),
            (int)Math.Round(b * 255));
    }

    private static float HueToRgb(float p, float q, float t)
    {
        if (t < 0f) t += 1f;
        if (t > 1f) t -= 1f;
        if (t < 1f / 6f) return p + (q - p) * 6f * t;
        if (t < 1f / 2f) return q;
        if (t < 2f / 3f) return p + (q - p) * (2f / 3f - t) * 6f;
        return p;
    }
}

// --- The M3 Simulation Class with Getters ---
public class MaterialColorScheme
{
    // Dictionary to hold all generated colors (internal storage)
    private readonly Dictionary<string, Color> _schemeColors = new Dictionary<string, Color>();

    // The fixed Tone (Luminance) steps we use to build the palettes
    private static readonly Dictionary<string, int> TonalAssignments = new Dictionary<string, int>
    {
        // Primary Group
        { "Primary", 40 }, { "OnPrimary", 100 }, { "PrimaryContainer", 90 }, { "OnPrimaryContainer", 10 },
        // Secondary Group
        { "Secondary", 40 }, { "OnSecondary", 100 }, { "SecondaryContainer", 90 }, { "OnSecondaryContainer", 10 },
        // Tertiary Group
        { "Tertiary", 40 }, { "OnTertiary", 100 }, { "TertiaryContainer", 90 }, { "OnTertiaryContainer", 10 },
        // Error Group
        { "Error", 40 }, { "OnError", 100 }, { "ErrorContainer", 90 }, { "OnErrorContainer", 10 },
        // Neutral/Surface Group
        { "Background", 98 }, { "OnBackground", 10 },
        { "Surface", 98 }, { "OnSurface", 10 },
        { "SurfaceVariant", 90 }, { "OnSurfaceVariant", 30 },
        { "Outline", 50 }, { "OutlineVariant", 80 },
        // Inverse and Scrim
        { "InverseSurface", 20 }, { "InverseOnSurface", 95 }, { "InversePrimary", 80 },
        { "Scrim", 0 },
        // Surface Tiers
        { "SurfaceLowest", 100 }, { "OnSurfaceLowest", 10 },
        { "SurfaceLow", 96 }, { "OnSurfaceLow", 10 },
        { "SurfaceHigh", 94 }, { "OnSurfaceHigh", 10 },
        { "SurfaceHighest", 92 }, { "OnSurfaceHighest", 10 }
    };

    public MaterialColorScheme(Color seedRgb)
    {
        // 1. Convert Seed RGB to HSL (Simulated HCT)
        HSL seedHsl = RgbToHsl(seedRgb);

        // 2. Derive Key Colors (Tonal Palettes)
        var palettes = new Dictionary<string, HSL>
        {
            { "Primary", new HSL(seedHsl.H, Math.Max(seedHsl.S, 0.5f), 0.40f) },
            { "Secondary", new HSL(seedHsl.H, 0.20f, 0.40f) }, 
            { "Tertiary", new HSL((seedHsl.H + 60) % 360, 0.30f, 0.40f) }, 
            { "Neutral", new HSL(seedHsl.H, 0.05f, 0.40f) }, 
            { "NeutralVariant", new HSL(seedHsl.H, 0.10f, 0.40f) },
            { "Error", new HSL(25, 0.8f, 0.40f) } 
        };

        // 3. Generate Final Scheme by Assigning Tones (Luminance)
        foreach (var assignment in TonalAssignments)
        {
            string role = assignment.Key;
            int targetTone = assignment.Value;
            float targetLightness = targetTone / 100.0f;

            string paletteKey;
            if (role.Contains("Primary") || role.Contains("InversePrimary")) paletteKey = "Primary";
            else if (role.Contains("Secondary")) paletteKey = "Secondary";
            else if (role.Contains("Tertiary")) paletteKey = "Tertiary";
            else if (role.Contains("Error")) paletteKey = "Error";
            else if (role.Contains("Variant") || role.Contains("Outline")) paletteKey = "NeutralVariant";
            else paletteKey = "Neutral";

            HSL keyHsl = palettes[paletteKey];
            
            HSL finalHsl = new HSL(keyHsl.H, keyHsl.S, targetLightness);
            
            // Store the converted RGB color
            _schemeColors[role] = finalHsl.ToRgb();
        }
    }
    
    private HSL RgbToHsl(Color rgb)
    {
        float r = rgb.R / 255f;
        float g = rgb.G / 255f;
        float b = rgb.B / 255f;
        float max = Math.Max(r, Math.Max(g, b));
        float min = Math.Min(r, Math.Min(g, b));
        float h = 0f;
        float s = 0f;
        float l = (max + min) / 2f;
        if (max != min)
        {
            float d = max - min;
            s = l > 0.5f ? d / (2f - max - min) : d / (max + min);
            if (max == r) h = (g - b) / d + (g < b ? 6f : 0f);
            else if (max == g) h = (b - r) / d + 2f;
            else if (max == b) h = (r - g) / d + 4f;
            h *= 60f;
        }
        return new HSL(h, s, l);
    }

    // Primary Group
    public Color Primary => _schemeColors["Primary"];
    public Color OnPrimary => _schemeColors["OnPrimary"];
    public Color PrimaryContainer => _schemeColors["PrimaryContainer"];
    public Color OnPrimaryContainer => _schemeColors["OnPrimaryContainer"];

    // Secondary Group
    public Color Secondary => _schemeColors["Secondary"];
    public Color OnSecondary => _schemeColors["OnSecondary"];
    public Color SecondaryContainer => _schemeColors["SecondaryContainer"];
    public Color OnSecondaryContainer => _schemeColors["OnSecondaryContainer"];

    // Tertiary Group
    public Color Tertiary => _schemeColors["Tertiary"];
    public Color OnTertiary => _schemeColors["OnTertiary"];
    public Color TertiaryContainer => _schemeColors["TertiaryContainer"];
    public Color OnTertiaryContainer => _schemeColors["OnTertiaryContainer"];

    // Error Group
    public Color Error => _schemeColors["Error"];
    public Color OnError => _schemeColors["OnError"];
    public Color ErrorContainer => _schemeColors["ErrorContainer"];
    public Color OnErrorContainer => _schemeColors["OnErrorContainer"];

    // Background and Base Surfaces
    public Color Background => _schemeColors["Background"];
    public Color OnBackground => _schemeColors["OnBackground"];
    public Color Surface => _schemeColors["Surface"];
    public Color OnSurface => _schemeColors["OnSurface"];

    // Surface Tiers
    public Color SurfaceLowest => _schemeColors["SurfaceLowest"];
    public Color OnSurfaceLowest => _schemeColors["OnSurfaceLowest"];
    public Color SurfaceLow => _schemeColors["SurfaceLow"];
    public Color OnSurfaceLow => _schemeColors["OnSurfaceLow"];
    public Color SurfaceHigh => _schemeColors["SurfaceHigh"];
    public Color OnSurfaceHigh => _schemeColors["OnSurfaceHigh"];
    public Color SurfaceHighest => _schemeColors["SurfaceHighest"];
    public Color OnSurfaceHighest => _schemeColors["OnSurfaceHighest"];

    // Surface Variant
    public Color SurfaceVariant => _schemeColors["SurfaceVariant"];
    public Color OnSurfaceVariant => _schemeColors["OnSurfaceVariant"];

    // Outline and Dividers
    public Color Outline => _schemeColors["Outline"];
    public Color OutlineVariant => _schemeColors["OutlineVariant"];

    // Special/Inverse Roles
    public Color Scrim => _schemeColors["Scrim"];
    public Color InverseSurface => _schemeColors["InverseSurface"];
    public Color InverseOnSurface => _schemeColors["InverseOnSurface"];
    public Color InversePrimary => _schemeColors["InversePrimary"];

    public Dictionary<string, Color> SchemeColors => _schemeColors;
}
