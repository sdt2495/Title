using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public GameObject gameOverText;
    public GameObject goalText;
    public GameObject retryButton;

    public Text timerText;

    private Vector2 moveInput;
    private Rigidbody rb;

    private bool isGrounded = false;   // ★ 最初は false にしておく
    private bool isGameOver = false;
    private bool isGoal = false;

    private float timer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Start()
    {
        if (gameOverText != null) gameOverText.SetActive(false);
        if (goalText != null) goalText.SetActive(false);
        if (retryButton != null) retryButton.SetActive(false);

        timer = 0f;

        if (timerText != null)
            timerText.text = "0.00 秒";
    }

    public void OnMove(InputValue value)
    {
        if (isGameOver || isGoal) return;
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (isGameOver || isGoal) return;

        // ★ スペースキーでジャンプ（1回だけ）
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;   // ★ 空中にいるので false
        }
    }

    void FixedUpdate()
    {
        // タイマー更新
        if (!isGameOver && !isGoal)
        {
            timer += Time.deltaTime;

            if (timerText != null)
                timerText.text = timer.ToString("F2") + " 秒";
        }

        if (isGameOver || isGoal) return;

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 vel = rb.linearVelocity;

        vel.x = move.x * moveSpeed;
        vel.z = move.z * moveSpeed;

        rb.linearVelocity = vel;

        // 落下判定
        if (transform.position.y < -5f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (gameOverText != null) gameOverText.SetActive(true);
        if (retryButton != null) retryButton.SetActive(true);
    }

    private void GoalClear()
    {
        isGoal = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (goalText != null)
        {
            Text goalUI = goalText.GetComponent<Text>();
            if (goalUI != null)
                goalUI.text = "GOAL!!\nTime: " + timer.ToString("F2") + " 秒";
        }

        goalText.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ★ Ground に触れたらジャンプ復活
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            GoalClear();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
