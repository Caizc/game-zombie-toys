using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    float smoothing = 5f;
    [SerializeField]
    Vector3 offset = new Vector3(0f, 15f, -22f);

    void FixedUpdate()
    {
        Vector3 targetCamPos = GameManager.instance.player.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
