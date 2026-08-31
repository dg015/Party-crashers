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
    [SerializeField] private float m_minThrowForce = 3f;
    [SerializeField] private float m_maxThrowForce = 10f;
    [SerializeField] private float m_maxForceTimer = 2f;
    [SerializeField] private float m_currentForce;

    [SerializeField] private float m_currentForceTimer;
    [SerializeField] private bool m_isCharging;
    [SerializeField] private Item m_recentlyThrownObject;


    [SerializeField] private bool canDrop;

    [SerializeField] private float m_consuptionDuration;


    public void interact()
    {
        if (m_dropCoroutine != null)
            return;

        if (heldObject != null)
            return;

        //Get colliding with overlapbox


        int hitCount = Physics.OverlapBoxNonAlloc(m_source.position, m_halfExtends, boxColResults,Quaternion.identity, m_layerMask);

        //Check colllisions if they have the component for interactable object
        //foreach (Collider col in boxColResults)
        for (int i = 0; i < hitCount; i++)
        {
            if (boxColResults[i].gameObject.TryGetComponent(out Iinteractable interactObj))
            {
                Item item = boxColResults[i].GetComponent<Item>();

                //for non-pickalble items
                if (item == null) 
                { 
                    interactObj.Interact(gameObject); 
                    continue; 
                }

                if (item == m_recentlyThrownObject)
                    return;

                interactObj.Interact(gameObject);
                heldObject = item;
                m_dropCoroutine = StartCoroutine(dropCoroutine());
                
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

    private IEnumerator ClearRecentlyThrown()
    {
        yield return new WaitForSeconds(0.25f);

        m_recentlyThrownObject = null;

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
        if (!m_isCharging || heldObject == null)
            return;

        m_currentForceTimer += Time.deltaTime;
        m_currentForceTimer = Mathf.Clamp(m_currentForceTimer, 0, m_maxForceTimer);
        
    }


    public void ChargeThrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (heldObject == null || !canDrop)
                return;

            m_isCharging = true;
            m_currentForceTimer = 0f;
        }

        if (context.canceled)
        {
            if (!m_isCharging)
                return;

            m_isCharging = false;

            float chargePercentage =
                m_currentForceTimer / m_maxForceTimer;

            m_currentForce = Mathf.Lerp(
                m_minThrowForce,
                m_maxThrowForce,
                chargePercentage
            );

            throwObject();
        }
    }

    public void throwObject()
    {
        if (!canDrop || heldObject == null)
            return;

        m_recentlyThrownObject = heldObject;
        //clear parent
        heldObject.transform.SetParent(null);

        //throw object
        Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();
        objectRb.AddForce(-m_source.right * m_currentForce, ForceMode.Impulse);

        //unfreeze positions
        // ~ means everything except this
        objectRb.constraints &= ~RigidbodyConstraints.FreezePosition;

        //reset it back to none
        heldObject = null;
        m_currentForceTimer = 0;
        m_currentForce = 0;
        canDrop = false;

        StartCoroutine(ClearRecentlyThrown());
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
