using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public TextMeshProUGUI scoreText; // InspectorでTextコンポーネントをアタッチ
    private int score = 0;

    void Start() {
        UpdateScoreText();
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
}
