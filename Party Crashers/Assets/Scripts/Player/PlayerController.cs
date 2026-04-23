using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Componenets")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput playerInputComponent;
    [SerializeField] private GameObject playerModel;

 


    [Header("Locomotion ")]
    [SerializeField] private Vector3 playerInput;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxAccelerationForce;

    [SerializeField] private Transform orientation;


    [Header("Rotation")]
    Quaternion targetRotation;
    [SerializeField] private float rotationSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerModel();
    }


    private void FixedUpdate()
    {
        movement();
    }

    public void checkHorizontalInput(InputAction.CallbackContext context)
    {
        
        playerInput = context.ReadValue<Vector3>();

    }

    private void movement()
    {
        Vector3 moveDirection = orientation.forward * playerInput.z + orientation.right * playerInput.x;
        moveDirection.Normalize();
        

        Vector3 targetVelocity = moveDirection * maxSpeed;
        //to stop fighting gravity (slow falling)
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        Vector3 neededAcceleration = ((targetVelocity - flatVelocity) / Time.fixedDeltaTime);

        neededAcceleration = Vector3.ClampMagnitude(neededAcceleration, acceleration);

        rb.AddForce(Vector3.Scale(neededAcceleration * rb.mass, new Vector3(1, 1, 1)));

        

    }

    //PLANO CARTESIANO, PEGA AS COORDENADAS, FAZ UM ATAN2 E APPLICA ESSE GRAU COMO AONDE DEVE OLHAR
    private void RotatePlayerModel()
    {

        if (playerInput.sqrMagnitude < 0.01f)
            return;
        float angle = Mathf.Atan2(playerInput.x, playerInput.z) * Mathf.Rad2Deg;
        angle += 90;

        playerModel.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);


    }
}
