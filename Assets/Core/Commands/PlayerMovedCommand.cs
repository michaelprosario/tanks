using Newtonsoft.Json;

namespace TankGame
{
  public class PlayerMovedCommand : BaseServerCommand
  {
    public PlayerMovedCommand()
    {
    }

    public string PlayerId { get; set; }
    public Position Position { get; set; }
    public Rotation Rotation { get; set; }
  }
}
