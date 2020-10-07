using System;
using System.Windows.Forms;

namespace _3D_Bender
{
    public partial class Form2 : Form
    {
        public string outputString = "";
        public string dataString = "";

        public Form2(string s)
        {
            dataString = s;
            InitializeComponent();
        }

        private void Form2_Load(object sender, System.EventArgs e)
        {
            //Figure out what's going on in the dataString
            string[] dataStringSplit = dataString.Split(',');
            string radiusFull = dataStringSplit[0];
            double feed = Convert.ToDouble(dataStringSplit[1]);

            if (radiusFull == "0")  //Line
            {
                helixSettingsGroupBox.Visible = false;
                helixSettingsGroupBox.Enabled = false;

                RadiusTxtBox.Text = radiusFull;

            }

            if (radiusFull != "0" && !radiusFull.Contains("|")) //Regular arc
            {
                helixCheckBox.Checked = false;
                RadiusTxtBox.Text = radiusFull;
                helixSettingsGroupBox.Visible = false;
                helixSettingsGroupBox.Enabled = false;

                double radius = Convert.ToDouble(radiusFull);

                arcAngleTxtBox.Text = Convert.ToString(Math.Round(Math.Abs(feed / radius) * 180 / Math.PI, 3));

            }

            if (radiusFull != "0" && radiusFull.Contains("|"))  //Helix
            {
                helixCheckBox.Checked = true;
                helixSettingsGroupBox.Visible = true;
                helixSettingsGroupBox.Enabled = true;

                string[] radiusSplit = radiusFull.Split('|');
                string radius = radiusSplit[0];
                string pitch = radiusSplit[1];

                RadiusTxtBox.Text = radius;
                pitchTxtBox.Text = pitch;
                feedTxtBox.Text = Convert.ToString(feed);
            }

        }

        private void HelixCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if(helixCheckBox.Checked)
            {
                helixSettingsGroupBox.Visible = true;
                helixSettingsGroupBox.Enabled = true;
            }

            if (!helixCheckBox.Checked)
            {
                helixSettingsGroupBox.Visible = false;
                helixSettingsGroupBox.Enabled = false;
            }
        }

        private void pitchFeedRadioBtn_CheckedChanged(object sender, System.EventArgs e)
        {
            if (pitchFeedRadioBtn.Checked)
            {
                rotationsTxtBox.ReadOnly = true;
                feedTxtBox.ReadOnly = false;
            }

            if (!pitchFeedRadioBtn.Checked)
            {
                rotationsTxtBox.ReadOnly = false;
                feedTxtBox.ReadOnly = true;
            }
        }

        void UpdateComputations()
        {
            if (!helixCheckBox.Checked)
            {
                if (String.IsNullOrEmpty(arcAngleTxtBox.Text) || String.IsNullOrEmpty(RadiusTxtBox.Text))
                    return;

                double radius = Convert.ToDouble(RadiusTxtBox.Text);
                double sweepAngle = Convert.ToDouble(arcAngleTxtBox.Text);

                double feed = Math.Round((sweepAngle * radius) * Math.PI / 180, 3);

                arcFeedTxtBox.Text = Convert.ToString(feed);
            }

            if (helixCheckBox.Checked)
            {
                if (pitchFeedRadioBtn.Checked)
                {
                    if (String.IsNullOrEmpty(pitchTxtBox.Text) || String.IsNullOrEmpty(feedTxtBox.Text) || String.IsNullOrEmpty(RadiusTxtBox.Text))
                        return;

                    double pitch = Convert.ToDouble(pitchTxtBox.Text);
                    double feed = Convert.ToDouble(feedTxtBox.Text);
                    double radius = Convert.ToDouble(RadiusTxtBox.Text);

                    double lengthPerRotation = Math.Sqrt(Math.Pow(pitch, 2) + Math.Pow(2 * Math.PI * radius, 2));
                    double rotations = Math.Round(feed / lengthPerRotation, 3);

                    rotationsTxtBox.Text = Convert.ToString(rotations);
                }

                if (pitchRotRadioBtn.Checked)
                {
                    if (String.IsNullOrEmpty(pitchTxtBox.Text) || String.IsNullOrEmpty(rotationsTxtBox.Text) || String.IsNullOrEmpty(RadiusTxtBox.Text))
                        return;

                    double pitch = Convert.ToDouble(pitchTxtBox.Text);
                    double rotations = Convert.ToDouble(rotationsTxtBox.Text);
                    double radius = Convert.ToDouble(RadiusTxtBox.Text);

                    double lengthPerRotation = Math.Sqrt(Math.Pow(pitch, 2) + Math.Pow(2 * Math.PI * radius, 2));
                    double feed = Math.Round(rotations * lengthPerRotation, 3);

                    feedTxtBox.Text = Convert.ToString(feed);
                }
            }

        }

        private void PitchTxtBox_TextChanged(object sender, System.EventArgs e)
        {
            UpdateComputations();
        }

        private void FeedTxtBox_TextChanged(object sender, System.EventArgs e)
        {
            UpdateComputations();
        }

        private void RotationsTxtBox_TextChanged(object sender, System.EventArgs e)
        {
            UpdateComputations();
        }

        private void RadiusTxtBox_TextChanged(object sender, EventArgs e)
        {
            UpdateComputations();
        }

        private void ArcAngleTxtBox_TextChanged(object sender, EventArgs e)
        {
            UpdateComputations();
        }

        private void InsertBendButton_Click(object sender, EventArgs e)
        {
            if (!helixCheckBox.Checked)
            {
                outputString = RadiusTxtBox.Text + "," + arcFeedTxtBox.Text;
            }

            if (helixCheckBox.Checked)
            {
                outputString = RadiusTxtBox.Text + "|" + pitchTxtBox.Text + "," + feedTxtBox.Text;
            }

            this.Close();
        }
    }
}
