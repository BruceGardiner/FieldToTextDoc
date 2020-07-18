using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FieldToTextDoc
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private StorageFolder pickedFolder;

        public MainPage()
        {
            InitializeComponent();
        }


        // These assume a "C:\Users\Public\TestFolder" folder on your machine.
        // You can modify the path if necessary.
        private async void AddToTextDoc_Click(object sender, RoutedEventArgs e)
        {
            var name = Name.Text;
            var age = Age.Text;
            string[] lines = {name, age};
            try
            {
                // var fp = new FolderPicker {SuggestedStartLocation = PickerLocationId.DocumentsLibrary};
                // fp.FileTypeFilter.Add("*");
                // pickedFolder = await fp.PickSingleFolderAsync();
                var pickedFolder = ApplicationData.Current.LocalFolder;
                var sampleFile = await pickedFolder.CreateFileAsync("WriteLines.txt",
                    CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile, string.Join(Environment.NewLine, lines));

                var result = await FileIO.ReadLinesAsync(sampleFile);
                ResultsBox.Text = string.Join(Environment.NewLine, result);
            }
            catch (Exception ex)
            {
                var errorDialog = new MessageDialog($"Problem saving {ex.Message}");
                await errorDialog.ShowAsync();
                return;
            }

            var dialog = new MessageDialog("Saved successfully");
            await dialog.ShowAsync();
        }

        private async void ApendToTextDoc_Click(object sender, RoutedEventArgs e)
        {
            var name2 = Name2.Text;
            var age2 = Age2.Text;
            // Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls
            // IDisposable.Dispose on the stream object.

  

            var lines = new[] {name2, age2};
            var text = $"{Environment.NewLine}{string.Join(Environment.NewLine, lines)}";
            var pickedFolder = ApplicationData.Current.LocalFolder;
            var pickedFile = await pickedFolder.GetFileAsync("WriteLines.txt");
            await FileIO.AppendTextAsync(pickedFile, text);

            var result = await FileIO.ReadLinesAsync(pickedFile);
            ResultsBox.Text = string.Join(Environment.NewLine, result);
        }
    }
}