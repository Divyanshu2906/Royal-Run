using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movespeed = 10f;
    [SerializeField] float xclamp = 3f;
    [SerializeField] float zclamp = 2f;
    Vector2 movement;
    Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        Handlemovement();
    }

    

    public void move(InputAction.CallbackContext context)
    {
         movement = context.ReadValue<Vector2>();
         Debug.Log(movement);
    }

    void Handlemovement()
    {
        Vector3 currentposition = rigidBody.position;
        Vector3 movedirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newposition = currentposition + movedirection * (movespeed * Time.fixedDeltaTime);
        
        newposition.x = Mathf.Clamp(newposition.x, -xclamp, xclamp);
        newposition.z = Mathf.Clamp(newposition.z, -zclamp, zclamp);
        rigidBody.MovePosition(newposition);
    }
}
