using Assets.Core.Responses;
using TankGame;
using UnityEngine;

namespace Assets.Scenes
{
    public class SceneOneView : MonoBehaviour, ISceneOneView
    {
        public GameObject PlayerPrefab;
        public GameObject CurrentPlayer;

        public void HandleGetPlayersResponse(GetPlayersResponse response, string currentPlayer)
        {
            foreach (var player in response.Players)
            {
                if (player.PlayerId != currentPlayer)
                {
                    var playerGameObject = GameObject.Find(player.PlayerId);
                    if (playerGameObject == null)
                    {
                        instantiatePlayerPrefab(player);
                    }
                }
            }
        }

        public void HandlePlayerStarted(PlayerStartedCommand command)
        {
            var player = GameObject.Find(command.PlayerId);
            if (player == null)
            {
                var playerData = new Player
                {
                    PlayerId = command.PlayerId,
                    Position = command.Position,
                    Rotation = command.Rotation
                };
                instantiatePlayerPrefab(playerData);
            }
        }

        private void instantiatePlayerPrefab(Player player)
        {
            var position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z);
            var rotation = Quaternion.Euler(0, player.Rotation.Y, 0);
            var newPlayer = Instantiate(this.PlayerPrefab, position, rotation);
            newPlayer.name = player.PlayerId;
        }

        public PlayerStartedCommand GetNewPlayerCommand(string currentPlayerId)
        {
            var command = new TankGame.PlayerStartedCommand();
            command.PlayerId = currentPlayerId;

            var position = CurrentPlayer.transform.position;
            var rotation = CurrentPlayer.transform.rotation;
            command.Position = new Position(position.x, position.y, position.z);
            command.Rotation = new Rotation(0, rotation.y, 0);
            return command;
        }

        public PlayerMovedCommand GetPlayerMovedCommand(string currentPlayerId)
        {
            var command = new PlayerMovedCommand();
            command.PlayerId = currentPlayerId;
            var position = CurrentPlayer.transform.position;
            var rotation = CurrentPlayer.transform.rotation;
            command.Position = new Position(position.x, position.y, position.z);
            command.Rotation = new Rotation(0, rotation.y, 0);
            return command;
        }

        public void HandlePlayerMoved(PlayerMovedCommand command)
        {
            var position = new Vector3(command.Position.X, command.Position.Y, command.Position.Z);
            var rotation = Quaternion.Euler(0, command.Rotation.Y, 0);

            var player = GameObject.Find(command.PlayerId);
            player.transform.position = new Vector3(position.x, position.y, position.z);
            player.transform.rotation = rotation;
        }
    }
}
