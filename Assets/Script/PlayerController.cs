using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] float speed = 5f;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float moveY = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        // ��ʓ��͈̔͂��w��i��: X��-7.0�`7.0�AY��-4.5�`4.5�j
        float clampedX = Mathf.Clamp(transform.position.x + moveX, -4.0f, 3.3f);
        float clampedY = Mathf.Clamp(transform.position.y + moveY, -2.3f, 3.0f);

        transform.position = new Vector2(clampedX, clampedY);
     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject); // �A�C�e��������
            Debug.Log("�A�C�e��������!");
        }
    }

}
