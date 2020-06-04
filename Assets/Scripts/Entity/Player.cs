using log4net;
using UnityEngine;

namespace VoxelValley.Entity
{
    public class Player : MonoBehaviour
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Player));

        //Singleton
        private static Player _instance;
        public static Player Instance { get { return _instance; } }

        [Header("Ground Check")]
        public LayerMask groundMask;
        public Transform groundCheck;
        public bool isGrounded = false;

        //Movemnet speeds      
        float walkSpeed = 7;
        float spritSpeed = 13;
        float currentSpeed;
        float graviy = Physics.gravity.y * 4;
        float jumpHeight = 1.5f;
        CharacterController controller;

        float velocityDown = 0;

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        void Start()
        {
            controller = gameObject.GetComponent<CharacterController>();
        }

        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

            if (isGrounded)
                velocityDown = 0;
            else
                velocityDown += graviy * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && isGrounded)
                velocityDown += Mathf.Sqrt(jumpHeight * -2f * graviy);


            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = spritSpeed;
            else
                currentSpeed = walkSpeed;

            controller.Move(move * currentSpeed * Time.deltaTime);
            controller.Move(Vector3.up * velocityDown * Time.deltaTime);
        }
    }
}