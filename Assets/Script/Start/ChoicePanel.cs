using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ChoicePanel : MonoBehaviour
{
    public CharacterStepper stepper;
    public TextMeshProUGUI nameText;
    public ColorData colorData;
    public Image namePlate;
    public int localPlayerNumber;
    public int playerNumber;
    public int PlayerNumber { get { return playerNumber; } }
    public Slider sizeSlider;
    public Slider weightSlider;
    public Slider frictionSlider;
    public GameObject eraserObject;
    private float moveSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        stepper.playerNumber = localPlayerNumber;
        namePlate.color = colorData.activeColorPackage[playerNumber - 1] + Color.white / 2;
        
    }
    public void Awake()
    {
        stepper.choicePanel = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetName(int number)
    {
        nameText.SetText(number.ToString() + "P");
        playerNumber = number;
    }
    public void SliderMove(CharacterData data)
    {
        sizeSlider.DOValue(data.size, moveSpeed);
        weightSlider.DOValue(data.weight, moveSpeed);
        frictionSlider.DOValue(data.friction, moveSpeed);
    }
    public void ChangeEraser()
    {
        //positionåvéZ
        Vector3 position = eraserObject.transform.position;
        position.y -= GetEraserHight(eraserObject);

        GameObject eraserPrefab = stepper.list[stepper.Value].characterPrefab;
        position.y += GetEraserHight(eraserPrefab);

        Quaternion quaternion = eraserObject.transform.rotation;
        Destroy(eraserObject);
        eraserObject = Instantiate(eraserPrefab,position,quaternion);
        eraserObject.GetComponent<Rigidbody>().isKinematic = true;
        eraserObject.GetComponent<EraserControlBase>().ChangeColor(colorData.activeColorPackage[playerNumber -1]);
        HardEraserBase hardEraserBase = eraserObject.GetComponent<HardEraserBase>();
        if (hardEraserBase != null)
        {
            hardEraserBase.SliderActive(false);
        }
    }
    public float GetEraserHight(GameObject eraserObject)
    {
        EraserControlBase controlBase = eraserObject.GetComponent<EraserControlBase>();
        Vector3 eraserCenter = controlBase.coverObject.transform.position;
        eraserCenter = eraserObject.transform.TransformPoint(eraserCenter);

        Vector3 topPosition = controlBase.topPosition.transform.position;
        topPosition = eraserObject.transform.TransformPoint(topPosition);

        return Vector3.Distance(topPosition,eraserCenter);
    }
}
