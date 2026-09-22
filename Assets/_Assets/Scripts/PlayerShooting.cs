using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header ("Object Reference")]
    public GameObject playerBullet;
    public AudioSource shootSound;
    public Transform bulletOrigin;

    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            shootSound.pitch = Random.Range(.8f, 2.1f);
            shootSound.Play();
            GameObject newBullet = Instantiate(playerBullet, bulletOrigin.position, Quaternion.identity);
            newBullet.transform.forward = bulletOrigin.forward;
        }
    }
}
