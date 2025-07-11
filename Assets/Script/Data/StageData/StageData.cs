using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="StageData",menuName ="Stage/StageData")]
public class StageData : ScriptableObject
{
    public string stageName;
    public string sceneName;
    public Sprite stageImage;
    public int miniPlayerCount;
    public int maxPlayerCount;
    public bool isGimmick;
}
