using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionApp
{
    public partial class Form1 : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        string[] names = { "IS2000-640", "IS2000-800", "IS7800-640", "IS7800-800", "IS7801", "IS7802", "IS7902", "IS7902P", "IS7905", "IS9912", "DM260", "DM262", "DM360", "DM362", "DM363", "DM374", "DM375", "DM474", "DM475" };
        double[] lengthS = { 2.4, 3, 2.88, 3.6, 5.76, 7.2, 5.52, 6.62, 8.445, 14.131,  4.512, 4.8, 3.6, 5.76, 7.2, 7.065, 8.4456, 7.065, 8.4456};
        double[] lengthP = { 640, 800, 640, 800, 1280, 1600, 1600, 1920, 2448, 4096, 752, 1280, 800, 1280, 1600, 2048, 2448, 2048, 2448 };
        bool showimage = false;
        bool showimageVA = false;
        bool showimageLens = false;
        public Form1()
        {
            InitializeComponent();
            LenscbCamS.DataSource = names;
            IDcbCamS.DataSource = names;

        }

        private void kryptonGroupBox1_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            gb2.Values.Heading = "ID Calculator";
            kgLens.Visible = false;
            kgVA.Visible = false;
            kgID.Show();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            gb2.Values.Heading = "Lens Selector";
            kgID.Visible = false;
            kgVA.Visible = false;
            kgLens.Show();
        }

        private void bVA_Click(object sender, EventArgs e)
        {
            gb2.Values.Heading = "Vision Accuracy";
            kgID.Visible = false;
            kgLens.Visible = false;
            kgVA.Show();
        }

        private void kryptonDropButton1_Click(object sender, EventArgs e)
        {

        }

        private void LensbtnCalc_Click(object sender, EventArgs e)
        {
            Insight visionSys = new Insight(lengthS[LenscbCamS.SelectedIndex], names[LenscbCamS.SelectedIndex]);
            try
            {
                if (LensrbFL.Checked == true && LenscbStd.Checked == false)
                {
                    double FLCalc = visionSys.Beta(double.Parse(LenstbFOV.Text), double.Parse(LenstbWD.Text));
                    LenstbFL.Text = FLCalc.ToString();
                }
                if (LensrbFOV.Checked == true && LenscbStd.Checked == false)
                {
                    double FOVCalc = visionSys.FOVcalc(double.Parse(LenstbWD.Text), double.Parse(LenstbFL.Text));
                    LenstbFOV.Text = FOVCalc.ToString();
                }
                if (LensrbWD.Checked == true && LenscbStd.Checked == false)
                {
                    double WDCalc = visionSys.WorkD(double.Parse(LenstbFOV.Text), double.Parse(LenstbFL.Text));
                    LenstbWD.Text = WDCalc.ToString();
                }
                if (LensrbFL.Checked == true && LenscbStd.Checked == true)
                {
                    double FOVInput = double.Parse(LenstbFOV.Text) * 25.4;
                    double WDInput = double.Parse(LenstbWD.Text) * 25.4;
                    double FLCalc = visionSys.Beta(FOVInput, WDInput);
                    LenstbFL.Text = FLCalc.ToString();
                }
                if (LensrbFOV.Checked == true && LenscbStd.Checked == true)
                {
                    double WDInput = double.Parse(LenstbWD.Text) * 25.4;                    
                    double FOVCalc = (visionSys.FOVcalc(WDInput, double.Parse(LenstbFL.Text))) / 25.4;
                    LenstbFOV.Text = FOVCalc.ToString();
                }
                if (LensrbWD.Checked == true && LenscbStd.Checked == true)
                {
                    double FOVInput = double.Parse(LenstbFOV.Text) * 25.4;
                    double WDCalc = (visionSys.WorkD(FOVInput, double.Parse(LenstbFL.Text))) / 25.4;
                    LenstbWD.Text = WDCalc.ToString();
                }
            }
            catch (Exception ex)
            {
                timer3.Start();
            }

        }

        private void LensrbWD_CheckedChanged(object sender, EventArgs e)
        {
            if (LensrbWD.Checked == true)
            {
                LensrbFL.Checked = false;
                LensrbFOV.Checked = false;
                LenstbWD.Enabled = false;
                LenstbFOV.Enabled = true;
                LenstbFL.Enabled = true;
            }
        }

        private void LensrbFOV_CheckedChanged(object sender, EventArgs e)
        {
            if (LensrbFOV.Checked == true)
            {
                LensrbFL.Checked = false;
                LensrbWD.Checked = false;
                LenstbFOV.Enabled = false;
                LenstbWD.Enabled = true;
                LenstbFL.Enabled = true;
            }
        }

        private void LensrbFL_CheckedChanged(object sender, EventArgs e)
        {
            if (LensrbFL.Checked == true)
            {
                LensrbFOV.Checked = false;
                LensrbWD.Checked = false;
                LenstbFL.Enabled = false;
                LenstbFOV.Enabled = true;
                LenstbWD.Enabled = true;
            }
        }

        private void IDcbBar_CheckedChanged(object sender, EventArgs e)
        {
            if (IDcbBar.Checked == true)
            {
                IDtbPPM.Enabled = true;
                IDtbMIL.Enabled = true;
            }
            else
            {
                IDtbPPM.Enabled = false;
                IDtbMIL.Enabled = false;
            }
        }

        private void IDllblMIL_LinkClicked(object sender, EventArgs e)
        {
            
        }

        private void IDbtnCalc_Click(object sender, EventArgs e)
        {
            IDCalcBlur calc = new IDCalcBlur(lengthP[IDcbCamS.SelectedIndex], lengthS[IDcbCamS.SelectedIndex], names[IDcbCamS.SelectedIndex]);
            double lineSpeed = 1;
            try
            {
                if (IDcbFTpM.Checked == true)
                {
                    if (IDtbLineS.Text == "")
                    {
                        lineSpeed = (double.Parse(IDtbSoP.Text) + double.Parse(IDtbDistbtwnPart.Text)) * double.Parse(IDtbPartspm.Text) / 60;
                        double LineSCalcF = (lineSpeed * 60) / (25.4 * 12);
                        IDtbLineS.Text = LineSCalcF.ToString();
                    }
                    else
                    {
                        lineSpeed = (double.Parse(IDtbLineS.Text) * 304.8) / 60;
                    }
                }
                if (IDcbFTpM.Checked == false)
                {
                    if (IDtbLineS.Text == "")
                    {
                        lineSpeed = (double.Parse(IDtbSoP.Text) + double.Parse(IDtbDistbtwnPart.Text)) * double.Parse(IDtbPartspm.Text) / 60;
                        IDtbLineS.Text = lineSpeed.ToString();
                    }
                    else
                    {
                        lineSpeed = double.Parse(IDtbLineS.Text);
                    }
                }
                if (IDcbBar.Checked == true)
                {
                    IDtbMaxE.Text = calc.MaxE(double.Parse(IDtbFOV.Text), lineSpeed).ToString();
                    IDtbMILF.Text = calc.MaxFOV(double.Parse(IDtbMIL.Text), double.Parse(IDtbPPM.Text)).ToString();
                }
                else
                {
                    IDtbMaxE.Text = calc.MaxE(double.Parse(IDtbFOV.Text), lineSpeed).ToString();
                }
            }
            catch (Exception ex)
            {
                timer1.Start();
            }

        }

        private void VAbtnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                Insight visionAccuracy = new Insight();
                double[] values = visionAccuracy.VA(double.Parse(VAtbFOV.Text), double.Parse(VAtbMinFeat.Text));
                VAtb1x1.Text = values[0].ToString();
                VAtb3x1.Text = values[1].ToString();
                VAtb5x1.Text = values[2].ToString();
                VAtb10x1.Text = values[3].ToString();
                VAtb20x1.Text = values[4].ToString();
                VAtb25x1.Text = values[5].ToString();
            }
            catch (Exception ex)
            {
                timer2.Start();
            }

        }


// ************************************************ ERROR HANDLING******************************************************************
// ***********************************************ID CALCULATOR*********************************************************************
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (showimage)
            {
                if (IDtbFOV.Text == "")
                {
                    IDpbFOV.Visible = true;
                }
                if (IDtbLineS.Text == "" && (IDtbPartspm.Text == "" && IDtbDistbtwnPart.Text == "" && IDtbSoP.Text == "" ))
                {
                    IDpbLS.Visible = true;
                }
                if (IDtbLineS.Text == "" && (IDtbPartspm.Text == "" || IDtbDistbtwnPart.Text == "" || IDtbSoP.Text == ""))
                {
                    IDpbLS.Visible = true;
                }
                if (IDcbBar.Checked == true && IDtbPPM.Text == "")
                {
                    IDpbPPM.Visible = true;
                }
                if (IDcbBar.Checked == true && IDtbMIL.Text == "")
                {
                    IDpbMIL.Visible = true;
                }
                showimage = false;
            }
            else
            {
                IDpbFOV.Visible = false;
                IDpbLS.Visible = false;
                IDpbPPM.Visible = false;
                IDpbMIL.Visible = false;
                showimage = true;
            }
        }

        private void IDtbFOV_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void hidePB()
        {
            IDpbFOV.Visible = false;
            IDpbLS.Visible = false;
            IDpbPPM.Visible = false;
            IDpbMIL.Visible = false;
        }

        private void IDtbFOV_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbFOV_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbFOV_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbLineS_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbPPM_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbMIL_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbSoP_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbDistbtwnPart_TextChanged(object sender, EventArgs e)
        {

        }

        private void IDtbDistbtwnPart_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

        private void IDtbPartspm_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            hidePB();
        }

// ******************************************* Vision Accuracy ************************************************************************************

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (showimageVA)
            {
                if (VAtbFOV.Text == "")
                {
                    VApbFOV.Visible = true;
                }
                if (VAtbMinFeat.Text == "")
                {
                    VApbMinFeat.Visible = true;
                }
                showimageVA = false;
            }
            else
            {
                VApbMinFeat.Visible = false;
                VApbFOV.Visible = false;
                showimageVA = true;
            }

        }

        private void VAhidePB()
        {
            VApbMinFeat.Visible = false;
            VApbFOV.Visible = false;
        }

        private void VAtbFOV_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            VAhidePB();
        }

        private void VAtbMinFeat_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            VAhidePB();
        }

// ******************************************** LENS SELECTOR ********************************************************************

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (showimageLens)
            {
                if (LensrbFL.Checked == true)
                {
                    if (LenstbFOV.Text == "")
                    {
                        LenspbFOV.Visible = true;
                    }
                    if (LenstbWD.Text == "")
                    {
                        LenspbWD.Visible = true;
                    }
                }
                if (LensrbWD.Checked == true)
                {
                    if (LenstbFOV.Text == "")
                    {
                        LenspbFOV.Visible = true;
                    }
                    if (LenstbFL.Text == "")
                    {
                        LenspbFL.Visible = true;
                    }
                }
                if (LensrbFOV.Checked == true)
                {
                    if(LenstbFL.Text == "")
                    {
                        LenspbFL.Visible = true;
                    }
                    if (LenstbWD.Text == "")
                    {
                        LenspbWD.Visible = true;
                    }
                }
                showimageLens = false;
            }
            else
            {
                LenspbWD.Visible = false;
                LenspbFL.Visible = false;
                LenspbFOV.Visible = false;
                showimageLens = true;
            }
        }

        private void hidePBLens()
        {
            LenspbWD.Visible = false;
            LenspbFL.Visible = false;
            LenspbFOV.Visible = false;
        }

        private void LenstbFOV_MouseEnter(object sender, EventArgs e)
        {
            hidePBLens();
            timer3.Stop();
        }

        private void LenstbWD_MouseEnter(object sender, EventArgs e)
        {
            hidePBLens();
            timer3.Stop();
        }

        private void LenstbFL_MouseEnter(object sender, EventArgs e)
        {
            hidePBLens();
            timer3.Stop();
        }

        private void kryptonLabel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
