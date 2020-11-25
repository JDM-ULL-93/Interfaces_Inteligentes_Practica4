using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject colorSelectorsParent = null;


    private Canvas canvas;
    private UnityEngine.EventSystems.EventTrigger eventHandler;
    private UnityEngine.UI.Button[] buttons;
    private static GameObject current = null;
    private void Awake()
    {
        if (colorSelectorsParent == null)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            throw new System.Exception("No se ha seteado un contenedor de botones para ColorPickerHandler");
        }

        eventHandler = this.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
        canvas = colorSelectorsParent.gameObject.GetComponentInParent<Canvas>();
        buttons = colorSelectorsParent.GetComponentsInChildren<UnityEngine.UI.Button>();
      
    }
    private void Start()
    {
        foreach (UnityEngine.UI.Button button in buttons)
            button.onClick.AddListener(() =>
            {
                Color colorToSet = button.GetComponent<UnityEngine.UI.Image>().color;
                current.GetComponent<Renderer>().material.color = colorToSet;
            });
        colorSelectorsParent.SetActive(false);

        UnityEngine.EventSystems.EventTrigger.Entry onPointerEnterGO = new UnityEngine.EventSystems.EventTrigger.Entry() { 
            eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter,
        };
        onPointerEnterGO.callback.AddListener(
            (UnityEngine.EventSystems.BaseEventData eventData) => {
                canvas.transform.SetParent(this.transform, false);
                colorSelectorsParent.SetActive(true);
                var extendedEventData = eventData as GvrPointerEventData;
                current = extendedEventData.pointerEnter;
                foreach (UnityEngine.UI.Button button in buttons)
                {
                    Color randomColor = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                    button.GetComponent<UnityEngine.UI.Image>().color = randomColor;
                }

            });
        eventHandler.triggers.Add(onPointerEnterGO);

        UnityEngine.EventSystems.EventTrigger.Entry onPointerExitGO = new UnityEngine.EventSystems.EventTrigger.Entry()
        {
            eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit,
        };
        onPointerExitGO.callback.AddListener(
            (UnityEngine.EventSystems.BaseEventData eventData) => {
                canvas.transform.SetParent(null, false);
                colorSelectorsParent.SetActive(false);
                //colorSelectorsParent.SetActive(false);
            });
        eventHandler.triggers.Add(onPointerExitGO);
    }

    // Update is called once per frame
    private void Update()
    {
        if (colorSelectorsParent.activeSelf)
        {
            canvas.transform.rotation = Camera.main.transform.rotation;
        }
    }
}
