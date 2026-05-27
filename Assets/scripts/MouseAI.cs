using UnityEngine; 
public class MouseAI : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 10;
    private Vector3 walking;
    private float walkingtime = 0;
    private float walkinginterval = 2;
    public Transform player;
    public float fleeDistance = 3;
    public float fleeMultiplier = 2;
    public float burstTimer = 0;
    public float burstDuration = 2;
    public float burstSpeed = 25f;
    bool burst = false;
    
    

    void OnCollisionEnter(Collision other) // another collision fucntion cuz for some reason it isnt registegriung XD
    {
        if (other.gameObject.tag == "Player")
        {
            currentState = MouseState.Caught;
        }
    }

    void OnTriggerEnter(Collider other)//hopefully it works 
    {
        if (other.gameObject.tag == "Player")
        {
            currentState = MouseState.Caught;
        }
    }
    public enum MouseState
    {
        Wandering,
        Fleeing,
        Caught,
    }
    public MouseState currentState = MouseState.Wandering;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == MouseState.Fleeing && burst == false)
        {
            burst = true;
            burstTimer =  0;
        }
        
        if (currentState != MouseState.Caught)// si no es igual al caught
        {
            if (Vector3.Distance(player.position, transform.position) < fleeDistance)
            {
                currentState = MouseState.Fleeing;
            }
            else
            {
                currentState = MouseState.Wandering;
            }
           
        }
        switch (currentState)
        {

            case MouseState.Wandering:
                HandleWandering();
                break;
            case MouseState.Caught:
                HandleCaught();
                break;
            case MouseState.Fleeing:
                HandleFleeing();
                break;


        }
        
    }

    void HandleWandering()
    {

        walkingtime += Time.deltaTime;
        if (walkingtime >= walkinginterval)
        {
            walking = Random.insideUnitSphere;
            walking.y = 0;
            walking = walking.normalized;
            walkingtime = 0;
        }
        rb.MovePosition(transform.position + walking * moveSpeed * Time.deltaTime);
        transform.forward = walking;
        Debug.Log("Wandering");
        
    }

    void HandleCaught()
    {
        rb.linearVelocity = Vector3.zero;
        Debug.Log("Caught");
    }

    void HandleFleeing()
    {       
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        transform.forward = fleeDirection;
        Debug.Log("Fleeing");
        
        if (burst == true)
        {
            burstTimer += Time.deltaTime;
            rb.MovePosition(transform.position + fleeDirection * burstSpeed * Time.deltaTime);
            if (burstTimer >= burstDuration)
            {
                burst =  false;
            }
            
        }
        else
        {
            rb.MovePosition(transform.position + fleeDirection * moveSpeed * Time.deltaTime);
        }
        
    }
}
