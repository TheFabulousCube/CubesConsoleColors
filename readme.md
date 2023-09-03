# Cube's Console Colors makes adding color to your console output easier!

![logo](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/image20.png)

[![NuGet](https://img.shields.io/nuget/dt/TheFabulousCube.ConsoleColors.svg)](https://www.nuget.org/packages/TheFabulousCube.ConsoleColors)
[![NuGet](https://img.shields.io/nuget/v/TheFabulousCube.ConsoleColors.svg)](https://www.nuget.org/packages/TheFabulousCube.ConsoleColors)

Tired of long boring walls of scrolling text? Need to call attention to something important?  
Adding a splash of color to your console output makes important events stand out!

---

There are a couple of ways to add colors to the console output, but neither one is very user friendly.  
The most common way is setting `Console.ForegroundColor` and `Console.BackgroundColor`. Just don't forget to `Console.ResetColor()` afterwards or **all** the rest of the output is colored.  
Here's an example:

```
   Console.ForegroundColor = ConsoleColor.Red;
   Console.BackgroundColor = ConsoleColor.DarkRed;
   Console.WriteLine("Error: Something failed to happen");
   Console.ResetColor();
```

Want to just color the word "Error:"? Totally possible!  
Break the output up, set the color, Write (**don't** WriteLine yet) the word "Error:", reset the colors, then WriteLine the rest of the error.  
Pretty quickly the Console output becomes complicated and getting that right takes focus away from the actual code!

You can also use ANSI Escape codes!  
`Console.WriteLine($"\u001B[91;41mError: Something failed to happen\u001B[0m");`  
Changing this to the single word version is much easier:  
`Console.WriteLine($"\u001B[91;41m Error: \u001B[0m Something failed to happen");`

Is it maintainable? Not really. What color is '91'? Why is there a '41'? Do you remember the syntax? You can look it up, but how much time do you want to spend looking up syntax for logging???

## Cube's Console Colors to the rescue!

I'm still using the ANSI escape color codes, that's the way to go. But they're self describing now! It's much easier to read and far quicker to write!  
(Unless you spend an unusual amount of time using colored output in Minecraft. It uses the same escape codes.)

The above lines would look like this:  
`Console.WriteLine(Color.Red($"Error: Something failed to happen").WithDarkRed());`  
`Console.WriteLine($"{Color.Red("Error:").WithDarkRed()} Something failed to happen");`  
and display:  
![Example of console output with red text on dark red background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/escape_example.PNG)

Now if you decide later you don't want the background color, could you figure out how to remove that? Sure you could!  
If you wanted to add a 'Success' message, could you figure out the syntax? I bet you could!

Here are some examples:  
![Example of different colors against a black background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/foreground_colors.PNG)  
![Example of different colors against a white background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/foreground_against_white.PNG)
![Example of black text against different colored backgrounds](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/background_black_white.PNG)
![Example of white text against different colored backgrounds](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/background_with_white.PNG)

**Note:** The background effect doesn't overwrite the global setting of Console.BackgroundColor

## How to use

This library adds some methods to help make adding color to the console output easier. These methods return the string wrapped in the ANSI escape codes for you. Since the escape codes work directly on the string, the color changes are 'local' to each line. If you want to set the whole background or default text color, you can still do that.

## Foreground colors (text)

The foreground colors are methods that take in a string and return the same string wrapped in the ANSI escape codes.  
Simply pass your text into a color method:  
`Console.WriteLine(Color.Blue("This prints the text in Blue"));`  
![Text with the full line in blue](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/text_full_line.PNG)  
The background is whatever Console.Background is set to.

You don't have to use the entire string. To color a single word, interpolate the command in the string:
`Console.WriteLine($"This prints the word {Color.Yellow("'Yellow'")} in Yellow");`
![Text with the word Yellow in yellow](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/text_single_word.PNG)  
The rest of the line is whatever the console is set to.

### The Color method also takes in a background color so you can do both at the same time:

`Console.WriteLine($"There are {Color.Blue("blue skys", ConsoleColor.White)} ahead");`  
![Text with 'blue skys' in blue with white background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/single_line.PNG)  
This is limited to the standard ConsoleColors

### Not enough colors?

There's a Color.RGB() method that takes in the string to color, followed by red, green, and blue values. If you know the hexadecimal, prefix it with "0x"  
`Console.WriteLine(Color.RGB("Let's try something new!", 255, 45, 45));`  
`Console.WriteLine(Color.RGB("Let's try something new!", 0x2d, 0xff, 0x2d));`
`Console.WriteLine(Color.RGB("Let's try something new!", 45, 45, 255));`  
![Examples of Color.RGB, mostly primary colors](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/rgb.PNG)  
This is still experimental and doesn't play well with nesting.

## Background colors

The backgound colors are string extension methods and are prefixed by ".with###()" so just add the background color you want to the end of the string:  
`Console.WriteLine("The other way around string method".WithBlack());`  
![Text with dark red background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/background_full_line.PNG)  
You can also use this to highlight specific words:  
`Console.WriteLine($"This is {"highlighted".WithDarkGreen()} in dark green");`  
![Text, the word highlighted has a dark green background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/background_single_word.PNG)

## Mixing foreground/background and nesting

When nested, the most immediate commands win out, like most nesting.
You can freely mix and match as much as you want:  
`Console.WriteLine(Color.DarkRed($" This is pretty {Color.Red("complicated", ConsoleColor.DarkRed)} nesting, but {Color.DarkGreen("perfectly")} fine").WithWhite());`  
![Text with lots of mixed colors and backgrounds](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/Nesting.PNG)

## Styling and convenience methods

### There are also some styling methods included

Support for ANSI escape codes around styling is a little spotty. They may or may not work well for your application. The 'bold' feature really only works well on dark colors, and all it does is translate them to the lighter version of the same color.  
**Bold**:  
`Console.WriteLine(Color.DarkCyan($" The {Color.Bold(" bold feature ")} really only works on dark colors, it just changes them to the {"light".WithBold()} version"));`

`Console.WriteLine(Color.Magenta($" The {Color.Bold("bold feature")} doesn't show any difference for already light colors"));`  
![example for bolded text](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/bold.PNG)

**Underline**:  
`Console.WriteLine($" The {Color.Underline("underline")} can be used for emphasis.  There are {"both formats".WithUnderline()} of the styling methods");`  
![Example of underlined text](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/underline.PNG)

**Inverse**: (swaps the background and foreground colors, kinda neat!)  
`Console.WriteLine($" You can {Color.Invert(" invert a section of text ")} in a line");`  
![Example of inverted text](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/invert.PNG)

### Convenience methods:

Success, writes green on dark green background:  
`Console.WriteLine($" The operation was a {Color.Success("success")}, please proceed");`  
![Success text with green on dark green background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/success.PNG)

Warning, writes yellow on dark yellow background:  
`Console.WriteLine($" Trouble {Color.Warning("parsing file")}, you may want to check results");`  
![Warning text with yellow on dark yellow background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/warning.PNG)

Failure, writes red on dark red background:  
`Console.WriteLine($" Parsing the arguments {Color.Failure("failed")}, check the logs");`  
![Failure text with red on dark red background](https://raw.githubusercontent.com/TheFabulousCube/CubesConsoleColors/master/img/failure.PNG)

## Pro tip!

Adding a static using lets you simplify coding even more:

```
using static ConsoleColors.Color;
.........
Console.WriteLine(DarkRed($"This is a simplified version with {Underline("underline")}. It makes typing a little easier."));
```
