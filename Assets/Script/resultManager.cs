using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class resultManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.5f;  // �_�ł̊Ԋu�i�b�j
    public bool isBlinking = true;      // �_�ł̐���t���O
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

        messageText.text = "���Ȃ��̏W�߂���񂲂�";
        messageText.gameObject.SetActive(true);
        this.aud.PlayOneShot(this.appleSE);

        yield return new WaitForSeconds(1.0f);

        pointText.text = PlayerController.pointSum.ToString() + "��";
        pointText.gameObject.SetActive(true);
        this.aud.PlayOneShot(this.appleSE);
        PlayerController.pointSum = 0;

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(BlinkPushStart());

    }

    // �_�ŏ���
    IEnumerator BlinkPushStart() {
        while (true) {

            pushStartImage.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            pushStartImage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
