using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media.Media3D;

namespace _3D_Bender
{
    public partial class Form1 : Form
    {
        //Test comment for github
        readonly string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
        string localPath = "";

        ElementHost host = new ElementHost();
        UserControl1 uc = new UserControl1();

        Matrix matrix = new Matrix();

        public DataTable dt = new DataTable() { TableName = "BendProfile" };
        public DataTable settings = new DataTable() { TableName = "Settings" };

        double materialLength = 12.0 * 25.4;    //Placeholder until I figure out something better
        double overBendMultiplier = 1.0;        //Additional radius bending multiplier

        double[,] R = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };  //Initialize coordinate converter R made of columns of x y z vectors

        string jogButtonPressed = "";
        string buffer = String.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            localPath = Path.GetDirectoryName(exePath) + "\\";   //Default path for saving things is at the .exe location

            dataGridView1.DataSource = null;

            dt.Columns.Add("Section",
                Type.GetType("System.Double"));
            dt.Columns.Add("Radius",
                Type.GetType("System.String"));
            dt.Columns.Add("Tilt Angle",
                Type.GetType("System.Double"));
            dt.Columns.Add("Feed Length",
                Type.GetType("System.Double"));

            //Make each column required
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].AllowDBNull = true;
            }

            dataGridView1.DataSource = dt;  //Set the datasource of the datagridview to the datatable
            dataGridView1.RowHeadersVisible = false;    //No row header
            dataGridView1.AllowUserToResizeColumns = false; //Don't allow column width to change
            dataGridView1.AllowUserToResizeRows = false;    //Don't allow row height to change

            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;  //Don't allow any columns to be sortable

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;

            var item = ContextMenuStrip1.Items.Add("Advanced Edit");      //Create the context menu strips (right click events)
            item.Click += AdvancedEdit_Click;
            item = ContextMenuStrip1.Items.Add("Add Section Above");
            item.Click += AddSectionAbove_Click;
            item = ContextMenuStrip1.Items.Add("Add Section Below");
            item.Click += AddSectionBelow_Click;
            item = ContextMenuStrip1.Items.Add("Remove Section");
            item.Click += RemoveSection_Click;

            host.Dock = DockStyle.Fill;
            host.Child = uc;
            groupBox1.Controls.Add(host);   //Make the groupbox the controller of the WPF form (matches size and whatnot)

            //Refresh the COM ports
            RefreshCOMPorts();

            //Do some other serial port stuff
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;
            serialPort1.ReadTimeout = 500;
            serialPort1.WriteTimeout = 500;

            //Set up the timer for holding down buttons


            //Setup the columns for the settings datatable
            settings.Columns.Add("ConfigName",
                Type.GetType("System.String"));
            settings.Columns.Add("DefaultUnits",
                Type.GetType("System.String"));
            settings.Columns.Add("StockLength",
                Type.GetType("System.Double"));
            settings.Columns.Add("XPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("XNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("YPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("YNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("ZPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("ZNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("APosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("ANegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("BPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("BNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitX",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitY",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitZ",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitA",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitB",
                Type.GetType("System.Double"));
            settings.Columns.Add("BendingHeadFixedDist",
                Type.GetType("System.Double"));

            //Make each column required
            for (int i = 0; i < settings.Columns.Count; i++)
            {
                settings.Columns[i].AllowDBNull = false;
            }

            //Load the settings.xml file
            using (StreamReader sr = new StreamReader(Path.GetDirectoryName(exePath) + "\\settings.xml"))
            {
                settings.ReadXml(sr);
            }

            dt.Rows.Add(new object[]
                {1, "0", 0, Convert.ToDouble(settings.Rows[0][18].ToString())});

            UpdateDataString(); //Update the rendering

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            GenerateGCode();
        }

        private void LoadIGES(List<Directory> directoryList, List<Parameter> parameterList)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IGES|*.IGS";    //Only display .IGS files

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                using (StreamReader r = new StreamReader(filePath))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        string section = line.Substring(line.Length - 8, 1);    //The 8th from last character is the section 

                        if (section.Contains("D"))  //Directory section
                        {
                            string[] directoryData = new string[20];
                            directoryData[0] = line.Substring(0, 8);
                            directoryData[1] = line.Substring(8, 8);
                            directoryData[2] = line.Substring(16, 8);
                            directoryData[3] = line.Substring(24, 8);
                            directoryData[4] = line.Substring(32, 8);
                            directoryData[5] = line.Substring(40, 8);
                            directoryData[6] = line.Substring(48, 8);
                            directoryData[7] = line.Substring(56, 8);
                            directoryData[8] = line.Substring(64, 8);
                            directoryData[9] = line.Substring(72, 8);

                            string nextLine = r.ReadLine(); //We have to read the next line because directories are always 2 lines long
                            directoryData[10] = nextLine.Substring(0, 8);
                            directoryData[11] = nextLine.Substring(8, 8);
                            directoryData[12] = nextLine.Substring(16, 8);
                            directoryData[13] = nextLine.Substring(24, 8);
                            directoryData[14] = nextLine.Substring(32, 8);
                            directoryData[15] = nextLine.Substring(40, 8);
                            directoryData[16] = nextLine.Substring(48, 8);
                            directoryData[17] = nextLine.Substring(56, 8);
                            directoryData[18] = nextLine.Substring(64, 8);
                            directoryData[19] = nextLine.Substring(72, 8);

                            directoryList.Add(new Directory(directoryData[0], directoryData[1], directoryData[2], directoryData[3], directoryData[4], directoryData[5], directoryData[6], directoryData[7], directoryData[8], directoryData[9], directoryData[10], directoryData[11], directoryData[12], directoryData[13], directoryData[14], directoryData[15], directoryData[16], directoryData[17], directoryData[18], directoryData[19]));
                        }

                        if (section.Contains("P"))  //P is the parameter section
                        {
                            string[] parameterData = new string[3];

                            parameterData[0] = line.Substring(0, 64);   //First 64 characters are comma separated data
                            parameterData[1] = line.Substring(64, 8);   //Directory Reference
                            parameterData[2] = line.Substring(72, 8);   //Line Number

                            parameterList.Add(new Parameter(parameterData[0], parameterData[1], parameterData[2]));

                        }
                    }
                }
            }
            else
            {
                return;
            }
        }

        public void ComputeIGES(List<Directory> directoryList, List<Parameter> parameterList, List<BendLine> lineList, List<CircArc> circArcList)
        {
            //First read the directory to figure out whats going on lol
            //Reset the R matrix
            R[0, 0] = 1;
            R[1, 0] = 0;
            R[2, 0] = 0;

            R[0, 1] = 0;
            R[1, 1] = 1;
            R[2, 1] = 0;

            R[0, 2] = 0;
            R[1, 2] = 0;
            R[2, 2] = 1;

            dt.Rows.Clear();

            foreach (Directory dir in directoryList)
            {
                int paramPointer = Convert.ToInt32(dir.Line1Field2);  //Get the corresponding ID of the parameter

                int paramTransMatrix = Convert.ToInt32(dir.Line1Field7.Replace("        ", "0"));    //If the parameter has a transformation matrix and the pointer to it
                paramTransMatrix = ((paramTransMatrix - 1) / 2);  //The directory list is one entry per two lines
                int paramLen = Convert.ToInt32(dir.Line2Field14);   //Figure out how many lines the parameter uses

                string fullParam = "";
                for (int i = paramPointer - 1; i < (paramPointer + paramLen - 1); i++)
                {
                    fullParam += parameterList[i].Field1;
                }

                fullParam = fullParam.Replace(";", ""); //Remove the semicolon
                string[] paramSplit = fullParam.Split(',');  //Then split it

                //Now to figure out what it is
                if (paramSplit[0] == "110") //Line. If its a line then store the data directly
                {
                    lineList.Add(new BendLine(Math.Round(Convert.ToDouble(paramSplit[1]), 5), Math.Round(Convert.ToDouble(paramSplit[2]), 5), Math.Round(Convert.ToDouble(paramSplit[3]), 5), Math.Round(Convert.ToDouble(paramSplit[4]), 5), Math.Round(Convert.ToDouble(paramSplit[5]), 5), Math.Round(Convert.ToDouble(paramSplit[6]), 5)));
                }

                if (paramSplit[0] == "100") //Circular arc. 
                {
                    //Check if there is a reference to a matrix
                    if (paramTransMatrix != 0)
                    {
                        int matrixPointer = Convert.ToInt32(directoryList[paramTransMatrix].Line1Field2);  //This is the beginning of the matrix
                        int matrixLen = Convert.ToInt32(directoryList[paramTransMatrix].Line2Field14);     //And this is how many lines it is

                        string matrixParam = "";
                        for (int i = matrixPointer - 1; i < (matrixPointer + matrixLen - 1); i++)
                        {
                            matrixParam += parameterList[i].Field1;
                        }

                        matrixParam = matrixParam.Replace(";", String.Empty);
                        string[] matrixSplit = matrixParam.Split(',');
                        //foreach (string m in matrixSplit)
                        //    MessageBox.Show(Convert.ToDouble(m).ToString());
                        //Create a new rotational matrix

                        RotationalMatrix rotationMatrix = new RotationalMatrix(Convert.ToDouble(matrixSplit[1]), Convert.ToDouble(matrixSplit[2]), Convert.ToDouble(matrixSplit[3]), Convert.ToDouble(matrixSplit[4]), Convert.ToDouble(matrixSplit[5]), Convert.ToDouble(matrixSplit[6]), Convert.ToDouble(matrixSplit[7]), Convert.ToDouble(matrixSplit[8]), Convert.ToDouble(matrixSplit[9]), Convert.ToDouble(matrixSplit[10]), Convert.ToDouble(matrixSplit[11]), Convert.ToDouble(matrixSplit[12]));

                        //Now do the matrix math to figure out where the circular arc is really at (R * Xin + T)
                        double[] circArcCMatrix = new double[3];    //Center Point
                        double[] circArcSMatrix = new double[3];    //Start Point
                        double[] circArcEMatrix = new double[3];    //End Point

                        circArcCMatrix[0] = Convert.ToDouble(paramSplit[2]);    //X
                        circArcCMatrix[1] = Convert.ToDouble(paramSplit[3]);    //Y
                        circArcCMatrix[2] = Convert.ToDouble(paramSplit[1]);    //Z

                        circArcSMatrix[0] = Convert.ToDouble(paramSplit[4]);    //X1
                        circArcSMatrix[1] = Convert.ToDouble(paramSplit[5]);    //Y1
                        circArcSMatrix[2] = Convert.ToDouble(paramSplit[1]);    //Z

                        circArcEMatrix[0] = Convert.ToDouble(paramSplit[6]);    //X2
                        circArcEMatrix[1] = Convert.ToDouble(paramSplit[7]);    //Y2
                        circArcEMatrix[2] = Convert.ToDouble(paramSplit[1]);    //Z

                        //Center
                        double X = (rotationMatrix.R11 * circArcCMatrix[0]) + (rotationMatrix.R12 * circArcCMatrix[1]) + (rotationMatrix.R13 * circArcCMatrix[2]) + rotationMatrix.T1;
                        double Y = (rotationMatrix.R21 * circArcCMatrix[0]) + (rotationMatrix.R22 * circArcCMatrix[1]) + (rotationMatrix.R23 * circArcCMatrix[2]) + rotationMatrix.T2;
                        double Z = (rotationMatrix.R31 * circArcCMatrix[0]) + (rotationMatrix.R32 * circArcCMatrix[1]) + (rotationMatrix.R33 * circArcCMatrix[2]) + rotationMatrix.T3;

                        //Start and End
                        double X1 = (rotationMatrix.R11 * circArcSMatrix[0]) + (rotationMatrix.R12 * circArcSMatrix[1]) + (rotationMatrix.R13 * circArcSMatrix[2]) + rotationMatrix.T1;
                        double X2 = (rotationMatrix.R11 * circArcEMatrix[0]) + (rotationMatrix.R12 * circArcEMatrix[1]) + (rotationMatrix.R13 * circArcEMatrix[2]) + rotationMatrix.T1;
                        double Y1 = (rotationMatrix.R21 * circArcSMatrix[0]) + (rotationMatrix.R22 * circArcSMatrix[1]) + (rotationMatrix.R23 * circArcSMatrix[2]) + rotationMatrix.T2;
                        double Y2 = (rotationMatrix.R21 * circArcEMatrix[0]) + (rotationMatrix.R22 * circArcEMatrix[1]) + (rotationMatrix.R23 * circArcEMatrix[2]) + rotationMatrix.T2;
                        double Z1 = (rotationMatrix.R31 * circArcSMatrix[0]) + (rotationMatrix.R32 * circArcSMatrix[1]) + (rotationMatrix.R33 * circArcSMatrix[2]) + rotationMatrix.T3;
                        double Z2 = (rotationMatrix.R31 * circArcEMatrix[0]) + (rotationMatrix.R32 * circArcEMatrix[1]) + (rotationMatrix.R33 * circArcEMatrix[2]) + rotationMatrix.T3;

                        circArcList.Add(new CircArc(Math.Round(X, 5), Math.Round(Y, 5), Math.Round(Z, 5), Math.Round(X1, 5), Math.Round(Y1, 5), Math.Round(Z1, 5), Math.Round(X2, 5), Math.Round(Y2, 5), Math.Round(Z2, 5)));

                    }
                }
            }

        }

        public void OrderIGES(List<BendLine> lineList, List<CircArc> circArcList)
        {
            double pointToFindX = 0.0;
            double pointToFindY = 0.0;
            double pointToFindZ = 0.0;

            int index;
            int bendCount = 0;  //Initialize a counter for which element of the bend profile we are on

            do
            {
                //First try to find it in the LineList Starting Point
                index = lineList.FindIndex(x => (x.X1 == pointToFindX) && (x.Y1 == pointToFindY) && (x.Z1 == pointToFindZ));
                if (index != -1)    //If a proper index was found
                {
                    //Set the new points to find to the end points of the line
                    pointToFindX = lineList[index].X2;
                    pointToFindY = lineList[index].Y2;
                    pointToFindZ = lineList[index].Z2;

                    AddLine(bendCount, lineList[index].X1, lineList[index].X2, lineList[index].Y1, lineList[index].Y2, lineList[index].Z1, lineList[index].Z2);
                    lineList.RemoveAt(index);   //Remove it so we don't accidentally find it again
                    bendCount++;
                    continue;
                }

                index = lineList.FindIndex(x => (x.X2 == pointToFindX) && (x.Y2 == pointToFindY) && (x.Z2 == pointToFindZ));
                if (index != -1)
                {
                    //If it was found on the end points then we swap the start and end to make our lives easier
                    double temp;
                    temp = lineList[index].X1;  //Save X1 in a temp variable
                    lineList[index].X1 = lineList[index].X2;    //Put X2 into X1
                    lineList[index].X2 = temp;  //Put X1 into X2

                    temp = lineList[index + bendCount].Y1;  //Save Y1 in a temp variable
                    lineList[index].Y1 = lineList[index].Y2;    //Put Y2 into Y1
                    lineList[index].Y2 = temp;  //Put Y1 into Y2

                    temp = lineList[index].Z1;  //Save Z1 in a temp variable
                    lineList[index].Z1 = lineList[index].Z2;    //Put Z2 into Z1
                    lineList[index].Z2 = temp;  //Put Z1 into Z2

                    //Set the new points to find to the end points of the line
                    pointToFindX = lineList[index].X2;
                    pointToFindY = lineList[index].Y2;
                    pointToFindZ = lineList[index].Z2;

                    AddLine(bendCount, lineList[index].X1, lineList[index].X2, lineList[index].Y1, lineList[index].Y2, lineList[index].Z1, lineList[index].Z2);
                    lineList.RemoveAt(index);   //Remove it so we don't accidentally find it again
                    bendCount++;
                    continue;
                }

                index = circArcList.FindIndex(x => (x.StartX == pointToFindX) && (x.StartY == pointToFindY) && (x.StartZ == pointToFindZ));
                if (index != -1)    //If a proper index was found
                {

                    //Set the new points to find to the end points of the line
                    pointToFindX = circArcList[index].EndX;
                    pointToFindY = circArcList[index].EndY;
                    pointToFindZ = circArcList[index].EndZ;

                    AddCircularArc(bendCount, circArcList[index].CenterX, circArcList[index].CenterY, circArcList[index].CenterZ, circArcList[index].StartX, circArcList[index].StartY, circArcList[index].StartZ, circArcList[index].EndX, circArcList[index].EndY, circArcList[index].EndZ);
                    circArcList.RemoveAt(index);
                    bendCount++;
                    continue;
                }

                //Try the CircleArcList end points
                index = circArcList.FindIndex(x => (x.EndX == pointToFindX) && (x.EndY == pointToFindY) && (x.EndZ == pointToFindZ));
                if (index != -1)
                {
                    //If it was found on the end points then we swap the start and end to make our lives easier
                    double temp;
                    temp = circArcList[index].StartX;  //Save StartX in a temp variable
                    circArcList[index].StartX = circArcList[index].EndX;    //Put EndX into StartX
                    circArcList[index].EndX = temp;  //Put StartX into EndX

                    temp = circArcList[index].StartY;  //Save StartY in a temp variable
                    circArcList[index].StartY = circArcList[index].EndY;    //Put End2 into StartY
                    circArcList[index].EndY = temp;  //Put StartY into EndY

                    temp = circArcList[index].StartZ;  //Save Z1 in a temp variable
                    circArcList[index].StartZ = circArcList[index].EndZ;    //Put EndZ into StartZ
                    circArcList[index].EndZ = temp;  //Put StartZ into EndZ

                    //Set the new points to find to the end points of the circlular arc
                    pointToFindX = circArcList[index].EndX;
                    pointToFindY = circArcList[index].EndY;
                    pointToFindZ = circArcList[index].EndZ;

                    AddCircularArc(bendCount, circArcList[index].CenterX, circArcList[index].CenterY, circArcList[index].CenterZ, circArcList[index].StartX, circArcList[index].StartY, circArcList[index].StartZ, circArcList[index].EndX, circArcList[index].EndY, circArcList[index].EndZ);
                    circArcList.RemoveAt(index);
                    bendCount++;
                    continue;
                }

            } while (index != -1);  //If we got here and never found a match then we must be at the end of the profile
        }

        public void AddLine(int i, double X1, double X2, double Y1, double Y2, double Z1, double Z2)
        {
            double feed = Math.Round(Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)), 3);    //The feed distance is simply the distance between staart & end

            //MessageBox.Show("Adding line");
            dt.Rows.Add(new object[]
                {i+1, "0", 0, feed});
        }

        public void AddCircularArc(int i, double CenterX, double CenterY, double CenterZ, double StartX, double StartY, double StartZ, double EndX, double EndY, double EndZ)
        {
            //Compute radius
            double radius = Math.Round(Math.Sqrt(Math.Pow(CenterX - StartX, 2) + Math.Pow(CenterY - StartY, 2) + Math.Pow(CenterZ - StartZ, 2)), 3);

            //Compute feed length
            double d = Math.Round(Math.Sqrt(Math.Pow(EndX - StartX, 2) + Math.Pow(EndY - StartY, 2) + Math.Pow(EndZ - StartZ, 2)), 3);
            double feed = Math.Round(radius * 2 * Math.Asin(d / (2 * radius)), 3);

            //Now to find the tilt
            double tiltAngle = 0;

            //First we have to convert the local coordinates to the regulat xyz coordinate system
            Point3D center = new Point3D(CenterX, CenterY, CenterZ);  //center coordinates
            Point3D start = new Point3D(StartX, StartY, StartZ);  //start coordinates
            Point3D end = new Point3D(EndX, EndY, EndZ);  //start coordinates
            Point3D tiltLine = new Point3D(CenterX - StartX, CenterY - StartY, CenterZ - StartZ);  //center - start coordinates

            double[,] Rp = new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; //Initialize inverse matrix

            matrix.Invert(Rp, R);

            Point3D centerP = matrix.Multiply3x1P(Rp, center);    //centerp = Rp * center
            Point3D startP = matrix.Multiply3x1P(Rp, start);    //startp = Rp * start
            Point3D endP = matrix.Multiply3x1P(Rp, end);    //endp = Rp * end

            Point3D tiltLineP = matrix.Multiply3x1P(Rp, tiltLine);

            double intersectHeight = 0;

            if (tiltLineP.X >= 0 && tiltLineP.Y >= 0)    //Positive X Positive Y
            {
                tiltAngle = (-1) * Math.Round(Math.Atan2(Math.Abs(tiltLineP.X), Math.Abs(tiltLineP.Y)) * 180 / Math.PI, 3);

                intersectHeight = radius / Math.Cos(tiltAngle * Math.PI / 180);
            }
            if (tiltLineP.X < 0 && tiltLineP.Y >= 0)    //Negative X Positive Y
            {
                tiltAngle = Math.Round(Math.Atan2(Math.Abs(tiltLineP.X), Math.Abs(tiltLineP.Y)) * 180 / Math.PI, 3);

                intersectHeight = radius / Math.Cos(tiltAngle * Math.PI / 180);
            }
            if (tiltLineP.X >= 0 && tiltLineP.Y < 0)    //Positive X Negative Y
            {
                tiltAngle = Math.Round(-180 + Math.Atan2(Math.Abs(tiltLineP.X), Math.Abs(tiltLineP.Y)) * 180 / Math.PI, 3);

                intersectHeight = -radius / Math.Cos((tiltAngle + 180) * Math.PI / 180);
            }
            if (tiltLineP.X < 0 && tiltLineP.Y < 0)    //Negative X Negative Y
            {
                tiltAngle = Math.Round(180 - Math.Atan2(Math.Abs(tiltLineP.X), Math.Abs(tiltLineP.Y)) * 180 / Math.PI, 3);

                intersectHeight = -radius / Math.Cos(((-1) * (tiltAngle - 180)) * Math.PI / 180);
            }

            Point3D planeVectorX = new Point3D(0, 0, 0);
            Point3D planeVectorY = new Point3D(0, 0, 0);
            Point3D planeVectorZ = new Point3D(0, 0, 0);


            planeVectorY.X = 0.0 - endP.X;    //Intersection must happen on the y axis
            planeVectorY.Y = intersectHeight - endP.Y;    //Intersect height - EndY'
            planeVectorY.Z = startP.Z - endP.Z;  //StartZ' - EndZ'
            planeVectorY = matrix.UnitVectorP(planeVectorY);

            Point3D planeVectorC = (Point3D)Point3D.Subtract(centerP, endP);

            planeVectorC = matrix.UnitVectorP(planeVectorC);

            planeVectorZ = matrix.CrossProductP(planeVectorY, planeVectorC);  //Find the z axis direction vector
            planeVectorZ = matrix.UnitVectorP(planeVectorZ);

            planeVectorX = matrix.CrossProductP(planeVectorY, planeVectorZ);  //Find the x axis direction vector
            planeVectorX = matrix.UnitVectorP(planeVectorX);

            //Now put the vectors where they actually belong with the transformation matrix R

            Point3D Rx = matrix.Multiply3x1P(R, planeVectorX);
            Point3D Ry = matrix.Multiply3x1P(R, planeVectorY);
            Point3D Rz = matrix.Multiply3x1P(R, planeVectorZ);

            //Now set the new transformation matrix R
            R[0, 0] = Rx.X;
            R[1, 0] = Rx.Y;
            R[2, 0] = Rx.Z;

            R[0, 1] = Ry.X;
            R[1, 1] = Ry.Y;
            R[2, 1] = Ry.Z;

            R[0, 2] = Rz.X;
            R[1, 2] = Rz.Y;
            R[2, 2] = Rz.Z;

            //MessageBox.Show("Adding circle");
            dt.Rows.Add(new object[]
                {i+1, radius.ToString(), tiltAngle, feed});

        }

        public void GenerateGCode()
        {
            double stepsPerUnitX = Math.Round(Convert.ToDouble(settings.Rows[0][13].ToString()), 3);
            double stepsPerUnitY = Math.Round(Convert.ToDouble(settings.Rows[0][14].ToString()), 3);
            double stepsPerUnitZ = Math.Round(Convert.ToDouble(settings.Rows[0][15].ToString()), 3);
            double stepsPerUnitA = Math.Round(Convert.ToDouble(settings.Rows[0][16].ToString()), 3);
            double stepsPerUnitB = Math.Round(Convert.ToDouble(settings.Rows[0][17].ToString()), 3);

            double zAccum = 0;

            string path = "";

            if (Convert.ToDouble(totalMaterialTextBox.Text) > materialLength)
            {
                MessageBox.Show("Bend profile too long for stock!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog()
            {
                //InitialDirectory = localPath,
                Title = "Save as",
                CheckFileExists = false,
                CheckPathExists = false,
                DefaultExt = ".gc",
                Filter = "gc files (*.gc)|*.gc",
                FilterIndex = 2,
                RestoreDirectory = true,
                FileName = Path.GetFileNameWithoutExtension(localPath),
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                path = sfd.FileName;
            }

            //Start by creating the memory stream that will eventually write to the g code
            using (var sw = new StreamWriter(path))
            {
                //First set the steps per unit (dependent on unit settings)
                sw.WriteLine("M92 X" + stepsPerUnitX.ToString() + " Y" + stepsPerUnitY.ToString() + " Z" + stepsPerUnitZ.ToString() + " A" + stepsPerUnitA.ToString() + " B" + stepsPerUnitB.ToString());

                sw.WriteLine("G21");    //Set units to mm (positions will be converted from english to metric if necessary

                sw.WriteLine("G90");    //Set to absolute mode
                sw.WriteLine("M17");    //Enable all steppers
                sw.WriteLine("G28.1 X0 Y0 Z0 A0 B0");   //Set home position to 0 0 0 0 0
                sw.WriteLine("G28");    //Move to home position


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string radiusFull = row.Cells[1].Value.ToString();

                        double tiltAngleZ = Convert.ToDouble(row.Cells[2].Value.ToString());
                        double feed = Convert.ToDouble(row.Cells[3].Value.ToString());

                        //Loop through each row and write the G code
                        if (radiusFull == "0")    //If the radius is 0 then it is a straight feed
                        {
                            zAccum += feed;  //Accumulate the total feed because we are in absolute position mode
                            sw.WriteLine("G1 X0 Y0 Z" + zAccum.ToString() + " A0 B0");
                        }
                        if (radiusFull != "0" && !radiusFull.Contains("|"))
                        {
                            //The radius must not be 0 which means its a circular arc
                            double radius = Convert.ToDouble(radiusFull) / overBendMultiplier;   //Radius / overBend to get new radius
                            zAccum += feed;     //Accumulate the total feed because we are in absolute position mode

                            HeadPosition hp = new HeadPosition();
                            ComputeHeadPosition(radius, tiltAngleZ, hp);
                            sw.WriteLine("G1 X" + hp.X.ToString() + " Y" + hp.Y.ToString() + " Z" + zAccum.ToString() + " A" + hp.A.ToString() + " B" + hp.B.ToString());
                        }
                        if (radiusFull != "0" && radiusFull.Contains("|"))
                        {
                            //This is the case where we have a helix with the number of rotations R|N
                            int pos = radiusFull.IndexOf("|");
                            double radius = Convert.ToDouble(radiusFull.Remove(pos)) / overBendMultiplier;
                            if (englishToolStripMenuItem.Checked)
                                radius = radius * 25.4; //Convert to mm

                            double pitch = Convert.ToDouble(radiusFull.Remove(0, pos + 1));
                            if (englishToolStripMenuItem.Checked)
                                pitch = pitch * 25.4; //Convert to mm

                            int N = 100;
                            double lengthPerRotation = Math.Sqrt(Math.Pow(pitch, 2) + Math.Pow(2 * Math.PI * radius, 2));
                            double turns = (feed / lengthPerRotation);
                            double height = turns * pitch;

                            double tiltTotal = 2 * Math.PI * Math.Atan2(height, 2 * Math.PI * radius) * 180 / Math.PI;

                            for (int k = 1; k <= N; k++)
                            {
                                double inProgFeed = feed / N; //Divide up the section into 100 sections

                                double inProgTiltAngleZ = (k * tiltTotal / N) + tiltAngleZ;

                                zAccum += inProgFeed;     //Accumulate the total feed because we are in absolute position mode

                                HeadPosition hp = new HeadPosition();
                                ComputeHeadPosition(radius, inProgTiltAngleZ, hp);
                                sw.WriteLine("G1 X" + hp.X.ToString() + " Y" + hp.Y.ToString() + " Z" + zAccum.ToString() + " A" + hp.A.ToString() + " B" + hp.B.ToString());
                            }

                        }
                    }
                }

                //Now we have to make sure we push out the remaining material, if necessary
                if (zAccum < materialLength)
                {
                    //double remainingMaterial = materialLength - zAccum;
                    sw.WriteLine("G1 Z" + materialLength.ToString());
                }

                //Once we are done with all of the bend profile we have to do a few more lines
                sw.WriteLine("G28");    //Go home after bending
                sw.WriteLine("M18");    //Disable stepper motors
                sw.WriteLine("M02");    //End of program

            }
        }

        public void ComputeHeadPosition(double R, double theta, HeadPosition hp)
        {
            //This function computes the requied position of the bending head to get the desired curve. Lots of math
            double z = Convert.ToDouble(settings.Rows[0][18].ToString());   //Distance from the fixed point to bending point. Constant, dependant on physical design

            theta = theta * Math.PI / 180;  //Convert to radians
            //First find the total distance from the support to bend head point
            double d = R - Math.Sqrt(Math.Pow(R, 2) - Math.Pow(z, 2));

            hp.X = -Math.Round((Math.Sin(theta) * d), 3);   //Flipped because the tilt angle is defined as rotation around z axis which is backwards for x
            hp.Y = Math.Round((Math.Cos(theta) * d), 3);

            Point3D bendPoint = new Point3D(hp.X, hp.Y, z);    //Create bend point
            Point3D centerPoint = new Point3D(R * Math.Sin(theta), R * Math.Cos(theta), 0);    //Center point of the circle
            Point3D centerZ = new Point3D(R * Math.Sin(theta), R * Math.Cos(theta), -R);       //Center point of z
            Point3D fixedPoint = new Point3D(0, 0, 0);     //Fixed point (support point, defined as 0,0,0)

            Point3D centerToFixed = (Point3D)Point3D.Subtract(fixedPoint, centerPoint); //Initialize centerToFixed point (fixedPoint - centerPoint)
            Point3D centerToZ = (Point3D)Point3D.Subtract(fixedPoint, centerZ);         //centerToZ = (fixedPoint - centerZ)
            Point3D normalTilt = matrix.CrossProductP(centerToZ, centerToFixed);        //normalTilt = centerToZ X centerToFixed
            Point3D normalTangent = (Point3D)Point3D.Subtract(bendPoint, centerPoint);  //normalTangent = bendPoint - centerPoint
            Point3D tangentLine = matrix.CrossProductP(normalTilt, normalTangent);      //tangentLine = normalTilt X normalTangent

            //Finally we can find the angles A and B
            hp.A = -Math.Round(90 - (Math.Acos(tangentLine.Y / (Math.Sqrt(Math.Pow(tangentLine.X, 2) + Math.Pow(tangentLine.Y, 2) + Math.Pow(tangentLine.Z, 2))))) * 180 / Math.PI, 3);
            hp.B = Math.Round(90 - (Math.Acos(tangentLine.X / (Math.Sqrt(Math.Pow(tangentLine.X, 2) + Math.Pow(tangentLine.Y, 2) + Math.Pow(tangentLine.Z, 2))))) * 180 / Math.PI, 3);

        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Any time the datagrid is changed then we update the rendering
            if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() != "0")    //Only update if the feed has a value in it, otherwise it will break things
                UpdateDataString();
        }

        public void UpdateDataString()
        {
            string dataTable = "";

            double totalLength = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null) //Check to make sure we aren't going to read any empty cells
                {
                    dataTable += row.Cells[0].Value.ToString();
                    dataTable += ",";
                    dataTable += row.Cells[1].Value.ToString();
                    dataTable += ",";
                    dataTable += row.Cells[2].Value.ToString();
                    dataTable += ",";
                    dataTable += row.Cells[3].Value.ToString();
                    dataTable += ";";

                    totalLength = totalLength + Convert.ToDouble(row.Cells[3].Value);
                }
            }

            //Update the total material used text box
            totalMaterialTextBox.Text = totalLength.ToString();

            if (totalLength > materialLength)
            {
                totalMaterialTextBox.BackColor = Color.Red;
            }
            else
                totalMaterialTextBox.BackColor = Color.White;


            UserControl1.DataTableString = dataTable;
            UserControl1.Units = metricToolStripMenuItem.Checked;
            uc.UpdateData();    //Start the function in the WPF 

        }

        public void OpenFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                dt.ReadXml(sr);
            }
        }

        public void SaveFile(string path)
        {
            //We simply write the data table to an xml file at the path
            using (StreamWriter sw = new StreamWriter(path))
            {
                dt.WriteXml(sw);
            }

        }

        public void DeleteProfile()
        {
            dt.Rows.Clear();    //Simply remove all the existing rows... and add in the default feed of the distance between bending head and fixed point

            dt.Rows.Add(new object[]
                {1, "0", 0, Convert.ToDouble(settings.Rows[0][18].ToString())});
            uc.UpdateData();    //Then have the 3D model reflect that something changed
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteProfile();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                //InitialDirectory = localPath,
                Title = "Browse for .bp file.",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".bp",
                Filter = "bp files (*.bp)|*.bp",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DeleteProfile();
                localPath = ofd.FileName;  //Use custom file extension .bp for bend profile (its actually a .xml)
                OpenFile(localPath);
            }

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Path.GetFileName(localPath) == string.Empty)
            {
                SaveFileDialog sfd = new SaveFileDialog()
                {
                    //InitialDirectory = localPath,
                    Title = "Save as",
                    CheckFileExists = false,
                    CheckPathExists = false,
                    DefaultExt = ".bp",
                    Filter = "bp files (*.bp)|*.bp",
                    FilterIndex = 2,
                    RestoreDirectory = true,
                    FileName = Path.GetFileNameWithoutExtension(localPath),
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    localPath = sfd.FileName;
                    SaveFile(localPath);
                }
            }
            SaveFile(localPath);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                //InitialDirectory = localPath,
                Title = "Save as",
                CheckFileExists = false,
                CheckPathExists = false,
                DefaultExt = ".bp",
                Filter = "bp files (*.bp)|*.bp",
                FilterIndex = 2,
                RestoreDirectory = true,
                FileName = Path.GetFileNameWithoutExtension(localPath),
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localPath = sfd.FileName;
                SaveFile(localPath);
            }

        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var directoryList = new List<Directory>();
            var parameterList = new List<Parameter>();

            var lineList = new List<BendLine>();
            var circArcList = new List<CircArc>();

            DeleteProfile();

            LoadIGES(directoryList, parameterList);
            ComputeIGES(directoryList, parameterList, lineList, circArcList);
            OrderIGES(lineList, circArcList);

            UpdateDataString(); //Update the rendering once the IGES file is loaded
        }

        private void SaveBenderConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void RefreshCOMPorts()
        {
            string[] ports = SerialPort.GetPortNames();

            COMPortComboBox.Items.Clear();

            foreach (string s in ports)
            {
                COMPortComboBox.Items.Add(s);
            }
        }

        public void UpdatePositionalData(string buffer)
        {
            //First split the string into two pieces
            string[] data = buffer.Split(';');
            string[] actualPos = data[0].Split(',');
            string[] commandedPos = data[1].Split(',');

            actualPosXTextBox.Invoke((Action)delegate
            {
                actualPosXTextBox.Text = actualPos[1].Replace("X", "");
            });

            actualPosYTextBox.Invoke((Action)delegate
            {
                actualPosYTextBox.Text = actualPos[2].Replace("Y", "");
            });

            actualPosZTextBox.Invoke((Action)delegate
            {
                actualPosZTextBox.Text = actualPos[3].Replace("Z", "");
            });

            actualPosATextBox.Invoke((Action)delegate
            {
                actualPosATextBox.Text = actualPos[4].Replace("A", "");
            });

            actualPosBTextBox.Invoke((Action)delegate
            {
                actualPosBTextBox.Text = actualPos[5].Replace("B", "");
            });

            //Update commanded position text boxes
            commandPosXTextBox.Invoke((Action)delegate
            {
                commandPosXTextBox.Text = commandedPos[1].Replace("X", "");
            });

            commandPosYTextBox.Invoke((Action)delegate
            {
                commandPosYTextBox.Text = commandedPos[2].Replace("Y", "");
            });
            commandPosZTextBox.Invoke((Action)delegate
            {
                commandPosZTextBox.Text = commandedPos[3].Replace("Z", "");
            });
            commandPosATextBox.Invoke((Action)delegate
            {
                commandPosATextBox.Text = commandedPos[4].Replace("A", "");
            });
            commandPosBTextBox.Invoke((Action)delegate
            {
                commandPosBTextBox.Text = commandedPos[5].Replace("B", "");
            });

        }

        private void ButtonTimer_Tick(object sender, EventArgs e)
        {
            //Check which button was pressed and do the jogging for that axis
            char axis = jogButtonPressed[0];
            char dir = jogButtonPressed[1];

            double nextPosition = 0;
            double deltaPosition;

            if (dir == '+')
                deltaPosition = 0.01;
            else
                deltaPosition = -0.01;

            if (axis == 'X')
            {
                nextPosition = Convert.ToDouble(actualPosXTextBox.Text) + deltaPosition;
            }
            if (axis == 'Y')
            {
                nextPosition = Convert.ToDouble(actualPosYTextBox.Text) + deltaPosition;
            }
            if (axis == 'Z')
            {
                nextPosition = Convert.ToDouble(actualPosZTextBox.Text) + deltaPosition;
            }
            if (axis == 'A')
            {
                nextPosition = Convert.ToDouble(actualPosATextBox.Text) + deltaPosition;
            }
            if (axis == 'B')
            {
                nextPosition = Convert.ToDouble(actualPosBTextBox.Text) + deltaPosition;
            }

            //Build the G-Code command: G1 X/Y/Z/A/Bnnnn.nnn
            string command = "G1 ";
            command += dir.ToString();
            command += " ";
            command += nextPosition.ToString();

            SerialWrite(command);   //Write the command to the serial interface
        }

        public void MouseUpGeneric()
        {
            buttonTimer.Enabled = false;    //Stop the timer and reset the job button pressed string
            jogButtonPressed = "";

            string command = "M00";

            SerialWrite(command);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(serialPort1.IsOpen))
                {
                    serialPort1.PortName = COMPortComboBox.SelectedItem.ToString();
                    serialPort1.Open();
                    serialConnectButton.Text = "Disconnect";

                    positionControlGroupBox.Enabled = true;
                    jogControlGroupBox.Enabled = true;
                    homeAxesGroupBox.Enabled = true;
                }
                else if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    serialConnectButton.Text = "Conenct";

                    positionControlGroupBox.Enabled = false;
                    jogControlGroupBox.Enabled = false;
                    homeAxesGroupBox.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        #region Jog Button Mouse Events
        private void JogPosXButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "X+";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogPosXButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }


        private void JogNegXButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "X-";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogNegXButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogPosYButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "Y+";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogPosYButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogNegYButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "Y-";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogNegYButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogPosZButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "Z+";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogPosZButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogNegZButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "Z-";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogNegZButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogPosAButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "A+";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogPosAButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogNegAButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "A-";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogNegAButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogPosBButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "B+";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void jogPosBButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }

        private void JogNegBButton_MouseDown(object sender, MouseEventArgs e)
        {
            jogButtonPressed = "B-";
            buttonTimer.Enabled = true; //Set the jog button selection and start the timer
        }

        private void JogNegBButton_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpGeneric();
        }
        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            settings.Columns.Add("ConfigName",
                Type.GetType("System.String"));

            settings.Columns.Add("DefaultUnits",
                Type.GetType("System.String"));

            settings.Columns.Add("StockLength",
                Type.GetType("System.Double"));

            settings.Columns.Add("XPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("XNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("YPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("YNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("ZPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("ZNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("APosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("ANegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("BPosLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("BNegLimit",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitX",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitY",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitZ",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitA",
                Type.GetType("System.Double"));
            settings.Columns.Add("StepsPerUnitB",
                Type.GetType("System.Double"));
            settings.Columns.Add("BendingHeadFixedDist",
                Type.GetType("System.Double"));

            //Make each column required
            for (int i = 0; i < settings.Columns.Count; i++)
            {
                settings.Columns[i].AllowDBNull = false;
            }

            double gearRatioXY = 5;
            double gearRatioZ = 1;
            double stepsPerUnitXY = (360 / 1.8) * gearRatioXY / 2;
            double stepsPerUnitZ = (360 / 1.8) * gearRatioZ / 2;

            double gearRatioAB = 100;
            double stepsPerUnitAB = gearRatioAB / 1.8;
            
            settings.Rows.Add(new object[]
                {"5-Axis Bender", "mm", 12 * 25.4, 28, -21, 8, 26, 305, -5, 60, -60, 30, -30, stepsPerUnitXY, stepsPerUnitXY, stepsPerUnitZ, stepsPerUnitAB, stepsPerUnitAB, 15.79 });

            using (StreamWriter sw = new StreamWriter("C:\\Users\\eric\\source\\repos\\3D Bender\\3D Bender\\bin\\Debug\\settings.xml"))
            {
                settings.WriteXml(sw);
            }
        }

        private void RefreshCOMPortsButton_Click(object sender, EventArgs e)
        {
            RefreshCOMPorts();
        }

        private void SerialWrite(string data)
        {
            data += "\n";   //Add a new line character at the end of the command string
            serialPort1.Write(data);
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //On data recieved we have to do some parsing to check whats going on
            //Should be in the format actual position, then commanded position
            buffer += serialPort1.ReadExisting();
            if (buffer.Contains("\n"))
            {
                UpdatePositionalData(buffer);
                buffer = String.Empty;
            }

        }

        private void AdvancedEdit_Click(Object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            string rowString = dt.Rows[rowIndex][1].ToString() + "," + dt.Rows[rowIndex][3].ToString();

            Form2 form2 = new Form2(rowString);

            form2.ShowDialog();

            if (form2.DialogResult != DialogResult.Cancel)
            {
                //Now put the results into the bend profile datagrid
                string[] dataSplit = form2.outputString.Split(',');

                dt.Rows[rowIndex][1] = dataSplit[0];
                dt.Rows[rowIndex][3] = dataSplit[1];
            }
        }

        private void AddSectionAbove_Click(Object sender, EventArgs e)
        {
            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = 0;
            row[2] = 0;
            row[3] = 0;

            int index = 0;
            try
            {
                index = dataGridView1.SelectedRows[0].Index;
            }
            catch { index = 0; }

            dt.Rows.InsertAt(row, index);

            UpdateSectionNumber();
        }

        private void AddSectionBelow_Click(Object sender, EventArgs e)
        {
            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = 0;
            row[2] = 0;
            row[3] = 0;

            int index = 0;
            try
            {
                index = dataGridView1.SelectedRows[0].Index;
            }
            catch { index = 1; }

            dt.Rows.InsertAt(row, index + 1);

            UpdateSectionNumber();
        }

        private void RemoveSection_Click(Object sender, EventArgs e)
        {
            try
            {
                //Remove the selected row
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                //Then re-compute the section numbers in the datagridview
                UpdateSectionNumber();
                UpdateDataString(); //Update the rendering once the IGES file is loaded
            }
            catch
            {
            }
        }

        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    if (hit.RowIndex <= dataGridView1.Rows.Count)
                    {
                        if (!dataGridView1.Rows[hit.RowIndex].IsNewRow)
                        {
                            dataGridView1.ClearSelection();
                            dataGridView1.Rows[hit.RowIndex].Selected = true;
                        }
                    }
                }
            }
        }

        public void UpdateSectionNumber()
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;
            }
        }

        void UpdateBendUnits(double conversion)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][1] = Math.Round(Convert.ToDouble(dt.Rows[i][1]) * conversion, 3);
                dt.Rows[i][3] = Math.Round(Convert.ToDouble(dt.Rows[i][3]) * conversion, 3);
            }
        }

        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Convert existing bend profile to inches?", "Confirmation", MessageBoxButtons.YesNoCancel);

            if (res == DialogResult.Yes)
            {
                metricToolStripMenuItem.Checked = false;
                metricToolStripMenuItem.Enabled = true;
                englishToolStripMenuItem.Checked = true;
                englishToolStripMenuItem.Enabled = false;
                UpdateBendUnits(1 / 25.4);
            }
            if (res == DialogResult.No)
            {
                metricToolStripMenuItem.Checked = false;
                metricToolStripMenuItem.Enabled = true;
                englishToolStripMenuItem.Checked = true;
                englishToolStripMenuItem.Enabled = false;
            }
            if (res == DialogResult.Cancel)
                return;

            UpdateDataString();
        }

        private void MetricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Convert existing bend profile to millimeters?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                englishToolStripMenuItem.Checked = false;
                englishToolStripMenuItem.Enabled = true;
                metricToolStripMenuItem.Checked = true;
                metricToolStripMenuItem.Enabled = false;
                UpdateBendUnits(25.4);
            }
            if (res == DialogResult.No)
            {
                englishToolStripMenuItem.Checked = false;
                englishToolStripMenuItem.Enabled = true;
                metricToolStripMenuItem.Checked = true;
                metricToolStripMenuItem.Enabled = false;
            }
            if (res == DialogResult.Cancel)
                return;

            UpdateDataString();

        }

        private void eStopButton_Click(object sender, EventArgs e)
        {
            SerialWrite("M00"); //Simply write the stop command
        }

        private void MoveToPosXButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G01 X" + actualPosXTextBox.Text);
        }

        private void MoveToPosYButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G01 Y" + actualPosYTextBox.Text);
        }

        private void MoveToPosZButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G01 Z" + actualPosZTextBox.Text);
        }

        private void MoveToPosAButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G01 A" + actualPosATextBox.Text);
        }

        private void MoveToPosBButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G01 B" + actualPosBTextBox.Text);
        }

        private void SetZeroXButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G92 X0");
            //actualPosXTextBox.Text = "0.000";
        }

        private void SetZeroYButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G92 Y0");
            //actualPosYTextBox.Text = "0.000";
        }

        private void SetZeroZButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G92 Z0");
            //actualPosZTextBox.Text = "0.000";
        }

        private void SetZeroAButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G92 A0");
            //actualPosATextBox.Text = "0.000";
        }

        private void SetZeroBButton_Click(object sender, EventArgs e)
        {
            SerialWrite("G92 B0");
            //actualPosBTextBox.Text = "0.000";
        }
        
        private void benderConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(localPath);
            form3.ShowDialog();
        }

    }

    public class Directory
    {
        public string Line1Field1 { get; set; }
        public string Line1Field2 { get; set; }
        public string Line1Field3 { get; set; }
        public string Line1Field4 { get; set; }
        public string Line1Field5 { get; set; }
        public string Line1Field6 { get; set; }
        public string Line1Field7 { get; set; }
        public string Line1Field8 { get; set; }
        public string Line1Field9 { get; set; }
        public string Line1Field10 { get; set; }
        public string Line2Field11 { get; set; }
        public string Line2Field12 { get; set; }
        public string Line2Field13 { get; set; }
        public string Line2Field14 { get; set; }
        public string Line2Field15 { get; set; }
        public string Line2Field16 { get; set; }
        public string Line2Field17 { get; set; }
        public string Line2Field18 { get; set; }
        public string Line2Field19 { get; set; }
        public string Line2Field20 { get; set; }

        public Directory(string Line1Field1, string Line1Field2, string Line1Field3, string Line1Field4, string Line1Field5, string Line1Field6, string Line1Field7,
            string Line1Field8, string Line1Field9, string Line1Field10, string Line2Field11, string Line2Field12, string Line2Field13, string Line2Field14, string Line2Field15,
            string Line2Field16, string Line2Field17, string Line2Field18, string Line2Field19, string Line2Field20)
        {
            this.Line1Field1 = Line1Field1;
            this.Line1Field2 = Line1Field2;
            this.Line1Field3 = Line1Field3;
            this.Line1Field4 = Line1Field4;
            this.Line1Field5 = Line1Field5;
            this.Line1Field6 = Line1Field6;
            this.Line1Field7 = Line1Field7;
            this.Line1Field8 = Line1Field8;
            this.Line1Field9 = Line1Field9;
            this.Line1Field10 = Line1Field10;
            this.Line2Field11 = Line2Field11;
            this.Line2Field12 = Line2Field12;
            this.Line2Field13 = Line2Field13;
            this.Line2Field14 = Line2Field14;
            this.Line2Field15 = Line2Field15;
            this.Line2Field16 = Line2Field16;
            this.Line2Field17 = Line2Field17;
            this.Line2Field18 = Line2Field18;
            this.Line2Field19 = Line2Field19;
            this.Line2Field20 = Line2Field20;
        }

    }

    public class Parameter
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }

        public Parameter(string Field1, string Field2, string Field3)
        {
            this.Field1 = Field1;
            this.Field2 = Field2;
            this.Field3 = Field3;
        }

    }


    public class BendLine
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double Z1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double Z2 { get; set; }

        public BendLine(double X1, double Y1, double Z1, double X2, double Y2, double Z2)
        {
            this.X1 = X1;
            this.Y1 = Y1;
            this.Z1 = Z1;
            this.X2 = X2;
            this.Y2 = Y2;
            this.Z2 = Z2;
        }
    }

    public class CircArc
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double CenterZ { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double StartZ { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
        public double EndZ { get; set; }

        public CircArc(double CenterX, double CenterY, double CenterZ, double StartX, double StartY, double StartZ, double EndX, double EndY, double EndZ)
        {
            this.CenterX = CenterX;
            this.CenterY = CenterY;
            this.CenterZ = CenterZ;
            this.StartX = StartX;
            this.StartY = StartY;
            this.StartZ = StartZ;
            this.EndX = EndX;
            this.EndY = EndY;
            this.EndZ = EndZ;
        }
    }

    public class RotationalMatrix
    {
        public double R11 { get; set; }
        public double R12 { get; set; }
        public double R13 { get; set; }
        public double T1 { get; set; }
        public double R21 { get; set; }
        public double R22 { get; set; }
        public double R23 { get; set; }
        public double T2 { get; set; }
        public double R31 { get; set; }
        public double R32 { get; set; }
        public double R33 { get; set; }
        public double T3 { get; set; }

        public RotationalMatrix(double R11, double R12, double R13, double T1, double R21, double R22, double R23, double T2, double R31, double R32, double R33, double T3)
        {
            this.R11 = R11;
            this.R12 = R12;
            this.R13 = R13;
            this.T1 = T1;
            this.R21 = R21;
            this.R22 = R22;
            this.R23 = R23;
            this.T2 = T2;
            this.R31 = R31;
            this.R32 = R32;
            this.R33 = R33;
            this.T3 = T3;
        }
    }

    public class HeadPosition
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double A { get; set; }
        public double B { get; set; }
    }


}




