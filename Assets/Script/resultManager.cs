using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class resultManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.5f;  // 点滅の間隔（秒）
    public bool isBlinking = true;      // 点滅の制御フラグ
    private Coroutine blinkCoroutine;

    [SerializeField] GameObject pushStartImage;
    public Text messageText;
    public Text pointText;
    AudioSource aud;
    public AudioClip appleSE;


    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ShowResultSequence());
        this.aud = GetComponent<AudioSource>();
        messageText.gameObject.SetActive(false);
        pointText.gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Title"); 
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }


    IEnumerator ShowResultSequence() {
        yield return new WaitForSeconds(1.0f);

        messageText.text = "あなたの集めたりんごは";
        messageText.gameObject.SetActive(true);
        this.aud.PlayOneShot(this.appleSE);

        yield return new WaitForSeconds(1.0f);

        pointText.text = PlayerController.pointSum.ToString() + "個";
        pointText.gameObject.SetActive(true);
        this.aud.PlayOneShot(this.appleSE);
        PlayerController.pointSum = 0;

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
}
