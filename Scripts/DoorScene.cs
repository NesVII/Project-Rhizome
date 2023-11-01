using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScene : MonoBehaviour
{
    public string sceneName; // Nom de la sc�ne de destination
    public Vector2 destinationPosition; // Coordonn�es de la porte dans la sc�ne de destination

    // G�rer le changement de sc�ne
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Appel�e lorsque la sc�ne de destination est charg�e
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Trouver la porte dans la sc�ne de destination
        GameObject door = GameObject.Find("Door");

        if (door != null)
        {
            // Placer le personnage aux coordonn�es de la porte
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.transform.position = door.transform.position + (Vector3)destinationPosition;
            }
        }

        // D�sactiver l'�coute de l'�v�nement pour �viter d'appeler cette m�thode � nouveau
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update est appel�e une fois par frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Charger la sc�ne de destination
            ChangeScene();
        }
    }
}