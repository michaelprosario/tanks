using Newtonsoft.Json;

namespace TankGame
{
  public class PlayerStartedCommand : BaseServerCommand
  {
    public PlayerStartedCommand()
    {
    }

    public string PlayerId { get; set; }
    public Position Position { get; set; }
    public Rotation Rotation { get; set; }
  }
}
