namespace Core
{
    public readonly struct Point
    {
        public readonly int rowIndex;
        public readonly int columnIndex;

        public static Point undefined => new(-1, -1);

        public bool isUndefined => Equal(undefined);

        public Point(int row, int column)
        {
            rowIndex = row;
            columnIndex = column;
        }

        public bool Equal(Point other)
        {
            return rowIndex == other.rowIndex && columnIndex == other.columnIndex;
        }

        public override string ToString()
        {
            return $"Row:{rowIndex}, Column:{columnIndex}";
        }
    }
}