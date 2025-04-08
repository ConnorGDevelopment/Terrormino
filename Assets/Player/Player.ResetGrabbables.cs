using UnityEngine;

public class ResetGrabbables : MonoBehaviour
{
    public Collider DropWatchCollider;
    public Vector3 StartingPosition;
    public Quaternion StartingRotation;

    // Start is called before the first frame update
    void Start()
    {
        DropWatchCollider = Helpers.Debug.TryFindByTag("DropWatch").GetComponent<Collider>();
        StartingPosition = gameObject.transform.position;
        StartingRotation = gameObject.transform.rotation;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == DropWatchCollider)
        {
            gameObject.transform.position = StartingPosition;
            gameObject.transform.rotation = StartingRotation;
        }
    }


}
