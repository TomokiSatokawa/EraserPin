using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListControl : MonoBehaviour
{
    public GameObject eraserIconPrefab;
    public GameObject content;
    public CharacterDataList eraserDataList;
    public ColorData colorData;
    private List<EraserIcon> icons = new List<EraserIcon>(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clone(List<GameManager.PlayerData> playerDataList)
    {
        int i = 0;
        foreach(GameManager.PlayerData data in playerDataList)
        {
            GameObject clonedObjcet = Instantiate(eraserIconPrefab, content.transform);
            if(data.deviceNumber == PlayerPrefs.GetInt("Dnumber"))
            {
                clonedObjcet.transform.localScale = Vector3.one;
            }
            else
            {
                clonedObjcet.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            EraserIcon eraserIcon = clonedObjcet.GetComponent<EraserIcon>(); 
            CharacterData characterData = data.eraserData;
            //Debug.Log(characterData.name);
            eraserIcon.SetData(characterData.bodyImage, characterData.coverImage, characterData.decoration);
            eraserIcon.ChangeColor(colorData.activeColorPackage[i]);
            eraserIcon.SetPlayerNumber(i + 1);
            icons.Add(eraserIcon);
            i++;
        }
    }
    public void DropoutCheck()
    {
        int i = 0;
        foreach(GameManager.PlayerData data in FindAnyObjectByType<GameManager>().playerList)
        {
            icons[i].Active(data.isAlive);
        }
    }
}
