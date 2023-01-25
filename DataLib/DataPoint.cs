namespace DataLib
{
    public class DataPoint
    {
        public double XValue { get; set; }
        public double YValue { get; set; }

        public DataPoint(double x, double y)
        {
            XValue = x;
            YValue = y;
        }
    }
}