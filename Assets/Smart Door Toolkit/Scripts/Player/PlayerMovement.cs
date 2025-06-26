using UnityEngine;


namespace FMS_SmartDoorToolkit
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public static bool IsInspecting = false;
        public float walkSpeed = 3.0f;
        public float runSpeed = 6.0f;
        [Space]
        public float GroundDistance = 0.3f;
        public Transform GroundCheck;
        public LayerMask groundMask;
        [Space]
        Vector3 velocity;
        public float jumpheight = 3f;
        public float gravity = -7f;
        public float lookSpeed = 2.0f;
        public Animator camHolderAnimator;
        private Camera cam;
        private CharacterController cc;
        private float rotationX = 0;


        
        private bool isGrounded;

        void Start()
        {
            cam = GetComponentInChildren<Camera>();
            cc = GetComponent<CharacterController>();
        }

        void Update()
        {
            HandleMovementInput();
            if (!IsInspecting) // Prevent camera movement when inspecting
            {
                HandleMouseLook();
            }
        }

        void HandleMovementInput()
        {

            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);



            Vector3 moveDir = Vector3.zero;

            // Get input from Unity's input system
            float horizontal = Input.GetAxis("Horizontal"); // Left (-1) to Right (+1)
            float vertical = Input.GetAxis("Vertical");     // Down (-1) to Up (+1)

            // Apply movement
            moveDir.x = horizontal;
            moveDir.z = vertical;




            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Horizontal") !=0 || Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") != 0)
            {
                moveDir *= runSpeed;


                camHolderAnimator.SetBool("Running", true);

                camHolderAnimator.SetBool("Walking", false);

            }
            else
            {
                moveDir *= walkSpeed;

                camHolderAnimator.SetBool("Walking", true);
                camHolderAnimator.SetBool("Running", false);

            }

            if (moveDir == Vector3.zero)
            {
                camHolderAnimator.SetBool("Walking", false);
                camHolderAnimator.SetBool("Running", false);

            }



            if (isGrounded && Input.GetKey(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
                isGrounded = false;
            }

            velocity.y += gravity * Time.deltaTime;
            cc.Move(velocity * Time.deltaTime);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= Time.deltaTime;

            cc.Move(moveDir);

        }

        void HandleMouseLook()
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -90, 90);

            cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

    }
}