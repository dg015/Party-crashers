using System.Collections;
using UnityEngine;

interface Iinteractable
{
    public void Interact(GameObject interactor);
}


public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform source;
    [SerializeField] private float interactRange;
    [SerializeField] private GameObject heldObject;
    [SerializeField] float throwForce =3;

    [SerializeField] private bool canDrop;

    public void interact()
    {
        Ray ray = new Ray(source.position, -source.transform.right);
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
            item.consume(gameObject);

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
            objectRb.AddForce(-source.right * force, ForceMode.Impulse);

            //unfreeze positions
            // ~ means everything except this

            objectRb.constraints &= ~RigidbodyConstraints.FreezePosition;

            //reset it back to none
            heldObject = null;

            
        }

    }


    private void OnDrawGizmos()
    {
        Ray ray = new Ray(source.position, -source.transform.right);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(ray.origin,ray.direction * interactRange);
    }
}
