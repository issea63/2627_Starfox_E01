using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour
{
    [Header ("Objects")]
    public GameObject playerBullet;
    public AudioSource shootSound;
    public Transform bulletOrigin;
    public Transform playerTransform;
    [Header("Stats")]
    public float rayDistance;
    public float wireRadius = 10f;
    public LayerMask enemyLayer;
    [Header ("Stats")]
    public PlayerMovement playerMovement;
    public bool isEnemyInRange;

    [SerializeField]
    InputActionReference moveAction;

    private void Update()
    {
        AimAssist();
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            shootSound.pitch = Random.Range(.8f, 2.1f);
            shootSound.Play();
            GameObject newBullet = Instantiate(playerBullet, bulletOrigin.position, UnityEngine.Quaternion.identity);
            newBullet.transform.forward = bulletOrigin.forward;
        }
    }
    public void AimAssist()
    {
        UnityEngine.Vector2 xyVector = moveAction.action.ReadValue<UnityEngine.Vector2>();
        Ray ray = new Ray(bulletOrigin.position, bulletOrigin.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, enemyLayer))
        {
            Debug.Log("RayHit");
            //hit.collider.gameObject.GetComponent<Transform>();
            Vector3 direction = (hit.collider.transform.localPosition - gameObject.transform.localPosition).normalized;
            playerMovement.RotationLook(hit.collider.transform.localPosition.x, hit.collider.transform.localPosition.y, playerMovement.rotationSpeed);

        }
        else playerMovement.RotationLook(xyVector.x, xyVector.y, playerMovement.rotationSpeed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Ray ray = new Ray(bulletOrigin.position, bulletOrigin.forward);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance);
        Gizmos.color = Color.purple;
    }
}
