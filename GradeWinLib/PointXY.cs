namespace GradeWinLib
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct PointXY
    {
        public int X;
        public int Y;
        
        public PointXY(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

}
