using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;         // �X�v���C�g�̕`��R���|�[�l���g
    public float blinkInterval = 0.5f;     // �_�ł̊Ԋu�i�b�j
    public bool isBlinking = true;         // �_�ł��s�����ǂ����̃t���O
    private Coroutine blinkCoroutine;      // �R���[�`���̎Q�Ƃ�ێ�

    // �Q�[���J�n���ɌĂ΂��
    private void Start()
    {
        // ���̃I�u�W�F�N�g��SpriteRenderer�R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();
        // �_�ŏ������J�n
        blinkCoroutine = StartCoroutine(BlinkSprite());
    }

    // ���t���[���Ă΂��
    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��uGame�v�V�[���ɐ؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game"); // �����ɐ؂�ւ������V�[�������L��
        }
        // �G�X�P�[�v�L�[�������ꂽ��A�v���P�[�V�������I��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // �X�v���C�g�̓_�ł𐧌䂷��R���[�`��
    private IEnumerator BlinkSprite()
    {
        while (isBlinking)
        {
            // �X�v���C�g�̕\���E��\����؂�ւ���
            spriteRenderer.enabled = !spriteRenderer.enabled;
            // �w�肵���Ԋu�����ҋ@
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    //// �_�ł��~���郁�\�b�h�i�K�v�ɉ����ė��p�j
    //public void StopBlinking() {
    //    if (blinkCoroutine != null) {
    //        StopCoroutine(blinkCoroutine);
    //        blinkCoroutine = null;
    //        spriteRenderer.enabled = true;  // �_�ŏI�����͕\����Ԃɖ߂�
    //    }
    //}
}
