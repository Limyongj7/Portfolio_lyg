using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.up * speed * Time.deltaTime); // 辟立
                break;
            default:
                break;
        }

        // Test code..
        if (Input.GetButtonDown("Jump"))
        {
            LevelUP(20, 5);
        }
    }

    public void LevelUP(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150; // 辟立
                Batch(); // 辟立?
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Wget(prefabId).transform;
                bullet.parent = transform;
            }
            
            bullet.localEulerAngles = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            
            Vector3 rotVec = Vector3.up * 360 * index / count; // 辟立
            bullet.Rotate(rotVec);  // 辟立
            bullet.Translate(bullet.forward * 1.5f, Space.World); //辟立
            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }
}
