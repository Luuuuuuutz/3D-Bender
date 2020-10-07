using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace _3D_Bender
{
    public partial class Form3 : Form
    {
        public DataTable settings = new DataTable() { TableName = "Settings" };
        public string settingsPath = "";

        public Form3(string path)
        {
            InitializeComponent();
            settingsPath = path;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(settingsPath))
                {
                    settings.ReadXml(sr);
                }
            }
            catch
            {
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    //InitialDirectory = localPath,
                    Title = "Choose bender config",
                    CheckFileExists = false,
                    CheckPathExists = false,
                    DefaultExt = ".xml",
                    Filter = "xml files (*.xml)|*.xml",
                    FilterIndex = 2,
                    RestoreDirectory = true,
                    FileName = Path.GetFileNameWithoutExtension(settingsPath),
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    settingsPath = ofd.FileName;
                    
                    using (StreamReader sr = new StreamReader(settingsPath))
                    {
                        settings.ReadXml(sr);
                    }
                }
                else
                {
                    return;
                }
            }


            foreach (DataRow row in settings.Rows)
                benderConfigComboBox.Items.Add(row[0]);


        }
    }
}
