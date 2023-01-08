using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    RectTransform currentPosition;
    Vector2 originalPosition;
    Vector2 selectedPosition;

    bool doOnce;

    private void Start()
    {
        // Set positions
        currentPosition = GetComponent<RectTransform>();
        originalPosition = currentPosition.anchoredPosition;
        selectedPosition = originalPosition + Vector2.right * 35;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            currentPosition.anchoredPosition = new Vector2(Mathf.Lerp(currentPosition.anchoredPosition.x, selectedPosition.x, 5 * Time.deltaTime), currentPosition.anchoredPosition.y);
            if (!doOnce)
                doOnce = true;
        }
        else if (EventSystem.current.currentSelectedGameObject != this.gameObject && originalPosition != GetComponent<RectTransform>().anchoredPosition)
        {
            currentPosition.anchoredPosition = new Vector2(Mathf.Lerp(currentPosition.anchoredPosition.x, originalPosition.x, 5 * Time.deltaTime), currentPosition.anchoredPosition.y);
            if (doOnce)
            {
                //AudioManager.Instance.PlaySound();
                doOnce = false;
            }
        }
    }
}
