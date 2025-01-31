using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour {

    public static TooltipUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TooltipTimer tooltipTimer;

    private void Awake() {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        Hide();
    }

    private void Update() {
        HandleFollowMouse();

        if (tooltipTimer != null) {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0f) {
                Hide();
            }
        }
    }

    public void HandleFollowMouse() {
        Vector2 anchoredPosition = rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        if (anchoredPosition.x < 0) {
            anchoredPosition.x = 0;
        }

        if (anchoredPosition.y < 0) {
            anchoredPosition.y = 0;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    public void Show(string tooltipText, TooltipTimer tooltipTimer = null) {
        this.tooltipTimer = tooltipTimer;

        this.gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

    private void SetText(string tooltipText) {
        textMeshPro.SetText(tooltipText);
        // Force it to update on this frame
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public class TooltipTimer {
        public float timer;
    }
}
