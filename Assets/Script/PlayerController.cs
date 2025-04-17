using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] float speed = 5f;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float moveY = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        // 画面内の範囲を指定（例: X軸-7.0～7.0、Y軸-4.5～4.5）
        float clampedX = Mathf.Clamp(transform.position.x + moveX, -4.0f, 3.3f);
        float clampedY = Mathf.Clamp(transform.position.y + moveY, -2.3f, 3.0f);

        transform.position = new Vector2(clampedX, clampedY);
     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject); // アイテムを消す
            Debug.Log("アイテムを消す!");
        }
    }

}
