using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData.asset", menuName = "ObjectData")]
public class GeometryObjectData : ScriptableObject
{
    public List<ClickColorData> ClicksData;
}
