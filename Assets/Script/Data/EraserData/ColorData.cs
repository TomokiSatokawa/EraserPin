using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ColorData")]
public class ColorData : ScriptableObject
{
    public Color hiddenColorBody;
    public Color hiddenColorPackage;
    public Color activeColorBody;
    public List<Color> activeColorPackage;
}
