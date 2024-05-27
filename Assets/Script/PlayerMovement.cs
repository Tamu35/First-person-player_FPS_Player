using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6.0f; // Normal yürüme hýzý
    public float sprintSpeed = 12.0f; // Koþma hýzý
    public float slowWalkSpeed = 3.0f; // Yavaþ yürüme hýzý
    public float jumpSpeed = 8.0f; // Zýplama hýzý
    public float gravity = 20.0f; // Yer çekimi

    public Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Yön tuþlarýyla hareketi al
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            // Koþma tuþu (Shift) basýlýysa hýz arttýr
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= sprintSpeed;
            }
            // Yavaþ yürüme tuþu (Ctrl) basýlýysa hýz azalt
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                moveDirection *= slowWalkSpeed;
            }
            // Normal yürüme
            else
            {
                moveDirection *= walkSpeed;
            }

            // Zýplama
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Yer çekimi uygulama
        moveDirection.y -= gravity * Time.deltaTime;

        // Hareketi uygula
        controller.Move(moveDirection * Time.deltaTime);
    }
}
