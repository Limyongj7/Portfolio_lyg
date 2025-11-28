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
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffZ = Mathf.Abs(playerPos.z - myPos.z);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirZ = playerDir.z < 0 ? -1 : 1;


        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffZ)
                {
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if (diffX < diffZ)
                {
                    transform.Translate(Vector3.forward * dirZ * 60);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 30 + new Vector3(Random.Range(-6f, 6f), 0f, Random.Range(-6f, 6f)), Space.World);
                }

                break;
        }

    }
}
