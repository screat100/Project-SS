using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed = 5f;        
	public float runSpeed = 7.5f;        

	Vector3 movement;                   
	Animator animator;                      
	Rigidbody playerRigidbody;          
    Quaternion Rotation=Quaternion.identity;
	//private Transform gun, ball;

	void Awake()
	{
		animator = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		//gun = transform.Find("gun");
		//ball = transform.Find("ball");
	}


	void FixedUpdate()
	{
		// 키 입력
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		// 캐릭터 위치변경
		Move(h, v);

		// 캐릭터 회전
		Turning();

		//GetComponent<CapsuleCollider>().enabled = !Input.GetMouseButton(1);
	}

	void Move(float h, float v)
	{
		// 좌표 이동
		movement.Set(h, 0f, v);

		// 좌표 이동 속도 조절
		movement = movement.normalized * (Input.GetKey(KeyCode.LeftShift)?runSpeed:walkSpeed) * Time.deltaTime;

        bool hasHorizontalInput = !Mathf.Approximately(h, 0f);
        bool hasVerticalInput = !Mathf.Approximately(v, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        bool isRun = isWalking && Input.GetKey(KeyCode.LeftShift);

        animator.SetBool("IsRun", isRun);
        animator.SetBool("IsWalking", isWalking);
       
		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning()
	{
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, 360f, 0f);
        Rotation = Quaternion.LookRotation(desiredForward);
        playerRigidbody.MoveRotation(Rotation);
    }
}
