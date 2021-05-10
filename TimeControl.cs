using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{

    [SerializeField] Text timeValue;
    [SerializeField] float time;
    private bool gameActive; // Oyun aktif mi bunu kontrol eden bir özellik olacak




    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
        timeValue.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if(gameActive==true)
        {
            time -= Time.deltaTime; // Time.deltaTime 2 frame arasýndaki geçiþ süresidir.
            timeValue.text = ((int)time).ToString(); // Unity de  Time kýsmý virgüllü þekilde görünüyordu o yüzden int yani tam sayý þekilde saniyeyi(time'ý) ayarlamýþ olduk.
        }       

        if(time<=0)
        {
            time = 60; // Artýk Player'ým defalarca ölmemiþ oldu.
            gameActive = false;
            GetComponent<PlayerController>().Die();
        }
    }
}
