using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public GameObject joueur;
    public CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == joueur)
        {
            virtualCamera.Follow = object2;
        }
    }


}
    