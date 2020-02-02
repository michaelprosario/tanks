using Assets.Core.Responses;
using Assets.Scenes;
using Newtonsoft.Json;
using SocketIO;
using System.Collections;
using UnityEngine;

namespace TankGame
{
    public class SceneOneSocketController : MonoBehaviour
    {
        private SocketIOComponent socket;
        private string currentPlayerId;
        private ISceneOneView view;

        public void Start()
        {
            currentPlayerId = System.Guid.NewGuid().ToString();

            GameObject go = GameObject.Find("SocketIO");
            socket = go.GetComponent<SocketIOComponent>();

            GameObject viewGameObject = GameObject.Find("View");
            view = viewGameObject.GetComponent<SceneOneView>();

            socket.On("open", HandleOpen);
            socket.On("error", HandleError);
            socket.On("close", HandleClose);
            socket.On("PlayerStartedEvent", HandlePlayerStartedEvent);
            socket.On("PlayerMovedEvent", HandlePlayerMovedEvent);
            socket.On("GetPlayersResponse", HandleGetPlayersResponse);

            StartCoroutine("SendPlayerStarted");
        }

        private void HandleGetPlayersResponse(SocketIOEvent e)
        {
            var jsonString = e.data.ToString();
            var response = JsonConvert.DeserializeObject<GetPlayersResponse>(jsonString);
            view.HandleGetPlayersResponse(response, currentPlayerId);
        }

        private void HandlePlayerStartedEvent(SocketIOEvent e)
        {
            var jsonString = e.data.ToString();
            Debug.Log(jsonString);
            var command = JsonConvert.DeserializeObject<PlayerStartedCommand>(jsonString);
            if (command.PlayerId == this.currentPlayerId)
                return;

            view.HandlePlayerStarted(command);
        }

        private void HandlePlayerMovedEvent(SocketIOEvent e)
        {
            var jsonString = e.data.ToString();
            //Debug.Log(jsonString);
            var command = JsonConvert.DeserializeObject<PlayerMovedCommand>(jsonString);
            if (command.PlayerId == this.currentPlayerId)
                return;
            view.HandlePlayerMoved(command);
        }

        public void Update()
        {
            sendCommand(view.GetPlayerMovedCommand(this.currentPlayerId));
        }

        private IEnumerator SendPlayerStarted()
        {
            yield return new WaitForSeconds(1);
            sendCommand(view.GetNewPlayerCommand(this.currentPlayerId));
            sendQuery(new GetPlayersQuery());
        }

        private void sendCommand(IServerCommand command)
        {
            socket.Emit(command.GetType().Name, new JSONObject(command.ToJsonString()));
        }

        private void sendQuery(IServerQuery query)
        {
            socket.Emit(query.GetType().Name, new JSONObject(query.ToJsonString()));
        }

        public void HandleOpen(SocketIOEvent e)
        {
            Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        }

        public void HandleError(SocketIOEvent e)
        {
            Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
        }

        public void HandleClose(SocketIOEvent e)
        {
            Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
        }
    }

}