using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public int playerState; // To Change Color, use this Num

    public TextMeshProUGUI tutorialText; // if Player match Tuto Col, change Tuto Text
    public TextMeshProUGUI endText; // if Player reach Goal, GameOver Text is Change
    [SerializeField] private GameObject gameOverObj; // if Player Die, this Obj Active
    [SerializeField] private GameObject skillNum; // if Player Die or Reach Goal, deactive

    [SerializeField] private Camera mainCamera; // if Player Die or Reach Goal, active

    [SerializeField] private SpriteRenderer playerEyeRenderer; // change PlayerEye Color
    [SerializeField] private SpriteRenderer groundRenderer; // change Background Color
    private Rigidbody2D playerRb; // use to Player Jump

    public bool isGameOver; // check GameOver Stance

    private float horizontalInput; // player Input Stance
    [SerializeField] private float playerSpeed; // player Move Speed
    
    [SerializeField] private float jumpPower;
    [SerializeField] private float maxJump; // player can jump as much as 'maxJump'
    private float jumpCount; // if Jump or fall, jumpCount++

    private bool isGround; // check Is Ground stance

    private int colorCount;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization Data        
        playerState = 0;
        isGameOver = false;
        playerRb = GetComponent<Rigidbody2D>();
        playerEyeRenderer.color = Color.white;
        groundRenderer.color = Color.white;
        GameManager.Instance.ObjectActive(0);

    }

    // Update is called once per frame
    void Update()
    {
        // Player Can Act when game Isn't over
        if (!isGameOver) PlayerAct();
    }

    void PlayerAct()
    {
        PlayerMove();
        PlayerJump();
        PlayerSkill();
    }

    void PlayerMove()
    {    
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * Time.deltaTime * playerSpeed * horizontalInput);
        
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            if (colorCount < 3)
            {
                colorCount++;
            }
            else colorCount = 1;

            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
            if(!isGround) jumpCount++;
        }
        
    }

    void PlayerSkill()
    {
        // if Player Input Num, change playerState and Color
        if(colorCount == 1)
        {
            playerState = 1;
            GameManager.Instance.ObjectActive(playerState);
            playerEyeRenderer.color = new Color(255 / 255f, 201 / 255f, 201 / 255f);
            groundRenderer.color = new Color(255/255f, 201/255f, 201/255f);
        }

        else if (colorCount == 2)
        {
            playerState = 2;
            GameManager.Instance.ObjectActive(playerState);
            playerEyeRenderer.color = new Color(85 / 255f, 166 / 255f, 255 / 255f);
            groundRenderer.color = new Color(85 / 255f, 166/ 255f, 255/ 255f);
        }

        else if (colorCount == 3)
        {
            playerState = 3;
            GameManager.Instance.ObjectActive(playerState);
            playerEyeRenderer.color = new Color(255 / 255f, 252 / 255f, 143 / 255f);
            groundRenderer.color = new Color(255 / 255f, 252 / 255f, 143 / 255f);
        }
    }

    void GameOver()
    { // when playerDie or reach Goal, this Method call
        isGameOver = true;
        skillNum.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        gameOverObj.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("JumpReset");
            isGround = true;
            jumpCount = 0;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    { // check to player Fall, use OnCollisionExit
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            jumpCount++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Destroy"))
        {
            GameOver();
            gameObject.SetActive(false);
            Debug.Log("GameOver");
        }

        else if(other.gameObject.CompareTag("Item"))
        { // item give One more Jump Count
            jumpCount--;
            StartCoroutine(ReSetItem());
        }

        else if(other.gameObject.CompareTag("Goal"))
        {
            endText.text = "The End.";
            GameOver();
            gameObject.SetActive(false);
            Debug.Log("EndGame");
        }

        IEnumerator ReSetItem()
        { // item regenerate after 3 sec
            other.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            other.gameObject.SetActive(true);
        }

        // if player reach tuto field
        switch (other.gameObject.name)
        {
            case "Tuto01":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Arrow Key To Move";
                break;
            case "Tuto02":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Space To Jump";
                break;
            case "Tuto03":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Double Jump is Possible";
                break;
            case "Tuto04":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Use 1, 2, 3 and Change Color";
                break;
            case "Tuto05":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Same Color Object is Gone";
                break;
            case "Tuto06":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Circle Give \nOne More Jump";
                break;
            case "Tuto07":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Triangle is Trap!!";
                break;
            case "Tuto08":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Item is Regenerate after 3 sec";
                break;
            case "Tuto09":
                tutorialText.gameObject.SetActive(true);
                tutorialText.text = "Good Luck!!";
                break;
        }
    }    

    void OnTriggerExit2D(Collider2D other)
    { // if player exit tuto collider, text is close
        tutorialText.gameObject.SetActive(false);
    }
}
