using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            List<string> animalEmojis = new List<string>()
            {
                "🎆", "🎆",
                "🎇", "🎇",
                "🧨", "🧨",
                "✨", "✨",
                "🎉", "🎉",
                "🎁", "🎁",
                "🧀", "🧀",
                "🌈", "🌈",
            };
            Random random = new Random();
            foreach (var textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                var index = random.Next(animalEmojis.Count);
                var nextEmojis = animalEmojis[index];
                textBlock.Text = nextEmojis;
                animalEmojis.RemoveAt(index);
            }

            // throw new NotImplementedException();
        }

        private TextBlock lasTextBlockClicked;
        private bool isMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("test!!!");
            TextBlock textBlock = sender as TextBlock;

            if (!isMatch)
            {
                Debug.Assert(textBlock != null, nameof(textBlock) + " != null");

                textBlock.Visibility = Visibility.Hidden;
                lasTextBlockClicked = textBlock;
                isMatch = true;
            }
            else
            {
                Debug.Assert(textBlock != null, nameof(textBlock) + " != null");
                if (textBlock.Text == lasTextBlockClicked.Text)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    isMatch = false;
                }

                else
                {
                    lasTextBlockClicked.Visibility = Visibility.Visible;
                    isMatch = false;
                }
            }
        }
    }
}