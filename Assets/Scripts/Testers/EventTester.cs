using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_Test, EventTest);
        EventCenter.Instance.AddEventListener<int>(E_EventType.E_Test2, EventTest2);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_EventType.E_Test, EventTest);
        EventCenter.Instance.RemoveEventListener<int>(E_EventType.E_Test2, EventTest2);
    }
    private void EventTest()
    {
        Debugger.LogPink("事件触发");
    }
    private void EventTest2(int i)
    {
        Debugger.LogPink("事件触发" + i);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Test);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventCenter.Instance.EventTrigger(E_EventType.E_Test2, 2);
        }
    }
}
