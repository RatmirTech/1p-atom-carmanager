using System;
using System.Windows;

namespace _1p_atom_carmanager.simulator.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = await ApiService.Get("info/get/");
                ResultLabel.Content = "Result: " + result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
