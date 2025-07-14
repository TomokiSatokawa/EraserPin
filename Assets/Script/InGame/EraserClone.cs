using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif
public class EraserClone : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public class ClonePosition
    {
        public int playerCount;
        public List<GameObject> positionObject = new List<GameObject>();
    }
    [SerializeField] public List<ClonePosition> positionList = new List<ClonePosition>();
    public GameObject eraserPrefab;
    public ColorData colorData;
    public CharacterDataList characterDataList;
    public TextMeshProUGUI textMeshProUGUI;
    public List<GameObject> cloneEraserObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void Awake()
    {
#if UNITY_EDITOR
        try
        {
            int a = PhotonNetwork.CurrentRoom.PlayerCount;
        }
        catch
        {
        SceneManager.LoadScene("Start");

        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("AllPlayer") == 0)
        {
            int playerCount = 0;
            for (int a = 1; a <= PhotonNetwork.CurrentRoom.PlayerCount; a++)
            {
                playerCount += (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + a] is int op) ? op : 0;
                playerCount += (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + a] is int oc) ? oc : 0;
            }
            PlayerPrefs.SetInt("AllPlayer", playerCount);
        }
        if (PlayerPrefs.GetInt("Dnumber") == 1 || cloneEraserObjects.Count == PlayerPrefs.GetInt("AllPlayer"))
        {
            return;

        }
        Debug.Log("ColorChange");
        int i = 0;
        foreach (GameObject eraser in GameObject.FindGameObjectsWithTag("Eraser")) 
        {//名前の最後をループカウンターに
            eraser.GetComponent<EraserControlBase>().ChangeColor(colorData.activeColorPackage[i]);
            cloneEraserObjects.Add(eraser);
            i++;
        }
    }
    public ClonePosition SwitchPlayerPosition()
    {
        int playerCount = Totalling();
        foreach(ClonePosition clonePosition in positionList)
        {
            if(clonePosition.playerCount == playerCount)
            {
               return clonePosition;
            }
        }
        textMeshProUGUI.text += "PlayerPosition not found. Number of players tried to find:" + playerCount;
        Debug.LogError("PlayerPosition not found. Number of players tried to find:" + playerCount);
        return null;
    }
    public void Clone()
    {

        if (PlayerPrefs.GetInt("Dnumber") != 1)
        {
            return;

        }

        int i = 0;
        foreach(GameObject position in SwitchPlayerPosition().positionObject)
        {
            string CharacterCode = (string)PhotonNetwork.CurrentRoom.CustomProperties["character" + (i+1).ToString()];
            Debug.Log(CharacterCode);
            string gameMode = CharacterCode[0].ToString();//null
            int Index = int.Parse(CharacterCode.Substring(1));
            GameObject clonePrefab;
            Debug.Log(i + " : " + Index);
            if (CharacterCode[0] == 'A')
            {
                clonePrefab = characterDataList.normalEraser[Index].characterPrefab;
            }
            else
            {
                clonePrefab = characterDataList.hardEraser[Index].characterPrefab;
            }
            GameObject newEraser =PhotonNetwork.Instantiate(clonePrefab.name, position.transform.position, position.transform.rotation);
            newEraser.name += i.ToString();
            newEraser.GetComponent<EraserControlBase>().ChangeColor(colorData.activeColorPackage[i]);
            EraserControlBase controlBase = newEraser.GetComponent<EraserControlBase>();
            controlBase.playerNumber = i + 1;
            cloneEraserObjects.Add(newEraser);
            newEraser.GetComponent<Rigidbody>().sleepThreshold = 1f;
            i++;
        }
    }
    public int Totalling()
    {
        int total = 0;
        for(int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount;i++)
        {
            int playerCount = (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + i.ToString()] is int p) ? p : 0;
            total += playerCount;
            int comCount = (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + i.ToString()] is int c) ? c : 0;
            total += comCount;
        }
        return total;
    }
}

