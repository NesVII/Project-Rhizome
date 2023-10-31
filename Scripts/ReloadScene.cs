using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
    public GameObject fadeToBlack;
    public GameObject cv;
    public GameObject cv2;

    private void Start()
    {
        /*fadeToBlack.enabled = false;
        cv.enabled = false;
        cv2.enabled = false;*/
        fadeToBlack.SetActive(false);
        cv.SetActive(false);
        //cv2.SetActive(false);
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}