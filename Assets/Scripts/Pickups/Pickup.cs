using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 100f;
    const string PlayerString = "Player"; //used const as it won't allow us to change player string

    void Update()
    {
        transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == PlayerString)
        {     
            OnPickup();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
}
