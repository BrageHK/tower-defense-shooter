using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;


public class DragDropSprite : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    public Tilemap tilemap;
    public GameObject tower;



    private RectTransform rectTransform;
    private RectTransformUtility rectTransformUtility;
    private Vector2 startPosition;
    private Camera cam;
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
        cam = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        
        //string cellName = tilemap.GetTile(cellPosition).ToString();
        Vector3 worldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPosition = tilemap.WorldToCell(worldPos);
        if (tilemap.GetTile(cellPosition).name == "grass")
        {
            Instantiate(tower, tilemap.CellToWorld(cellPosition), Quaternion.identity);
        }
        rectTransform.anchoredPosition = startPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
