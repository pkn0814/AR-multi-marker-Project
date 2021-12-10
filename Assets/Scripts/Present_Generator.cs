using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Present_Generator : MonoBehaviour
{
    public GameObject presentPrefab;
    public GameObject glow;
    int cnt = 0;
    public Text score;

    float span = 0.5f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        check();
    }

    void check()
    {
        
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            float randomX = Random.Range(-3f, 3f);
            if (true)
            {
                GameObject item = (GameObject)Instantiate(presentPrefab, new Vector3(randomX, 1.1f, 1f), Quaternion.identity); // ¼±¹° ¾ÆÀÌÅÛ »ý¼º
                if((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
                    {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        if (raycastHit.collider.CompareTag("Present"))
                        {
                            Destroy(item);
                            Instantiate(glow, Camera.main.transform.position, Camera.main.transform.rotation); // ÀÌÆåÆ®
                            getscr(); // Á¡¼ö¸¦ È¹µæÇÑ´Ù.
                        }
                    }
                }
            }
        }
    }

    public void getscr()
    {
        cnt++; // Á¡¼ö È¹µæ
        score.text = cnt + "Á¡";
    }
}
