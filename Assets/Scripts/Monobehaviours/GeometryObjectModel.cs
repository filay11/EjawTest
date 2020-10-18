using UnityEngine;

public class GeometryObjectModel : MonoBehaviour
{
    public int ClickCount;
    public Color Color;
    public GeometryObjectData objectData;

    private Renderer geometryObjectRender;

    private void Awake()
    {
        geometryObjectRender = gameObject.GetComponent<Renderer>();

        // set default color
        Color = geometryObjectRender.material.color;
    }

    public void IncreaseObjectClicks()
    {
        ClickCount++;
    }
    public void CheckClickCountToChangeColor()
    {
        if (objectData == null)
        {            
            return;
        }
        
        foreach (var data in objectData.ClicksData)
        {
            if (gameObject.name == data.ObjectType && ClickCount >= data.MinClicksCount && ClickCount <= data.MaxClicksCount)
            {                
                ChangeColor(data.Color);                
                break;
            }
        }
    }
    public void ChangeColorRandomly()
    {
        Color c = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );
        ChangeColor(c);
    }
    private void ChangeColor (Color color)
    {
        if (geometryObjectRender != null)
        {
            Color = color;
            geometryObjectRender.material.color = color;
        }
    }
}
