using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ConsoleColors
{

    /// <summary>
    /// 
    /// </summary>
    public static class Color
    {
        private static string code;
        /**********************************  This from Pastel  ******************************/
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        private static bool _enabled;

        /***********************************************************************************/
        static Color()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);

                var enable = GetConsoleMode(iStdOut, out var outConsoleMode)
                             && SetConsoleMode(iStdOut, outConsoleMode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
            }


            if (Environment.GetEnvironmentVariable("NO_COLOR") == null)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }
        /// <summary>
        /// Enables any future console color output produced by ConsoleColors
        /// </summary>
        public static void Enable()
        {
            _enabled = true;
        }

        /// <summary>
        /// Disables any future console color output produced by ConsoleColors
        /// </summary>
        public static void Disable()
        {
            _enabled = false;
        }

        private static string BuildCodes(string text, string fore, ConsoleColor back)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            ConsoleColor currentBackground = Console.BackgroundColor;
 
            int thisForeground = Foreground[fore];
            int thisBackground = Background[back.ToString()];

            int thatForeground = Foreground[currentForeground.ToString()];
            int thatBackground = Background[currentBackground.ToString()];
            var codes = text.Split('\u001b');
            for (int i = 1; i < codes.Length; i++)
            {
                if (codes[i].Contains('m'))
                {
                    int m = codes[i].IndexOf('m');
                    codes[i] = codes[i][..m].Replace(thatBackground.ToString(), thisBackground.ToString()) + codes[i][m..];
                    codes[i] = codes[i][..m].Replace(thatForeground.ToString(), thisForeground.ToString()) + codes[i][m..];
                }
            }
            string rejoined = String.Join('\u001b',codes);

            code = $"\u001B[{thisForeground};{thisBackground}m{rejoined}\u001B[{thatForeground};{thatBackground}m";
            return (code);
        }

        /// <summary>
        /// Convenience method for dynamic coloring at runtime <br/>
        /// Set the text, foreground, and background by passing them in
        /// </summary>
        /// <param name="str"></param>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Builder(string str, ConsoleColor foreground, ConsoleColor? background)
        {
            return BuildCodes(str, Enum.GetName(typeof(ConsoleColor), foreground), background ?? Console.BackgroundColor);
        }

        /// <summary>
        /// String Extention method <br/>
        /// Set the background by passing it in
        /// </summary>
        /// <param name="str"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string With(this string str, ConsoleColor background)
        {
            ConsoleColor thisColor = background;
            return BuildCodes(str, Console.ForegroundColor.ToString(), thisColor);
        }

        /// <summary>
        /// Sets the text to Black <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Black(string text, ConsoleColor? background = null)
        {
            string thisColor = "Black";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Dark Red <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string DarkRed(string text, ConsoleColor? background = null)
        {
            string thisColor = "DarkRed";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Dark Green <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string DarkGreen(string text, ConsoleColor? background = null)
        {
            string thisColor = "DarkGreen";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Dark Yellow <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string DarkYellow(string text, ConsoleColor? background = null)
        {
            string thisColor = "DarkYellow";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }

        /// <summary>
        /// Sets the text to Dark Blue <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string DarkBlue(string text, ConsoleColor? background = null)
        {
            string thisColor = "DarkBlue";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Dark Magenta <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string DarkMagenta(string text, ConsoleColor? background = null)
        {
            string thisColor = "DarkMagenta";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Dark Cyan <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string DarkCyan(string text, ConsoleColor? background = null)
        {
            string thisColor = "DarkCyan";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Dark Gray <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string DarkGray(string text, ConsoleColor? background = null)
        {
            string thisColor = "DarkGray";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Gray <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Gray(string text, ConsoleColor? background = null)
        {
            string thisColor = "Gray";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Red <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Red(string text, ConsoleColor? background = null)
        {
            string thisColor = "Red";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Green <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Green(string text, ConsoleColor? background = null)
        {
            string thisColor = "Green";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Yellow <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Yellow(string text, ConsoleColor? background = null)
        {
            string thisColor = "Yellow";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Blue <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Blue(string text, ConsoleColor? background = null)
        {
            string thisColor = "Blue";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Magenta <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Magenta(string text, ConsoleColor? background = null)
        {
            string thisColor = "Magenta";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to Cyan <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string Cyan(string text, ConsoleColor? background = null)
        {
            string thisColor = "Cyan";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// Sets the text to White <br/>
        /// Optionaly sets the background to whatever color you pass in
        /// </summary>
        /// <param name="text"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        public static string White(string text, ConsoleColor? background = null)
        {
            string thisColor = "White";
            return BuildCodes(text, thisColor, background ?? Console.BackgroundColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a black background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"black".WithBlack()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithBlack(this string text)
        {
            var thisColor = ConsoleColor.Black;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a dark red background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"dark red".WithDarkRed()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithDarkRed(this string text)
        {
            var thisColor = ConsoleColor.DarkRed;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a dark green background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"dark green".WithDarkGreen()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithDarkGreen(this string text)
        {
            var thisColor = ConsoleColor.DarkGreen;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a dark yellow background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"dark yellow".WithDarkYellow()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithDarkYellow(this string text)
        {
            var thisColor = ConsoleColor.DarkYellow;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a dark blue background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"dark blue".WithDarkBlue()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithDarkBlue(this string text)
        {
            var thisColor = ConsoleColor.DarkBlue;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a dark magenta background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"dark magenta".WithDarkMagenta()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithDarkMagenta(this string text)
        {
            var thisColor = ConsoleColor.DarkMagenta;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a dark cyan background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"dark cyan".WithDarkCyan()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithDarkCyan(this string text)
        {
            var thisColor = ConsoleColor.DarkCyan;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a dark gray background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"dark gray".WithDarkGray()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithDarkGray(this string text)
        {
            var thisColor = ConsoleColor.DarkGray;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a gray background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"gray".WithGray()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithGray(this string text)
        {
            var thisColor = ConsoleColor.Gray;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a red background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"red".WithRed()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithRed(this string text)
        {
            var thisColor = ConsoleColor.Red;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a green background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"green".WithGreen()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithGreen(this string text)
        {
            var thisColor = ConsoleColor.Green;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a yellow background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"yellow".WithYellow()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithYellow(this string text)
        {
            var thisColor = ConsoleColor.Yellow;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a blue background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"blue".WithBlue()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithBlue(this string text)
        {
            var thisColor = ConsoleColor.Blue;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a magenta background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"magenta".WithMagenta()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithMagenta(this string text)
        {
            var thisColor = ConsoleColor.Magenta;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a cyan background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"cyan".WithCyan()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithCyan(this string text)
        {
            var thisColor = ConsoleColor.Cyan;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }
        /// <summary>
        /// String Extention method <br/>
        /// The string has a white background<br/>
        /// May be nested for a highlight: <br/>
        /// Console.WriteLine($"This line has{"white".WithWhite()} highlighting");
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WithWhite(this string text)
        {
            var thisColor = ConsoleColor.White;
            return BuildCodes(text, Console.ForegroundColor.ToString(), thisColor);
        }

        /// <summary>
        /// Inverts the colors <br/>
        /// i.e. the background and foreground are swapped for this string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Invert(string str)
        {
            return ($"\u001B[7m{str}\u001B[27m");
        }

        /// <summary>
        /// String Extention method <br/>
        /// Inverts the colors <br/>
        /// i.e. the background and foreground are swapped for this string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WithInvert(this string str)
        {
            return ($"\u001B[7m{str}\u001B[27m");
        }

        /// <summary>
        /// Displays colors in bold style <br/>
        /// This really only works for the dark colors, which display as the bright versions
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Bold(string str)
        {
            return ($"\u001B[1m{str}\u001B[22m");
        }

        /// <summary>
        /// String Extention method <br/>
        /// Displays colors in bold style <br/>
        /// This really only works for the dark colors, which display as the bright versions
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string WithBold(this string s)
        {
            return ($"\u001B[1m{s}\u001B[22m");
        }

        /// <summary>
        /// The string is underlined
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Underline(string str)
        {
            return ($"\u001B[4m{str}\u001B[24m");
        }

        /// <summary>
        /// String Extention method <br/>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string WithUnderline(this string s)
        {
            return ($"\u001B[4m{s}\u001B[24m");
        }

        // ToDo: use the Build codes method . It could act up with nesting
        /// <summary>
        /// Build your own 24-bit color using RGB values of 0-255
        /// </summary>
        /// <param name="str"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static string RGB(string str, int red, int green, int blue)
        {
            return ($"\u001B[38;2;{red};{green};{blue}m{str}\u001B[0m");
        }

        /// <summary>
        /// This should ring the system bell. <br/>
        /// It's actually kinda silly now.
        /// </summary>
        /// <returns></returns>
        public static string Bell()
        {
            code = $"\u0007";
            return (code);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Demo()
        {
            for (int i = 1; i <= 107; i++)
            {
                Console.WriteLine($"\u001B[{i}m   This line is using code {i} to print.  How do you like it?    \u001B[0m");
            }
            var test = Color.WithBlack("string");
        }

        static readonly Dictionary<string, int> Foreground = new()
        {
        {"Black", 30},
        {"DarkRed", 31},
        {"DarkGreen", 32},
        {"DarkYellow", 33},
        {"DarkBlue", 34},
        {"DarkMagenta", 35},
        {"DarkCyan", 36},
        {"DarkGray", 90},
        {"Gray", 37},
        {"Red", 91},
        {"Green", 92},
        {"Yellow", 93},
        {"Blue", 94},
        {"Magenta", 95},
        {"Cyan", 96},
        {"White", 97 }
        };
        static readonly Dictionary<string, int> Background = new()
        {
        {"Black", 40},
        {"DarkRed", 41},
        {"DarkGreen", 42},
        {"DarkYellow", 43},
        {"DarkBlue", 44},
        {"DarkMagenta", 45},
        {"DarkCyan", 46},
        {"DarkGray", 100},
        {"Gray", 47},
        {"Red", 101},
        {"Green", 102},
        {"Yellow", 103},
        {"Blue", 104},
        {"Magenta", 105},
        {"Cyan", 106},
        {"White", 107 }
        };
    }

}
