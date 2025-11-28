using UnityEngine;

public class AreaFollow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        // 플레이어 위치는 따라가지만 회전은 따라가지 않음
        transform.position = player.position;
    }
}
