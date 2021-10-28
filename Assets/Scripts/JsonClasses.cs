using System.Collections.Generic;
namespace sc_t3
{
    [System.Serializable]
    public class Params
    {
        public int ViewerID;
        public string Signature;
    }

    [System.Serializable]
    public class Request
    {
        public bool subscribe;
        public bool bulkResult;
        public Params @params;
    }

    [System.Serializable]
    public class Doc
    {
        public List<double> map;
        public string _id;
        public int ViewerID;
        public int BotID;
        public int patchID;
        public int x;
        public int y;
    }
    [System.Serializable]
    public class Root
    {
        public Doc doc;
    }
}