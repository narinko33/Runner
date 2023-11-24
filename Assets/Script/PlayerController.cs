using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;

    Vector3 startTouchPos;
    Vector3 endTouchPos;

    float flickValue_x;

    float startTouchTime;
    float endTouchTime;
    float flickTime;


    Rigidbody rd;
    bool isGrounded = false;
    public Animator animator;
    public GameController gameController;

    public Vector3 moveDirection = Vector3.zero;
    int targetLane;
    public int canJump = 2;

    float gravity = 20.0f;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;
    public Text ScoreText;
    public EventSystem eventSystem;

    bool IsStop()
    {
        return gameController.GetCountDownTime() <= 0.0f;
    }

    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        if (Input.GetMouseButtonDown(0) == true && !eventSystem.IsPointerOverGameObject())
        {
            startTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            startTouchTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0) == true && !eventSystem.IsPointerOverGameObject())
        {
            endTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            endTouchTime = Time.time;
            FlickDirection();
            if (flickTime <= 0.1f)
            {
                Jump();
            }
            else
            {
                GetDirection();
            }

        }

    }

    void FixedUpdate()
    {
        // if (gameController.state == State.GameOver) return;
        if (IsStop())
        {

            gameController.GameOver();

        }
        else
        {
            if (rd.isKinematic) return;

            // 徐々に加速しZ方向に常に前進させる
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            // X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;

            // 速度が0以上なら走っているフラグをtrueにする
            animator.SetFloat("Blend", acceleratedZ / speedZ);


        }

        // 重力分の力を毎フレーム追加
        if (!isGrounded) moveDirection.y -= gravity * Time.fixedDeltaTime;

        // 移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        rd.velocity = globalDirection;


    }

    void FlickDirection() //デバッグ用
    {
        flickValue_x = endTouchPos.x - startTouchPos.x;
        flickTime = endTouchTime - startTouchTime;

        Debug.Log("x スワイプ量は" + flickValue_x);
        Debug.Log("経過時間は" + flickTime);
    }

    void GetDirection()
    {
        if (flickValue_x > 50.0f)
        {
            MoveToRight();
        }
        else
        if (flickValue_x < -50.0f)
        {
            MoveToLeft();
        }

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
        if (gameController.isPause) return;
        if (gameController.state == State.Ready) return;
        if (canJump >= 1)
        {
            moveDirection.y = speedJump;
            isGrounded = false;

            // ジャンプトリガーを設定
            animator.SetTrigger("Jump");
            canJump--;
        }
    }

    public void Fly()
    {
        StartCoroutine(ResetFly(gravity));
        gravity = 5.0f;
    }

    IEnumerator ResetFly(float gravity)
    {
        yield return new WaitForSeconds(2.0f);
        this.gravity = gravity;
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            moveDirection.y = 0;
            canJump = 2;
        }
        if (other.gameObject.tag == "BlueBox")
        {
            gameController.AddTime();
            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "RedBox")
        {
            gameController.DecreaseTime();
            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "FlyBox")
        {
            Fly();
            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
        }
        // if (other.gameObject.tag == "BoostBox")
        // {
        //     moveDirection.x *= 2;
        //     moveDirection.z *= 2;
        //     // ヒットしたオブジェクトは削除
        //     Destroy(other.gameObject);
        // }

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

    public void SetSteerActive(bool active)
    {
        //Rigidbodyのオン、オフを切り替える
        rd.isKinematic = !active;
    }

}
