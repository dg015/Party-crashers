using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;


    [Header("new Input system")]
    [SerializeField] private PlayerInput playerInputComponent;
    [SerializeField] private Vector3 playerInput;


    [Header("Locomotion ")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxAccelerationForce;

    [SerializeField] private Transform orientation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void FixedUpdate()
    {
        movement();
    }

    public void checkHorizontalInput(InputAction.CallbackContext context)
    {
        Debug.Log("reading");
        playerInput = context.ReadValue<Vector3>();

    }

    private void movement()
    {
        Vector3 moveDirection = orientation.forward * playerInput.z + orientation.right * playerInput.x;
        moveDirection.Normalize();
        

        Vector3 targetVelocity = moveDirection * maxSpeed;
        Vector3 neededAcceleration = ((targetVelocity - playerRB.linearVelocity) / Time.fixedDeltaTime);

        neededAcceleration = Vector3.ClampMagnitude(neededAcceleration, acceleration);

        playerRB.AddForce(Vector3.Scale(neededAcceleration * playerRB.mass, new Vector3(1, 1, 1)));

        Debug.Log(neededAcceleration);

    }



}
