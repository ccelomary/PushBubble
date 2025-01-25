using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Sprite Muted, Sound;
    [SerializeField]
    Image audioImage;

    AudioSource backgroundAudio;
    bool isMuted = false;

    private void Start()
    {
        backgroundAudio = GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AdjustSound()
    {
        if (isMuted)
        {
            backgroundAudio.volume = 1.0f;
            audioImage.sprite = Muted;
        }
        else
        {
            backgroundAudio.volume = 0.0f;
            audioImage.sprite = Sound;
        }
        isMuted = !isMuted;
    }
}
