using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;

namespace featFishCheck01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private CogCalibNPointToNPointTool CalibNPointToNPointTool;
        double LocationXRobote;
        double LocationYRobote;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Mechanical coordinate input
                LocationXRobote = Convert.ToDouble(numericUpDown3.Value);
                LocationYRobote = Convert.ToDouble(numericUpDown4.Value);
                //Get image coordinate input
                CalibNPointToNPointTool.Calibration.GetComputedUncalibratedFromRawCalibratedTransform().MapPoint(Convert.ToDouble(numericUpDown1.Value), Convert.ToDouble(numericUpDown2.Value), out var plcx1, out var plcy1);
                //Display the calibration coordinate
                label5.Text = Convert.ToDecimal(plcx1 + LocationXRobote).ToString("f2");
                label6.Text = Convert.ToDecimal(plcy1 + LocationYRobote).ToString("f2");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "CalError");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                CalibNPointToNPointTool = (CogCalibNPointToNPointTool)CogSerializer.LoadObjectFromFile("./NPointClibrationTool.vpp");
            }catch (Exception ex) {
                MessageBox.Show(ex.ToString(),"Calibration tool is missing or file name is incorrect");
            }
        }
    }
}
