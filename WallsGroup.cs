using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsGroup : MonoBehaviour
{
    GameObject theScreenControl;
    ScreenControl screenControlScript;
    public Transform w1, w2, w3, w4;
    public Transform[] walls;
    public List<int> wallsInd,wallsIndTemp;
    // Start is called before the first frame update
    void Start()
    {
        theScreenControl = GameObject.Find("Screen");
        screenControlScript = theScreenControl.GetComponent<ScreenControl>();
        walls = new Transform[4];
        wallsInd = new List<int>();
        wallsIndTemp = new List<int>();


        walls[0] = w1;
        walls[1] = w2;
        walls[2] = w3;
        walls[3] = w4;
        for(int i = 0; i < 4; i++)
        {
            GetRandInt();
        }
        if (screenControlScript.gameStep == 1)
        {
            Destroy(walls[wallsInd[3]].gameObject);
            Destroy(walls[wallsInd[2]].gameObject);
            Destroy(walls[wallsInd[1]].gameObject);
        }

        if (screenControlScript.gameStep == 2)
        {
            Destroy(walls[wallsInd[3]].gameObject);
            Destroy(walls[wallsInd[2]].gameObject);

        }

        if (screenControlScript.gameStep == 3)
        {
            Destroy(walls[wallsInd[3]].gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    int GetRandInt()
    {
        int rnd = Random.Range(0, 4);
        while (wallsInd.Contains(rnd))
        {
            rnd = Random.Range(0, 4);
        }
        wallsInd.Add(rnd);
        return rnd;
    }
}
