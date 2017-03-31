using UnityEngine;

public class MouseLocation : MonoBehaviour
{
    public static MouseLocation instance;

    [HideInInspector]
    public Vector3 mousePosition;
    [HideInInspector]
    public bool isValid;

    [SerializeField]
    LayerMask whatIsGround;

    Ray mouseRay;
    RaycastHit hit;
    Vector2 screenPosition;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        isValid = false;

        screenPosition = Input.mousePosition;

        mouseRay = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(mouseRay, out hit, 100f, whatIsGround))
        {
            isValid = true;
            mousePosition = hit.point;
        }
    }
}
