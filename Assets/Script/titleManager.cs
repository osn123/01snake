using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;         // スプライトの描画コンポーネント
    public float blinkInterval = 0.5f;     // 点滅の間隔（秒）
    public bool isBlinking = true;         // 点滅を行うかどうかのフラグ
    private Coroutine blinkCoroutine;      // コルーチンの参照を保持

    // ゲーム開始時に呼ばれる
    private void Start()
    {
        // このオブジェクトのSpriteRendererコンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 点滅処理を開始
        blinkCoroutine = StartCoroutine(BlinkSprite());
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // スペースキーが押されたら「Game」シーンに切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game"); // ここに切り替えたいシーン名を記入
        }
        // エスケープキーが押されたらアプリケーションを終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // スプライトの点滅を制御するコルーチン
    private IEnumerator BlinkSprite()
    {
        while (isBlinking)
        {
            // スプライトの表示・非表示を切り替える
            spriteRenderer.enabled = !spriteRenderer.enabled;
            // 指定した間隔だけ待機
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    //// 点滅を停止するメソッド（必要に応じて利用）
    //public void StopBlinking() {
    //    if (blinkCoroutine != null) {
    //        StopCoroutine(blinkCoroutine);
    //        blinkCoroutine = null;
    //        spriteRenderer.enabled = true;  // 点滅終了時は表示状態に戻す
    //    }
    //}
}
