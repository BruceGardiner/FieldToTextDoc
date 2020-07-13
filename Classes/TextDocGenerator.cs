﻿using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace FieldToTextDoc.Classes
{
    // These examples assume a "C:\Users\Public\TestFolder" folder on your machine.
    // You can modify the path if necessary.

    class TextDocGenerator // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
    {
        static void Example1()
        {
            // Example #1: Write an array of strings to a file.
            // Create a string array that consists of three lines.
            string[] lines = { "First line", "Second line", "Third line" };
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);

            //Output (to WriteLines.txt):
            //   First line
            //   Second line
            //   Third line
        }

        static void Example2()
        {
            // Example #2: Write one string to a text file.
            string text = "A class is the most powerful data type in C#. Like a structure, " +
                           "a class defines the data and behavior of the data type. ";
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", text);

            //Output (to WriteText.txt):
            //   A class is the most powerful data type in C#. Like a structure, a class defines the data and behavior of the data type.
        }

        static void Example3()
        {
            // Example #3: 
            // Write an array of strings to a file.
            // Create a string array that consists of three lines.
            string[] lines = { "First line", "Second line", "Third line" };
            // Write only some strings in an array to a file.
            // The using statement automatically flushes AND CLOSES the stream and calls
            // IDisposable.Dispose on the stream object.
            // NOTE: do not use FileStream for text files because it writes bytes, but StreamWriter
            // encodes the output as text. 
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt"))
            {
                foreach (string line in lines)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }
                }
            }

            //Output to WriteLines2.txt after Example #3:
            //   First line
            //   Third line
        }

        static void Example4()
        {
            // Example #4: Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls
            // IDisposable.Dispose on the stream object.
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt", true))
            {
                file.WriteLine("Fourth line");
            }

            //Output to WriteLines2.txt after Example #4:
            //   First line
            //   Third line
            //   Fourth line
        }
    }
}