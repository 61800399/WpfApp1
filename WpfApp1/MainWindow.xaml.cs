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

namespace WpfApp1
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsofsecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsofsecondsElapsed++;
            timetextblock.Text = (tenthsofsecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timetextblock.Text = timetextblock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalemoji = new List<string>()
            {
                "😁", "😁",
                "🤣", "🤣",
                "👀", "👀",
                "😒", "😒",
                "😎", "😎",
                "😉", "😉",
                "😜", "😜",
                "🤷‍♂️", "🤷‍♂️",
            };
            Random random = new Random();
            foreach (TextBlock textblock in maingrid.Children.OfType<TextBlock>())
            {
                if (textblock.Name != "timetextblock")
                {
                    textblock.Visibility = Visibility.Visible;
                    int index = random.Next(animalemoji.Count);
                    string nextemoji = animalemoji[index];
                    textblock.Text = nextemoji;
                    animalemoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsofsecondsElapsed = 0;
            matchesFound = 0;
        }
        TextBlock lasttextblockclicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textblock = sender as TextBlock;
            if (findingMatch == false)
            {
                textblock.Visibility = Visibility.Hidden;
                lasttextblockclicked = textblock;
                findingMatch = true;
            }
            else if (textblock.Text == lasttextblockclicked.Text)
            {
                matchesFound++;
                textblock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lasttextblockclicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void timetextblock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
                
            }
        }
    }
}
