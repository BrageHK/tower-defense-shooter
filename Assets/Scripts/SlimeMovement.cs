using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SlimeMovement : MonoBehaviour
{
    public float HP = 100f;
    public float HPBarOffset = 0.01f;
    public float speed = 0.3f;
    public int level;
    public Tilemap tilemap;
    public float distanceFromLastCheckpoint { get; private set; }
    public int movePositionIndex;
    public int deathMoney = 5;

    private Vector2Int[] movePositions;
    private Rigidbody2D rb;
    private HealthBar healthBar;
    private GameObject healthBarObject;
    private GameObject healthBarObjectCopy;
    private Camera cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        level = 0;
        movePositions = new Vector2Int[LevelData.PathCorners.GetLength(1)];
        tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        healthBarObject = Resources.Load("Prefabs/HP Bar", typeof(GameObject)) as GameObject;
        healthBarObjectCopy = Instantiate(healthBarObject, transform.position + Vector3.up*HPBarOffset, Quaternion.identity, GameObject.Find("MainCanvas").transform);
        healthBar = healthBarObjectCopy.GetComponent<HealthBar>();
        healthBar.SetMaxHealth((int)HP);
        cam = Camera.main;
        
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
        distanceFromLastCheckpoint = Vector2.Distance(transform.position, GetWorldPosition(movePositions[movePositionIndex-1]));
        healthBarObjectCopy.transform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0, HPBarOffset, 0));

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
            healthBar.SetHealth((int)HP);
            if (HP <= 0) {
                OnDeath();
            }
        }
    }

    public Vector2 GetDirectionVector(float extraDistance)
    {
        return Vector2.ClampMagnitude(GetWorldPosition(movePositions[movePositionIndex]) - (Vector2)transform.position, extraDistance);
    }

    private void OnDeath()
    {
        
        GameObject.FindGameObjectWithTag("Grid").GetComponent<LevelController>().money += deathMoney;
        Destroy(healthBarObjectCopy);
        Destroy(gameObject);
    }
}
