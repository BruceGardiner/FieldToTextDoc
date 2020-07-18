using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using FieldToTextDoc.Classes;
using System.ServiceModel.Channels;
using Windows.Storage;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FieldToTextDoc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StorageFolder pickedFolder;
        public MainPage()
        {
            this.InitializeComponent();
        }


        // These assume a "C:\Users\Public\TestFolder" folder on your machine.
        // You can modify the path if necessary.
        private async void AddToTextDoc_Click(object sender, RoutedEventArgs e)
        {
            string Name = name.Text;
            string Age = age.Text;
            // Write an array of strings to a file.
            // Create a string array that consists of three lines.
            string[] lines = { Name, Age };
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().

            var fp = new FolderPicker();
            fp.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            fp.FileTypeFilter.Add("*");
            pickedFolder = await fp.PickSingleFolderAsync();

            var sampleFile = await pickedFolder.CreateFileAsync("WriteLines.txt",
                    CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, string.Join(Environment.NewLine, lines));
        }

        private async void ApendToTextDoc_Click(object sender, RoutedEventArgs e)
        {
            string Name2 = name2.Text;
            string Age2 = age2.Text;
            // Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls
            // IDisposable.Dispose on the stream object.

            if (pickedFolder == null)
            {
                return;
            }

            var lines = new[] {Name2, Age2};
            var text = $"{Environment.NewLine}{string.Join(Environment.NewLine, lines)}";
            var pickedFile = await pickedFolder.GetFileAsync("WriteLines.txt");
            await FileIO.AppendTextAsync(pickedFile, text);

        }
    }
}
