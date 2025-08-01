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
        previewControl.ChangeCharacter(PlayerNumber,stepper.Value);
    }
}
