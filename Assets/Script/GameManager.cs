using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // スコア表示用のTextMeshProUGUI。Inspectorでアタッチする
    public TextMeshProUGUI scoreText;
    // 現在のスコア
    private int score = 0;

    // りんごのプレハブ。Inspectorでアタッチする
    public GameObject applePrefab;
    // 現在シーン上に存在するりんご
    private GameObject currentApple;

    // ゲーム開始時に呼ばれる
    void Start()
    {
        UpdateScoreText(); // スコア表示を初期化
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // りんごが存在しない場合、新しく生成する
        if (currentApple == null)
        {
            SpawnApple();
        }
    }

    // りんごをランダムな位置に生成するメソッド
    void SpawnApple()
    {
        Vector3 spawnPos;
        do
        {
            // x: -3〜3, y: -2〜2 の範囲でランダムな位置を決定
            spawnPos = new Vector3(Random.Range(-3, 4), Random.Range(-2, 3), 0);
        } while (transform.position == spawnPos); // 万が一同じ位置を避ける

        // りんごプレハブを指定した位置に生成
        currentApple = Instantiate(applePrefab, spawnPos, Quaternion.identity);
    }

    // スコアを加算し、表示を更新するメソッド
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // スコアをリセットし、表示を更新するメソッド
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // スコア表示を更新するメソッド
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // 現在のスコアを取得するメソッド（外部から呼び出し用）
    int GetScore()
    {
        return score;
    }
}
