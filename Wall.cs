using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    GameObject theScreenControl;
    ScreenControl screenControlScript;
    public int type;// 0 - white, 1 - black
    public Sprite black;
    public Sprite white;

    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        theScreenControl = GameObject.Find("Screen");
        screenControlScript = theScreenControl.GetComponent<ScreenControl>();
        type = Random.Range(0, 2);

        render = GetComponent<SpriteRenderer>();

       if(type == 0)
        {
            render.sprite = white;
        }

        if (type == 1)
        {
            render.sprite = black;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (screenControlScript.gameRun)
        {
            this.transform.Translate(Vector2.down * screenControlScript.wallSpeed * Time.deltaTime);
        }

        if (screenControlScript.gameOver)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Black" && type == 1)
        {
            //Debug.Log("Collide Trigger!");
            screenControlScript.scorePoints++;
            Destroy(this.transform.gameObject);
        }

        if (collision.tag == "White" && type == 0)
        {
            //Debug.Log("Collide Trigger!");
            screenControlScript.scorePoints++;
            Destroy(this.transform.gameObject);
        }

        if (collision.tag == "Black" && type == 0) // if Black collide with white
        {
            screenControlScript.gameRun = false;
            screenControlScript.gameOver = true;
            screenControlScript.rotateObject.gameObject.SetActive(false);

        }

        if (collision.tag == "White" && type == 1) // if White collide with black
        {
            screenControlScript.gameRun = false;
            screenControlScript.gameOver = true;
            screenControlScript.rotateObject.gameObject.SetActive(false);
        }

    }
}
