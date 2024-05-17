using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;



public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform spriteTransform;

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.instance.path[pathIndex];
    }

    private void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            Debug.Log(pathIndex);

            if(pathIndex == LevelManager.instance.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.instance.path[pathIndex];

                // Tính hướng vector giữa enemy và điểm tiếp theo
                Vector2 direction = (target.position - transform.position).normalized;

                // Tính góc quay từ hướng hiện tại sang hướng mới
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) +90f;

                // Quay enemy đến hướng mới
                spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);    
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
