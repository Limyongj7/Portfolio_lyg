using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotateSpeed;
    public Vector3 inputVec;
    public float speed;
    public Scanner scanner;


    Rigidbody rd;
    Animator anim;

    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");
        inputVec.y = 0;

    }

    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rd.MovePosition(rd.position + nextVec);
    }

    private void LateUpdate()
    {
        // 애니메이션
        anim.SetFloat("Speed", inputVec.magnitude);

        // 스캐너가 없거나, 스캐너가 타겟을 못 잡으면 회전하지 않음
        if (scanner == null || scanner.nearestTarget == null)
            return;

        // 바라볼 방향 계산
        Vector3 dir = scanner.nearestTarget.position - transform.position;
        dir.y = 0f;   // Y축 회전만 남기기 (위/아래 기울어짐 방지)

        // 너무 가까우면 회전 계산 패스
        if (dir.sqrMagnitude < 0.0001f)
            return;

        // 목표 회전값 계산
        Quaternion targetRot = Quaternion.LookRotation(dir);

        // 부드럽게 회전
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );

    }


}
