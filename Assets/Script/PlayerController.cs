using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {

    // ��ʒ[�̋��E�l�i���[���h���W�j
    private float xMin, xMax, yMin, yMax;

    [SerializeField] float moveSp = 5f;
    [SerializeField] private GameObject gameManager;

    [SerializeField] private TextMeshProUGUI timerText;  // �ǉ��F�^�C�}�[�\���p�� TextMeshProUGUI �ϐ�

    Animator animator;
    Rigidbody2D rb;

    private Vector2 moveDirection = Vector2.down; // ��������

    [SerializeField] float gameOverTime = 20f; // �Q�[���I�[�o�[�ɂȂ�܂ł̎��ԁi�b�j
    private float timer = 0f;

    bool isStart;
    int point=0;

    void Awake() {
        // �J�����̃r���[�|�[�g���烏�[���h���W���擾���ċ��E��ݒ�
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // �L�����N�^�[�̃T�C�Y���擾�i��: SpriteRenderer���g���Ă���ꍇ�j
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 charExtents = sr.bounds.extents*2;  // �����̃T�C�Y�i���E�����j

        xMin = bottomLeft.x;
        yMin = bottomLeft.y + charExtents.y;
        xMax = topRight.x - charExtents.x;
        yMax = topRight.y;

        animator = GetComponent<Animator>();
        if (animator == null) {
            Debug.LogError("Animator��������܂���I");
        }

        rb = GetComponent<Rigidbody2D>();

    }

    void Start() {
        isStart = false; // �Q�[���J�n�t���O��������
        animator.speed = 0f; // �A�j���[�V�������~
    }

    [System.Obsolete]
    void Update() {
        if (!isStart) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                isStart = true;
                timer = 0f; // �Q�[���J�n���Ƀ^�C�}�[�����Z�b�g

                animator.speed = 1f; // �A�j���[�V�������ĊJ
                transform.position = new Vector3(0,0,0); // ��F�����ʒu��(0, 0, 0)�ɐݒ�
                moveDirection = Vector2.down;
                moveSp = 5f;
                gameManager.GetComponent<ScoreManager>().ResetScore();                
            }
            return;
        }

        timer += Time.deltaTime; // ���t���[���^�C�}�[���X�V

        // ���Ԍo�߂ŃQ�[���I�[�o�[����
        if (timer >= gameOverTime) {
            GameOver();
            return; // �^�C�}�[�ɂ��Q�[���I�[�o�[������������A�ȍ~�� Update �����͍s��Ȃ�
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDirection = Vector2.right;

        animator.SetFloat("MoveX",moveDirection.x);
        animator.SetFloat("MoveY",moveDirection.y);

        rb.velocity = moveDirection * moveSp;

        // ��ʒ[����
        Vector2 pos = transform.position;
        if (pos.x < xMin || pos.x > xMax || pos.y < yMin || pos.y > yMax) {
            GameOver();
        }

        timerText.text = "Time: " + (gameOverTime - timer).ToString("F2");  // �ǉ��F�^�C�}�[��TextMeshProUGUI�ɕ\��

    }

    [System.Obsolete]
    void GameOver() {
        // �Q�[���I�[�o�[����
        Debug.Log("�Q�[���I�[�o�[�I");
        rb.velocity = Vector2.zero; // �������~�߂�
        isStart = false;

        animator.speed = 0f; // �A�j���[�V�������~
                             // �����ɃQ�[���I�[�o�[��ʂ̕\���⃊�X�^�[�g������ǉ����Ă�������
                             // ��: SceneManager.LoadScene("GameOverScene");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Item")) {
            //Destroy(other.gameObject); // �A�C�e��������
            //Debug.Log("�A�C�e��������!");

            int px =Random.Range(-3,3);
            int py =Random.Range(-2,2);

            other.transform.position = new Vector3(px,py,0);
            if (moveSp < 10f) {
                moveSp*=1.2f;
            }
            point = 100;

            gameManager.GetComponent<ScoreManager>().AddScore(point);

        }
    }

}
