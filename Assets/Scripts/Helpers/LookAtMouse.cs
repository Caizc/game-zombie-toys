using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    void Update()
    {
        if (null != MouseLocation.instance && MouseLocation.instance.isValid)
        {
            transform.LookAt(MouseLocation.instance.mousePosition);
        }
    }
}
