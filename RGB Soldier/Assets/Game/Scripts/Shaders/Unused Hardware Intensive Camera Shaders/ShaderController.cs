using UnityEngine;

public class ShaderController : MonoBehaviour {
    public bool isDisco = true;
    public float lastSwitch;
    public Red red;
    public Grayscale gray;
    public Blue blue;
    public Green green;

    public string current;

    void Start()
    {
        red = this.gameObject.GetComponent<Red>();
        green = this.gameObject.GetComponent<Green>();
        blue = this.gameObject.GetComponent<Blue>();

        gray = this.gameObject.GetComponent<Grayscale>();
        lastSwitch = Time.time;

        current = "grey";
    }

    void Update()
    {
        if (lastSwitch + 2 < Time.time)
        {
            if (isDisco)
            {
                Switch();
            }
        }
    }

    public void Switch()
    {
        lastSwitch = Time.time;
        if (current == "grey")
        {
            current = "red";
            SetColourScheme(current);
        }
        else if (current == "red")
        {
            current = "green";
            SetColourScheme(current);
        }
        else if (current == "green")
        {
            current = "blue";
            SetColourScheme(current);
        }
        else if (current == "blue")
        {
            current = "grey";
            SetColourScheme(current);
        }
    }

    public void SetColourScheme(string colour)
    {
        if (colour == "grey")
        {
            red.enabled = false;
            green.enabled = false;
            blue.enabled = false;
            gray.enabled = true;
        }
        else if (colour == "red")
        {
            red.enabled = true;
            green.enabled = false;
            blue.enabled = false;
            gray.enabled = false;
        }
        else if (colour == "green")
        {
            red.enabled = false;
            green.enabled = true;
            blue.enabled = false;
            gray.enabled = false;
        }
        else if (colour == "blue")
        {
            red.enabled = false;
            green.enabled = false;
            blue.enabled = true;
            gray.enabled = false;
        }
    }
}
