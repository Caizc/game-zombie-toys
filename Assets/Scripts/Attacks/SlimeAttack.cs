using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    [HeaderAttribute("Weapon Specs")]
    public float cooldown = 3.5f;

    [SerializeField]
    LayerMask whatIsShootable;

    [HeaderAttribute("Weapon References")]
    [SerializeField]
    SlimeProjectile slimeProjectile;
    [SerializeField]
    Renderer targetReticule;

    [HeaderAttribute("Reticule Colors")]
    [SerializeField]
    Color invalidTargetTint = Color.red;
    [SerializeField]
    Color notReadyTint = Color.yellow;
    [SerializeField]
    Color readyTint = Color.green;

    float timeOfLastAttack = -10f;
    Transform target;
    Vector3 targetPosition;

    void OnEnable()
    {
        target = null;
    }

    public bool Fire()
    {
        if (null != target)
        {
            LaunchProjectile();
            return true;
        }

        return false;
    }

    void Update()
    {
        if (null == MouseLocation.instance || !MouseLocation.instance.isValid)
        {
            return;
        }

        targetPosition = MouseLocation.instance.mousePosition;

        RaycastHit hit;
        if (Physics.Raycast(targetPosition, Vector3.up, out hit, 2f, whatIsShootable))
        {
            target = hit.transform;
        }

        UpdateReticule();
    }

    void UpdateReticule()
    {
        if (null != target)
        {
            targetReticule.transform.position = target.position;
        }
        else
        {
            targetReticule.transform.position = targetPosition;
        }

        if (null == target)
        {
            targetReticule.material.SetColor("_TintColor", invalidTargetTint);
        }
        else if (timeOfLastAttack + cooldown > Time.time)
        {
            targetReticule.material.SetColor("_TintColor", notReadyTint);
        }
        else
        {
            targetReticule.material.SetColor("_TintColor", readyTint);
        }
    }

    void LaunchProjectile()
    {
        timeOfLastAttack = Time.time;

        slimeProjectile.transform.position = transform.position;
        slimeProjectile.gameObject.SetActive(true);
        slimeProjectile.StartPath(target);

        target = null;
    }
}
