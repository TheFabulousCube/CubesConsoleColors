# Colors

Give your console app a nicer look by adding some color to the output it produces. 
This is achieved by wrapping strings of the output in [ANSI codes](https://www.jerriepelser.com/blog/using-ansi-color-codes-in-net-console-apps/) that instruct the terminal to color the string based on the interpreted code. Tested on both Windows (requires at least Windows 10, v1511 [November Update]) and Linux.

## Introduction

Modern terminals have a feature that allows them to print text in different colors. To enable this, a string is wrapped with a special sequence of characters containing a directive to the terminal to color the string that follows and stop coloring when it encounters an end code. Producing these character sequences can be cumbersome, which is the reason why I decided to build this small library that turns this into a very easy task.  
Because Pastel only alters the output string, there is no need to manipulate or extend the built-in `System.Console` class.

## Playground
# Pastel

![logo](https://raw.githubusercontent.com/silkfire/Pastel/master/img/logo.png)

[![NuGet](https://img.shields.io/nuget/dt/Pastel.svg)](https://www.nuget.org/packages/Pastel)
[![NuGet](https://img.shields.io/nuget/v/Pastel.svg)](https://www.nuget.org/packages/Pastel)