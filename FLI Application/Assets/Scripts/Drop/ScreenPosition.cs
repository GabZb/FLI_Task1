using UnityEngine;

public class ScreenPosition : MonoBehaviour
{
    public Camera cam;
    public Transform target;

    void Update()
    {
        Vector3 _screenPos = cam.WorldToScreenPoint(target.position);

        Debug.Log("screen width: " + Screen.width);
        Debug.Log("screen height: " + Screen.height);
        Debug.Log("target is " + _screenPos.x + " pixels from the left");
    }
}
