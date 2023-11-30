using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int MinLane = -2;
    public int MaxLane = 2;
    const float LaneWidth = 1.0f;

    Vector3 startTouchPos;
    Vector3 endTouchPos;

    AudioSource jumpSound;

    float flickValue_x;

    float startTouchTime;
    float endTouchTime;
    float flickTime;


    Rigidbody rd;
    bool isGrounded = false;
    bool isInvincible = false;
    public Animator animator;
    public GameController gameController;
    public BGM bgm;

    public Vector3 moveDirection = Vector3.zero;
    int targetLane;
    public int canJump = 2;
    float gravity = 20.0f;
    float boostZ;
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
        jumpSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        boostZ = speedZ;

    }

    void Update()
    {
        if (gameController.isPause) return;
        Debug.Log("Update実行");
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        if (Input.GetMouseButtonDown(0) == true)
        {
            startTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            startTouchTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0) == true)
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
        Debug.Log("GetDirection実行");
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
        if (gameController.state == State.GameOver) return;
        Debug.Log("Jump実行");
        if (canJump >= 1)
        {
            moveDirection.y = speedJump;
            isGrounded = false;

            // ジャンプトリガーを設定
            animator.SetTrigger("Jump");
            canJump--;
            jumpSound.Play();
        }
    }

    // FlyBoxを取ったら飛べる
    public void Fly()
    {
        Jump();
        StartCoroutine(ResetFly(gravity));
        // 重力が20.0fから5.0fになる
        gravity = 5.0f;
    }

    IEnumerator ResetFly(float gravity)
    {
        // 2秒後に元の重力に戻る
        yield return new WaitForSeconds(2.0f);
        this.gravity = gravity;
    }

    // BoostBoxを取ったら加速
    public void Boost()
    {
        if (speedZ > boostZ) return;
        StartCoroutine(ResetBoost(speedZ));
        this.speedZ = speedZ * 2.0f;

    }

    IEnumerator ResetBoost(float speedZ)
    {
        // 3秒後に元のスピードに戻る
        yield return new WaitForSeconds(3.0f);
        this.speedZ = speedZ;

    }

    // InvincibleBoxを取ったら無敵
    public void Invincible()
    {
        if (isInvincible) return;
        isInvincible = true;
        bgm.InvincibleBGM();
        StartCoroutine(ResetInvincible());

    }
    IEnumerator ResetInvincible()
    {
        // 10秒後に元に戻る
        yield return new WaitForSeconds(10.0f);
        isInvincible = false;
        bgm.MainBGM();

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
            if (isInvincible)
            {
                gameController.AddTime();
            }
            else
            {
                gameController.DecreaseTime();
            }
            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Drone")
        {
            Fly();
            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bike")
        {
            Boost();
            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Jet")
        {
            Invincible();
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

    public void SetSteerActive(bool active)
    {
        //Rigidbodyのオン、オフを切り替える
        rd.isKinematic = !active;
    }

}