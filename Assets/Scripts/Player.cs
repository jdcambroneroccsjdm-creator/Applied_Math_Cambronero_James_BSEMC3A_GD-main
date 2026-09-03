using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    

    [SerializeField] private Animator animator;

    // The Start method executes once prior to the initial Update call, following the creation of the MonoBehaviour.
    void Start()
    {
        
    }

    // The Update method is invoked once every frame.
    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.Playing)
        {
            return;
        }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction != Vector3.zero)
        {
            // Rotate in the direction of movement.
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // Move
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Running animation
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.killedEnemyCount++;
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.OnDied();
        }
    }
}
