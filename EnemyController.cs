using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] bool onGround; // BU onGround dedigimiz Bizim Engelimizin zemini
    [SerializeField] float speed;
    private float width;    // I��k Cambaz objesinde Tam ortada duruyor bunu ayarlamak i�in b�yle bir de�i�ken olu�turduk.
    private Rigidbody2D myBody;
    [SerializeField] LayerMask engel; // Beyaz ���g�m�z� LayerMask yaparak SADECE onGround'dayken I�ININ OLU�MASINI SA�LAR. LayerMask: Layer'�n(katman�) �evreleyen

    private static int totalEnemyNumber=0; // STAT�C YAZDIGIN ZAMAN BU �ZELL�K ARTIK ENEMYCONTROLLER CLASS'INA(SINIFINA) A�T B�R �ZELL�K OLARAK ��LEV G�R�R.YAN� ENEMYCONTROLLER SINIFINA SAH�P OBJELER�N TAMAMINI �LG�LEND�REN B�R �ZELL�K OLDU !!!!!

    // Toplam d��man say�s�n� tutucag�m�z bir �zellik tan�mlad�k. [STAT�C YAZDIGIN ZAMAN BU �ZELL�K ARTIK ENEMYCONTROLLER CLASS'INA(SINIFINA) A�T B�R �ZELL�K OLARAK ��LEV G�R�R.


    // Fizik Hat�rlayal�m :
    // 5 * (3,3,3) = (15,15,15)
    // (1,0,0) (0,1,0) (0,0,1) 

    // Start is called before the first frame update
    void Start()
    {
        totalEnemyNumber++; // Her bir canavar dogdugunda  totalEnemyNumber �zelligim 1 artacak.
       // Debug.Log("D��man ismi :"+gameObject.name+" olu�tu."+"Oyundaki Toplam D��man Say�s� : "+totalEnemyNumber) ;

        width = GetComponent<SpriteRenderer>().bounds.extents.x;     // bounds : S�n�rlar extents(kapsam�) : Uzunluk ve Geni�li�in yar�s�n� al�r .x diyerek GEN��L���N YARISINI ALMI� OLDUK.
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width/ 2) , Vector2.down,10f);
                            // transform.right : Bizim objemizin sa� taraf� nereye bak�yorsa onun birim vekt�r�n� buluyoruz.
                           // RaycastHit2D ���g�n �arpmas�n� kontrol eden objedir. \\\\\Physics2D.Raycast = I��g�n �izilmesini sa�lar.\\\\\ transform.pozition = Objemizin pozisyonunu ald�k. Hit : De�mek
                          // I��n objemizin pozisyonundan ba�layacak, y�n� a�ag� olacak ve ���n�n uzunlugu 2f kadar olacak.

         RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width/ 2) , Vector2.down,10f,engel); // En sona engel '� yazarak Bu Hit'in yanii ���n�n hangi Layer 'larda �al��acag�na karar vermi� olduk.


        if(hit.collider !=null) // I��k herhangi bir �eye �ARPIYORSA bana �u i�lemleri yap : 
        {
            onGround = true; // B�zim cambaz�m�n zemine(onGround'a)  �arp�yorsa zeminin(onGround'�n) �zerindedir.
        }
        else // I��n herhangi bir objeye �arpm�yorsa : 
        {
            onGround = false;
        }

        Flip();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 playerRealPosition = transform.position + (transform.right * width / 2);
        Gizmos.DrawLine(playerRealPosition , playerRealPosition + new Vector3(0, -10f, 0));  //ARTIK ��ZG�M�Z �STED�G�M�Z G�B� CAMBAZ 'IN EN SA�INA GELM�� OLDU.
        // Gizmo(�ey) bir �izgi �izsin.BU �izgi nerden ba�las�n ? Objemin Pozisyonundan ba�las�n. Nerde sonlans�n ? objemin Y y�n�nde 2f kadar a�ag�s�nda sonlas�n.
    }

    void Flip() // Cambaz�n D�NME ��LEM� KODLARI [Flip : D�nme ]
    {
        if (!onGround) // Cambaz objem zemin �zerinde degilse bana �u i�lemleri yap :
        {
            transform.eulerAngles += new Vector3(0, 180f, 0); // Cambaz objemin Derece cinsinden a�� olarak d�nd�r�cem  \\\\\ Yani Cambaz objemi 180 derece olarak d�nd�rm�� oldum. \\\\\\ eulerAngles : Derece cinsinden a�� olarak d�nd�rmedir.
        }
        // �imdi Hareket ��lemlerimizi yaz�caz : 
        myBody.velocity = new Vector2(transform.right.x * speed, 0f); //  Cambaz art�k  sa� ve sola hareketlerini yapabilecek
    }




}
