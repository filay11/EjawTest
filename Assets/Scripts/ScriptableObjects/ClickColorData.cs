using UnityEngine;

[CreateAssetMenu(fileName = "ColorData.asset", menuName = "ColorData")]
public class ClickColorData : ScriptableObject
{
    public string ObjectType;
    public int MinClicksCount;
    public int MaxClicksCount;
    public Color Color;
}