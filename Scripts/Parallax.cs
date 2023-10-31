using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    public Transform[] layers;                      // Tableau des différentes couches de parallaxe
    public float[] parallaxScales;                   // Facteurs de vitesse de déplacement des couches de parallaxe
    public float smoothing = 1f;                     // Facteur de lissage du déplacement des couches de parallaxe
    public Transform player;                         // Référence au joueur
    public CinemachineVirtualCamera virtualCamera;   // Référence à la caméra virtuelle Cinemachine

    private Vector3[] initialPositions;               // Positions initiales des couches de parallaxe
    private Vector3 previousPlayerPosition;          // Position précédente du joueur
    private Vector3 previousCameraPosition;          // Position précédente de la caméra

    void Start()
    {
        // Enregistre les positions initiales des couches de parallaxe
        initialPositions = new Vector3[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            initialPositions[i] = layers[i].position;
        }

        // Enregistre les positions initiales du joueur et de la caméra
        previousPlayerPosition = player.position;
        previousCameraPosition = virtualCamera.transform.position;
    }

    void LateUpdate()
    {
        // Calcule le déplacement du joueur depuis la frame précédente
        Vector3 playerMovement = player.position - previousPlayerPosition;

        // Calcule le déplacement de la caméra depuis la frame précédente
        Vector3 cameraMovement = virtualCamera.transform.position - previousCameraPosition;

        for (int i = 0; i < layers.Length; i++)
        {
            // Calcule le déplacement sur l'axe X en fonction du mouvement du joueur et de la caméra
            float parallaxX = (playerMovement.x + cameraMovement.x) * parallaxScales[i];
            float parallaxY = 0f; // Aucun déplacement sur l'axe Y

            // Calcule la position cible de la couche de parallaxe
            Vector3 backgroundTargetPos = initialPositions[i] + new Vector3(parallaxX, parallaxY, 0f);

            // Lisse le déplacement de la couche de parallaxe vers la position cible
            layers[i].position = Vector3.Lerp(layers[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Met à jour les positions précédentes du joueur et de la caméra
        previousPlayerPosition = player.position;
        previousCameraPosition = virtualCamera.transform.position;
    }
}
