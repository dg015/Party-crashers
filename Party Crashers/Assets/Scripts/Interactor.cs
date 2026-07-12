using System;
using System.Collections;
using UnityEditor.PackageManager;
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
    [SerializeField] private Vector3 m_halfExtends = new Vector3(.5f,.5f,.5f);
    [SerializeField] private LayerMask m_layerMask;



    [SerializeField] private float interactRange;
    [SerializeField] private GameObject heldObject;
    [SerializeField] float throwForce =3;


    [SerializeField] private bool canDrop;

    [SerializeField] private float m_consuptionDuration;
    public void interact()
    {
        Collider[] BoxCol = Physics.OverlapBox(m_source.position, m_halfExtends, Quaternion.identity, m_layerMask);

        foreach (Collider col in BoxCol)
        {
            if(col.gameObject.TryGetComponent(out Iinteractable interactObj))
            {
                interactObj.Interact(gameObject);
                if (col.transform.gameObject.GetComponent<Item>())
                {
                    heldObject = col.transform.gameObject;
                    StartCoroutine(dropCoroutine());
                }
            }

        }


        /*
        Ray ray = new Ray(m_source.position, -m_source.transform.right);
        if(Physics.Raycast(ray,out RaycastHit hitInfo,interactRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out Iinteractable interactObj))
            {
                interactObj.Interact(gameObject);
                if(hitInfo.transform.gameObject.GetComponent<Item>())
                {
                    heldObject = hitInfo.transform.gameObject;
                    StartCoroutine(dropCoroutine());
                }
            }
        }
        */
    }


    private IEnumerator dropCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        if (heldObject != null)
        {
            canDrop = true;
        }
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
            throwObject(1);
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
