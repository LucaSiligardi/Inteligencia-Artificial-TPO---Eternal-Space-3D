using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    public float minX = 420f;
    public float maxX = 445f;
    public float minY = 30f;
    public float maxY = 45f;

    [SerializeField] private Animator animator;

   
    private static readonly int IsMovingLeft = Animator.StringToHash("IsMovingLeft");
    private static readonly int IsMovingRight = Animator.StringToHash("IsMovingRight");


    void Update()
    {
      
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // booleanos del animator
        animator.SetBool(IsMovingLeft, h < 0);
        animator.SetBool(IsMovingRight, h > 0);
       

        // Movimiento
        Vector3 movimiento = new Vector3(h, v, 0f) * speed * Time.deltaTime;
        transform.position += movimiento;

        // Límites
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}