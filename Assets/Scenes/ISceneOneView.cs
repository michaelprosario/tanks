using Assets.Core.Responses;
using TankGame;

namespace Assets.Scenes
{
    public interface ISceneOneView
    {
        void HandleGetPlayersResponse(GetPlayersResponse response, string currentPlayer);
        PlayerMovedCommand GetPlayerMovedCommand(string currentPlayerId);
        PlayerStartedCommand GetNewPlayerCommand(string currentPlayerId);
        void HandlePlayerStarted(PlayerStartedCommand command);
        void HandlePlayerMoved(PlayerMovedCommand command);
    }
}