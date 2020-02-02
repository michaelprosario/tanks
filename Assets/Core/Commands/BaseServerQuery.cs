using Newtonsoft.Json;

namespace TankGame
{
  public class BaseServerQuery : TankGame.IServerQuery
  {
    public BaseServerQuery()
    {
    }
    public string ToJsonString()
    {
      return JsonConvert.SerializeObject(this);
    }

    public string QueryName => GetType().Name.ToString();
  }
}
