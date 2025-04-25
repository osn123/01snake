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


    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ShowResultSequence());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Title"); // ここに切り替えたいシーン名を記入
        }
    }


    IEnumerator ShowResultSequence() {
        yield return new WaitForSeconds(1.0f);
        messageText.text = "あなたの集めたりんごは";
        // countText.text = "";
        //pressStartText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        text.text = PlayerController.pointSum.ToString() + "個";
        PlayerController.pointSum=0;
        //countText.text = appleCount.ToString() + "個";
        yield return new WaitForSeconds(1.0f);

        //pressStartText.gameObject.SetActive(true);
        //StartCoroutine(BlinkPushStart());

        StartCoroutine(BlinkPushStart());

    }

    // 点滅処理
    IEnumerator BlinkPushStart() {
        while (true) {
            Debug.Log("Blink!"); // 追加
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




