using UnityEngine;
using UnityEngine.UI;

public class Affichage : MonoBehaviour {
    private Text textComponent;
    public Canvas cv;

    void Start() {
        cv.enabled = false;
        Debug.Log("test");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            cv.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            cv.enabled = false;
        }
    }
}