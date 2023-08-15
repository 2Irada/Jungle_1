using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public BoxCollider2D groundChecker;

    private bool isGameEnd;
    private bool isGround;
    private Rigidbody2D rigid;

    private float horInput;
    [SerializeField] private float playerSpeed;

    #region PlayerJump
    [SerializeField]private bool isJump;

    [SerializeField] private float jumpHeight;
    [SerializeField] private int maxJump;
    private float jumpForce;
    [SerializeField] private int jumpCount;
    #endregion

    #region MonoBehaviour Method
    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 0;
        rigid = GetComponent<Rigidbody2D>();
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rigid.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        isGround = isGrounded();

        if (!isGameEnd) PlayerAct();
        
    }
    #endregion

    void PlayerAct()
    {
        PlayerJump();
        PlayerMove();
    }

    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            ColorManager.instance.AutoSwitchMainColoring();
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJump = true;
        }
    }

    void PlayerMove()
    {
        horInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * Time.deltaTime * playerSpeed * horInput);

    }


    void OnCollisionExit2D(Collision2D collision)
    { // check to player Fall, use OnCollisionExit
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            if (!isGround)
            {
                isJump = false;
                jumpCount = 0;
            }       
            other.gameObject.SetActive(false);

            StartCoroutine(ResetItem());

            IEnumerator ResetItem()
            { // item regenerate after 3 sec
                other.gameObject.SetActive(false);
                yield return new WaitForSeconds(3);
                other.gameObject.SetActive(true);
            }
        }
    }

    bool isGrounded()
    {
        float extraHeightText = .2f;
        RaycastHit2D rayCastHit = Physics2D.Raycast(groundChecker.bounds.center, Vector2.down, groundChecker.bounds.extents.y + extraHeightText, platformLayerMask);
        Color rayColor;
          
        if(rayCastHit.collider != null)
        {
            jumpCount = 0;
            rayColor = Color.green;
        }
        else
        {
            if (isJump)
            { 
                jumpCount++;
                isJump = false;
            }
            rayColor = Color.red;
        }

        Debug.DrawRay(groundChecker.bounds.center, Vector2.down * (groundChecker.bounds.extents.y + extraHeightText), rayColor);
        Debug.Log(rayCastHit.collider);
        return rayCastHit.collider != null;
    }
}
