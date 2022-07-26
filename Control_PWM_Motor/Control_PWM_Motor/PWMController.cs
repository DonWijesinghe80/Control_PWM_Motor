using System.IO;
using System.Text;

namespace Control_PWM_Motor
{
    public class PWMController
    {
        public void CheckPWM()
        {
            using (FileStream periodFile = File.Open("/sys/class/pwm/pwmchip0/pwm0/period", FileMode.Open, FileAccess.ReadWrite))
            {
                int period = 100000;
                byte[] bytes = Encoding.ASCII.GetBytes(period.ToString());
                periodFile.Write(bytes, 0, bytes.Length);
            }

            using (FileStream enableFile = File.Open("/sys/class/pwm/pwmchip0/pwm0/enable", FileMode.Open, FileAccess.ReadWrite))
            {
                int enable = 1;
                byte[] bytes2 = Encoding.ASCII.GetBytes(enable.ToString());
                enableFile.Write(bytes2, 0, bytes2.Length);
            }
        }

        public void UncheckPWM()
        {
            using (FileStream disableFile = File.Open("/sys/class/pwm/pwmchip0/pwm0/enable", FileMode.Open, FileAccess.ReadWrite))
            {
                int disable = 0;
                byte[] bytes3 = Encoding.ASCII.GetBytes(disable.ToString());
                disableFile.Write(bytes3, 0, bytes3.Length);
            }
        }

    }
}
