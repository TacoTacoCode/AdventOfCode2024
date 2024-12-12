using System.Numerics;

namespace Day6;

public class CellData
{
    public char CellValue { get; set; }
    public Direction Direction { get; set; }

    public CellData(char cellValue) : this(cellValue, Direction.None) { }

    public CellData(char cellValue, Direction direction)
    {
        CellValue = cellValue;
        Direction = direction;
    }

    public static CellData Clone(CellData data)
    {
        return new CellData(data.CellValue, data.Direction);
    }
}