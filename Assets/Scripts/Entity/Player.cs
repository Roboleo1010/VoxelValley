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

        // [Header("Ground Check")]
        // public LayerMask groundMask;
        // public Transform groundCheck;
        // public bool isGrounded = false;
        // float groundCheckDistance = 0.05f;

        //Movemnet speeds
        // float walkSpeed = 7;
        // float spritSpeed = 13;
        // float graviy = -4f;

        // float velocityDown = 0;

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

        // void Update()
        // {
        //     isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        //     if (isGrounded)
        //         velocityDown = 0;
        //     else
        //         velocityDown += graviy * Time.deltaTime;

        //     float x = Input.GetAxis("Horizontal");
        //     float z = Input.GetAxis("Vertical");

        //     Vector3 move = transform.right * x + transform.forward * z;

        //     transform.Translate(move * walkSpeed * Time.deltaTime, Space.World);
        //     transform.Translate(new Vector3(0, velocityDown, 0) * Time.deltaTime, Space.World);
        // }
    }
}