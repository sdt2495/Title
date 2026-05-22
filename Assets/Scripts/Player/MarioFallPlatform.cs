using UnityEngine;
using System.Collections;

public class MarioFallPlatform : MonoBehaviour
{
    [Header("揺れ設定")]
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.1f;

    [Header("落下設定")]
    public float fallDistance = 5f;
    public float fallSpeed = 10f;

    [Header("復活設定")]
    public float respawnDelay = 2f;

    [Header("落下までの待ち時間")]
    public float requiredStayTime = 1f; // ★ Player が乗ってから落ちるまでの秒数

    private Vector3 originalLocalPos;
    private bool isTriggered = false;
    private bool playerOnPlatform = false;
    private float stayTimer = 0f;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        // Player が乗っている間だけタイマー進行
        if (playerOnPlatform && !isTriggered)
        {
            stayTimer += Time.deltaTime;

            // 一定時間乗り続けたら落下開始
            if (stayTimer >= requiredStayTime)
            {
                isTriggered = true;
                StartCoroutine(ShakeAndFall());
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            stayTimer = 0f; // 乗った瞬間にタイマーリセット
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            stayTimer = 0f; // 降りたらタイマーリセット
        }
    }

    IEnumerator ShakeAndFall()
    {
        // --- 揺れ ---
        float timer = 0f;
        while (timer < shakeDuration)
        {
            float offset = Mathf.Sin(Time.time * 50f) * shakeAmount;
            transform.localPosition = originalLocalPos + new Vector3(offset, 0, 0);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalLocalPos;

        // --- 落下 ---
        Vector3 targetPos = originalLocalPos + Vector3.down * fallDistance;

        while (Vector3.Distance(transform.localPosition, targetPos) > 0.05f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                targetPos,
                fallSpeed * Time.deltaTime
            );
            yield return null;
        }

        // --- 復活待ち ---
        yield return new WaitForSeconds(respawnDelay);

        // --- 復活 ---
        transform.localPosition = originalLocalPos;

        // 状態リセット
        isTriggered = false;
        playerOnPlatform = false;
        stayTimer = 0f;
    }
}
