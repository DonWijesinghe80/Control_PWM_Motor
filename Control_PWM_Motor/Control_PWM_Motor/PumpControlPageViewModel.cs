using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Control_PWM_Motor
{
    internal class PumpControlPageViewModel : INotifyPropertyChanged
    {
        private bool pWMSwitch;
        private double dutyCycle;

        #region ----- CONSTRUCTOR -----

        public PumpControlPageViewModel()
        {
            PWMController = new PWMController();
        }

        #endregion

        #region ----- EVENTS -----

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ----- METHODS -----

        private void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion

        #region ----- PROPERTIES -----

        public PWMController PWMController { get; set; }
        public double DutyCycle
        {
            get => dutyCycle;
            set
            {
                try
                {
                    dutyCycle = value;
                    OnPropertyChanged(nameof(DutyCycle));
                    using (FileStream dutyCycleFile = System.IO.File.Open("/sys/class/pwm/pwmchip0/pwm0/duty_cycle", FileMode.Open, FileAccess.ReadWrite))
                    {
                        int persent = (int)(DutyCycle * 1000);
                        int duty_cycle = persent;
                        byte[] buffer = Encoding.ASCII.GetBytes(duty_cycle.ToString());
                        dutyCycleFile.Write(buffer, 0, buffer.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public bool PWMSwitch
        {
            get => pWMSwitch;
            set
            {
                try
                {
                    pWMSwitch = value;
                    OnPropertyChanged(nameof(PWMSwitch));
                    if (PWMSwitch)
                    {
                        PWMController.CheckPWM();
                    }
                    else
                    {
                        PWMController.UncheckPWM();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion

    }
}
