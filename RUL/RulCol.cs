﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    public static class RulCol
    {
        #region Private Fields

        private static Dictionary<Hues, Dictionary<LuminosityTypes, Col>> _predefinedColors;
        private const float DEFAULT_MAX_VARIANCE = 0.1F;

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a completely random, opaque color
        /// </summary>
        public static UnityEngine.Color RandColor()
        {
            return new UnityEngine.Color(Rul.RandFloat(), Rul.RandFloat(), Rul.RandFloat());
        }

        /// <summary>
        ///Returns a completely random color with the specified alpha value
        /// </summary>
        /// /// <param name="alpha">Opacity between 0 and 1</param>
        public static UnityEngine.Color RandColor(float alpha)
        {
            return new UnityEngine.Color(Rul.RandFloat(), Rul.RandFloat(), Rul.RandFloat(), alpha);
        }

        /// <summary>
        /// Returns a random color with the specified alpha value and lightness
        /// </summary>
        /// <param name="alpha">Opacity between 0 and 1</param>
        /// <param name="lightness">Lightness between 0 and 1</param>
        /// <returns></returns>
        public static UnityEngine.Color RandColor(float alpha, float lightness)
        {
            return AdjustLightness(RandColor(alpha), lightness);
        }

        /// <summary>
        /// Returns a color that looks like the specified base color
        /// </summary>
        /// <param name="baseColor">The base for the random color</param>
        /// <param name="maxRelativeVariance">A value between 0 and 1 specifying the maximum variance from the base color's RGB components</param>
        /// <returns></returns>
        public static UnityEngine.Color RandColor(UnityEngine.Color baseColor, float maxRelativeVariance)
        {
            return RandomizeColor(baseColor, maxRelativeVariance);
        }

        /// <summary>
        /// Returns a random color with the specified hue and luminosity
        /// </summary>
        /// <param name="hue">The approximate hue of the random color color</param>
        /// <param name="luminosity">The approximate luminosity of the random color</param>
        public static UnityEngine.Color RandColor(Hues hue, LuminosityTypes luminosity)
        {
            if (_predefinedColors == null)
                DefineColors();
            UnityEngine.Color baseColor = GetPredefinedColor(hue, luminosity);
            return RandomizeColor(baseColor, DEFAULT_MAX_VARIANCE, hue == Hues.Monochrome);
        }

        /// <summary>
        /// Returns a random color with the specified hue and random luminosity
        /// </summary>
        /// <param name="hue">The approximate hue of the random color</param>
        public static UnityEngine.Color RandColor(Hues hue)
        {
            LuminosityTypes luminosity = Rul.RandElement(LuminosityTypes.Light, LuminosityTypes.Medium, LuminosityTypes.Dark);
            return RandColor(hue, luminosity);
        }

        /// <summary>
        /// Returns a random color whose r,g,b and a components are between those of colA and colB
        /// </summary>
        public static UnityEngine.Color RandColor(UnityEngine.Color colA, UnityEngine.Color colB)
        {
            return new UnityEngine.Color(Rul.RandFloat(colA.r, colB.r), Rul.RandFloat(colA.g, colB.g), Rul.RandFloat(colA.b, colB.b), Rul.RandFloat(colA.a, colB.a));
        }

        #endregion

        #region Private Methods

        private static UnityEngine.Color AdjustLightness(UnityEngine.Color baseColor, float lightness)
        {
            if (lightness >= 0 && lightness <= 1)
            {
                //Lightness is the average intensity of the color : (r+g+b) / 3
                lightness *= 3;

                float correction = 0;
                try
                {
                    correction = lightness / (baseColor.r + baseColor.g + baseColor.b);
                }
                catch (DivideByZeroException) { }

                
                float r = Math.Min(1, baseColor.r * correction);
                float g = Math.Min(1, baseColor.g * correction);
                float b = Math.Min(1, baseColor.b * correction);
                return new UnityEngine.Color(r, g, b);
            }
            else
                throw new ArgumentException("Lightness must be between 0 and 1");
        }

        private static void DefineColors()
        {
            Dictionary<LuminosityTypes, Col> red = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(255,0,0) },
               { LuminosityTypes.Medium, new Col(165,0,0) },
               { LuminosityTypes.Dark, new Col(75,0,0) }

            };
            Dictionary<LuminosityTypes, Col> green = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(0,255,0) },
               { LuminosityTypes.Medium, new Col(0,165,0) },
               { LuminosityTypes.Dark, new Col(0,75,0) }

            };
            Dictionary<LuminosityTypes, Col> blue = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(0,0,255) },
               { LuminosityTypes.Medium, new Col(0,0,165) },
               { LuminosityTypes.Dark, new Col(0,0,75) }

            };
            Dictionary<LuminosityTypes, Col> orange = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(255,165,0) },
               { LuminosityTypes.Medium, new Col(255,140,0) },
               { LuminosityTypes.Dark, new Col(228,125,0) }

            };
            Dictionary<LuminosityTypes, Col> yellow = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(255,255,0) },
               { LuminosityTypes.Medium, new Col(230,230,0) },
               { LuminosityTypes.Dark, new Col(205,205,0) }

            };
            Dictionary<LuminosityTypes, Col> cyan = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(0,255,255) },
               { LuminosityTypes.Medium, new Col(0,210,210) },
               { LuminosityTypes.Dark, new Col(0,165,165) }

            };
            Dictionary<LuminosityTypes, Col> purple = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(255,0,255) },
               { LuminosityTypes.Medium, new Col(210,0,210) },
               { LuminosityTypes.Dark, new Col(165,0,165) }

            };
            Dictionary<LuminosityTypes, Col> pink = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(255,192,203) },
               { LuminosityTypes.Medium, new Col(255,105,180) },
               { LuminosityTypes.Dark, new Col(255,20,147) }

            };
            Dictionary<LuminosityTypes, Col> monochrome = new Dictionary<LuminosityTypes, Col>()
            {
               { LuminosityTypes.Light, new Col(255,255,255) },
               { LuminosityTypes.Medium, new Col(127,127,127) },
               { LuminosityTypes.Dark, new Col(0,0,0) }

            };
            _predefinedColors = new Dictionary<Hues, Dictionary<LuminosityTypes, Col>>()
            {
                {Hues.Red, red},
                {Hues.Blue, blue},
                {Hues.Green, green},
                {Hues.Orange, orange},
                {Hues.Yellow, yellow},
                {Hues.Purple, purple},
                {Hues.Pink, pink},
                {Hues.Cyan, cyan},
                {Hues.Monochrome, monochrome},
            };

        }

        private static UnityEngine.Color GetPredefinedColor(Hues hue, LuminosityTypes luminosity)
        {
            if (_predefinedColors.ContainsKey(hue) && _predefinedColors[hue].ContainsKey(luminosity))
            {
                Col original = _predefinedColors[hue][luminosity];
                return ConvertColor(original.R,original.G,original.B,original.A);
            }
            return new UnityEngine.Color();
        }

        /// <summary>
        /// Returns a coefficient between 0 and 1 that determines how much variety will actually be permitted for RandomizeColor.
        /// This makes getting natural, appealing variants of colors easier.
        /// </summary>
        private static float GetVarianceFactor(float value)
        {
            //This method works best with values > 0.4
            if (value >= 0.4)
                return value * value;
            return 0.15F;
        }

        private static UnityEngine.Color RandomizeColor(UnityEngine.Color baseColor, float maxRelativeVariance, bool monochrome = false)
        {
            if (maxRelativeVariance >= 0 && maxRelativeVariance <= 1)
            {
                //Monochrome colors should stay monochrome
                if (!monochrome)
                {
                    float variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.r));
                    float r = MathHelper.Clamp(baseColor.r + Rul.RandSign() * variance, 0, 1);
                    variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.g));
                    float g = MathHelper.Clamp(baseColor.g + Rul.RandSign() * variance, 0, 1);
                    variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.b));
                    float b = MathHelper.Clamp(baseColor.b + Rul.RandSign() * variance, 0, 1);
                    return new UnityEngine.Color(r, g, b, baseColor.a);
                }
                else
                {
                    float variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.r));
                    float newValue = MathHelper.Clamp(baseColor.r + Rul.RandSign() * variance, 0, 1);
                    return new UnityEngine.Color(newValue, newValue, newValue, baseColor.a);
                }
            }
            else
                throw new ArgumentException("Value must be between 0 and 1");
        }

        /// <summary>
        /// Converts the given color from the original 0-255 to unity's 0-1 range
        /// </summary>
        private static UnityEngine.Color ConvertColor(float r, float g, float b, float a = 255)
        {
            return new UnityEngine.Color(r / 255F, g / 255F, b / 255F, a / 255F);
        }

        #endregion
    }
}
