using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterDataList",menuName = "Character/CharacterDataList")]
public class CharacterDataList : ScriptableObject
{
    public List<CharacterData> normalEraser;
    public List<CharacterData> hardEraser;
}
