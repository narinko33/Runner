using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;


    Rigidbody rd;
    bool isGrounded = false;
    Animator animator;
    public GameController gameController;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;
    float recoverTime = 0.0f;
    int canJump = 2;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;


    bool IsStop()
    {
        return gameController.GetCountDownTime() <= 0.0f;
    }

    void Start()
    {
        rd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

    }

    void FixedUpdate()
    {
        if (IsStop())
        {
            moveDirection.x = 0.0f;
            moveDirection.y = 0.0f;
            moveDirection.z = 0.0f;
            animator.SetFloat("Blend", 0.0f);

            // animator.SetBool("Blend", moveDirection.z < 0.0f);

        }
        else
        {
            // 徐々に加速しZ方向に常に前進させる
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            // X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;

            // 速度が0以上なら走っているフラグをtrueにする
            // animator.SetBool("Blend", moveDirection.z > 0.0f);
            animator.SetFloat("Blend", acceleratedZ / speedZ);

        }

        // 重力分の力を毎フレーム追加
        if (!isGrounded) moveDirection.y -= gravity * Time.fixedDeltaTime;

        // 移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        rd.velocity = globalDirection;


    }

    // 左のレーンに移動を開始
    public void MoveToLeft()
    {
        if (targetLane > MinLane) targetLane--;
    }
    // 右のレーンに移動を開始
    public void MoveToRight()
    {
        if (targetLane < MaxLane) targetLane++;

    }

    public void Jump()
    {
        if (canJump >= 1)
        {
            moveDirection.y = speedJump;
            isGrounded = false;

            // ジャンプトリガーを設定
            animator.SetTrigger("Jump");
            canJump--;
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            moveDirection.y = 0;
            canJump = 2;
        }
        if (other.gameObject.tag == "Robo")
        {
            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
