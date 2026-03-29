using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDebugger : MonoBehaviour
{
    public GameObject enemyToTest;  // 在Inspector中拖入一个敌人

    private void Start()
    {
        Debug.Log("===== 物理系统诊断开始 =====");

        // 1. 检查 Player 配置
        CheckPlayerSetup();

        // 2. 检查 AttackDetector 配置
        CheckAttackDetectorSetup();

        // 3. 检查敌人配置
        if (enemyToTest != null)
        {
            CheckEnemySetup(enemyToTest);
        }

        // 4. 检查 Layer Collision Matrix
        CheckLayerCollisionMatrix();

        // 5. 测试距离
        if (enemyToTest != null)
        {
            TestDistance(enemyToTest);
        }

        Debug.Log("===== 诊断完成 =====");
    }

    private void CheckPlayerSetup()
    {
        Debug.Log("\n--- Player 父对象检查 ---");

        var rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("❌ Player 没有 Rigidbody2D！");
        }
        else
        {
            Debug.Log($"✅ Rigidbody2D: bodyType={rb.bodyType}, simulated={rb.simulated}");
        }

        var mainCollider = GetComponent<Collider2D>();
        if (mainCollider != null)
        {
            Debug.Log($"✅ 主碰撞器: {mainCollider.GetType().Name}, enabled={mainCollider.enabled}");
        }

        Debug.Log($"✅ Player Layer: {LayerMask.LayerToName(gameObject.layer)}");
    }

    private void CheckAttackDetectorSetup()
    {
        Debug.Log("\n--- AttackDetector 检查 ---");

        var detector = GetComponentInChildren<Player_colliderControl>();
        if (detector == null)
        {
            Debug.LogError("❌ 找不到 Player_colliderControl！");
            return;
        }

        var obj = detector.gameObject;
        Debug.Log($"✅ AttackDetector 对象: {obj.name}");

        var col = obj.GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError("❌ AttackDetector 没有 Collider2D！");
        }
        else
        {
            Debug.Log($"✅ Collider2D: {col.GetType().Name}");
            Debug.Log($"  - enabled: {col.enabled}");
            Debug.Log($"  - isTrigger: {col.isTrigger}");
            Debug.Log($"  - bounds: {col.bounds}");

            // 检查 Layer Overrides
            var includeLayers = col.includeLayers;
            var excludeLayers = col.excludeLayers;
            Debug.Log($"  - includeLayers: {LayerMaskToString(includeLayers)}");
            Debug.Log($"  - excludeLayers: {LayerMaskToString(excludeLayers)}");
        }

        Debug.Log($"✅ AttackDetector Layer: {LayerMask.LayerToName(obj.layer)}");
    }

    private void CheckEnemySetup(GameObject enemy)
    {
        Debug.Log($"\n--- 敌人 '{enemy.name}' 检查 ---");

        var rb = enemy.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("❌ 敌人没有 Rigidbody2D！");
        }
        else
        {
            Debug.Log($"✅ 敌人 Rigidbody2D: bodyType={rb.bodyType}, simulated={rb.simulated}");
        }

        var col = enemy.GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError("❌ 敌人没有 Collider2D！");
        }
        else
        {
            Debug.Log($"✅ 敌人 Collider2D: {col.GetType().Name}");
            Debug.Log($"  - enabled: {col.enabled}");
            Debug.Log($"  - isTrigger: {col.isTrigger}");
            Debug.Log($"  - bounds: {col.bounds}");
        }

        Debug.Log($"✅ 敌人 Layer: {LayerMask.LayerToName(enemy.layer)}");
        Debug.Log($"✅ 敌人 Tag: '{enemy.tag}' (expected: 'Enemy')");
    }

    private void CheckLayerCollisionMatrix()
    {
        Debug.Log("\n--- Layer Collision Matrix 检查 ---");

        int playerLayer = gameObject.layer;
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (enemyLayer == -1)
        {
            Debug.LogError("❌ 'Enemy' 层不存在！");
            return;
        }

        bool canCollide = !Physics2D.GetIgnoreLayerCollision(playerLayer, enemyLayer);
        Debug.Log($"✅ Player Layer ({LayerMask.LayerToName(playerLayer)}) 和 Enemy Layer 可以碰撞: {canCollide}");

        if (!canCollide)
        {
            Debug.LogError("❌ Layer Collision Matrix 未勾选！请打开 Project Settings → Physics2D");
        }
    }

    private void TestDistance(GameObject enemy)
    {
        Debug.Log("\n--- 距离测试 ---");

        var detector = GetComponentInChildren<Collider2D>();
        var enemyCol = enemy.GetComponent<Collider2D>();

        if (detector != null && enemyCol != null)
        {
            float distance = Vector2.Distance(detector.bounds.center, enemyCol.bounds.center);
            Debug.Log($"✅ 触发器中心到敌人中心距离: {distance:F2}");

            // 检查是否重叠
            bool overlaps = detector.bounds.Intersects(enemyCol.bounds);
            Debug.Log($"✅ 是否重叠: {overlaps}");

            if (!overlaps)
            {
                Debug.LogWarning("⚠️ 触发器和敌人没有重叠，请调整位置或大小");
            }
        }
    }

    private string LayerMaskToString(int mask)
    {
        List<string> layers = new List<string>();
        for (int i = 0; i < 32; i++)
        {
            if ((mask & (1 << i)) != 0)
            {
                string layerName = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(layerName))
                {
                    layers.Add(layerName);
                }
            }
        }
        return layers.Count > 0 ? string.Join(", ", layers) : "Nothing";
    }

    // 运行时测试：按空格键
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("\n===== 运行时碰撞测试 =====");

            var detector = GetComponentInChildren<Player_colliderControl>();
            if (detector != null)
            {
                Debug.Log($"当前 hasEnemy: {detector.hasEnemy}");
            }

            // 手动检测
            var col = GetComponentInChildren<Collider2D>();
            if (col != null)
            {
                var contacts = new List<Collider2D>();
                col.GetContacts(contacts);
                Debug.Log($"触发器接触数量: {contacts.Count}");

                foreach (var contact in contacts)
                {
                    Debug.Log($"  - 接触: {contact.name}, Tag={contact.tag}");
                }
            }
        }
    }
}
