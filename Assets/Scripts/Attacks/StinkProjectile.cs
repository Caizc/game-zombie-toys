using System.Collections;
using UnityEngine;

public class StinkProjectile : MonoBehaviour
{
    [SerializeField]
    float speed = 20f;
    [SerializeField]
    AnimationCurve arc;
    [SerializeField]
    ParticleSystem trailPartiles;
    [SerializeField]
    StinkHit stinkHit;

    Vector3 startPoint;
    Vector3 endPoint;
    bool isFlying;

    void Reset()
    {
        trailPartiles = GetComponent<ParticleSystem>();
    }

    public void StartPath(Vector3 start, Vector3 end)
    {
        isFlying = true;

        startPoint = start;
        endPoint = end;

        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        trailPartiles.Stop(true);

        transform.position = startPoint;

        Vector3 pathVector = endPoint - startPoint;

        float totalDistance = pathVector.magnitude;
        float traveledDistance = 0f;

        trailPartiles.Play(true);

        while (totalDistance - traveledDistance > 0f)
        {
            traveledDistance += speed * Time.deltaTime;

            Vector3 newPosition = startPoint + (pathVector.normalized * traveledDistance);

            float arcHeight = arc.Evaluate(traveledDistance / totalDistance);
            newPosition.y += arcHeight;

            transform.position = newPosition;

            yield return null;
        }

        Explode();
    }

    void Explode()
    {
        isFlying = false;

        stinkHit.transform.position = transform.position;
        stinkHit.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isFlying)
        {
            Explode();
        }
    }
}
