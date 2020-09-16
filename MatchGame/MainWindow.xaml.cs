using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

// using System.Windows.Media;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using System.Windows.Shapes;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Data;
// using System.Windows.Documents;


namespace MatchGame

{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public partial class MainWindow
    {
        DispatcherTimer timer = new DispatcherTimer();
        private static int _tenthOfSecondsElapsed;
        private int _matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _tenthOfSecondsElapsed++;
            timeTextBlock.Text = (_tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (_matchesFound == 8)
            {
                timer.Stop();
                // timeTextBlock.Text = timeTextBlock.Text + "- Play again?";
                timeTextBlock.Text += "- Play again?";
            }
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
                if (textBlock.Name == "timeTextBlock")
                {
                    continue;
                }

                textBlock.Visibility = Visibility.Visible;
                var index = random.Next(animalEmojis.Count);
                var nextEmojis = animalEmojis[index];
                textBlock.Text = nextEmojis;
                animalEmojis.RemoveAt(index);
            }

            timer.Start();
            _tenthOfSecondsElapsed = 0;
            _matchesFound = 0;
        }

        private TextBlock _lasTextBlockClicked;
        private bool _isMatch;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Debug.WriteLine("mouse down even fired!!!");
            var textBlock = sender as TextBlock;

            if (_isMatch)
            {
                Debug.Assert(textBlock != null, nameof(textBlock) + " != null");
                if (textBlock.Text == _lasTextBlockClicked.Text)
                {
                    _matchesFound++;
                    textBlock.Visibility = Visibility.Hidden;
                    _isMatch = false;
                }

                else
                {
                    _lasTextBlockClicked.Visibility = Visibility.Visible;
                    _isMatch = false;
                }
            }
            else
            {
                Debug.Assert(textBlock != null, nameof(textBlock) + " != null");

                textBlock.Visibility = Visibility.Hidden;
                _lasTextBlockClicked = textBlock;
                _isMatch = true;
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}