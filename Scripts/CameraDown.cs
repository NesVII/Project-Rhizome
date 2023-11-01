using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraDown : MonoBehaviour
{

    public Animator animatorCam;

    private void Awake()
    {
        animatorCam = GetComponent<Animator>();
    }

    public void LookDown(InputAction.CallbackContext context)
    {
         if (context.performed)
         {
            Debug.Log("late ?");
            animatorCam.SetBool("IsLookingDown", true);
         }
         else if (context.canceled)
         {
            animatorCam.SetBool("IsLookingDown", false);
         }
    }



}
