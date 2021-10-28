using NativeWebSocket;
using System;
using UnityEngine;

namespace sc_t3
{
    public class WssClient : MonoBehaviour
    {
        public event Action<Doc> OnMessage;
        
        private WebSocket websocket;

        [SerializeField]
        private string url= "wss://storage2.speedlight.io/land_patches";
        [SerializeField]
        private bool bulkResult = false;
        [SerializeField]
        private bool subscribe = true;
        [SerializeField]
        private int ViewerID = 5678;
        [SerializeField]
        private string Signature = "4d713fc31dd26bd902ad9d753326a9b111fe0747f3dabdae535e981258fe1d4f";


        public void SendRequest()
        {
            Request req = new Request();
            req.bulkResult = bulkResult;
            req.subscribe = subscribe;
            req.@params = new Params();
            req.@params.ViewerID = ViewerID;
            req.@params.Signature = Signature;
            SendWebSocketMessage(JsonUtility.ToJson(req));
        }

        async void Start()
        {
            websocket = new WebSocket(url);

            websocket.OnOpen += () =>
            {
                Debug.Log("Connection open!");
                SendRequest();
               
            };

            websocket.OnError += (e) =>
            {
                Debug.Log("Error! " + e);
            };

            websocket.OnClose += (e) =>
            {
                Debug.Log("Connection closed!");
            };

            websocket.OnMessage += (bytes) =>
            {
                string message = System.Text.Encoding.UTF8.GetString(bytes);
                Root j = JsonUtility.FromJson<Root>(message);
                OnMessage?.Invoke(j.doc);
               
            };
          
            await websocket.Connect();
        }

        void Update()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
#endif
           
        }
        async void SendWebSocketMessage( string msg)
        {
            if (websocket.State == WebSocketState.Open)
            {
               await websocket.SendText(msg);
            }
        }

        private async void OnApplicationQuit()
        {
            await websocket.Close();
        }
    }
}