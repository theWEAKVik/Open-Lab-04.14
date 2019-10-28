# Open-Lab-04.14
(17 XP) A function that prints strings into a console in a frame.

1. Clone this repository to your GitHub account.
2. Open solution file in Visual Studio.
3. Open file `FramePrinter.cs`.
4. Implement the method `Print(string[] strings)` that prints strings into a console each to separate line in a frame.
5. Run with `CTRL+F5` to test it.
6. If all tests are passed, commit & sync your repository.
7. Send a link to your repository for Lab Master (in #slack) to check it.

## Examples: 
```C#
FramePrinter printer = new FramePrinter();
string[] strings = new string[]{ "Hello", "World", "in", "a", "frame" };
printer.Print(strings);


/* This code should print this:
*********  
* Hello *  
* World *  
* in    *  
* a     *  
* frame *  
*********
```

## Hints:
* Don't forget to print a space between words and borders.
