using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Sockets;
public class ChoicePanel : MonoBehaviour
{
    public CharacterStepper stepper;
    public TextMeshProUGUI nameText;
    public GameObject comRavel;
    public ColorData colorData;
    public Image namePlate;
    public int localPlayerNumber;
    public int playerNumber;
    public int PlayerNumber { get { return playerNumber; } }
    public Slider sizeSlider;
    public Slider weightSlider;
    public Slider frictionSlider;
    public PreviewControl previewControl;
    private float moveSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        stepper.playerNumber = localPlayerNumber;
        namePlate.color = colorData.activeColorPackage[playerNumber - 1] + Color.white / 2;
        previewControl = FindAnyObjectByType<PreviewControl>();
        
    }
    public void Awake()
    {
        stepper.choicePanel = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetName(int number,bool isCom)
    {
        nameText.SetText(number.ToString() + "P");
        playerNumber = number;
        comRavel.SetActive(isCom);
        namePlate.color = AddWhite(colorData.activeColorPackage[playerNumber - 1]);
        previewControl = FindAnyObjectByType<PreviewControl>();
    }
    public Color AddWhite(Color def)
    {
        float h1;
        float s1;
        float v1;
        Color.RGBToHSV(def, out h1, out s1, out v1);
        s1 = 0.5f;
        return Color.HSVToRGB(h1, s1, v1);
    }
    public void SliderMove(CharacterData data)
    {
        sizeSlider.DOValue(data.size, moveSpeed);
        weightSlider.DOValue(data.weight, moveSpeed);
        frictionSlider.DOValue(data.friction, moveSpeed);
    }
    public void ChangeEraser()
    {
        if(previewControl == null)
        {
            previewControl = FindAnyObjectByType<PreviewControl>();
        }
        previewControl.ChangeCharacter(localPlayerNumber,stepper.Value);
    }
}
