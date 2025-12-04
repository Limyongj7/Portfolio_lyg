using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag ("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        

        switch (transform.tag)
        {
            case "Ground":
                float diffX = (playerPos.x - myPos.x);
                float diffZ = (playerPos.z - myPos.z);
                float dirX = diffX < 0 ? -1 : 1;
                float dirZ = diffZ < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffZ = Mathf.Abs(diffZ);

                if (diffX > diffZ)
                {
                    transform.Translate(Vector3.right * dirX * 90);
                }
                else if (diffX < diffZ)
                {
                    transform.Translate(Vector3.forward * dirZ * 90);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-6, 6), 0, Random.Range(-6, 6));
                    transform.Translate(ran + dist * 2);
                }

                break;
        }

    }
}
