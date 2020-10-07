using System;
using System.Windows.Media.Media3D;

namespace _3D_Bender
{
    class Matrix
    {

        public void UnitVector(double[] B, double[] A)
        {
            double Aa, Ab, Ac;
            Aa = A[0];
            Ab = A[1];
            Ac = A[2];

            double mag = Math.Sqrt((Aa * Aa) + (Ab * Ab) + (Ac * Ac));

            B[0] = Aa / mag;
            B[1] = Ab / mag;
            B[2] = Ac / mag;
        }

        public Point3D UnitVectorP(Point3D A)
        {
            double Aa, Ab, Ac;
            Aa = A.X;
            Ab = A.Y;
            Ac = A.Z;

            double mag = Math.Sqrt((Aa * Aa) + (Ab * Ab) + (Ac * Ac));

            Point3D B = new Point3D();
            B.X = Aa / mag;
            B.Y = Ab / mag;
            B.Z = Ac / mag;
            return B;
        }

        public void Multiply3x1(double[] C, double[,] A, double[] B)
        {
            double Aa, Ab, Ac, Ad, Ae, Af, Ag, Ah, Ak;
            Aa = A[0, 0];
            Ab = A[0, 1];
            Ac = A[0, 2];
            Ad = A[1, 0];
            Ae = A[1, 1];
            Af = A[1, 2];
            Ag = A[2, 0];
            Ah = A[2, 1];
            Ak = A[2, 2];

            double Ba, Bb, Bc;
            Ba = B[0];
            Bb = B[1];
            Bc = B[2];

            C[0] = (Aa * Ba) + (Ab * Bb) + (Ac * Bc);
            C[1] = (Ad * Ba) + (Ae * Bb) + (Af * Bc);
            C[2] = (Ag * Ba) + (Ah * Bb) + (Ak * Bc);

        }

        public Point3D Multiply3x1P(double[,] A, Point3D B)
        {
            double Aa, Ab, Ac, Ad, Ae, Af, Ag, Ah, Ak;
            Aa = A[0, 0];
            Ab = A[0, 1];
            Ac = A[0, 2];
            Ad = A[1, 0];
            Ae = A[1, 1];
            Af = A[1, 2];
            Ag = A[2, 0];
            Ah = A[2, 1];
            Ak = A[2, 2];

            double Ba, Bb, Bc;
            Ba = B.X;
            Bb = B.Y;
            Bc = B.Z;

            Point3D C = new Point3D();
            C.X = (Aa * Ba) + (Ab * Bb) + (Ac * Bc);
            C.Y = (Ad * Ba) + (Ae * Bb) + (Af * Bc);
            C.Z = (Ag * Ba) + (Ah * Bb) + (Ak * Bc);

            return C;
        }

        public void Multiply3x3(double[,] Cc, double[,] A, double[,] B)
        {
            double Aa, Ab, Ac, Ad, Ae, Af, Ag, Ah, Ak;
            Aa = A[0, 0];
            Ab = A[0, 1];
            Ac = A[0, 2];
            Ad = A[1, 0];
            Ae = A[1, 1];
            Af = A[1, 2];
            Ag = A[2, 0];
            Ah = A[2, 1];
            Ak = A[2, 2];

            double Ba, Bb, Bc, Bd, Be, Bf, Bg, Bh, Bk;
            Ba = B[0, 0];
            Bb = B[0, 1];
            Bc = B[0, 2];
            Bd = B[1, 0];
            Be = B[1, 1];
            Bf = B[1, 2];
            Bg = B[2, 0];
            Bh = B[2, 1];
            Bk = B[2, 2];

            Cc[0, 0] = (Aa * Ba) + (Ab * Bd) + (Ac * Bg);
            Cc[0, 1] = (Aa * Bb) + (Ab * Be) + (Ac * Bh);
            Cc[0, 2] = (Aa * Bc) + (Ab * Bf) + (Ac * Bk);
            Cc[1, 0] = (Ad * Ba) + (Ae * Bd) + (Af * Bg);
            Cc[1, 1] = (Ad * Bb) + (Ae * Be) + (Af * Bh);
            Cc[1, 2] = (Ad * Bc) + (Ae * Bf) + (Af * Bk);
            Cc[2, 0] = (Ag * Ba) + (Ah * Bd) + (Ak * Bg);
            Cc[2, 1] = (Ag * Bb) + (Ah * Be) + (Ak * Bh);
            Cc[2, 2] = (Ag * Bc) + (Ah * Bf) + (Ak * Bk);

        }

        public void Invert(double[,] Rp, double[,] R)
        {
            //R is input matrix
            //Rp is "R prime" or inv(R)

            //     | a b c |
            // R = | d e f |
            //     | g h k |

            double a, b, c, d, e, f, g, h, k;
            a = R[0, 0];
            b = R[0, 1];
            c = R[0, 2];
            d = R[1, 0];
            e = R[1, 1];
            f = R[1, 2];
            g = R[2, 0];
            h = R[2, 1];
            k = R[2, 2];

            double det;
            det = (a * ((e * k) - (f * h))) - (b * ((d * k) - (f * g))) + (c * ((d * h) - (e * g)));

            Rp[0, 0] = ((e * k) - (f * h)) / det;
            Rp[0, 1] = (-(b * k) + (c * h)) / det;
            Rp[0, 2] = ((b * f) - (c * e)) / det;
            Rp[1, 0] = (-(d * k) + (f * g)) / det;
            Rp[1, 1] = ((a * k) - (c * g)) / det;
            Rp[1, 2] = (-(a * f) + (c * d)) / det;
            Rp[2, 0] = ((d * h) - (e * g)) / det;
            Rp[2, 1] = (-(a * h) + (b * g)) / det;
            Rp[2, 2] = ((a * e) - (b * d)) / det;

        }

        public void CrossProduct(double[] C, double[] A, double[] B)
        {
            //A x B = C
            C[0] = (A[1] * B[2]) - (A[2] * B[1]);     //(a2 b3) - (a3 b2) i
            C[1] = (-A[0] * B[2]) + (A[2] * B[0]);      //-(a1 b3) + (a3 b1) j
            C[2] = (A[0] * B[1]) - (A[1] * B[0]);      //(a1 b2) - (a2 b1) k
        }

        public Point3D CrossProductP(Point3D A, Point3D B)
        {
            //A x B = C
            Point3D C = new Point3D();
            C.X = (A.Y * B.Z) - (A.Z * B.Y);     //(a2 b3) - (a3 b2) i
            C.Y = (-A.X * B.Z) + (A.Z * B.X);      //-(a1 b3) + (a3 b1) j
            C.Z = (A.X * B.Y) - (A.Y * B.X);      //(a1 b2) - (a2 b1) k

            return C;
        }

    }
}
