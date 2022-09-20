using System;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Timer = System.Timers.Timer;

namespace Intermediate_Inge_WPF;

public partial class Gaming : Window
{
    /// <summary>
    /// COMBO METHOD MAKEN
    /// </summary>
    bool isRed = false;
    private int pointCount = 0;
    private object comboLock = new object();
    private volatile int comboCount = 0;
    private int MaxComboCount = 0;
    
    static Uri moleUri = new Uri("mole.png", UriKind.Relative);
    private BitmapImage moleImage = new BitmapImage(moleUri);
    
    System.Timers.Timer delay = new System.Timers.Timer(2000);
    private System.Timers.Timer bonked = new Timer(1000);
    public Gaming(bool cheems)
    {
        InitializeComponent();
        
        delay.AutoReset = true;
        delay.Enabled = true;
        delay.Elapsed += TimerEvent;
        delay.Start();
    }

    private void TimerEvent(object? sender, ElapsedEventArgs e)
    {
        Random rand = new Random();
       
        if (isRed == false)
        {
            switch (rand.Next(0, 6))
            {
                case 0:
                    Application.Current.Dispatcher.Invoke(new Action(() => { TopLeft.Background = new ImageBrush(moleImage);}));
                    isRed = true;
                    break;
                case 1:
                    Application.Current.Dispatcher.Invoke(new Action(() => { TopMiddle.Background = new ImageBrush(moleImage);}));
                    isRed = true;
                    break;
                case 2: 
                    Application.Current.Dispatcher.Invoke(new Action(() => { TopRight.Background = new ImageBrush(moleImage);}));
                    isRed = true;
                    break;
                case 3:
                    Application.Current.Dispatcher.Invoke(new Action(() => { BottomLeft.Background = new ImageBrush(moleImage); }));
                    isRed = true;
                    break;
                case 4:
                    Application.Current.Dispatcher.Invoke(new Action(() => { BottomMiddle.Background = new ImageBrush(moleImage); }));
                    isRed = true;
                    break;
                case 5: 
                    Application.Current.Dispatcher.Invoke(new Action(() => { BottomRight.Background = new ImageBrush(moleImage);}));
                    isRed = true;
                    break;
                default: 
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        TopLeft.Background = Brushes.Red;
                        TopRight.Background = Brushes.Red;
                        BottomLeft.Background = Brushes.Red;
                        BottomRight.Background = Brushes.Red;;}));
                    isRed = true;
                    break;
            }
        }
        else
        {
            lock (comboLock)
            {
                if (comboCount > MaxComboCount)
                {
                    MaxComboCount = comboCount;
                }
                
                comboCount = 0;
                delay.Interval = 1000;
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    MaxComboCounter.Content = MaxComboCount.ToString();
                    ComboCounter.Content = comboCount.ToString();
                    TopLeft.Background = Brushes.White;
                    TopMiddle.Background = Brushes.White;
                    TopRight.Background = Brushes.White;
                    BottomLeft.Background = Brushes.White;
                    BottomMiddle.Background = Brushes.White;
                    BottomRight.Background = Brushes.White;
                }));
                isRed = false;
            }
        }
    }

    private void Mole_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        Canvas molePosition = (Canvas)sender;
        if (molePosition.Background != Brushes.White)
        {
            molePosition.Background = Brushes.White;
            isRed = false;
            lock (comboLock)
            {
                comboCount += 1;
                delay.Interval -= 100;
            }

            pointCount += 1;
            ComboCounter.Content = comboCount.ToString();
            PointCounter.Content = pointCount.ToString();
        }
    }

    private ElapsedEventHandler BonkedEvent(Canvas position)
    {
        position.Background = Brushes.White;
        return null;
    }
}