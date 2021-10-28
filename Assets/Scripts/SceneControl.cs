using UnityEngine;
namespace sc_t3
{
    public class SceneControl : MonoBehaviour
    {
        
        private WssClient wssClient;

        private MeshGen meshGen;

        void Awake()
        {
            wssClient=GetComponent<WssClient>();
            meshGen = new MeshGen(256);
        }
        private void OnEnable()
        {
            wssClient.OnMessage += WssClient_OnMessage;
        }
        private void OnDisable()
        {
            wssClient.OnMessage -= WssClient_OnMessage;
        }
        private void WssClient_OnMessage(Doc doc)
        {
            int mapLenSqrt = 16;
            if (doc.map.Count != mapLenSqrt * mapLenSqrt)
                return;
            for (int i = 0; i < mapLenSqrt; i++)
            {
                for (int j = 0; j < mapLenSqrt; j++)
                {
                    meshGen.SetVertice(doc.y * mapLenSqrt + i,
                        doc.x * mapLenSqrt + j,
                        doc.map[i * mapLenSqrt + j]);
                }
            }
            if (doc.x == mapLenSqrt - 1 && doc.y == mapLenSqrt - 1)
            {
                meshGen.CreateMesh();
            }
        }

    }
}