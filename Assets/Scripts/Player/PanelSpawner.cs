using UnityEngine;
using System.Collections.Generic;

public class PanelSpawner : MonoBehaviour
{
    [Header("最初のパネル（固定）")]
    public Transform firstPanel;

    [Header("生成するパネル")]
    public GameObject panelPrefab;

    [Header("設定")]
    public int panelCount = 100;
    public float spacing = 3f;

    [Header("Goal 用マテリアル")]
    public Material goalMaterial;   // ★ 光るマテリアル

    private HashSet<Vector3> usedPositions = new HashSet<Vector3>();

    void Start()
    {
        Transform prev = firstPanel;
        usedPositions.Add(firstPanel.position);

        for (int i = 0; i < panelCount; i++)
        {
            List<Vector3> candidates = new List<Vector3>();

            Vector3 forward = prev.position + Vector3.forward * spacing;
            Vector3 right = prev.position + Vector3.right * spacing;
            Vector3 left = prev.position + Vector3.left * spacing;

            forward.y = firstPanel.position.y;
            right.y = firstPanel.position.y;
            left.y = firstPanel.position.y;

            if (!usedPositions.Contains(forward)) candidates.Add(forward);
            if (!usedPositions.Contains(right)) candidates.Add(right);
            if (!usedPositions.Contains(left)) candidates.Add(left);

            Vector3 spawnPos;

            if (candidates.Count > 0)
                spawnPos = candidates[Random.Range(0, candidates.Count)];
            else
                spawnPos = forward;

            usedPositions.Add(spawnPos);

            GameObject newPanel = Instantiate(panelPrefab, spawnPos, firstPanel.rotation);

            // ★ Goal だけ光らせる
            if (i == panelCount - 1)
            {
                newPanel.tag = "Goal";

                Renderer r = newPanel.GetComponent<Renderer>();
                if (r != null && goalMaterial != null)
                {
                    r.material = goalMaterial;
                }
            }

            prev = newPanel.transform;
        }
    }
}
