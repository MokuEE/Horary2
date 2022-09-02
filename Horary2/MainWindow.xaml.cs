using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Horary2
{
    /// <summary>
    /// Made by MokuEE
    /// </summary>   

    public partial class MainWindow : Window
    {
        DispatcherTimer Timer = new DispatcherTimer();       

        bool IsTiming = false;
        float TimerCount = 0;
        const int ANEGLE_INTERVAL = 360 / 60;

        List<Image> ColorPlate_light_List = new List<Image>();
        List<Image> Astrolabe_List = new List<Image>();


        public MainWindow()
        {
            InitializeComponent();

            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = TimeSpan.FromSeconds(1);

            ColorPlate_light_List.Add(ColorPlate_light_Blue);
            ColorPlate_light_List.Add(ColorPlate_light_Purple);
            ColorPlate_light_List.Add(ColorPlate_light_Yellow);
            ColorPlate_light_List.Add(ColorPlate_light_White);

            Astrolabe_List.Add(Astrolabe_Blue);
            Astrolabe_List.Add(Astrolabe_Pueple);
            Astrolabe_List.Add(Astrolabe_Yellow);
            Astrolabe_List.Add(Astrolabe_White);

            CenterStar.Opacity = CenterStarOpacity.normal;

            ColorPlate_light_AllOff();
            Astrolabe_AllOff();
        }

        #region Events

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            BackGround.Opacity = WindowOpacity.mouseEnter;
            CloseButton.Opacity = CloseButtonOpacity.mouseEnter;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            BackGround.Opacity = WindowOpacity.normal;
            CloseButton.Opacity = CloseButtonOpacity.normal;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CenterStar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsTiming)
                CenterStar.Opacity = CenterStarOpacity.mouseEnter;
        }

        private void CenterStar_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!IsTiming)
                CenterStar.Opacity = CenterStarOpacity.normal;
        }

        private void CenterStar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsTiming)
            {
                IsTiming = true;
                Timer.Start();
                CenterStar.Opacity = CenterStarOpacity.isTiming;
                ColorPlate_light_Switch(ColorPlate_light_Blue, true);
                Astrolabe_Switch(Astrolabe_Blue, true);
            }
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    //Pause
                    IsTiming = false;
                    Timer.Stop();
                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    //Reset
                    IsTiming = false;
                    Timer.Stop();
                    SetPointerAngle(0);
                    TimerCount = 0f;
                    ColorPlate_light_AllOff();
                    Astrolabe_AllOff();
                    CenterStar.Opacity = CenterStarOpacity.normal;
                }

            }
        }

        private void CloseButton_Outline_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton_Outline.Opacity = CloseButtonOpacity.mouseEnter;
        }

        private void CloseButton_Outline_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseButton_Outline.Opacity = CloseButtonOpacity.normal;
        }

        private void CloseButton_Outline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion


        #region Custom Methods 

        void Timer_Tick(object sender, EventArgs e)
        {
            TimerCount = (TimerCount + 1) >= 60 ? 0 : TimerCount + 1;

            SetPointerAngle(TimerCount * ANEGLE_INTERVAL);

            switch (TimerCount)
            {
                case 0:
                    ColorPlate_light_AllOff();
                    ColorPlate_light_Switch(ColorPlate_light_Blue, true);
                    Astrolabe_AllOff();
                    Astrolabe_Switch(Astrolabe_Blue, true);
                    break;
                case 20:
                    ColorPlate_light_AllOff();
                    ColorPlate_light_Switch(ColorPlate_light_Purple, true);
                    Astrolabe_AllOff();
                    Astrolabe_Switch(Astrolabe_Pueple, true);
                    break;
                case 40:
                    ColorPlate_light_AllOff();
                    ColorPlate_light_Switch(ColorPlate_light_Yellow, true);
                    Astrolabe_AllOff();
                    Astrolabe_Switch(Astrolabe_Yellow, true);
                    break;
                case 55:
                    ColorPlate_light_AllOff();
                    ColorPlate_light_Switch(ColorPlate_light_White, true);
                    Astrolabe_AllOff();
                    Astrolabe_Switch(Astrolabe_White, true);
                    break;
                default:
                    break;
            }
        }

        void SetPointerAngle(float _angle) {
            RotateTransform rotateTransform = new RotateTransform(_angle);
            Pointer.RenderTransform = rotateTransform;
        }

        void Astrolabe_Switch(Image _Astrolabe, bool _bool) {
            if (_bool) { _Astrolabe.Opacity = AstrolabeOpacity.enabled; } else { _Astrolabe.Opacity = AstrolabeOpacity.disabled; }
        }


        void Astrolabe_AllOff() {
            foreach (Image item in Astrolabe_List)
            {
                Astrolabe_Switch(item, false);
            }
        }

        void ColorPlate_light_Switch(Image _ColorPlate_light, bool _bool)
        {
            if (_bool) { _ColorPlate_light.Opacity = ColorPlate_lightOpacity.enabled; } else { _ColorPlate_light.Opacity = ColorPlate_lightOpacity.disabled; }
        }

        void ColorPlate_light_AllOff()
        {
            foreach (Image item in ColorPlate_light_List)
            {
                ColorPlate_light_Switch(item, false);
            }
        }

        #endregion


    }
}

