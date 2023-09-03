# Cube's console colors makes adding color to your console output easier!

## Introduction

Here are some examples:  
![](/img/foreground_colors.PNG)
![](/img/foreground_against_white.PNG)
![](/img/background_black_white.PNG)
![](/img/background_with_white.PNG)

**Note:** The background effect doesn't overwrite the global setting

## How to use

This library adds some methods to help make adding color to the console output easier. These methods return the string wrapped in the ANSI escape codes for you. Since the escape codes work directly on the string, the color changes are 'local' to each line. If you want to set the whole background or default text color, you can still do that.

## Foreground colors (text)

The foreground colors are methods that take in a string and return the same string wrapped in the ANSI escape codes.  
Simply pass your text into a color method:  
`Console.WriteLine(Color.Blue("This prints the text in Blue"));`  
![Text with the full line in blue](/img/text_full_line.PNG)  
The background is whatever Console.Background is set to.

You don't have to use the entire string. To color a single word, interpolate the command in the string:
`Console.WriteLine($"This prints the word {Color.Yellow("'Yellow'")} in Yellow");`
![Text with the word Yellow in yellow](/img/text_single_word.PNG)  
The rest of the line is whatever the console is set to.

### The Color method also takes in a background color so you can do both at the same time:

`Console.WriteLine($"There are {Color.Blue("blue skys", ConsoleColor.White)} ahead");`  
![Text with 'blue skys' in blue with white background](/img/single_line.PNG)  
This is limited to the standard ConsoleColors

### Not enough colors?

There's a Color.RGB() method that takes in the string to color, followed by red, green, and blue values. If you know the hexadecimal, prefix it with "0x"  
`Console.WriteLine(Color.RGB("Let's try something new!", 255, 45, 45));`  
`Console.WriteLine(Color.RGB("Let's try something new!", 0x2d, 0xff, 0x2d));`
`Console.WriteLine(Color.RGB("Let's try something new!", 45, 45, 255));`  
![Examples of Color.RGB, mostly primary colors](/img/rgb.PNG)  
This is still experimental and doesn't play well with nesting.

## Background colors

The backgound colors are string extention methods and are prefixed by ".with###()" so just add the background color you want to the end of the string:  
`Console.WriteLine("The other way around string method".WithBlack());`  
![Text with dark red background](/img/background_full_line.PNG)  
You can also use this to highlight specific words:  
`Console.WriteLine($"This is {"highlighted".WithDarkGreen()} in dark green");`  
![Text, the word highlighted has a dark green background](/img/background_single_word.PNG)

## Mixing foreground/background and nesting

When nested, the most immediate commands win out, like most nesting.
You can freely mix and match as much as you want:  
`Console.WriteLine(Color.DarkRed($" This is pretty {Color.Red("complicated", ConsoleColor.DarkRed)} nesting, but {Color.DarkGreen("perfectly")} fine").WithWhite());`  
![Text with lots of mixed colors and backgrounds](/img/Nesting.PNG)

## Styling and convenience methods

There are also some styling methods included  
Support for ANSI escape codes around styling is a little spotty. They may or may not work well for your application. The 'bold' feature really only works well on dark colors, and all it does is translate them to the lighter version of the same color.  
**Bold**:
`Console.WriteLine(Color.DarkCyan($" The {Color.Bold(" bold feature ")} really only works on dark colors, it just changes them to the {"light".WithBold()} version"));`

`Console.WriteLine(Color.Magenta($" The {Color.Bold("bold feature")} doesn't show any difference for already light colors"));`
![example for bolded text](/img/bold.PNG)

**Underline**:
`Console.WriteLine($" The {Color.Underline("underline")} can be used for emphasis.  There are {"both formats".WithUnderline()} of the styling methods");`  
![Example of underlined text](/img/underline.PNG)

**Inverse**: (swaps the background and foreground colors, kinda neat!)
`Console.WriteLine($" You can {Color.Invert(" invert a section of text ")} in a line");`  
![Example of inverted text](/img/invert.PNG)

Convenience methods:
Success, writes green on dark green background:
`Console.WriteLine($" The operation was a {Color.Success("success")}, please proceed");`  
![Success text with green on dark green background](/img/success.PNG)

Warning, writes yellow on dark yellow background:
`Console.WriteLine($" Trouble {Color.Warning("parsing file")}, you may want to check results");`  
![Warning text with yellow on dark yellow background](/img/warning.PNG)

Failure, writes red on dark red background:
`Console.WriteLine($" Parsing the arguments {Color.Failure("failed")}, check the logs");`  
![Failure text with red on dark red background](/img/failure.PNG)
