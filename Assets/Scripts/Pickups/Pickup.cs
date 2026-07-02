using UnityEngine;

public class Pickup : MonoBehaviour
{
    const string PlayerString = "Player"; //used const as it won't allow us to change player string
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == PlayerString)
        {     
            Debug.Log(other.gameObject.name); 
        }
    }
}
