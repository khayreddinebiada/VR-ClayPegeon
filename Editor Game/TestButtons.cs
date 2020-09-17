using UnityEngine;
using UnityEngine.UI;

public class TestButtons : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text1.text = "OVRInput.Button.Down: " + OVRInput.Get(OVRInput.Button.Down).ToString();
        text2.text = "OVRInput.Button.PrimaryIndexTrigger: " + OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger).ToString();
        text3.text = "OVRInput.Button.Start: " + OVRInput.Get(OVRInput.Button.Start).ToString();
        text4.text = "OVRInput.Button.Up: " + OVRInput.Get(OVRInput.Button.Up).ToString();
        text5.text = "OVRInput.Button.Down: " + OVRInput.Get(OVRInput.Button.Down).ToString();
        text6.text = "OVRInput.Button.Right: " + OVRInput.Get(OVRInput.Button.Right).ToString();
    }
}
