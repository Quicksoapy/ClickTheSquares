using System;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
    System.Timers.Timer delay = new System.Timers.Timer(2000);
    public Gaming()
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
            switch (rand.Next(0, 4))
            {
                case 0:
                    Application.Current.Dispatcher.Invoke(new Action(() => { TopLeft.Background = Brushes.Red;}));
                    isRed = true;
                    break;
                case 1: 
                    Application.Current.Dispatcher.Invoke(new Action(() => { TopRight.Background = Brushes.Red;}));
                    isRed = true;
                    break;
                case 2:
                    Application.Current.Dispatcher.Invoke(new Action(() => { BottomLeft.Background = Brushes.Red; }));
                    isRed = true;
                    break;
                case 3: 
                    Application.Current.Dispatcher.Invoke(new Action(() => { BottomRight.Background = Brushes.Red;}));
                    isRed = true;
                    break;
                default: 
                    Application.Current.Dispatcher.Invoke(new Action(() => { 
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
                comboCount = 0;
                delay.Interval = 2000;
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    ComboCounter.Content = comboCount.ToString();
                    TopLeft.Background = Brushes.White;
                    TopRight.Background = Brushes.White;
                    BottomLeft.Background = Brushes.White;
                    BottomRight.Background = Brushes.White;
                }));
                isRed = false;
            }
        }
    }

    private void TopLeft_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (TopLeft.Background == Brushes.Red)
        {
            TopLeft.Background = Brushes.White;
            isRed = false;
            ComboCounter.Content = comboCount.ToString();
            lock (comboLock)
            {
                delay.Interval = 1000 / comboCount;
                comboCount += 1;
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
                delay.Interval = 1000 / comboCount;
                comboCount += 1;
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
                delay.Interval = 1000 / comboCount;
                comboCount += 1;
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
    }
}