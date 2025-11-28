using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint;

    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer = Time.deltaTime;

        if (timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameManager.instance.pool.Get(Random.Range(0,2));
    }
}
