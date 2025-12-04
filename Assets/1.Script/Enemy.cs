using System.Collections;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealt;


    public Rigidbody target;


    private float rotateSpeed = 10f;

    bool isLive;

    Rigidbody rd;
    Collider coll;
    Animator anim;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();

    }

    private void FixedUpdate() //Player어에게 가는 움직임, Enemy 회전
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        isLive = true;

        coll.enabled = true;
        rd.isKinematic = false;
        anim.SetBool("Dead", false);
        health = maxHealt;
    }

    public void Init(SpawnData data)
    {
        speed = data.speed;
        maxHealt = data.health;
        health = data.health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet") || !isLive)
            return;

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;

            coll.enabled = false;
            rd.isKinematic = true;
            anim.SetBool("Dead", true);

            StartCoroutine(DeadRoutine()); // 죽는 애니매이션 딜레이 코르틴 실행

            GameManager.instance.kill++;
            GameManager.instance.GetExp();

        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rd.AddForce(dirVec.normalized * 3, ForceMode.Impulse);
    }


    void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadRoutine() // 죽는 애니매이션 실행 관련 비활성화 딜레이
    {
        yield return new WaitForSeconds(2f); // Dead 애니 길이
        gameObject.SetActive(false);
    }
}
