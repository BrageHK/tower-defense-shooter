using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathText : MonoBehaviour
{

    private float timer = 0;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        Instantiate<TextMeshProUGUI>(GameObject.Find("DeathMoneyTextTemplate").GetComponent<TextMeshProUGUI>(), transform);
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + timer, transform.position.z);
        timer += Time.deltaTime;
        if (timer >= 0.7f)
        {
            Destroy(gameObject);
        }
    }
}
