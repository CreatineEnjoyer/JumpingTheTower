using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpforce;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask jumpLayerMask;

    private Rigidbody2D rb;
    private BoxCollider2D boxColl;
    private PlayerControlActions playerAction;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        boxColl = this.GetComponent<BoxCollider2D>();
        playerAction = new PlayerControlActions();
    }

    private void Jumping(InputAction.CallbackContext obj)
    {
        if (Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, .1f, jumpLayerMask))
        {
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, jumpSpeed);
        }
        //Walljumping();
    }

    private void Walljumping()
    {
        if (Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.left, .1f, jumpLayerMask))
        {
            rb.AddForce(Vector2.right * (jumpforce * 2f), ForceMode2D.Force);
            rb.AddForce(Vector2.up * (jumpforce * 3f), ForceMode2D.Force);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, jumpSpeed);
        }
        else if (Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.right, .1f, jumpLayerMask))
        {
            rb.AddForce(Vector2.left * (jumpforce * 2f), ForceMode2D.Force);
            rb.AddForce(Vector2.up * (jumpforce * 3f), ForceMode2D.Force);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, jumpSpeed);
        }
    }

    private void OnEnable()
    {
        playerAction.Player.Enable();
        playerAction.Player.Jump.started += Jumping;
    }

    private void OnDisable()
    {
        playerAction.Player.Jump.started -= Jumping;
        playerAction.Player.Disable();
    }
}
