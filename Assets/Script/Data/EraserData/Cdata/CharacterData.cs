using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="CharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string eraserName;
    public GameObject characterPrefab;
    public Sprite bodyImage;
    public Sprite coverImage;
    public Sprite decoration;
    public float size;
    public float weight;
    public float friction;
}
