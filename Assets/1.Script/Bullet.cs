using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    Rigidbody rd;


    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rd.linearVelocity = dir * 15f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") || per == -1)
            return;
        per--;

        if (per == -1)
        {
            rd.linearVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
