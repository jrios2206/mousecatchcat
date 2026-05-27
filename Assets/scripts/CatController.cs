
using UnityEngine;
public class CatController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5;
    public float pounceSpeed = 5;
    private bool isPouncing = false;
    public enum CatState
    {
        Idle,
        Running,
        Pouncing,
    }

    public CatState currentState = CatState.Idle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPouncing = true;
            
        } 
        if (isPouncing)
        {
            currentState = CatState.Pouncing;
        }

        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            currentState = CatState.Running;
        } 
        else
        {
            currentState = CatState.Idle;
        }
            
        switch (currentState)
        {
            case CatState.Idle:
                HandleIdle();
                break;
            case CatState.Running:
                HandleRunning();
                break;
            case CatState.Pouncing:
                HandlePouncing();
                break;


        }
    }
    
    
    void HandleIdle()
    {
        Debug.Log("Idle");
    }

    void HandleRunning()
    {
        
        float moveX =  Input.GetAxis("Horizontal");
        float moveZ =  Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        transform.forward = movement;
        rb.linearVelocity = movement * moveSpeed;
        Debug.Log("Running");
        Debug.Log(rb);
    }

    void HandlePouncing()
    {
        rb.AddForce(transform.forward * pounceSpeed, ForceMode.Impulse);// dont use vector 3 cuz we still dont know where the cat is facing 
        Debug.Log("Pouncing");
        isPouncing = false;
    }
}