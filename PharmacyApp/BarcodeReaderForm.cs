using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using AForge.Video.DirectShow;
using AForge.Video;

namespace PharmacyApp
{
    public partial class BarcodeReaderForm : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        public BarcodeReaderForm()
        {
            InitializeComponent();
        }

        private void BarcodeReaderForm_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.AudioInputDevice);
            foreach(FilterInfo device in filterInfoCollection)
            {
                cmbCamera.Items.Add(device.Name);
            }
            cmbCamera.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cmbCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDeviceForm_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDeviceForm_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if(result != null)
            {
                txtBarcode.Invoke(new MethodInvoker(delegate ()
                {
                    txtBarcode.Text = result.ToString();
                }));
            }
            pictureBox.Image = bitmap;
        }

        private void BarcodeReaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                    videoCaptureDevice.Stop();
            }
        }

        private void cmbCamera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
