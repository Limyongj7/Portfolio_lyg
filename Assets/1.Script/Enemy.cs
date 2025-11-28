using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody target;

    public float rotateSpeed = 10f;

    bool isLive = true;

    Rigidbody rd;

    private void Awake()
    {
        rd = GetComponent<Rigidbody>();

    }

    private void FixedUpdate() //Player어에게 가는 움직임, Enemy 회전
    {
        if (!isLive)
            return;

        Vector3 dirVec = target.position - rd.position;

        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rd.MovePosition(rd.position + nextVec);
        rd.linearVelocity = Vector3.zero;

        if (dirVec.sqrMagnitude > 0.0001f)   // 0으로 나누는 상황 방지
        {
            Quaternion targetRot = Quaternion.LookRotation(dirVec); // 바라봐야 할 회전
            Quaternion smoothRot = Quaternion.Slerp(
                rd.rotation,             // 현재 회전
                targetRot,               // 목표 회전
                rotateSpeed * Time.fixedDeltaTime // 얼마나 빨리 돌릴지
            );

            rd.MoveRotation(smoothRot);
        }

    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody>();
    }


}
