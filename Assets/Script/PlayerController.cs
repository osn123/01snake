using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // 画面端の境界値（ワールド座標）
    private float xMin, xMax, yMin, yMax;

    [SerializeField] float moveSp = 3f;
    [SerializeField] private GameObject gameManager;

    [SerializeField] private TextMeshProUGUI timerText;  // 追加：タイマー表示用の TextMeshProUGUI 変数

    Animator animator;
    Rigidbody2D rb;

    private Vector2 moveDirection = Vector2.down; // 初期方向

    [SerializeField] float gameOverTime = 20f; // ゲームオーバーになるまでの時間（秒）
    private float timer = 0f;

    bool isStart;
    bool isGameOver;
    int point = 0;
    public static int pointSum = 0;

    [SerializeField] GameObject gameOverImage;
    [SerializeField] GameObject pushStartImage;

    public AudioClip appleSE;
    AudioSource aud;

    private Coroutine blinkCoroutine;

    public AudioClip bgmClip;
    private AudioSource audioSource;


    void Awake() {
        // カメラのビューポートからワールド座標を取得して境界を設定
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // キャラクターのサイズを取得（例: SpriteRendererを使っている場合）
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 charExtents = sr.bounds.extents * 2;  // 半分のサイズ（幅・高さ）

        xMin = bottomLeft.x;
        yMin = bottomLeft.y + charExtents.y;
        xMax = topRight.x - charExtents.x;
        yMax = topRight.y;

        animator = GetComponent<Animator>();
        if (animator == null) {
            Debug.LogError("Animatorが見つかりません！");
        }

        rb = GetComponent<Rigidbody2D>();

    }

    void Start() {
        isStart = false; // ゲーム開始フラグを初期化
        isGameOver = false; // 
        //animator.speed = 0f; // アニメーションを停止
        this.aud = GetComponent<AudioSource>();
        pushStartImage.SetActive(false);
        gameOverImage.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.Play();
    }

    [System.Obsolete]
    void Update() {
        //if (!isStart) {
        //    if (Input.GetKeyDown(KeyCode.Space)) {
        //        isStart = true;
        //        timer = 0f; // ゲーム開始時にタイマーをリセット
        //        gameManager.GetComponent<ScoreManager>().ResetScore();

        //        animator.speed = 1f; // アニメーションを再開
        //        transform.position = new Vector3(0, 0, 0); // 例：初期位置を(0, 0, 0)に設定
        //        moveDirection = Vector2.down;
        //        moveSp = 5f;
        //    }
        //    return;
        //}

        if (isGameOver) {

            gameOverImage.SetActive(true);

            if (isGameOver && blinkCoroutine == null) {
                blinkCoroutine = StartCoroutine(BlinkPushStart());
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                //isStart = true;
                //timer = 0f; // ゲーム開始時にタイマーをリセット
                //gameManager.GetComponent<ScoreManager>().ResetScore();

                //animator.speed = 1f; // アニメーションを再開
                //transform.position = new Vector3(0, 0, 0); // 例：初期位置を(0, 0, 0)に設定
                //moveDirection = Vector2.down;
                //moveSp = 5f;

                SceneManager.LoadScene("result"); // ここに切り替えたいシーン名を記入

            }
            return;
        }

        timer += Time.deltaTime; // 毎フレームタイマーを更新

        // 時間経過でゲームオーバー判定
        if (timer >= gameOverTime) {
            GameOver();
            return; // タイマーによるゲームオーバーが発生したら、以降の Update 処理は行わない
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDirection = Vector2.right;

        animator.SetFloat("MoveX",moveDirection.x);
        animator.SetFloat("MoveY",moveDirection.y);

        rb.velocity = moveDirection * moveSp;

        // 画面端判定
        Vector2 pos = transform.position;
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax) {
            GameOver();
        }

        timerText.text = "Time: " + (gameOverTime - timer).ToString("F2");  // 追加：タイマーをTextMeshProUGUIに表示

    }

    [System.Obsolete]
    void GameOver() {
        // ゲームオーバー処理
        Debug.Log("ゲームオーバー！");
        rb.velocity = Vector2.zero; // 動きを止める
        isStart = false;
        isGameOver = true;

        animator.speed = 0f; // アニメーションを停止
                             // ここにゲームオーバー画面の表示やリスタート処理を追加してください
                             // 例: SceneManager.LoadScene("GameOverScene");

        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("result"); // ここに切り替えたいシーン名を記入
        }
    }

    // 点滅処理
    IEnumerator BlinkPushStart() {
        while (true) {
            pushStartImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            pushStartImage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Item")) {
            //Destroy(other.gameObject); // アイテムを消す
            //Debug.Log("アイテムを消す!");

            int px = Random.Range(-3, 3);
            int py = Random.Range(-2, 2);

            this.aud.PlayOneShot(this.appleSE);

            other.transform.position = new Vector3(px,py,0);
            if (moveSp < 10f) {
                moveSp += 1f;
            }
            point = 1;
            pointSum += 1;

            gameManager.GetComponent<ScoreManager>().AddScore(point);


        }
    }

}
