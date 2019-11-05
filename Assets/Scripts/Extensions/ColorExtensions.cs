using UnityEngine;

namespace Extensions.UnityEngine
{
    public static class ColorExtensions
    {
        public static float GetHue(this Color color)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            return h;
        }
        
        public static float GetSaturation(this Color color)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            return s;
        }
        
        public static float GetValue(this Color color)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            return v;
        }
        
        public static Color Lighter(this Color color)
        {
            return new Color(color.r + (1.0f - color.r) * 0.25f,
                             color.g + (1.0f - color.g) * 0.25f,
                             color.b + (1.0f - color.b) * 0.25f,
                             color.a);
        }
        
        public static Color Darker(this Color color)
        {
            return new Color(color.r * 0.75f, color.g * 0.75f, color.b * 0.75f, color.a);
        }
        
        public static float Brightness(this Color color)
        {
            return (color.r + color.g + color.b) * 0.33333333f;
        }
        
        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
        
        public static Color WithBrightness(this Color color, float brightness)
        {
            if (color.Brightness() < 0.001f)
                return new Color(brightness, brightness, brightness, color.a);
            
            float factor = brightness / color.Brightness();
            
            float r = color.r * factor;
            float g = color.g * factor;
            float b = color.b * factor;
            
            float a = color.a;
            
            return new Color(r, g, b, a);
        }
        
        public static Color Opaque(this Color color)
        {
            return new Color(color.r, color.g, color.b);
        }
    }
    
}