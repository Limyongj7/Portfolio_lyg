using UnityEngine;

public class Player : MonoBehaviour
{
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
        Vector3 nextVec = inputVec.normalized * speed *Time.fixedDeltaTime;
        rd.MovePosition(rd.position + nextVec);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);
    }
}
