namespace TankGame
{
  public class Position
  {
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public Position(float x, float y, float z)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }
  }
}