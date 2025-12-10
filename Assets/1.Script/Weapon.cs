using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int per;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            default:

                break;
        }

        // Test code..
        if (Input.GetButtonDown("Jump"))
        {
            LevelUP(10, 1);
        }
    }

    public void LevelUP(float damage, int per)
    {
        this.damage = damage;
        this.per += per;

    }

    public void Init(ItemData data)
    {
        // Basic set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property
        id = data.itemId;
        damage = data.baseDamage;
        per = data.basePer;

        for (int index = 0; index < GameManager.instance.pool.weaponPrefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.weaponPrefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 0.5f;
                break;
            default:

                break;
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;

        dir.y = 0f;

        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Wget(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.forward, dir);
        bullet.GetComponent<Bullet>().Init(damage, per, dir);

    }

}
