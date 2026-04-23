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
                }
            }
        }
    }




    public void throwObject()
    {
        if (heldObject != null)
        {
            //clear parent
            heldObject.transform.SetParent(null);

            //throw object
            Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();
            objectRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

            //unfreeze everything

            objectRb.constraints = RigidbodyConstraints.None;

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
