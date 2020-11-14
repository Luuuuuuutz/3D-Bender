using HelixToolkit.Wpf;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3D_Bender
{

    public partial class UserControl1 : UserControl
    {

        public static string _dataTableString;
        public static string DataTableString
        {
            get
            {
                return _dataTableString;
            }
            set
            {
                _dataTableString = value;
            }
        }

        public static bool _units;
        public static bool Units
        {
            get
            {
                return _units;
            }
            set
            {
                _units = value;
            }
        }

        HelixViewport3D hVp3D = new HelixViewport3D();
        PerspectiveCamera camera = new PerspectiveCamera();
        Matrix matrix = new Matrix();

        double[,] R = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };  //Initialize coordinate converter R made of columns of x y z vectors


        public UserControl1()
        {
            InitializeComponent();
            Create3DViewPort();
        }

        private void Create3DViewPort()
        {
            var lights = new DefaultLights();

            hVp3D.PanGesture = new MouseGesture(MouseAction.LeftClick);
            hVp3D.RotateGesture = new MouseGesture(MouseAction.MiddleClick);

            camera.Position = new Point3D(100, 50, 200);
            camera.LookDirection = new Vector3D(-100, 0, -100);
            camera.UpDirection = new Vector3D(0, 1, 0);
            camera.FieldOfView = 60;

            hVp3D.Camera = camera;

            hVp3D.Children.Add(lights);

            this.AddChild(hVp3D);

        }

        public void CreateTube(Point3DCollection path)
        {
            double diameter = 0;

            if (Units == true)  //Metric
            {
                diameter = 25.4 * 0.125;
                //Update camera position so it is zoomed in enough to see
                camera.Position = new Point3D(100, 50, 200);
                camera.LookDirection = new Vector3D(-100, 0, -100);
                camera.UpDirection = new Vector3D(0, 1, 0);
                camera.FieldOfView = 60;
            }

            if (Units == false) //Inches
            {
                diameter = 0.125;
                //Update camera position so it is zoomed in enough to see
                camera.Position = new Point3D(10, 5, 20);
                camera.LookDirection = new Vector3D(-10, 0, -10);
                camera.UpDirection = new Vector3D(0, 1, 0);
                camera.FieldOfView = 60;
            }

            var tube = new TubeVisual3D();

            tube.Path = path;
            tube.Diameter = diameter;
            tube.IsPathClosed = false;
            tube.Fill = Brushes.Gray;

            hVp3D.Children.Add(tube);

        }

        public Point3DCollection ComputePath()
        {

            Point3DCollection path = new Point3DCollection();

            try
            {
                double section;
                string radiusFull;
                double tiltAngleZ = 0;
                double feed;

                Point3D startPoint = new Point3D(0, 0, 0);  //Initialize the starting points to 0
                Point3D endPoint = new Point3D(0, 0, 0);

                string[] splitRow = DataTableString.Split(';'); //The string contains the data by separating rows with semicolons
                int rowCount = splitRow.Length;

                //Before we start, we add the point 0,0,0 because that is where we start
                path.Add(new Point3D(0, 0, 0));

                for (int i = 0; i < rowCount; i++)  //Loop through the data table which holds the bend profile info
                {
                    string[] splitCol = splitRow[i].Split(','); //Each cell is separated by a comma
                    if (!String.IsNullOrEmpty(splitCol[0]))    //Make sure we don't try to read a null character
                    {
                        section = Double.Parse(splitCol[0]);
                        radiusFull = splitCol[1];
                        tiltAngleZ = Double.Parse(splitCol[2]);  //Previous tilt minus current tilt
                        feed = Double.Parse(splitCol[3]);

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///Line
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        if (radiusFull == "0")
                        {
                            //If the radius is 0 then it must be a straight line
                            //To find the end point of the line, simply convert the starting point to the xyz coordinate system and add the length to the z axis then convert back to real coordinates
                            double[,] Rp = new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; //Initialize inverse matrix

                            matrix.Invert(Rp, R);

                            Point3D startPointP = new Point3D(0, 0, 0);

                            startPointP = matrix.Multiply3x1P(Rp, startPoint); //Find start point'
                            startPointP.Z = startPointP.Z + feed; //add length to z' axis
                            endPoint = matrix.Multiply3x1P(R, startPointP);

                            path.Add(endPoint);    //Add the new point

                            startPoint = endPoint;   //The last point found is the actual end point of the line
                        }

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///Circular Arc
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        if (radiusFull != "0" && !radiusFull.Contains("|"))
                        {

                            double radius = Convert.ToDouble(radiusFull);

                            //If the radius is not 0 then it must be a circular arc
                            double sweepAngle = Math.Abs((feed / radius) * 180 / Math.PI);   //Sweep angle in radians = arc length / radius

                            if (tiltAngleZ == 0)    //if the tilt is 0 then we just make it very close to 0 for computational purposes
                            {
                                tiltAngleZ = 0.0001;
                            }

                            //Now we have to convert all the points to the x'y'z' coordinates
                            Point3D startPointP = new Point3D(0, 0, 0);
                            Point3D centerPoint = new Point3D(0, 0, 0);
                            Point3D centerPointP = new Point3D(0, 0, 0);

                            double[,] Rp = new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; //Initialize inverse matrix

                            matrix.Invert(Rp, R);
                            startPointP = matrix.Multiply3x1P(Rp, startPoint); //Find start point in x'y'z'
                            centerPointP = RotateZ(startPointP, (-1) * tiltAngleZ);
                            centerPointP.Y = centerPointP.Y + radius;   //We rotated the points so they are point in the -z direction
                            centerPointP = RotateZ(centerPointP, tiltAngleZ);

                            centerPoint = matrix.Multiply3x1P(R, centerPointP);

                            //Then we use the center point, rotate it on the Y-Z plane and add the radius

                            int repeat = 100;   //Get 100 points on the circle
                            Point3D endPointP = new Point3D(0, 0, 0);  //Initialize endPointP

                            //Since the path is a bunch of points, we compute the "end points" for a range of feed lengths, aka points along the curve until we reach the end
                            for (int j = 1; j <= repeat; j++)
                            {
                                double smallArc = (sweepAngle * j) / repeat;

                                endPointP = RotateZ(centerPointP, (-1) * tiltAngleZ);
                                endPointP = RotateX(endPointP, smallArc);
                                endPointP.Y = endPointP.Y - Math.Abs(radius);
                                endPointP = RotateX(endPointP, (-1) * smallArc);
                                endPointP = RotateZ(endPointP, tiltAngleZ);

                                //We need to convert the points to xyz coordinates before adding the point to the collection
                                endPoint = matrix.Multiply3x1P(R, endPointP); //Shift back to xyz from x'y'z'

                                path.Add(endPoint);    //Add the new point
                            }

                            ///////////////////////////////////////////////////////////////

                            //Now we have to find the new transformation matrix R

                            double intersectHeight = 0;
                            bool flipY = false;  //Flip the axes if the angle is larger than 90

                            Point3D planeVectorY = new Point3D(0, 0, 0);

                            if (tiltAngleZ > 0 && tiltAngleZ <= 90)  //Between 0 and 90
                            {
                                intersectHeight = radius / Math.Cos(Math.Abs(tiltAngleZ) * Math.PI / 180);
                                flipY = false;
                            }

                            if (tiltAngleZ < 0 && tiltAngleZ > -90) //Between 0 and -90
                            {
                                intersectHeight = radius / Math.Cos(Math.Abs(tiltAngleZ) * Math.PI / 180);
                                flipY = true;
                            }

                            if (tiltAngleZ > 90 && tiltAngleZ < 180)    //Between 90 and 180
                            {
                                intersectHeight = radius / Math.Cos((Math.Abs(tiltAngleZ)) * Math.PI / 180);
                                flipY = true;
                            }
                            if (tiltAngleZ < -90 && tiltAngleZ > -180)  //Between -90 and -180
                            {
                                intersectHeight = radius / Math.Cos((Math.Abs(tiltAngleZ)) * Math.PI / 180);
                                flipY = false;
                            }

                            Point3D intersectPointP = new Point3D(startPointP.X, intersectHeight + startPointP.Y, startPointP.Z);

                            planeVectorY = (Point3D)Point3D.Subtract(intersectPointP, endPointP);

                            if (flipY)
                            {
                                planeVectorY.X = planeVectorY.X * -1;
                                planeVectorY.Y = planeVectorY.Y * -1;
                                planeVectorY.Z = planeVectorY.Z * -1;
                            }
                            planeVectorY = matrix.Multiply3x1P(R, planeVectorY);
                            planeVectorY = matrix.UnitVectorP(planeVectorY);


                            Point3D planeVectorC = (Point3D)Point3D.Subtract(centerPoint, endPoint);
                            planeVectorC = matrix.UnitVectorP(planeVectorC);

                            Point3D planeVectorZ = matrix.CrossProductP(planeVectorY, planeVectorC);  //Find the z axis direction vector
                            planeVectorZ = matrix.UnitVectorP(planeVectorZ);

                            Point3D planeVectorX = matrix.CrossProductP(planeVectorY, planeVectorZ);  //Find the x axis direction vector
                            planeVectorX = matrix.UnitVectorP(planeVectorX);


                            //Now put the vectors where they actually belong with the transformation matrix R

                            //Now set the new transformation matrix R
                            R[0, 0] = planeVectorX.X;
                            R[1, 0] = planeVectorX.Y;
                            R[2, 0] = planeVectorX.Z;

                            R[0, 1] = planeVectorY.X;
                            R[1, 1] = planeVectorY.Y;
                            R[2, 1] = planeVectorY.Z;

                            R[0, 2] = planeVectorZ.X;
                            R[1, 2] = planeVectorZ.Y;
                            R[2, 2] = planeVectorZ.Z;

                            startPoint = endPoint;
                        }

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ///Helix
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        if (radiusFull != "0" && radiusFull.Contains("|"))
                        {
                            //This is the case where we have a helix with the number of rotations R|N
                            int pos = radiusFull.IndexOf("|");
                            double radius = Convert.ToDouble(radiusFull.Remove(pos));
                            double pitch = Convert.ToDouble(radiusFull.Remove(0, pos + 1));

                            //Basically loop through the circle method
                            int N = 100;
                            double lengthPerRotation = Math.Sqrt(Math.Pow(pitch, 2) + Math.Pow(2 * Math.PI * radius, 2));
                            double turns = (feed / lengthPerRotation);
                            double height = turns * pitch;

                            double tiltTotal = 2 * Math.PI * Math.Atan2(height, 2 * Math.PI * radius) * 180 / Math.PI;

                            for (int k = 1; k <= N; k++)
                            {
                                double inProgFeed = feed / N; //Divide up the section into 100 sections

                                double inProgTiltAngleZ = (k * tiltTotal / N) + tiltAngleZ;

                                //If the radius is not 0 then it must be a circular arc
                                double sweepAngle = Math.Abs((inProgFeed / radius) * 180 / Math.PI);   //Sweep angle in radians = arc length / radius

                                if (tiltAngleZ == 0)    //if the tilt is 0 then we just make it very close to 0 for computational purposes
                                {
                                    tiltAngleZ = 0.0001;
                                }

                                //Now we have to convert all the points to the x'y'z' coordinates
                                Point3D startPointP = new Point3D(0, 0, 0);
                                Point3D centerPoint = new Point3D(0, 0, 0);
                                Point3D centerPointP = new Point3D(0, 0, 0);

                                double[,] Rp = new double[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; //Initialize inverse matrix

                                matrix.Invert(Rp, R);
                                startPointP = matrix.Multiply3x1P(Rp, startPoint); //Find start point in x'y'z'
                                centerPointP = RotateZ(startPointP, (-1) * inProgTiltAngleZ);
                                centerPointP.Y = centerPointP.Y + radius;   //We rotated the points so they are point in the -z direction
                                centerPointP = RotateZ(centerPointP, inProgTiltAngleZ);

                                centerPoint = matrix.Multiply3x1P(R, centerPointP);

                                //Then we use the center point, rotate it on the Y-Z plane and add the radius

                                int repeat = 100;   //Get 100 points on the circle
                                Point3D endPointP = new Point3D(0, 0, 0);  //Initialize endPointP

                                //Since the path is a bunch of points, we compute the "end points" for a range of feed lengths, aka points along the curve until we reach the end
                                for (int j = 1; j <= repeat; j++)
                                {
                                    double smallArc = (sweepAngle * j) / repeat;

                                    endPointP = RotateZ(centerPointP, (-1) * inProgTiltAngleZ);
                                    endPointP = RotateX(endPointP, smallArc);
                                    endPointP.Y = endPointP.Y - Math.Abs(radius);
                                    endPointP = RotateX(endPointP, (-1) * smallArc);
                                    endPointP = RotateZ(endPointP, inProgTiltAngleZ);

                                    //We need to convert the points to xyz coordinates before adding the point to the collection
                                    endPoint = matrix.Multiply3x1P(R, endPointP); //Shift back to xyz from x'y'z'

                                    path.Add(endPoint);    //Add the new point
                                }

                                ///////////////////////////////////////////////////////////////

                                //Now we have to find the new transformation matrix R

                                double intersectHeight = 0;
                                bool flipY = false;  //Flip the axes if the angle is larger than 90

                                Point3D planeVectorY = new Point3D(0, 0, 0);

                                if (inProgTiltAngleZ > 0 && inProgTiltAngleZ <= 90)  //Between 0 and 90
                                {
                                    intersectHeight = radius / Math.Cos(Math.Abs(inProgTiltAngleZ) * Math.PI / 180);
                                    flipY = false;
                                }

                                if (inProgTiltAngleZ < 0 && inProgTiltAngleZ > -90) //Between 0 and -90
                                {
                                    intersectHeight = radius / Math.Cos(Math.Abs(inProgTiltAngleZ) * Math.PI / 180);
                                    flipY = true;
                                }

                                if (inProgTiltAngleZ > 90 && inProgTiltAngleZ < 180)    //Between 90 and 180
                                {
                                    intersectHeight = radius / Math.Cos((Math.Abs(inProgTiltAngleZ)) * Math.PI / 180);
                                    flipY = true;
                                }
                                if (inProgTiltAngleZ < -90 && inProgTiltAngleZ > -180)  //Between -90 and -180
                                {
                                    intersectHeight = radius / Math.Cos((Math.Abs(inProgTiltAngleZ)) * Math.PI / 180);
                                    flipY = false;
                                }

                                Point3D intersectPointP = new Point3D(startPointP.X, intersectHeight + startPointP.Y, startPointP.Z);

                                planeVectorY = (Point3D)Point3D.Subtract(intersectPointP, endPointP);

                                if (flipY)
                                {
                                    planeVectorY.X = planeVectorY.X * -1;
                                    planeVectorY.Y = planeVectorY.Y * -1;
                                    planeVectorY.Z = planeVectorY.Z * -1;
                                }
                                planeVectorY = matrix.Multiply3x1P(R, planeVectorY);
                                planeVectorY = matrix.UnitVectorP(planeVectorY);


                                Point3D planeVectorC = (Point3D)Point3D.Subtract(centerPoint, endPoint);
                                planeVectorC = matrix.UnitVectorP(planeVectorC);

                                Point3D planeVectorZ = matrix.CrossProductP(planeVectorY, planeVectorC);  //Find the z axis direction vector
                                planeVectorZ = matrix.UnitVectorP(planeVectorZ);

                                Point3D planeVectorX = matrix.CrossProductP(planeVectorY, planeVectorZ);  //Find the x axis direction vector
                                planeVectorX = matrix.UnitVectorP(planeVectorX);


                                //Now put the vectors where they actually belong with the transformation matrix R

                                //Now set the new transformation matrix R
                                R[0, 0] = planeVectorX.X;
                                R[1, 0] = planeVectorX.Y;
                                R[2, 0] = planeVectorX.Z;

                                R[0, 1] = planeVectorY.X;
                                R[1, 1] = planeVectorY.Y;
                                R[2, 1] = planeVectorY.Z;

                                R[0, 2] = planeVectorZ.X;
                                R[1, 2] = planeVectorZ.Y;
                                R[2, 2] = planeVectorZ.Z;

                                startPoint = endPoint;
                            }
                        }

                    }

                }

                return path;
            }
            catch
            {
                return path;
            }
        }

        public Point3D RotateX(Point3D origPoint, double angle)
        {
            angle = angle * Math.PI / 180;  //Change degrees to radians
            double[,] RM = new double[3, 3] { { 1, 0, 0 }, { 0, Math.Cos(angle), -Math.Sin(angle) }, { 0, Math.Sin(angle), Math.Cos(angle) } };

            Point3D output = new Point3D();

            output.X = (origPoint.X * RM[0, 0]) + (origPoint.Y * RM[0, 1]) + (origPoint.Z * RM[0, 2]);
            output.Y = (origPoint.X * RM[1, 0]) + (origPoint.Y * RM[1, 1]) + (origPoint.Z * RM[1, 2]);
            output.Z = (origPoint.X * RM[2, 0]) + (origPoint.Y * RM[2, 1]) + (origPoint.Z * RM[2, 2]);

            return output;
        }

        public Point3D RotateY(Point3D origPoint, double angle)
        {
            angle = angle * Math.PI / 180;  //Change degrees to radians
            double[,] RM = new double[3, 3] { { Math.Cos(angle), 0, Math.Sin(angle) }, { 0, 1, 0 }, { -Math.Sin(angle), 0, Math.Cos(angle) } };

            Point3D output = new Point3D();

            output.X = (origPoint.X * RM[0, 0]) + (origPoint.Y * RM[0, 1]) + (origPoint.Z * RM[0, 2]);
            output.Y = (origPoint.X * RM[1, 0]) + (origPoint.Y * RM[1, 1]) + (origPoint.Z * RM[1, 2]);
            output.Z = (origPoint.X * RM[2, 0]) + (origPoint.Y * RM[2, 1]) + (origPoint.Z * RM[2, 2]);

            return output;
        }

        public Point3D RotateZ(Point3D origPoint, double angle)
        {
            angle = angle * Math.PI / 180;  //Change degrees to radians
            double[,] RM = new double[3, 3] { { Math.Cos(angle), -Math.Sin(angle), 0 }, { Math.Sin(angle), Math.Cos(angle), 0 }, { 0, 0, 1 } };

            Point3D output = new Point3D();

            output.X = (origPoint.X * RM[0, 0]) + (origPoint.Y * RM[0, 1]) + (origPoint.Z * RM[0, 2]);
            output.Y = (origPoint.X * RM[1, 0]) + (origPoint.Y * RM[1, 1]) + (origPoint.Z * RM[1, 2]);
            output.Z = (origPoint.X * RM[2, 0]) + (origPoint.Y * RM[2, 1]) + (origPoint.Z * RM[2, 2]);

            return output;
        }

        public void UpdateData()
        {
            R[0, 0] = 1;
            R[1, 0] = 0;
            R[2, 0] = 0;

            R[0, 1] = 0;
            R[1, 1] = 1;
            R[2, 1] = 0;

            R[0, 2] = 0;
            R[1, 2] = 0;
            R[2, 2] = 1;

            hVp3D.Children.Clear();

            var lights = new DefaultLights();
            hVp3D.Children.Add(lights);
            try
            {
                Point3DCollection path = ComputePath();
                CreateTube(path);
            }
            catch { }

        }

        private void resetCameraButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
