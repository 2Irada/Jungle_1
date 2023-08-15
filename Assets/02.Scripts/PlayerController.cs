using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameEnd) PlayerAct();
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rigid.gravityScale));
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
            if(!isJump) ColorManager.instance.AutoSwitchMainColoring();
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            if (!isGround) jumpCount++;

        }
    }

    void PlayerMove()
    {
        horInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * Time.deltaTime * playerSpeed * horInput);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isJump = false;
            Debug.Log("JumpReset");
            jumpCount = 0;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    { // check to player Fall, use OnCollisionExit
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            isJump = true;
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
                jumpCount--;
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

   
}
