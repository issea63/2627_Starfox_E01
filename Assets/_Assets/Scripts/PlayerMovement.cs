using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Essential")]
    public bool MaxiIsGay = true;
    public bool JonIsGay = true;

    [Header("Objects")]
    public GameObject aimObject;
    public Transform shipObject;

    [Header ("Stats")]
    public float xySpeed = 10f;
    public float rotationSpeed = 100f;
    public float lerpSpeed = .1f;
    public float axisMultiplier = 1f;

    [Header("Viewport Stats")]
    public float wireRadius = .5f;

    [SerializeField]
    InputActionReference moveAction;
    
    void Start()
    {
        MaxiIsGay = true;
        if(MaxiIsGay)
        {
            JonIsGay = true;
        }
        
        else JonIsGay = false;
    }
    void Update()
    {
        MaxiIsGay = true;

        Vector2 xyVector = moveAction.action.ReadValue<Vector2>();
        LocalMove(xyVector.x,xyVector.y, xySpeed);
        ClampPosition();
        RotationLook(xyVector.x, xyVector.y, rotationSpeed);
        HorizontalTilt(shipObject, xyVector.x,axisMultiplier, lerpSpeed);
    }
    public void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }
    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    void RotationLook(float h, float v, float speed)
    {
        aimObject.transform.localPosition = new Vector3(h, v, 2);
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimObject.transform.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }
    void HorizontalTilt(Transform target, float axis,float tiltMultiplier, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3 (targetEulerAngles.x,targetEulerAngles.y,Mathf.LerpAngle(targetEulerAngles.z, -axis * tiltMultiplier, Time.deltaTime * lerpTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(aimObject.transform.position, wireRadius);
        Gizmos.DrawSphere(aimObject.transform.position, wireRadius-0.35f);
    }
}
