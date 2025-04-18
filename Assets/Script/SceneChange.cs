using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
     SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.5f;  // �_�ł̊Ԋu�i�b�j
    public bool isBlinking = true;      // �_�ł̐���t���O
    private Coroutine blinkCoroutine;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        blinkCoroutine = StartCoroutine(BlinkSprite());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Game"); // �����ɐ؂�ւ������V�[�������L��
        }
    }

    private IEnumerator BlinkSprite() {
        while (isBlinking) {
            spriteRenderer.enabled = !spriteRenderer.enabled;  // �\���E��\����؂�ւ�
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    //public void StopBlinking() {
    //    if (blinkCoroutine != null) {
    //        StopCoroutine(blinkCoroutine);
    //        blinkCoroutine = null;
    //        spriteRenderer.enabled = true;  // �_�ŏI�����͕\����Ԃɖ߂�
    //    }
    //}
}




