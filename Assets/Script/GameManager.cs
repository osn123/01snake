using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    public TextMeshProUGUI scoreText; // InspectorでTextコンポーネントをアタッチ
    private int score = 0;

    public GameObject applePrefab; // りんごのプレハブ
    private GameObject currentApple; // 現在のりんご
    void Start() {
        UpdateScoreText();
    }

    void Update() {
        // りんごが存在しない場合、新たに生成
        if (currentApple == null) {
            SpawnApple();
        }
    }

    void SpawnApple() {
        // ランダムな位置
        Vector3 spawnPos;
        do {
        spawnPos = new Vector3(Random.Range(-3,3), Random.Range(-2,2),0);
        } while (transform.position == spawnPos);     

        currentApple = Instantiate(applePrefab,spawnPos,Quaternion.identity);
    }

    public void AddScore(int points) {
        score += points;
        UpdateScoreText();
    }

    public void ResetScore() {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText() {
        scoreText.text = "Score: " + score;
    }
    int GetScore() {
        return score;
    }
}
