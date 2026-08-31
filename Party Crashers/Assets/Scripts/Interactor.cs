using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

interface Iinteractable
{
    public void Interact(GameObject interactor);
}


public class Interactor : MonoBehaviour
{
    [Header("Box cast")]
    [SerializeField] private Transform m_source;
    [SerializeField] private Vector3 m_halfExtends = new Vector3(.5f, .5f, .5f);
    [SerializeField] private LayerMask m_layerMask;

    Collider[] boxColResults = new Collider[3];

    [Header("Drop Coroutine")]
    [SerializeField] private Coroutine m_dropCoroutine;

    [SerializeField] private float interactRange;
    [SerializeField] private Item heldObject;

    //This is for the more the player holds the stronger they toss it
    [SerializeField] private float m_currentForceTimer;
    [SerializeField] private float m_maxForceTimer;

    [SerializeField] float throwForce = 3;
    [SerializeField] private float throwForceModifier;
    private InputAction.CallbackContext throwActionContext;

    [SerializeField] private bool canDrop;

    [SerializeField] private float m_consuptionDuration;

    [SerializeField] private bool m_isCharging;


    public void interact()
    {
        if (m_dropCoroutine != null)
            return;

        //Get colliding with overlapbox


        int hitCount = Physics.OverlapBoxNonAlloc(m_source.position, m_halfExtends, boxColResults,Quaternion.identity, m_layerMask);

        //Check colllisions if they have the component for interactable object
        //foreach (Collider col in boxColResults)
        for (int i = 0; i < hitCount; i++)
        {
            if (boxColResults[i].gameObject.TryGetComponent(out Iinteractable interactObj))
            {
                interactObj.Interact(gameObject);
                if (boxColResults[i].transform.gameObject.GetComponent<Item>())
                {
                    heldObject = boxColResults[i].GetComponent<Item>();
                    Debug.Log("Grabbed object");
                    m_dropCoroutine = StartCoroutine(dropCoroutine());
                }
            }

        }
    }
    private IEnumerator dropCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        if (heldObject != null)
        {
            canDrop = true;
        }
        m_dropCoroutine = null;
    }

    public void consumeItem()
    {
        Item item = heldObject.GetComponent<Item>();
        if (item.consumable == true)
        {
            item.consume(gameObject, m_consuptionDuration);
        }
    }


    private void Update()
    {
        Throw();
    }

    private void Throw()
    {
        if(m_isCharging && heldObject != null)
        {
            m_currentForceTimer += Time.deltaTime;
            throwForceModifier = Mathf.Clamp(m_currentForceTimer, 0, m_maxForceTimer);
            Debug.Log("Charging");
        }
    }


    public void ChargeThrow(InputAction.CallbackContext context)
    {
        throwActionContext = context;
        //start holding
        if (context.performed)
        {
            m_isCharging = true;
        }
        else if( context.canceled)
        {
            m_isCharging = false;
            throwObject(throwForce * throwForceModifier);
        }
    }

    public void throwObject(float force)
    {
        if (!canDrop)
            return;

        if (heldObject != null & !m_isCharging)
        {

            Debug.Log("throw object");
            //clear parent
            heldObject.transform.SetParent(null);

            //throw object
            Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();
            objectRb.AddForce(-m_source.right * force, ForceMode.Impulse);
            Debug.Log("Object thrown");

            //unfreeze positions
            // ~ means everything except this

            objectRb.constraints &= ~RigidbodyConstraints.FreezePosition;

            //reset it back to none
            heldObject = null;
            m_currentForceTimer = 0; 
            throwForceModifier = 0;
            canDrop = false;
        }
    }


    private void OnDrawGizmos()
    {
        Ray ray = new Ray(m_source.position, -m_source.transform.right);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(ray.origin,ray.direction * interactRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(m_source.position, m_halfExtends * 2);
    }
}
