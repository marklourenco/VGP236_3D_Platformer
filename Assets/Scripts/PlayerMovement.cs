using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    //References:
    public Transform orientation;
    Rigidbody rb;
    [SerializeField]
    Animator animator;

    public float moveSpeed;
    public float jumpForce;
    public float groundDrag;
    public float rotateSpeed;
    public float raycastDistance;
    private float raycastOffset = 3.0f;

    float horizontalInput;
    float verticalInput;
    float rotationInput;
    float yRotation;
    Vector3 moveDirection;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        GameController.Instance.SetPlayerStartPosition(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rotationInput = Input.GetAxisRaw("Mouse X");

        yRotation += rotationInput * Time.deltaTime * rotateSpeed;
        orientation.rotation = Quaternion.Euler(0.0f, yRotation, 0.0f);

        animator.SetFloat("Walk", verticalInput);
        animator.SetFloat("Strafe", horizontalInput);
        animator.SetBool("IsMoving", (Mathf.Abs(horizontalInput) > 0.0f || Mathf.Abs(verticalInput) > 0.0f));
        //Process Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        Debug.Log(IsGrounded());
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }

    void FixedUpdate()
    {
        //Physics based movement
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);

        //SPEED LIMIT
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitVelocity.x, rb.linearVelocity.y, limitVelocity.z);
        }

        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * raycastOffset;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
}
