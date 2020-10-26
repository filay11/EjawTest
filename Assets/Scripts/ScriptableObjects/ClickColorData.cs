using UnityEngine;

[CreateAssetMenu(fileName = "ColorData.asset", menuName = "ColorData")]
public class ClickColorData : ScriptableObject
{
    // changes from test
    public string ObjectType;
    public int MinClicksCount;
    public int MaxClicksCount;
    public Color Color;
}