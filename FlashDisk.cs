using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace Login_System_Tut_ecnrypted
{
    public class FlashDisk
    {
        public static bool IsDiskAvailable
        {
            get
            {
                var deviceInstancePaths = new[]
                {
                    @"USBSTOR\DISK&VEN_GENERIC&PROD_USB_FLASH_DISK&REV_7.76\6&23F69CE7&0",

                };
                var searcher =
                    new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive")
                        .Get().Cast<ManagementObject>()
                        .FirstOrDefault(x =>
                            deviceInstancePaths.Contains((string)x["PNPDeviceID"])
                            && (string)x["InterfaceType"] == "USB");

                return searcher != null;
            }
        }

        public static void Check()
        {
            if (!IsDiskAvailable)
            {
                MessageBox.Show("Необхідного флеш-пристрою не знайдено. Роботу програми буде завершено.");
                Environment.Exit(0);
            }
        }
    }
}
