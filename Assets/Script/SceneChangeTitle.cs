using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;

public class SceneChangeTitle : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.5f;  // 点滅の間隔（秒）
    public bool isBlinking = true;      // 点滅の制御フラグ
    private Coroutine blinkCoroutine;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] GameObject pushStartImage;
    [SerializeField] GameObject pressStartText;
    public Text messageText;
    AudioSource aud;
    public AudioClip appleSE;


    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ShowResultSequence());
        this.aud = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Title"); // ここに切り替えたいシーン名を記入
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
        }
    }


    IEnumerator ShowResultSequence() {
        yield return new WaitForSeconds(1.0f);

        this.aud.PlayOneShot(this.appleSE);
        messageText.text = "あなたの集めたりんごは";

        yield return new WaitForSeconds(1.0f);

        this.aud.PlayOneShot(this.appleSE);
        text.text = PlayerController.pointSum.ToString() + "個";
        PlayerController.pointSum=0;

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(BlinkPushStart());

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

    //public void StopBlinking() {
    //    if (blinkCoroutine != null) {
    //        StopCoroutine(blinkCoroutine);
    //        blinkCoroutine = null;
    //        spriteRenderer.enabled = true;  // 点滅終了時は表示状態に戻す
    //    }
    //}
}




