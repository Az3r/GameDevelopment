using UnityEngine;

public class FrameCapture : MonoBehaviour
{
    public UnityEngine.UI.Text text;

    private float delta;
    // Update is called once per frame
    void Update()
    {
        delta += (Time.unscaledDeltaTime - delta) * 0.1f;
    }

    void OnGUI()
    {
        text.text = Mathf.Ceil(1f / delta).ToString();
    }
}
