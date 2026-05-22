using UnityEngine;

public class CameraSample : MonoBehaviour
{
    public Transform target;   // Player
    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraSample: target が設定されていません");
            return;
        }

        // 最初の相対位置を記録
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 位置だけ追従（回転はしない）
        transform.position = target.position + offset;
    }
}
