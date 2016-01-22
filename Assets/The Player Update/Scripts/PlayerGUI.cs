using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerGUI : MonoBehaviour 
{
    public static PlayerGUI Instance;

    public GUITextElement AmmoLabel;
    
    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        AmmoLabel.UpdateElement();
    }
    
}
[System.Serializable]
public class GUIElement
{
    public Vector2 OriginalPosition;
    public Vector2 OffScreenPosition;

    public bool OnScreen = false;

    public RectTransform transform;
    private Vector2 dampingVelocity = Vector2.zero;

    [Tooltip("In Seconds")]
    public float SmoothTime = 0.5f;

    public virtual void UpdateElement()
    {
        Vector2 desiredPosition = OnScreen ? OriginalPosition : OffScreenPosition;
        transform.anchoredPosition = Vector2.SmoothDamp(transform.anchoredPosition, desiredPosition, ref dampingVelocity, SmoothTime);
    }

    [ContextMenu("Set On Screen Pos")]
    public void SetOnScreenPosition()
    {
        OriginalPosition = transform.anchoredPosition;
    }

    [ContextMenu("Set Off Screen Pos")]
    public void SetOffScreenPosition()
    {
        OffScreenPosition = transform.anchoredPosition;
    }
}

[System.Serializable]
public class GUITextElement : GUIElement
{
    public Text text;
    
    public float OnScreenTime = 0.5f;
    public float currentOnScreenTime;

    public void Start()
    {
        if(text != null)
            text = transform.GetComponent<Text>();
    }

    public void SetText(string text)
    {
        PopGUI(OnScreenTime);
        this.text.text = text;
    }

    public void PopGUI(float time)
    {
        OnScreen = true;
        currentOnScreenTime = time;
    }

    public override void UpdateElement()
    {
        base.UpdateElement();

        if(currentOnScreenTime > 0)
            currentOnScreenTime -= Time.deltaTime;

        if (currentOnScreenTime <= 0)
        {
            currentOnScreenTime = 0;
            OnScreen = false;
        }
    }
}
