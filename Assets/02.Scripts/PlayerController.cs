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
    [SerializeField] private float gameEndFdt;
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

    [SerializeField] private Camera gameOverCamera;
    [SerializeField] private GameObject gameOverObj;

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

        if (!isGround) fdt += Time.deltaTime;
        
        CheckGameOver();
      
        if (!isGameEnd) PlayerAct();

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

        if (rayCastHit.collider != null)
        {
            fdt = 0;
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
            isGameEnd = true;
            gameOverCamera.gameObject.SetActive(true);
            gameOverObj.SetActive(true);
        }
    }
}
