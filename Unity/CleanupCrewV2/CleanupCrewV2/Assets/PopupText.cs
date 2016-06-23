using UnityEngine;
using System.Collections;

public class PopupText : MonoBehaviour {

    float timer = 5.0f;
    UnityEngine.UI.Text _Text;
    // Use this for initialization
    void Start () {
	
	}

    public void SetValue(float value)
    {
        _Text = gameObject.GetComponent<UnityEngine.UI.Text>();
        _Text.color = value >=0? Color.green : Color.red;
        _Text.text = (value >=0? "+": "-") + value.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0) GameObject.Destroy(gameObject.transform.parent.gameObject);
        transform.Translate(Vector3.down * Time.deltaTime * 25f);
        if (_Text != null) _Text.color = new Color(_Text.color.r, _Text.color.g, _Text.color.b, _Text.color.a - Time.deltaTime * 0.5f);
	}
}
