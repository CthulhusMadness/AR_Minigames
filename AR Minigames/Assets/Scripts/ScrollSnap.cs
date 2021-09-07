using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollSnap : MonoBehaviour
{
    #region Fields

    [Serializable]
    public struct ScrollElement
    {
        public GameObject element;
        public int index;
        public float stepValue;

        public ScrollElement(GameObject element, int index, float step)
        {
            this.element = element;
            this.index = index;
            this.stepValue = step;
        }
    }

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField, Min(0)] private float snapOffset = .5f;
    [SerializeField] private float snapSpeed = 1f;
    [SerializeField] private List<ScrollElement> scrollElements = new List<ScrollElement>();

    private Transform content = null;
    private Scrollbar scrollbarHoriz = null;
    [SerializeField] private ScrollElement focus = new ScrollElement();
    [SerializeField] private ScrollElement newFocus = new ScrollElement();
    //private Scrollbar scrollbarVert = null;

    #endregion

    #region UnityCallbacks

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        scrollbarHoriz = scrollRect.horizontalScrollbar;

        LoadElements();
        focus = scrollElements[0];
        scrollbarHoriz.value = focus.stepValue;
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
            CheckElementOnFocusPosition();
        else
            Snap();
    }

    #endregion

    #region Methods

    private void LoadElements()
    {
        scrollElements.Clear();
        for (int i = 0; i < content.childCount; i++)
        {
            GameObject element = content.GetChild(i).gameObject;
            float step = (float)i / (content.childCount - 1f);
            ScrollElement scrollElement = new ScrollElement(element, i, step);
            scrollElements.Add(scrollElement);
        }
    }

    private void CheckElementOnFocusPosition()
    {
        float leftSnap = focus.stepValue - snapOffset / content.childCount;
        float rightSnap = focus.stepValue + snapOffset / content.childCount;
        if (focus.index > 0 && scrollbarHoriz.value < leftSnap)
            newFocus = scrollElements[focus.index-1];
        else if (focus.index < content.childCount - 1 && scrollbarHoriz.value > rightSnap)
            newFocus = scrollElements[focus.index+1];
        else
            newFocus = focus;
    }

    private void Snap()
    { 
        focus = newFocus;
        scrollbarHoriz.value = Mathf.MoveTowards(scrollbarHoriz.value, focus.stepValue, Time.deltaTime * snapSpeed);
    }

    #endregion

}
