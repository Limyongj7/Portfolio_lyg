using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public float speed;

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
            rd.linearVelocity = dir * speed;
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
