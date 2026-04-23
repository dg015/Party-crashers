using UnityEngine;

interface Iinteractable
{
    public void Interact();
}


public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform source;
    [SerializeField] private float interactRange;

    public void interact()
    {
        Ray ray = new Ray(source.position, -source.transform.right);
        if(Physics.Raycast(ray,out RaycastHit hitInfo,interactRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out Iinteractable interactObj))
            {
                interactObj.Interact();
            }
        }
    }


    private void OnDrawGizmos()
    {
        Ray ray = new Ray(source.position, -source.transform.right);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(ray.origin,ray.direction * interactRange);
    }
}
