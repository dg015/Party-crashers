using System;
using System.Collections;
using UnityEngine;
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

    [Header("Drop Coroutine")]
    [SerializeField] private Coroutine m_dropCoroutine;

    [SerializeField] private float interactRange;
    [SerializeField] private Item heldObject;

    //This is for the more the player holds the stronger they toss it
    [SerializeField] private float m_currentForceTimer;
    [SerializeField] private float m_maxForceTimer;

    [SerializeField] float throwForce = 3;


    [SerializeField] private bool canDrop;

    [SerializeField] private float m_consuptionDuration;


    public void interact()
    {
        if (m_dropCoroutine != null)
            return;

        //Get colliding with overlapbox
        Collider[] boxColResults = new Collider[3];
        int hitCount = Physics.OverlapBoxNonAlloc(m_source.position, m_halfExtends, boxColResults,Quaternion.identity, m_layerMask);
        //Collider[] BoxCol = Physics.OverlapBox(m_source.position, m_halfExtends, Quaternion.identity, m_layerMask);

        //Check colllisions if they have the component for interactable object
        foreach (Collider col in boxColResults)
        {
            if(col.gameObject.TryGetComponent(out Iinteractable interactObj))
            {
                interactObj.Interact(gameObject);
                if (col.transform.gameObject.GetComponent<Item>())
                {
                    heldObject = col.GetComponent<Item>();
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


    public void drop()
    {
        if(canDrop) 
            throwObject(throwForce);
    }

    public void throwObject(float force)
    {
        if (heldObject != null)
        {
            canDrop = false;

            //clear parent
            heldObject.transform.SetParent(null);

            //throw object
            Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();
            objectRb.AddForce(-m_source.right * force, ForceMode.Impulse);

            //unfreeze positions
            // ~ means everything except this

            objectRb.constraints &= ~RigidbodyConstraints.FreezePosition;

            //reset it back to none
            heldObject = null;
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
