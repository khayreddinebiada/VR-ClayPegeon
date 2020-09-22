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

    public Camera[] camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text1.text = "01: " + camera[0].fieldOfView.ToString();
        text2.text = "02: " + camera[1].fieldOfView.ToString();
        text3.text = "03: " + camera[2].fieldOfView.ToString();
    }
}
