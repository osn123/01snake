using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // 画面端の境界値（ワールド座標）
    private float xMin, xMax, yMin, yMax;

    float moveSp = 2f; // プレイヤーの移動速度
    float gameOverTime = 10f; // ゲームオーバーになるまでの時間（秒）

    [SerializeField] private GameObject gameManager; // GameManagerオブジェクトへの参照

    [SerializeField] private TextMeshProUGUI timerText;  // タイマー表示用のTextMeshProUGUI

    Animator animator; // プレイヤーのアニメーター
    Rigidbody2D rb;    // プレイヤーのRigidbody2D

    private Vector2 moveDirection = Vector2.down; // 初期移動方向（下）

    private float timer = 0f; // 経過時間

    bool isGameOver; // ゲームオーバー状態かどうか
    int point = 0;   // 取得ポイント
    public static int pointSum = 0; // 累計ポイント（静的変数）

    [SerializeField] GameObject gameOverImage;    // ゲームオーバー画像
    [SerializeField] GameObject pushStartImage;   // PUSH START画像

    public AudioClip appleSE; // アイテム取得時のSE
    AudioSource aud;          // 効果音再生用AudioSource

    private Coroutine blinkCoroutine; // 点滅コルーチン

    public AudioClip bgmClip; // BGM
    private AudioSource audioSource; // BGM再生用AudioSource

    // 初期化処理
    void Awake()
    {
        // メインカメラを取得
        Camera cam = Camera.main;

        // ビューポートの左下(0,0)をワールド座標に変換
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        // ビューポートの右上(1,1)をワールド座標に変換
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // キャラクターのSpriteRendererコンポーネントを取得
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        // キャラクターの幅・高さを取得（bounds.extentsは中心から端までの距離なので2倍して全幅/全高に）
        Vector3 charExtents = sr.bounds.extents * 2;

        // キャラクターが画面外に出ないようにX座標の最小値を設定
        xMin = bottomLeft.x;
        // キャラクターが画面外に出ないようにY座標の最小値を設定（キャラの高さ分だけ底上げ）
        yMin = bottomLeft.y + charExtents.y;
        // キャラクターが画面外に出ないようにX座標の最大値を設定（キャラの幅分だけ内側に）
        xMax = topRight.x - charExtents.x;
        // キャラクターが画面外に出ないようにY座標の最大値を設定
        yMax = topRight.y;

        // Animatorコンポーネントを取得
        animator = GetComponent<Animator>();
        // Animatorが見つからなければエラーメッセージを表示
        if (animator == null)
        {
            Debug.LogError("Animatorが見つかりません！");
        }

        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }


    // ゲーム開始時の処理
    void Start()
    {
        isGameOver = false; // ゲームオーバー状態を初期化
        this.aud = GetComponent<AudioSource>();
        pushStartImage.SetActive(false); // PUSH START画像を非表示
        gameOverImage.SetActive(false);  // ゲームオーバー画像を非表示

        // BGM再生
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.Play();
    }

    // 毎フレーム呼ばれる処理
    [System.Obsolete]
    void Update()
    {
        // ESCキーでアプリ終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // ゲームオーバー時の処理
        if (isGameOver)
        {
            gameOverImage.SetActive(true); // ゲームオーバー画像表示

            // PUSH START画像の点滅開始（1回だけ）
            if (isGameOver && blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkPushStart());
            }

            // スペースキーでリザルト画面へ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("result");
            }
            return;
        }

        timer += Time.deltaTime; // タイマー更新

        // 時間経過でゲームオーバー
        if (timer >= gameOverTime)
        {
            GameOver();
            return;
        }

        // 矢印キーで移動方向を変更
        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDirection = Vector2.right;

        // アニメーションパラメータ更新
        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);

        // プレイヤー移動
        rb.velocity = moveDirection * moveSp;

        // 画面端判定
        Vector2 pos = transform.position;
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax)
        {
            // 画面内に座標を補正
            pos.x = Mathf.Clamp(pos.x, xMin, xMax);
            pos.y = Mathf.Clamp(pos.y, yMin, yMax);
            transform.position = pos;

            GameOver(); // ゲームオーバー処理
        }

        // タイマー表示を更新
        timerText.text = "Time: " + (gameOverTime - timer).ToString("F2");
    }

    // ゲームオーバー処理
    [System.Obsolete]
    void GameOver()
    {
        rb.velocity = Vector2.zero; // 動きを止める
        isGameOver = true;          // ゲームオーバー状態に

        animator.speed = 0f;        // アニメーション停止

        // スペースキーでリザルト画面へ（Updateでも処理しているのでここは不要かも）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("result");
        }
    }

    // PUSH START画像の点滅処理
    IEnumerator BlinkPushStart()
    {
        while (true)
        {
            pushStartImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            pushStartImage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // アイテム取得時の処理
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject); // アイテムを消す

            this.aud.PlayOneShot(this.appleSE); // 効果音再生

            // 移動速度アップ（2～8まで）
            if (moveSp < 8f)
            {
                moveSp += 1f;
            }
            point = 1;
            pointSum += 1;

            // スコア加算
            gameManager.GetComponent<GameManager>().AddScore(point);
        }
    }
}
