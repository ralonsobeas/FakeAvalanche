using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float speed;
    public float scroll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        float contentHeight = scrollRect.content.sizeDelta.y;
        float contentShift = speed * scroll * Time.deltaTime;
        scrollRect.verticalNormalizedPosition += contentShift / contentHeight;
    }

}
