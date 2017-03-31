using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    [HideInInspector]
    public Vector3 endPoint;

    [HeaderAttribute("Bolt Properties")]
    [SerializeField]
    float rayHeight = 2.0f;
    [SerializeField]
    float effectDuration = 0.75f;
    [SerializeField]
    float phaseDuration = 0.1f;

    [HeaderAttribute("Bolt Rendering")]
    [SerializeField]
    LineRenderer rayRenderer;
    [SerializeField]
    AnimationCurve[] rayPhases;

    int phaseIndex = 0;
    float timeToChangePhase;
    float timeSinceEffectStarted;
    Vector3 vectorOfBolt;

    void OnEnable()
    {
        timeToChangePhase = 0f;
        timeSinceEffectStarted = 0f;
    }

    void Update()
    {
        timeSinceEffectStarted += Time.deltaTime;

        if (timeSinceEffectStarted >= effectDuration)
        {
            gameObject.SetActive(false);
        }
        else
        {
            vectorOfBolt = endPoint - transform.position;

            if (timeSinceEffectStarted >= timeToChangePhase)
            {
                timeToChangePhase = timeSinceEffectStarted + phaseDuration;
                ChangePhase();
            }
        }
    }

    void ChangePhase()
    {
        phaseIndex++;

        if (phaseIndex >= rayPhases.Length)
        {
            phaseIndex = 0;
        }

        AnimationCurve curve = rayPhases[phaseIndex];
        rayRenderer.numPositions = curve.keys.Length;

        for (int index = 0; index < curve.keys.Length; index++)
        {
            Keyframe key = curve.keys[index];

            Vector3 point = transform.position + vectorOfBolt * key.time;
            point += Vector3.up * key.value * rayHeight;

            rayRenderer.SetPosition(index, point);
        }
    }
}
