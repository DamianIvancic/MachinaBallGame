using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {


    private iconAnimation ballAnim;
    private TMPro.TextMeshProUGUI MessageText;

    [HideInInspector]
    public float RefreshPeriod = 1;
    private float DeltaTime = 0;

	void Start () {

        ballAnim = GetComponentInChildren<iconAnimation>();
        MessageText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
	}
	
    void Update()
    {
        if(MessageText.text != null && RefreshPeriod != 0)
        {
            DeltaTime += Time.deltaTime;
            if (DeltaTime > RefreshPeriod)
            {
                Clear();
                DeltaTime = 0;
            }
        }
    }

    public void Display(string Message)
    {
        if (!MessageText)
            Debug.Log("Error! No MessageText");

        MessageText.text = Message;
    }

    public void Clear()
    {
        MessageText.text = null;
        RefreshPeriod = 1;
    }

    public void WarningMessage()
    {     
        ballAnim.playAnimation();
        RefreshPeriod = 2;
        Display("HEY! Watch out...");
    }
}
