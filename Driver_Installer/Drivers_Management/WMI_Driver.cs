using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Driver_Installer.Drivers_Management
{
    public class WMI_Driver
    {
        public struct DriverInfo
        {

            public string Name;
            public string Description;
            public string DeviceID;
            public string Manufacturer;

            public string Type;

        }

        public struct SignedDriverInfo
        {
            public string DeviceClass;
            public string DeviceName;
            public string Description;
            public string DeviceID;
            public string Manufacturer;
            public string Version;
            public string Provider;
            public string DriverDate;
            public string InfName;
            public string Type;

        }

        ManagementObjectSearcher searcher = null;
        ManagementObjectSearcher searcher1 = null;

        public WMI_Driver()
        {
            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");
            searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver");
        }



        /// <summary>
        /// Get all drivers
        /// </summary>
        /// <returns></returns>
        public List<DriverInfo> GetDrivers()
        {
            List<DriverInfo> driverInfoList = new List<DriverInfo>();

            foreach (ManagementObject obj in searcher.Get())
            {
                driverInfoList.Add(new DriverInfo
                {
                    DeviceID = obj["DeviceID"]?.ToString() ?? string.Empty,
                    Name = obj["Name"]?.ToString() ?? string.Empty,
                    Manufacturer = obj["Manufacturer"]?.ToString() ?? string.Empty,
                    Description = obj["Description"]?.ToString() ?? string.Empty,
                    Type = obj["PNPClass"]?.ToString() ?? string.Empty
                });
            }

            return driverInfoList;
        }


        /// <summary>
        /// Get all signed drivers.
        /// </summary>
        /// <returns></returns>
        public List<SignedDriverInfo> GetSignedDrivers()
        {
            List<SignedDriverInfo> driverInfoList = new List<SignedDriverInfo>();
            // Tạo một đối tượng TextInfo cho ngôn ngữ hiện tại
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            if (searcher1 != null)
                foreach (ManagementObject obj in searcher1.Get())
                {
                    if (obj["DeviceName"] != null )
                    driverInfoList.Add(new SignedDriverInfo
                    {
                        DeviceClass = obj["DeviceClass"]?.ToString() ?? string.Empty,
                        Description = obj["Description"]?.ToString() ?? string.Empty,
                        Type = textInfo.ToTitleCase(obj["DeviceClass"]?.ToString().ToLower()) ?? string.Empty,
                        DeviceName = obj["DeviceName"]?.ToString() ?? string.Empty,
                        DeviceID = obj["DeviceID"]?.ToString() ?? string.Empty,
                        Manufacturer = obj["Manufacturer"]?.ToString() ?? string.Empty,
                        Version = obj["DriverVersion"]?.ToString(),
                        Provider = obj["DriverProviderName"]?.ToString() ?? string.Empty,
                        DriverDate = obj["DriverDate"]?.ToString() ?? string.Empty,
                        InfName = obj["InfName"]?.ToString() ?? string.Empty,
                    });
                }
            return driverInfoList;
        }

    }
}
