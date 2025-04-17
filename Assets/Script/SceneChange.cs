using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game"); // ‚±‚±‚ÉØ‚è‘Ö‚¦‚½‚¢ƒV[ƒ“–¼‚ğ‹L“ü
        }
    }
}

