using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���U���g��ʂ̊Ǘ��N���X
public class resultManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer; // �X�v���C�g�̃����_���[
    public float blinkInterval = 0.5f;  // �_�ł̊Ԋu�i�b�j
    public bool isBlinking = true;      // �_�ł̐���t���O
    private Coroutine blinkCoroutine;   // �R���[�`������p

    [SerializeField] GameObject pushStartImage; // �uPush Start�v�摜
    public Text messageText;    // ���b�Z�[�W�e�L�X�g
    public Text pointText;      // �|�C���g�\���e�L�X�g
    AudioSource aud;            // �I�[�f�B�I�\�[�X
    public AudioClip appleSE;   // ���ʉ��i��񂲎擾���j

    // ����������
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // �X�v���C�g�����_���[�擾

        StartCoroutine(ShowResultSequence()); // ���U���g�\���V�[�P���X�J�n
        this.aud = GetComponent<AudioSource>(); // �I�[�f�B�I�\�[�X�擾
        messageText.gameObject.SetActive(false); // ���b�Z�[�W��\��
        pointText.gameObject.SetActive(false);   // �|�C���g��\��
    }

    // ���t���[���Ă΂��
    void Update()
    {
        // �X�y�[�X�L�[�Ń^�C�g����ʂɖ߂�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Title");
        }
        // �G�X�P�[�v�L�[�ŃA�v���I��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // ���U���g�\���̗�����Ǘ�����R���[�`��
    IEnumerator ShowResultSequence()
    {
        yield return new WaitForSeconds(1.0f); // 1�b�ҋ@

        messageText.text = "���Ȃ��̏W�߂���񂲂�"; // ���b�Z�[�W�\��
        messageText.gameObject.SetActive(true); // ���b�Z�[�W�\��ON
        this.aud.PlayOneShot(this.appleSE);     // ���ʉ��Đ�

        yield return new WaitForSeconds(1.0f); // 1�b�ҋ@

        pointText.text = PlayerController.pointSum.ToString() + "��"; // �|�C���g�\��
        pointText.gameObject.SetActive(true); // �|�C���g�\��ON
        this.aud.PlayOneShot(this.appleSE);   // ���ʉ��Đ�
        PlayerController.pointSum = 0;        // �|�C���g���Z�b�g

        yield return new WaitForSeconds(1.0f); // 1�b�ҋ@

        StartCoroutine(BlinkPushStart());      // �uPush Start�v�_�ŊJ�n
    }

    // �uPush Start�v�摜��_�ł�����R���[�`��
    IEnumerator BlinkPushStart()
    {
        while (true)
        {
            pushStartImage.SetActive(true);   // �摜�\��
            yield return new WaitForSeconds(0.5f); // 0.5�b�ҋ@
            pushStartImage.SetActive(false);  // �摜��\��
            yield return new WaitForSeconds(0.5f); // 0.5�b�ҋ@
        }
    }
}
