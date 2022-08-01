using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SlimeMovement : MonoBehaviour
{
    public float HP = 100f;
    public float speed = 0.3f;
    public int level;
    public Tilemap tilemap;
    public float distanceFromLastCheckpoint;
    public int movePositionIndex;

    private Vector2Int[] movePositions;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        level = 0;
        movePositions = new Vector2Int[LevelData.PathCorners.GetLength(1)];
        tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }
    
    void Start()
    {
        for (int i = 0; i < LevelData.PathCorners.Length; i++) {
            movePositions[i] = LevelData.PathCorners[level, i];
        }
        transform.position = GetWorldPosition(movePositions[0]);
    }

    void Update()
    { 
        //Vector3 transform.position
        transform.position = Vector2.MoveTowards(transform.position, GetWorldPosition(movePositions[movePositionIndex]), speed * Time.deltaTime);
        CheckPosition();
    }

    private void CheckPosition()
    {
        if (Vector2.Distance(transform.position, GetWorldPosition(movePositions[movePositionIndex])) < 0.01f) {
            movePositionIndex++;
            if (movePositionIndex >= movePositions.Length) {
                Destroy(gameObject);
            }
        }
    }

    private Vector2 GetWorldPosition(Vector2Int position) {
        return tilemap.CellToWorld(new Vector3Int(position.x, position.y, 0));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bullet")) {
            HP -= other.gameObject.GetComponent<BulletController>().damage;
            if (HP <= 0) {
                Destroy(gameObject);
            }
        }
    }

    public Vector2 GetDirectionVector(float extraDistance)
    {
        return Vector2.ClampMagnitude(GetWorldPosition(movePositions[movePositionIndex]) - (Vector2)transform.position, extraDistance);
    }

}
