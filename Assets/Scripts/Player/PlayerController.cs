using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float initialSpeed = 5f;
    public float speed = 5f;
    public float dashSpeed = 10f;
    public float dashMana = 30f;
    public float dashDuration = 5f;
    public float dashCooltime = 12f;
    public float jumpForce = 80f;
    public LayerMask groundLayerMask;
    private Vector2 curMovementInput;

    [Header("Camera")]
    public Transform cameraContainer;
    public float minXRot;
    public float maxXRot;
    private float curXRot;
    public float cameraSensitivity;

    private Vector2 mouseDelta;

    private float timeSinceLastDash = float.MinValue;

    private Rigidbody _rigidbody;

    private bool canLook = true;
    public event Action<float> UseDash;
    public event Action option;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        speed = initialSpeed;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
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
                return true;
            }
        }
        return false;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && CanDash())
        {
            timeSinceLastDash = Time.time;
            StartCoroutine(Dash());
        }
    }
    private bool CanDash()
    {
        return Time.time - timeSinceLastDash > dashCooltime;
    }


    private IEnumerator Dash()
    {
        if (CharacterManager.Instance.Player.condition.UseMana(dashMana))
        {
            speed = dashSpeed;
            yield return new WaitForSeconds(dashDuration);
            speed = initialSpeed;
        }
    }
    public void OnOption(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            option?.Invoke();
            Toggle();
        }
    }
    private void Toggle()
    {
        bool islocked = (Cursor.lockState == CursorLockMode.Locked);
        Cursor.lockState = islocked ? CursorLockMode.None : CursorLockMode.Locked;  
        canLook = !islocked;
    }
}
