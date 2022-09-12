using System;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Intermediate_Inge_WPF;

public partial class Gaming : Window
{
    /// <summary>
    /// COMBO METHOD MAKEN
    /// </summary>
    bool isRed = false;
    private int pointCount = 0;
    private object comboLock= new object();
    private volatile int comboCount = 0;
    private int MaxComboCount = 0;
    
    private bool localCheems = false;
    static Uri moleUri = new Uri("mole.png", UriKind.Relative);
    private BitmapImage moleImage = new BitmapImage(moleUri);
    
    static Uri cheemsBonkedUri = new Uri("cheemsbonked.png", UriKind.Relative);
    private BitmapImage cheemsBonkedImg = new BitmapImage(cheemsBonkedUri);
    
    System.Timers.Timer delay = new System.Timers.Timer(2000);
    private System.Timers.Timer bonked = new Timer(1000);
    public Gaming(bool cheems)
    {
        InitializeComponent();
        
        if (cheems)
        {
            moleUri = new Uri("cheemsmole.png", UriKind.Relative);
            moleImage = new BitmapImage(moleUri);
            localCheems = true;
        }
        
        delay.AutoReset = true;
        delay.Enabled = true;
        delay.Elapsed += TimerEvent;
        delay.Start();

        string cursorPath = Directory.GetCurrentDirectory() + "\\cursor.cur";
        Cursor myCur = new Cursor(cursorPath);
        this.Cursor = myCur;
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
                delay.Interval = 2000;
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

    /*private void TopLeft_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (TopLeft.Background == Brushes.Red)
        {
            TopLeft.Background = Brushes.White;
            isRed = false;
            ComboCounter.Content = comboCount.ToString();
            lock (comboLock)
            {
                comboCount += 1;
                delay.Interval = 1000 / comboCount;
            }
            
            pointCount += 1;
            PointCounter.Content = pointCount.ToString();
        }
    }

    private void TopRight_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (TopRight.Background == Brushes.Red)
        {
            TopRight.Background = Brushes.White;
            isRed = false;
            ComboCounter.Content = comboCount.ToString();
            lock (comboLock)
            {
                comboCount += 1;
                delay.Interval = 1000 / comboCount;
            }
            pointCount += 1;
            PointCounter.Content = pointCount.ToString();
        }
    }

    private void BottomLeft_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (BottomLeft.Background == Brushes.Red)
        {
            BottomLeft.Background = Brushes.White;
            isRed = false;
            ComboCounter.Content = comboCount.ToString();
            lock (comboLock)
            {
                comboCount += 1;
                delay.Interval = 1000 / comboCount;
            }
            pointCount += 1;
            PointCounter.Content = pointCount.ToString();
        }
    }

    private void BottomRight_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (BottomRight.Background == Brushes.Red)
        {
            BottomRight.Background = Brushes.White;
            isRed = false;
            
            ComboCounter.Content = comboCount.ToString();
            lock (comboLock)
            {
                comboCount += 1;
                delay.Interval = 1000 / comboCount;
            }
            pointCount += 1;
            PointCounter.Content = pointCount.ToString();
        }
    }*/

    private void TopLeft_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (TopLeft.Background != Brushes.White)
        {
            if (localCheems)
            {
                TopLeft.Background = new ImageBrush(cheemsBonkedImg);
                bonked.Enabled = true;
                bonked.Elapsed += BonkedEvent(TopLeft);
                bonked.Start();
            }
            TopLeft.Background = Brushes.White;
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

    private void TopRight_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (TopRight.Background != Brushes.White)
        {
            if (localCheems)
            {
                TopRight.Background = new ImageBrush(cheemsBonkedImg);
                bonked.Enabled = true;
                bonked.Elapsed += BonkedEvent(TopRight);
                bonked.Start();
            }
            TopRight.Background = Brushes.White;
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

    private void BottomLeft_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (BottomLeft.Background != Brushes.White)
        {
            if (localCheems)
            {
                BottomLeft.Background = new ImageBrush(cheemsBonkedImg);
                bonked.Enabled = true;
                bonked.Elapsed += BonkedEvent(BottomLeft);
                bonked.Start();
            }
            BottomLeft.Background = Brushes.White;
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

    private void BottomRight_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (BottomRight.Background != Brushes.White)
        {
            if (localCheems)
            {
                BottomRight.Background = new ImageBrush(cheemsBonkedImg);
                bonked.Enabled = true;
                bonked.Elapsed += BonkedEvent(BottomRight);
                bonked.Start();
            }
            BottomRight.Background = Brushes.White;
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

    private void TopMiddle_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (TopMiddle.Background != Brushes.White)
        {
            if (localCheems)
            {
                TopMiddle.Background = new ImageBrush(cheemsBonkedImg);
                bonked.Enabled = true;
                bonked.Elapsed += BonkedEvent(TopMiddle);
                bonked.Start();
            }
            TopMiddle.Background = Brushes.White;
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

    private void BottomMiddle_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (BottomMiddle.Background != Brushes.White)
        {
            if (localCheems)
            {
                BottomMiddle.Background = new ImageBrush(cheemsBonkedImg);
                bonked.Enabled = true;
                bonked.Elapsed += BonkedEvent(BottomMiddle);
                bonked.Start();
            }
            BottomMiddle.Background = Brushes.White;
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