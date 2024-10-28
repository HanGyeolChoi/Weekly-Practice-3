using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 80f;
    public LayerMask groundLayerMask;
    private Vector2 curMovementInput;

    public Transform cameraContainer;
    public float minXRot;
    public float maxXRot;
    private float curXRot;
    public float cameraSensitivity;

    private Vector2 mouseDelta;

    private float timeSinceLastDash;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Move();
        if(timeSinceLastDash > 0)
        {
            timeSinceLastDash -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= speed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void CameraLook()
    {
        curXRot += mouseDelta.y * cameraSensitivity;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
        cameraContainer.localEulerAngles = new Vector3(-curXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * cameraSensitivity, 0);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays =
        {
            new Ray(transform.position + transform.forward * 0.2f, Vector3.down),
            new Ray(transform.position - transform.forward * 0.2f, Vector3.down),
            new Ray(transform.position + transform.right * 0.2f, Vector3.down),
            new Ray(transform.position - transform.right * 0.2f, Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return false;
            }
        }
        return true;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && CanDash())
        {
            Dash();
        }
    }
    private bool CanDash()
    {
        return timeSinceLastDash == 0;
    }

    private void Dash()
    {
        
    }
}