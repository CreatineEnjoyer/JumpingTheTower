using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private InputAction move;
    private PlayerControlActions playerAction;
    private Rigidbody2D rb;
    private Vector2 movement = Vector2.zero;


    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerAction = new PlayerControlActions();
    }

    private void FixedUpdate()
    {
        Moving();
    }

    private void Moving()
    {
        movement = move.ReadValue<Vector2>();
        rb.velocity += speed * Time.fixedDeltaTime * movement;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void OnEnable()
    {
        move = playerAction.Player.Move;
        playerAction.Player.Enable();
    }

    private void OnDisable()
    {
        playerAction.Player.Disable();
    }
}
