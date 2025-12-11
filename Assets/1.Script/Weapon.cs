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
            case 20:
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

    void Fire() //  총알 생성 스크립트
    {
        if (!player.scanner.nearestTarget) // 플레이어, 스켄어에 타겟이 잡혔을때만 실행
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position; // 스케너에 잡힌 타겟을 포지션
        Vector3 dir = targetPos - transform.position; // 타겟 포시션 - 현오브젝트 포지션 

        dir.y = 0f; // 방향값의, y축은 0으로 고정

        dir = dir.normalized; // 대각선의 방향도 평균화

        Transform bullet = GameManager.instance.pool.Wget(prefabId).transform; // pool에서 prefabId에 저장된 숫자, 무기를 꺼낸다
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.forward, dir);
        bullet.GetComponent<Bullet>().Init(damage, per, dir);

    }

}
