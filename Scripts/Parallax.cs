using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    public Transform[] layers;                      // Tableau des diff�rentes couches de parallaxe
    public float[] parallaxScales;                   // Facteurs de vitesse de d�placement des couches de parallaxe
    public float smoothing = 1f;                     // Facteur de lissage du d�placement des couches de parallaxe
    public Transform player;                         // R�f�rence au joueur
    public CinemachineVirtualCamera virtualCamera;   // R�f�rence � la cam�ra virtuelle Cinemachine

    private Vector3[] initialPositions;               // Positions initiales des couches de parallaxe
    private Vector3 previousPlayerPosition;          // Position pr�c�dente du joueur
    private Vector3 previousCameraPosition;          // Position pr�c�dente de la cam�ra

    void Start()
    {
        // Enregistre les positions initiales des couches de parallaxe
        initialPositions = new Vector3[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            initialPositions[i] = layers[i].position;
        }

        // Enregistre les positions initiales du joueur et de la cam�ra
        previousPlayerPosition = player.position;
        previousCameraPosition = virtualCamera.transform.position;
    }

    void LateUpdate()
    {
        // Calcule le d�placement du joueur depuis la frame pr�c�dente
        Vector3 playerMovement = player.position - previousPlayerPosition;

        // Calcule le d�placement de la cam�ra depuis la frame pr�c�dente
        Vector3 cameraMovement = virtualCamera.transform.position - previousCameraPosition;

        for (int i = 0; i < layers.Length; i++)
        {
            // Calcule le d�placement sur l'axe X en fonction du mouvement du joueur et de la cam�ra
            float parallaxX = (playerMovement.x + cameraMovement.x) * parallaxScales[i];
            float parallaxY = 0f; // Aucun d�placement sur l'axe Y

            // Calcule la position cible de la couche de parallaxe
            Vector3 backgroundTargetPos = initialPositions[i] + new Vector3(parallaxX, parallaxY, 0f);

            // Lisse le d�placement de la couche de parallaxe vers la position cible
            layers[i].position = Vector3.Lerp(layers[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Met � jour les positions pr�c�dentes du joueur et de la cam�ra
        previousPlayerPosition = player.position;
        previousCameraPosition = virtualCamera.transform.position;
    }
}
