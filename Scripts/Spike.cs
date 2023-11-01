using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spike : MonoBehaviour
{
    public Transform RespawnPoint;

    public float teleportDelay = 0.5f;

    private Canvas cv;
    private Image fadeImage;

    private void Start()
    {
        GameObject cvObject = GameObject.Find("FadeToBlack");
        cv = cvObject.GetComponent<Canvas>();
        Transform panelTransform = cvObject.transform.GetChild(0);
        fadeImage = panelTransform.GetChild(0).GetComponent<Image>();
        cv.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cv.enabled = true;
            StartCoroutine(TeleportPlayerWithDelay(collision.gameObject));
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }

    private IEnumerator TeleportPlayerWithDelay(GameObject player)
    {
        // Appliquer le fondu au noir
        fadeImage.color = Color.black;
        fadeImage.canvasRenderer.SetAlpha(0);
        fadeImage.CrossFadeAlpha(1, teleportDelay, false);

        // Attendre le d�lai sp�cifi�
        yield return new WaitForSeconds(teleportDelay);

        // T�l�porter le joueur
        player.transform.position = RespawnPoint.position;

        // R�tablir la visibilit� en fondu depuis le noir
        fadeImage.CrossFadeAlpha(0, teleportDelay, false);
        Invoke("Disable", teleportDelay);
    }

    private void Disable()
    {
        cv.enabled = false;
    }
}
