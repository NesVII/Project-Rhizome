using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScene : MonoBehaviour
{
    public string sceneName; // Nom de la scène de destination
    public Vector2 destinationPosition; // Coordonnées de la porte dans la scène de destination

    // Gérer le changement de scène
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Appelée lorsque la scène de destination est chargée
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Trouver la porte dans la scène de destination
        GameObject door = GameObject.Find("Door");

        if (door != null)
        {
            // Placer le personnage aux coordonnées de la porte
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.transform.position = door.transform.position + (Vector3)destinationPosition;
            }
        }

        // Désactiver l'écoute de l'événement pour éviter d'appeler cette méthode à nouveau
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update est appelée une fois par frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Charger la scène de destination
            ChangeScene();
        }
    }
}