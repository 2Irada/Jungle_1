using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public BoxCollider2D groundChecker;

    #region GameEndVariable
    private bool isGameEnd;
    [SerializeField] private float fdt;
    private float gameEndFdt = 2.5f;
    #endregion
   
    private Rigidbody2D rigid;

    private float horInput;
    [SerializeField] private float playerSpeed;

    #region PlayerJumpVariable
    private bool isGround;
    [SerializeField] private bool isJump;

    [SerializeField] private float jumpHeight;
    [SerializeField] private int maxJump;
    private float jumpForce;
    [SerializeField] private int jumpCount;
    #endregion

    [SerializeField] private GameObject gameOverObj;

    #region MonoBehaviour Method
    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 0;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        isGround = isGrounded();

        if (!isGround) fdt += Time.deltaTime;
        
        CheckGameOver();
      
        if (!isGameEnd) PlayerAct();

    }

    private void FixedUpdate()
    {
        if (rigid != null)
        {
            if (rigid.velocity.y < -15)
            {
                rigid.velocity = new Vector3(rigid.velocity.x, -15,0f);
            }
        }
    }
    #endregion

    void PlayerAct()
    {   // check Player Act     
        PlayerJump();
        PlayerMove();
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            ColorManager.instance.AutoSwitchMainColoring();
            rigid.velocity = Vector2.zero;
            jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rigid.gravityScale));
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJump = true;
        }
    }

    void PlayerMove()
    {
        horInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * Time.deltaTime * playerSpeed * horInput);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            ColoredObject _co = collision.gameObject.GetComponent<ColoredObject>();
            if (_co.currentColoring != ColorManager.instance.mainColoring)
            {
                GameOver();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            ColoredObject _co = collision.gameObject.GetComponent<ColoredObject>();
            if (_co.currentColoring != ColorManager.instance.mainColoring)
            {
                GameOver();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    { // check to player Fall, use OnCollisionExit
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;
            jumpCount++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (!isGround)
            {
                fdt = 0f;
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
        float extraHeightText = 0.2f;
        // RaycastHit2D rayCastHit = Physics2D.Raycast(groundChecker.bounds.center, Vector2.down, groundChecker.bounds.extents.y + extraHeightText, platformLayerMask);
        RaycastHit2D rayCastHit = Physics2D.BoxCast(groundChecker.bounds.center, groundChecker.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        if (rayCastHit.collider != null && !rayCastHit.collider.isTrigger)
        {
            fdt = 0f;
            jumpCount = 0;
        }
        else
        {
            if (isJump)
            {
                jumpCount++;
                isJump = false;
            }
        }

        return rayCastHit.collider != null;
    }

    void CheckGameOver()
    {
        if(fdt > gameEndFdt)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (isGameEnd == true) return;

        isGameEnd = true;
        UIManager.instance._isGameEnd = true;
        gameOverObj.SetActive(true);
    }
}
