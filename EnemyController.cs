using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] bool onGround; // BU onGround dedigimiz Bizim Engelimizin zemini
    [SerializeField] float speed;
    private float width;    // Iþýk Cambaz objesinde Tam ortada duruyor bunu ayarlamak için böyle bir deðiþken oluþturduk.
    private Rigidbody2D myBody;
    [SerializeField] LayerMask engel; // Beyaz ýþýgýmýzý LayerMask yaparak SADECE onGround'dayken IÞININ OLUÞMASINI SAÐLAR. LayerMask: Layer'ýn(katmaný) çevreleyen

    private static int totalEnemyNumber=0; // STATÝC YAZDIGIN ZAMAN BU ÖZELLÝK ARTIK ENEMYCONTROLLER CLASS'INA(SINIFINA) AÝT BÝR ÖZELLÝK OLARAK ÝÞLEV GÖRÜR.YANÝ ENEMYCONTROLLER SINIFINA SAHÝP OBJELERÝN TAMAMINI ÝLGÝLENDÝREN BÝR ÖZELLÝK OLDU !!!!!

    // Toplam düþman sayýsýný tutucagýmýz bir özellik tanýmladýk. [STATÝC YAZDIGIN ZAMAN BU ÖZELLÝK ARTIK ENEMYCONTROLLER CLASS'INA(SINIFINA) AÝT BÝR ÖZELLÝK OLARAK ÝÞLEV GÖRÜR.


    // Fizik Hatýrlayalým :
    // 5 * (3,3,3) = (15,15,15)
    // (1,0,0) (0,1,0) (0,0,1) 

    // Start is called before the first frame update
    void Start()
    {
        totalEnemyNumber++; // Her bir canavar dogdugunda  totalEnemyNumber özelligim 1 artacak.
       // Debug.Log("Düþman ismi :"+gameObject.name+" oluþtu."+"Oyundaki Toplam Düþman Sayýsý : "+totalEnemyNumber) ;

        width = GetComponent<SpriteRenderer>().bounds.extents.x;     // bounds : Sýnýrlar extents(kapsamý) : Uzunluk ve Geniþliðin yarýsýný alýr .x diyerek GENÝÞLÝÐÝN YARISINI ALMIÞ OLDUK.
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width/ 2) , Vector2.down,10f);
                            // transform.right : Bizim objemizin sað tarafý nereye bakýyorsa onun birim vektörünü buluyoruz.
                           // RaycastHit2D ýþýgýn çarpmasýný kontrol eden objedir. \\\\\Physics2D.Raycast = Iþýgýn çizilmesini saðlar.\\\\\ transform.pozition = Objemizin pozisyonunu aldýk. Hit : Deðmek
                          // Iþýn objemizin pozisyonundan baþlayacak, yönü aþagý olacak ve ýþýnýn uzunlugu 2f kadar olacak.

         RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width/ 2) , Vector2.down,10f,engel); // En sona engel 'ý yazarak Bu Hit'in yanii ýþýnýn hangi Layer 'larda çalýþacagýna karar vermiþ olduk.


        if(hit.collider !=null) // Iþýk herhangi bir þeye ÇARPIYORSA bana þu iþlemleri yap : 
        {
            onGround = true; // BÝzim cambazýmýn zemine(onGround'a)  Çarpýyorsa zeminin(onGround'ýn) üzerindedir.
        }
        else // Iþýn herhangi bir objeye çarpmýyorsa : 
        {
            onGround = false;
        }

        Flip();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 playerRealPosition = transform.position + (transform.right * width / 2);
        Gizmos.DrawLine(playerRealPosition , playerRealPosition + new Vector3(0, -10f, 0));  //ARTIK ÇÝZGÝMÝZ ÝSTEDÝGÝMÝZ GÝBÝ CAMBAZ 'IN EN SAÐINA GELMÝÞ OLDU.
        // Gizmo(þey) bir çizgi çizsin.BU çizgi nerden baþlasýn ? Objemin Pozisyonundan baþlasýn. Nerde sonlansýn ? objemin Y yönünde 2f kadar aþagýsýnda sonlasýn.
    }

    void Flip() // Cambazýn DÖNME ÝÞLEMÝ KODLARI [Flip : Dönme ]
    {
        if (!onGround) // Cambaz objem zemin üzerinde degilse bana þu iþlemleri yap :
        {
            transform.eulerAngles += new Vector3(0, 180f, 0); // Cambaz objemin Derece cinsinden açý olarak döndürücem  \\\\\ Yani Cambaz objemi 180 derece olarak döndürmüþ oldum. \\\\\\ eulerAngles : Derece cinsinden açý olarak döndürmedir.
        }
        // Þimdi Hareket Ýþlemlerimizi yazýcaz : 
        myBody.velocity = new Vector2(transform.right.x * speed, 0f); //  Cambaz artýk  sað ve sola hareketlerini yapabilecek
    }




}
