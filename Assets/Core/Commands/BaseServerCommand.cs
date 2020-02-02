using Newtonsoft.Json;

namespace TankGame
{
    public class BaseServerCommand : TankGame.IServerCommand
    {
        public BaseServerCommand()
        {
        }
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string CommandName => GetType().Name.ToString();
    }
}
