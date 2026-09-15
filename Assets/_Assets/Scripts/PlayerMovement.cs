using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [Header("Essential")]
    public bool MaxiIsGay = true;
    public bool JonIsGay = true;

    [Header("Objects")]
    public GameObject cameraHolder;
    public GameObject aimObject;
    public Transform shipObject;
    public CinemachineSplineCart splineCart;
    public TrailRenderer trailRenderer;

    [Header ("Stats")]
    public float xySpeed = 10f;
    public float rotationSpeed = 100f;
    public float lerpSpeed = .1f;
    public float axisMultiplier = 1f;
    public float zSpeed = .5f;
    public float FOVValue;
    public float zoomValue;
    public float zoomDuration;

    [Header("Viewport Stats")]
    public float wireRadius = .5f;

    [SerializeField]
    InputActionReference moveAction;
    
    void Start()
    {
        if(MaxiIsGay)
        {
            JonIsGay = true;
        }
        
        else JonIsGay = false;

        if(JonIsGay == false)
        {
            xySpeed = 0f;
            rotationSpeed = 0f;
            lerpSpeed = 0f;
            axisMultiplier = 0f;
            zSpeed = 0f;
        }


        FOV(FOVValue);

        trailRenderer.emitting = false;

        SetSpeed(zSpeed);
    }
    void Update()
    {
        //MaxiIsGay = true;
        FOV(FOVValue);

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
        aimObject.transform.parent.position = Vector3.zero;
        aimObject.transform.localPosition = new Vector3(h, v, 2);
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimObject.transform.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }
    void HorizontalTilt(Transform target, float axis,float tiltMultiplier, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3 (targetEulerAngles.x,targetEulerAngles.y,Mathf.LerpAngle(targetEulerAngles.z, -axis * tiltMultiplier, Time.deltaTime * lerpTime));
    }

    void SetSpeed(float zSpeed)
    {
       var cartSpeed = splineCart.AutomaticDolly.Method as SplineAutoDolly.FixedSpeed;
        cartSpeed.Speed = zSpeed;
    }
    public void Boost(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SetSpeed(zSpeed * 3.5f);
            trailRenderer.emitting = true;
        }
        else if(context.canceled)
        {
            SetSpeed(zSpeed);
            trailRenderer.emitting = false;
        }
    }
    void FOV(float FOVnumber)
    {
        cameraHolder.GetComponentInChildren<CinemachineCamera>().Lens.FieldOfView = FOVnumber;
    }
    void SetCameraZoom(float zoom, float duration)
    {
        cameraHolder.transform.DOLocalMoveZ(zoom, duration);
    }
    void Chromatic(float cValue)
    {
        Camera.main.GetComponent<Volume>().profile.TryGet(out ChromaticAberration c);
        c.intensity.value = cValue;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(aimObject.transform.position, wireRadius);
        Gizmos.DrawSphere(aimObject.transform.position, wireRadius-0.35f);
    }
}
