using System.Collections;
using UnityEngine;
namespace sc_t3
{
    public class SceneControl : MonoBehaviour
    {
        
        private WssClient wssClient;

        private MeshGen meshGen;

       
        private IEnumerator Start()
        {
            wssClient = GetComponent<WssClient>();
            meshGen = new MeshGen(256);
            yield return new WaitUntil(()=>wssClient.IsSocketOpen);
            wssClient.SendRequest(WssClient_OnMessage);
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