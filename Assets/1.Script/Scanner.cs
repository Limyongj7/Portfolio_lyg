using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;

    public Collider[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        targets = Physics.OverlapSphere(transform.position, scanRange, targetLayer);
        nearestTarget = GetNearest();

    }

    Transform GetNearest()
    {
        Transform result = null;

        float bestDist   = Mathf.Infinity;
        Vector3 myPos = transform.position;

        foreach (Collider target in targets)
        {
            
            Vector3 targetPos = target.transform.position;
            float curDiff = (targetPos - myPos).sqrMagnitude;

            if (curDiff < bestDist)
            {
                bestDist = curDiff;
                result = target.transform;
            }
        }

        return result;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }

}
