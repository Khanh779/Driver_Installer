using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_Installer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<ListViewItem> driverItems = new List<ListViewItem>();

        private void Form1_Load(object sender, EventArgs e)
        {
            Drivers_Management.WMI_Driver wmiDriver = new Drivers_Management.WMI_Driver();
            List<Drivers_Management.WMI_Driver.SignedDriverInfo> driverInfoList = wmiDriver.GetSignedDrivers();
            // Thêm các cột vô listview
            listViewDrivers.Columns.Add("Tên thiết bị", 100);
            listViewDrivers.Columns.Add("Mô tả", 100);
            listViewDrivers.Columns.Add("ID thiết bị", 100);
            listViewDrivers.Columns.Add("Nhà sản xuất", 100);
            listViewDrivers.Columns.Add("Phiên bản", 100);
            listViewDrivers.Columns.Add("Nhà cung cấp", 100);
            listViewDrivers.Columns.Add("Ngày", 100);
            listViewDrivers.Columns.Add("Tên Inf (InfName)", 100);
            listViewDrivers.Columns.Add("Loại", 100);

            comboBox1.Items.Add("Tất cả");

            foreach (Drivers_Management.WMI_Driver.SignedDriverInfo driverInfo in driverInfoList)
            {
                ListViewItem item = new ListViewItem(driverInfo.DeviceName);
                item.SubItems.Add(driverInfo.Description);
                item.SubItems.Add(driverInfo.DeviceID);
                item.SubItems.Add(driverInfo.Manufacturer);
                item.SubItems.Add(driverInfo.Version);
                item.SubItems.Add(driverInfo.Provider);
                item.SubItems.Add(driverInfo.DriverDate);
                item.SubItems.Add(driverInfo.InfName);
                item.SubItems.Add(driverInfo.Type);
                if(comboBox1.Items.Contains(driverInfo.Type) == false)
                {
                    comboBox1.Items.Add(driverInfo.Type);
                }
                driverItems.Add(item);
            }

            comboBox1.SelectedIndex = 0;
           
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewDrivers.Items.Clear();
            if (comboBox1.SelectedIndex == 0)
                listViewDrivers.Items.AddRange(driverItems.ToArray());
            else
            listViewDrivers.Items.AddRange(driverItems.Where(x => x.SubItems[8].Text == comboBox1.Items[comboBox1.SelectedIndex].ToString()).ToArray());
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                // Ghi thông tin driver từ lístview ra file theo định dạng txt
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (ListViewItem item in listViewDrivers.Items)
                        {
                            file.WriteLine(item.SubItems[0].Text);
                            file.WriteLine(item.SubItems[1].Text);
                            file.WriteLine(item.SubItems[2].Text);
                            file.WriteLine(item.SubItems[3].Text);
                            file.WriteLine(item.SubItems[4].Text);
                            file.WriteLine(item.SubItems[5].Text);
                            file.WriteLine(item.SubItems[6].Text);
                            file.WriteLine(item.SubItems[7].Text);
                            file.WriteLine(item.SubItems[8].Text);
                            file.WriteLine("-------------------------------------------------");
                        }
                    }
                }
            }    
        }
    }
}
