using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
     SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.5f;  // 点滅の間隔（秒）
    public bool isBlinking = true;      // 点滅の制御フラグ
    private Coroutine blinkCoroutine;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        blinkCoroutine = StartCoroutine(BlinkSprite());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Game"); // ここに切り替えたいシーン名を記入
        }
    }

    private IEnumerator BlinkSprite() {
        while (isBlinking) {
            spriteRenderer.enabled = !spriteRenderer.enabled;  // 表示・非表示を切り替え
            yield return new WaitForSeconds(blinkInterval);
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




