using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// リザルト画面の管理クラス
public class resultManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer; // スプライトのレンダラー
    public float blinkInterval = 0.5f;  // 点滅の間隔（秒）
    public bool isBlinking = true;      // 点滅の制御フラグ
    private Coroutine blinkCoroutine;   // コルーチン制御用

    [SerializeField] GameObject pushStartImage; // 「Push Start」画像
    public Text messageText;    // メッセージテキスト
    public Text pointText;      // ポイント表示テキスト
    AudioSource aud;            // オーディオソース
    public AudioClip appleSE;   // 効果音（りんご取得音）

    // 初期化処理
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // スプライトレンダラー取得

        StartCoroutine(ShowResultSequence()); // リザルト表示シーケンス開始
        this.aud = GetComponent<AudioSource>(); // オーディオソース取得
        messageText.gameObject.SetActive(false); // メッセージ非表示
        pointText.gameObject.SetActive(false);   // ポイント非表示
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // スペースキーでタイトル画面に戻る
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title");
        }
        // エスケープキーでアプリ終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // リザルト表示の流れを管理するコルーチン
    IEnumerator ShowResultSequence()
    {
        yield return new WaitForSeconds(1.0f); // 1秒待機

        messageText.text = "あなたの集めたりんごは"; // メッセージ表示
        messageText.gameObject.SetActive(true); // メッセージ表示ON
        this.aud.PlayOneShot(this.appleSE);     // 効果音再生

        yield return new WaitForSeconds(1.0f); // 1秒待機

        pointText.text = PlayerController.pointSum.ToString() + "個"; // ポイント表示
        pointText.gameObject.SetActive(true); // ポイント表示ON
        this.aud.PlayOneShot(this.appleSE);   // 効果音再生
        PlayerController.pointSum = 0;        // ポイントリセット

        yield return new WaitForSeconds(1.0f); // 1秒待機

        StartCoroutine(BlinkPushStart());      // 「Push Start」点滅開始
    }

    // 「Push Start」画像を点滅させるコルーチン
    IEnumerator BlinkPushStart()
    {
        while (true)
        {
            pushStartImage.SetActive(true);   // 画像表示
            yield return new WaitForSeconds(0.5f); // 0.5秒待機
            pushStartImage.SetActive(false);  // 画像非表示
            yield return new WaitForSeconds(0.5f); // 0.5秒待機
        }
    }
}
