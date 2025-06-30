using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
public class InGameLoad : MonoBehaviourPunCallbacks
{
    private static Hashtable propHash = new Hashtable();
    public GameObject LoadObject;
    public EraserClone eraserClone;
    public CameraWork cameraWork;
    private bool a = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!a)
        {
            propHash["Load" + "" + PlayerPrefs.GetInt("Dnumber").ToString()] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
            propHash.Clear();
            a = true;
        }
        bool inScene = true;
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            bool load = (PhotonNetwork.CurrentRoom.CustomProperties["Load" + "" + i.ToString()] is bool p) ? p : false;
            inScene = inScene && load;
        }
        LoadObject.SetActive(!inScene);

        if (inScene)
        {
            eraserClone.Clone();
            cameraWork.AnimationActive(true);
            Destroy(this.gameObject.GetComponent<InGameLoad>());
        }
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        

        bool inScene = true;
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            bool load = (PhotonNetwork.CurrentRoom.CustomProperties["Load" + "" + i.ToString()] is bool p) ? p : false;
            inScene = inScene && load;
        }
        LoadObject.SetActive(!inScene);

        if (inScene)
        {
            eraserClone.Clone();
            cameraWork.AnimationActive(true);
            Destroy(this.gameObject.GetComponent<InGameLoad>());

        }
    }
}
