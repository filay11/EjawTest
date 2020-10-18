using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }


public class MouseManager : MonoBehaviour
{
    public EventVector3 OnClickCreateObject;
    public LayerMask clickableLayer;

    private readonly float zOffset = -0.5f;  // offset for z-axis for instantiate GeometryObject correctly

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.MaxValue, clickableLayer.value))
            {
                if (hit.collider.gameObject.CompareTag("GameObject"))
                {
                    var obj = hit.collider.GetComponent<GeometryObjectModel>();
                    if (obj != null)
                    {
                        obj.IncreaseObjectClicks();
                        obj.CheckClickCountToChangeColor();
                    }
                }
                else
                {
                    Vector3 mousePos = new Vector3(hit.point.x, hit.point.y, zOffset);
                    OnClickCreateObject.Invoke(mousePos);
                }
            }
        }
    }
}
