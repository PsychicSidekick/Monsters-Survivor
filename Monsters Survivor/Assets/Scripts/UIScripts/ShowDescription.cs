using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string title;
    [TextArea(15, 20)]
    public string description;
    public GameObject descriptionPanel;
    public bool fixedPos;
    private bool mouseOvered = false;

    private void Start()
    {
        SetDescriptionText();
    }

    private void Update()
    {
        if (mouseOvered && !fixedPos)
        {
            SetDescriptionPos();
        }

        if (mouseOvered && Input.GetKey("s"))
        {
            mouseOvered = false;
            descriptionPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOvered = true;
        SetDescriptionText();
        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOvered = false;
        descriptionPanel.SetActive(false);
    }

    private void SetDescriptionText()
    {
        TMP_Text[] texts = descriptionPanel.GetComponentsInChildren<TMP_Text>();
        texts[0].text = title;
        texts[1].text = description;
    }

    private void SetDescriptionPos()
    {
        float xPos = FindDescriptionXPos();
        float yPos = FindDescriptionYPos();

        descriptionPanel.transform.position = new Vector2(xPos, yPos);
    }

    private float FindDescriptionXPos()
    {
        float descriptionHorizontalOffset = GetComponent<RectTransform>().rect.width / 2;

        // Puts description to the right if this UI element is on the left of the screen, and vise versa
        if (transform.localPosition.x > 0)
        {
            descriptionHorizontalOffset += descriptionPanel.GetComponent<RectTransform>().rect.width;
            descriptionHorizontalOffset *= -1;
        }

        return transform.position.x + descriptionHorizontalOffset;
    }

    private float FindDescriptionYPos()
    {
        float descriptionVerticalOffset = descriptionPanel.GetComponent<RectTransform>().rect.height / 2;

        RectTransform descriptionRect = descriptionPanel.GetComponent<RectTransform>();
        RectTransform parentRect = transform.parent.GetComponent<RectTransform>();

        // Prevents description from going off screen vertically
        float minValue = descriptionRect.rect.height;
        float maxValue = parentRect.rect.height;

        return Mathf.Clamp(transform.position.y + descriptionVerticalOffset, minValue, maxValue);
    }
}
