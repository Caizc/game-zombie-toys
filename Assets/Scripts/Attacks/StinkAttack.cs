using UnityEngine;

public class StinkAttack : MonoBehaviour
{
    [HeaderAttribute("Weapon Specs")]
    public float cooldown = 5f;

    [SerializeField]
    float range = 5f;

    [HeaderAttribute("Weapon References")]
    [SerializeField]
    StinkProjectile stinkProjectile;
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
    Vector3 targetPosition;
    bool inRange = false;

    public bool Fire()
    {
        if (inRange)
        {
            LaunchProjectile();
            return true;
        }
        
        return false;
    }

    void Update()
    {
        inRange = false;

        if (null == MouseLocation.instance || !MouseLocation.instance.isValid)
        {
            return;
        }

        targetPosition = MouseLocation.instance.mousePosition;

        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance <= range)
        {
            inRange = true;
        }

        UpdateReticule();
    }

    void UpdateReticule()
    {
        targetReticule.transform.position = targetPosition;

        if (!inRange)
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

        stinkProjectile.gameObject.SetActive(true);
        stinkProjectile.StartPath(transform.position, targetPosition);
    }
}
