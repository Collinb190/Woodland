using UnityEngine;

public class BoarMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isMovingLeft = true;
    private bool isOnScreen = false;

    public float moveSpeed = 3f;
    public LayerMask groundLayer;

   private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    GameObject clio = GameObject.FindGameObjectWithTag("Player");
    if (clio != null)
    {
        Vector3 clioPosition = clio.transform.position;

        //Debug.Log("Boar position: " + transform.position);
        //Debug.Log("Clio position: " + clioPosition);

        if (transform.position.x > clioPosition.x)
        {
            spriteRenderer.flipX = true;
            //Debug.Log("Boar is to the right of Clio, flipping sprite");
        }
        else
        {
            //Debug.Log("Boar is to the left of Clio, sprite remains unchanged");
        }
    }
    else
    {
        //Debug.LogWarning("Clio not found. Ensure Clio has the correct tag.");
    }
}




    private void Update()
    {
        if (!isOnScreen && IsVisibleOnScreen())
        {
            isOnScreen = true;
        }

        if (isOnScreen)
        {
            Move();
        }
    }

    private void Move()
{
    Vector3 targetPosition = GameObject.FindWithTag("Player").transform.position;

    Vector2 direction = (targetPosition - transform.position).normalized;

    rb.velocity = direction * moveSpeed;

    if (rb.velocity.x < 0)
    {
        spriteRenderer.flipX = false;
    }
    else if (rb.velocity.x > 0)
    {
        spriteRenderer.flipX = true;
    }
}


    private bool IsVisibleOnScreen()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private bool IsWallAhead()
    {
        Vector2 direction = isMovingLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, groundLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D[] contacts = new ContactPoint2D[16];
            int contactCount = collision.GetContacts(contacts);
            
            bool collidedFromAbove = false;
            foreach (ContactPoint2D contact in contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    collidedFromAbove = true;
                    break;
                }
            }

            if (collidedFromAbove)
            {
                Destroy(gameObject);
            }
            else if (Mathf.Abs(collision.contacts[0].normal.x) > 0.1f)
            {
                ClioLives clioLives = collision.gameObject.GetComponent<ClioLives>();
                if (clioLives != null)
                {
                    clioLives.RemoveLife();
                }
            }
        }
    }
}
