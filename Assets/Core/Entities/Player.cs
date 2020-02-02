using Newtonsoft.Json;

namespace TankGame
{
    public class Player 
    {
        public Player()
        {
        }

        public string PlayerId { get; set; }
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
    }
}
