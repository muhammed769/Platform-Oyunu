using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{

    [SerializeField] Text timeValue;
    [SerializeField] float time;
    private bool gameActive; // Oyun aktif mi bunu kontrol eden bir �zellik olacak




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
            time -= Time.deltaTime; // Time.deltaTime 2 frame aras�ndaki ge�i� s�residir.
            timeValue.text = ((int)time).ToString(); // Unity de  Time k�sm� virg�ll� �ekilde g�r�n�yordu o y�zden int yani tam say� �ekilde saniyeyi(time'�) ayarlam�� olduk.
        }       

        if(time<=0)
        {
            time = 60; // Art�k Player'�m defalarca �lmemi� oldu.
            gameActive = false;
            GetComponent<PlayerController>().Die();
        }
    }
}
