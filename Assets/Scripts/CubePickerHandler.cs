using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePickerHandler : MonoBehaviour
{
    private UnityEngine.EventSystems.EventTrigger eventHandler;
    private Renderer render;
    private void Awake()
    {
        eventHandler = this.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
        render = this.GetComponent<Renderer>();
        render.sharedMaterial.SetFloat("_Progress", 1.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.EventSystems.EventTrigger.Entry onPointerClick = new UnityEngine.EventSystems.EventTrigger.Entry()
        {
            eventID = UnityEngine.EventSystems.EventTriggerType.PointerClick,
        };
        onPointerClick.callback.AddListener(
            (UnityEngine.EventSystems.BaseEventData eventData) => {

                StartCoroutine("fade");

            });
        eventHandler.triggers.Add(onPointerClick);
    }

    public IEnumerator fade()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.01f)
        {
            render.sharedMaterial.SetFloat("_Progress", ft);
            yield return new WaitForSeconds(.01f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
