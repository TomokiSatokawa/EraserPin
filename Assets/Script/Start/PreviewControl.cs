using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewControl : MonoBehaviour
{
    public List<GameObject> erasers;
    public CharacterDataList characterDatas;
    public ColorData colorData;
    private List<CharacterData> list = new List<CharacterData>();
    private int sumPlayer;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Active(int playerCount)
    {
        Debug.Log(playerCount);
        int i = 1;
        foreach (GameObject obj in erasers)
        {
            obj.SetActive(i <= playerCount);
            i++;
        }
        sumPlayer = playerCount;
    }
    public void ChangeCharacter(int playerNumber,int characterNumber)
    {
        GameObject eraserObject = erasers[playerNumber - 1];
        //positionŒvŽZ
        Vector3 position = eraserObject.transform.position;
        position.y -= GetEraserHight(eraserObject);

        if (list.Count == 0 || list == null)
        {
            if (PlayerPrefs.GetString("mode") == "ƒm[ƒ}ƒ‹")
            {
                list = characterDatas.normalEraser;
            }
            else
            {
                list = characterDatas.hardEraser;
            }
        }
        GameObject eraserPrefab = list[characterNumber].characterPrefab;
        position.y += GetEraserHight(eraserPrefab);

        Quaternion quaternion = eraserObject.transform.rotation;
        Destroy(eraserObject);
        eraserObject = Instantiate(eraserPrefab, position, quaternion);
        eraserObject.GetComponent<Rigidbody>().isKinematic = true;
        eraserObject.GetComponent<EraserControlBase>().ChangeColor(colorData.activeColorPackage[playerNumber - 1]);
        HardEraserBase hardEraserBase = eraserObject.GetComponent<HardEraserBase>();
        if (hardEraserBase != null)
        {
            hardEraserBase.SliderActive(false);
        }
        erasers[playerNumber -1] = eraserObject;
        Active(sumPlayer);
    }
    public float GetEraserHight(GameObject eraserObject)
    {
        EraserControlBase controlBase = eraserObject.GetComponent<EraserControlBase>();
        Vector3 eraserCenter = controlBase.coverObject.transform.position;
        eraserCenter = eraserObject.transform.TransformPoint(eraserCenter);

        Vector3 topPosition = controlBase.topPosition.transform.position;
        topPosition = eraserObject.transform.TransformPoint(topPosition);

        return Vector3.Distance(topPosition, eraserCenter);
    }
}
