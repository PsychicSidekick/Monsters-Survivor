using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShowDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string title;
    [TextArea(15, 20)]
    public string description;
    public GameObject descriptionPanel;

    private bool mouseOvered = false;

    private void Start()
    {
        SetDescriptionText();
    }

    private void Update()
    {
        if (mouseOvered)
        {
            float xPos = FindDescriptionXPos();
            float yPos = FindDescriptionYPos();

            descriptionPanel.transform.position = new Vector2(xPos, yPos);
        }

        if (Input.GetKey("s"))
        {
            descriptionPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetDescriptionText();
        mouseOvered = true;
        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetDescriptionText();
        mouseOvered = false;
        descriptionPanel.SetActive(false);
    }

    private void SetDescriptionText()
    {
        TMP_Text[] texts = descriptionPanel.GetComponentsInChildren<TMP_Text>();
        texts[0].text = title;
        texts[1].text = description;
    }

    private void ResetDescriptionText()
    {
        TMP_Text[] texts = descriptionPanel.GetComponentsInChildren<TMP_Text>();
        texts[0].text = "";
        texts[1].text = "";
    }

    private float FindDescriptionXPos()
    {
        float descriptionHorizontalOffset = GetComponent<RectTransform>().rect.width / 2;

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

        float minValue = descriptionRect.rect.height;
        float maxValue = parentRect.rect.height;

        return Mathf.Clamp(transform.position.y + descriptionVerticalOffset, minValue, maxValue);
    }
}
