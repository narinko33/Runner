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

    Vector3 playerPosition;
    Vector3 startTouchPos;
    Vector3 endTouchPos;

    AudioSource jumpSound;

    float flickValue_x;

    float startTouchTime;
    float endTouchTime;
    float flickTime;
    float noMovementTime = 0.0f;
    float noMovementThreshold = 2.0f;


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

        //ボタンを押した(タップした)ときの処理
        if (Input.GetMouseButtonDown(0) == true)
        {
            //ボタンを押した瞬間の座標を取得
            startTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            //ボタンを押した瞬間の時間
            startTouchTime = Time.time;
        }
        //ボタンから指を離した(タップが終わった)ときの処理
        if (Input.GetMouseButtonUp(0) == true)
        {
            //ボタンから指が離れた瞬間の座標を取得
            endTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            //ボタンから指が離れた瞬間の時間
            endTouchTime = Time.time;
            FlickDirection();
            if (flickTime <= 0.1f)
            {
                //ボタンを押していた時間が0.1ｆ以下なら
                Jump();
            }
            else
            {
                //ボタンを押していた時間が0.1ｆ以上なら
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

            ReturnPosition();
        }

        // 重力分の力を毎フレーム追加
        if (!isGrounded) moveDirection.y -= gravity * Time.fixedDeltaTime;

        // 移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        rd.velocity = globalDirection;


    }

    void FlickDirection() //デバッグ用
    {
        //ボタンから指を離した瞬間の座標-ボタンを押した瞬間の座標＝スワイプ量
        flickValue_x = endTouchPos.x - startTouchPos.x;
        //ボタンから指を離した瞬間の時間-ボタンを押した瞬間の時間＝ボタンを押していた時間
        flickTime = endTouchTime - startTouchTime;

        Debug.Log("x スワイプ量は" + flickValue_x);
        Debug.Log("経過時間は" + flickTime);
    }

    void GetDirection()
    {
        Debug.Log("GetDirection実行");
        if (flickValue_x > 50.0f)
        {
            //スワイプ量が50.0ｆ以上なら
            MoveToRight();
        }
        else
        if (flickValue_x < -50.0f)
        {
            //スワイプ量が-50.0ｆ以下なら
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
        //ポーズ状態の時とステートがReady,GameOverの時はリターン
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

    void ReturnPosition()
    {
        //プレイヤーの位置を取得
        Vector3 currentPlayerPosition = transform.position;
        // プレイヤーの位置が前回のフレームと3ｍ以内の時
        if (currentPlayerPosition.z - playerPosition.z <= 3.0f)
        {
            // プレイヤーの位置が変化しない時間を更新
            noMovementTime += Time.deltaTime;

            // プレイヤーの位置が変化しない時間が、指定した時間以上になった場合
            if (noMovementTime >= noMovementThreshold)
            {
                // プレイヤーの位置が変化しなかったという処理を行う
                // (ここでは、「2秒間プレイヤーの位置が変化しなかった」という文字列をログに出力する)
                Debug.Log("2秒間プレイヤーの位置が変化しなかった");
                //プレイヤーの位置を少し戻す
                transform.position += new Vector3(0.0f, 20.0f, -20.0f);
                moveDirection.z = 0;
                //移動後の座標を代入
                playerPosition = transform.position;
                noMovementTime = 0f;
            }
        }
        else
        {
            // プレイヤーの位置が変化した場合、プレイヤーの位置を更新し、
            // プレイヤーの位置が変化しない時間をリセットする
            playerPosition = currentPlayerPosition;
            noMovementTime = 0f;
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