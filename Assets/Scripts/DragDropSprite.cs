using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSprite : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private bool isLegalPosition = false;
    private Vector2 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
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
        if (!isLegalPosition)
        {
            
        }
        
        rectTransform.anchoredPosition = startPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    private Vector2 ClosestEnemyPosition()
    {
        //Vector2 closestEnemyPosition;
        //int highestIndex = 0;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++) {
            //if(enemies[i].GetComponentInChildren(SlimeMovement).)
        }
        return new Vector2(1, 1);
    }

    
}
