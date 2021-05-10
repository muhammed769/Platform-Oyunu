using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float mySpeedX;

    [SerializeField] float speed; // SerializeField PRÝVATE bir ÖZELLÝKTÝR.Biz BUNU YAZDIGIMIZDA UNÝTY ÜZERÝNDE Speed KISMI GELÝR VE BURAYA  KENDÝM BÝR HIZ DEÐERÝ VEREBÝLÝRÝM. (Yani Unity'nin ara yüzünden bu speed deðerini degiþtirebilirim.)
    [SerializeField] float jumpPower;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround; // Bu kod  Player zemin üzerinde mi degil mi bunun kontrolünü yapcak.
    private bool canDoubleJump; // 2 kere zýplayýp zýplamadýgýný kontrolünü yapcaz.
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked; // Player'ýn atak durumunda mý deðil mi bunu kontrol eden bir özellik tanýmladýk.
    [SerializeField] float currentAttackTimer; // Mevcut atak zamanlýyýcý özelligini tanýmladýk.
    [SerializeField] float defaultAttackTimer; // Varsayýlan atak zamanlýyýcý özelligini tanýmladýk.
    private Animator myAnimator;
    [SerializeField] int arrowNumber;
    [SerializeField] Text arrowNumberText;        // Unity kýsmýnda Becent'teki Arrow Number  a 3 yazmýþtýk iþte bunu UI'daki(Ara Yüzdeki)  Ok Sayýsý alanýndada göstermek için bu kod blogunu yazdýk.
    // Bu kod blogunun ÝÞEYEBÝLMESÝN ÝÇÝN YANINDAKÝ SARI KUTUCUKTAN IU KÜTÜPHANESÝNÝ SEÇEREK  EN ÜSTTE UI KÜTÜPHANESÝNÝN EKLENMÝÞ OLMASI GEREKÝR.

    [SerializeField] AudioClip dieMusic;

    [SerializeField] GameObject winPanel, losePanel;


    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Start() // Oyun baþladýgý anda sadece bir kez çagrýlýr, yani ilk deðer atama iþlemlerinde kullanýyoruz.Yani oyun baþladýgýnda ilk yapýlmasý gerekenler yapýlsýn   daha sonra bu metotlar bi daha devreye girmesin.Daha sonra zaten update uzerinden devam ediyor iþlemler.
    {
        attacked = false;  // Oyun baþladýgý anda atak durumunda OLMADIGIMIZI SÝSTEME SÖYLEMÝÞ OLDUK.
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale; //  Oyun içerisinde diyelim mySpeedX 'i begenmedik o yüzden bunu DÝNAMÝK HALE GETÝRMÝÞ OLDUK.
        arrowNumberText.text = arrowNumber.ToString();
    }


    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update() // Her FRAME ' DE(ÇERCEVEDE) YENÝLENÝR !!! 1 saniyede ortalama 25-50 arasýnda Frame yenilenir.YANÝ BENÝM BURAYA YAZDIGIM KODLAR UNITY'DE  SANÝYEDE ORTALAMA 40 DEFA ÇAÐRILIYOR !!!!!!!!!!!!!!!
    {

        #region Player'ýn yatay eksendeki hýzý
        //  Debug.Log(Input.GetAxis("Horizontal"));

        // Debug.Log("Frame Mantýðý");

        mySpeedX = Input.GetAxis("Horizontal"); // Yataydaki hýzlarýmý almýþ oldum.  -1 LE +1 ARASINDA SAÐ VE SOL OK TUÞUNA BASILMA SÜRESÝNE BAÐLI OLARAK DEÐERLER GELECEK.

        //************************************************************************************************* ÇOK ÖNEMLÝ ************************************************************************************************************

        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));  //Mathf.Abs diyerek MUTLAK deðer içindeki hýzý aldýk ve  mySpeedX 'i( HIZIMIZI ) SCALER BÝR BÜYÜKLÜK(GERÇEK) HALÝNE GETÝRMÝÞ OLDUK. 

              /*        - PlayerController Scriptimiz Hangi obje içerisindeyse git ondan bileþen al.Neyi alsýn?Animator bileþenini alsýn.
                        - BÖYLELÝKLE Player objemin Animator BÝLEÞENÝNE ULAÞMIÞ OLDUM.Float veritipini KUR DEDÝM VE PARAMETRE ÝSMÝNÝ VE DEÐERÝNÝ GÝRMÝÞ OLDUM.
              */

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



       // GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeedX, GetComponent<Rigidbody2D>().velocity.y);  
        
                                                                   // Bu Script Zaten Player objesine ait oldugu için Direk GetComponent DÝyerek Obje al dedik. Player objesinde hangi objeyi alsýn dedik RigidBody2d objesini alsýn demiþ olduk.
                                                                   // velocity 'nin(SÜRAT) tool type'inde Vector2 yazýyo yani bunun tipi Vector2 'ymiþ. o yüzden new Vector2 dedik.
                                                                   // X EKSENÝNDEKÝ HIZLARINI BÝZ OK TUÞLARIYLA BELÝRLEMÝÞ OLDUK.AMA Y EKSENÝNDEKÝ HIZINI ÝSE PROGRAMIN KENDÝSÝNÝN HIZI NE ÝSE ONU KULLANMIÞ OLDUK.

      

     // GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeedX  * speed, GetComponent<Rigidbody2D>().velocity.y);  // Yani ARTIK UNITY KISMINDA Speed kýsmýna yazdýgým sayýyla PROGRAMDAN OTOMATIK OLARAK GELEN -1 LE +1 ARASINDA
                                                                                                                         // GELEN  SAYI ÇARPILIP ARTIK BENÝM X EKSENÝNDEKÝ HIZIM O OLMUÞ OLACAK !!!


        myBody.velocity=new Vector2(mySpeedX * speed, myBody.velocity.y);

        #endregion


        #region Player'ýn sað ve sol hareket yönüne göre yüzünün dönmesi

        if (mySpeedX>0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z); // hýzýmýn pozitif oldugunda Player'ýmýn yüzü Sað tarafa bakacak   þekilde dursun demiþ olduk.
        }

        else if (mySpeedX<0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);  // hýzýmýn negatif oldugunda Player'ýmýn yüzü Sol tarafa bakacak   þekilde dursun demiþ olduk.
        }

        #endregion


        #region Player'ýn zýplamasýnýn kontrol edilmesi 

        if (Input.GetKeyDown(KeyCode.Space)) //INPUT = KULLANICININ KOMUT VERMESÝ;  KEY = TUÞ GETKEYDOWN = KLAVYENÝN TUÞUNA BÝR KEZ BASILDIGINDA ;KOD SATIRININ TAMAMININ ANLAMI: KULLANICI BOÞLUK TUÞUNA BASTIGINDA BANA ÞU ÝÞLEMLERÝ YAP ! ! !

        {
            //  Debug.Log("Boþluk tuþuna basýldý");

            if(onGround==true) // Yani OnGround'daysa( ZEMÝN ÜZERÝNDEYSEK bu boþluk tuþuyla zýplayabilelim.)
                              
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = true; // 1 kere zýpladýgýmýz 1 kere daha zýplama hakký tanýmýþ oldum.
                myAnimator.SetTrigger("Jump");
            }

            // myBody.velocity = new Vector2(myBody.velocity.x, 5f); // Boþluk tuþuna bastýgýmda x ekseninde sürati sistemden gelen otomatik sürati korusun  y eksenimdeki sürati ise 5 katý olsun.

            else
            {
                if(canDoubleJump==true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = false; // 2 'inci defada zýpladý artýk bý daha zýplama durumunu false diyerek kapattýk.
                }
            }
        }

        #endregion


        #region Player'ýn ok atmasýnýn kontrolü

        if(Input.GetMouseButtonDown(0) && arrowNumber>0) // MAUSUN SOLUNA 1 KEZ týklandýgýnda ve ok sayýsý 1 den büyükse  bana þu iþlemleri yap : 
        {
            if(attacked==false)
            {
                attacked = true;
                myAnimator.SetTrigger("Attack");
                Invoke("Fire",0.5f);//  YANÝ ATAK ANÝMASYONU ÇALIÞACAK YARIM SANÝYE SONRADA Fire(Ateþ etme) metotu çalýþacak.     [Invoke = Metodu SANÝYE CÝNSÝNDEN ÇAÐIRIR.] 
            }
            
        }
        if (attacked == true) // Player Atak DURUMUNDAYSA (OKUN ATILMASI DURUMU YANÝ)  BANA ÞU ÝÞLEMLERÝ YAP : 
        {
            currentAttackTimer -= Time.deltaTime;     // Atagýn üzerinden yani ok 'la BÝRKEZ  ATEÞ ETTÝÐÝMDE  bunun üzerinden kaç saniye geçtigini GÖREBÝLDÝK. ///////  Time.deltaTime : 2 frame arasýndaki çalýþma süresini ayarlar.
                                                      // Debug.Log(currentAttakTimer); 
        }

        else
        {
            currentAttackTimer = defaultAttackTimer;  // Yani atak bitiginde (Ok atmadýgýmýzda) mevcutAttakZamalýyýcým VarsayýlanAttakZamanlayýcýma eþit olmuþ olacak. [DefaultAttackTimer Deðerine Unity Arayüzünde 1 yazdýk ]
        }

        if (currentAttackTimer <= 0)
        {
            attacked = false;  // Artýk   2 ,3 , 4 'üncü oklarý atabilecegiz.[ EN BAÞTAKÝ ÝF KONTROLÜNE GÝDECEK VE TEKRAR OK ATMA ÝÞLEMÝ BÖYLECE SAÐLANABÝLECEK. ]
        }

        #endregion

    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Fire()
    {
        // Instantiate(arrow);   // MAUSUN SOL TUÞUNA 1 KEZ BASTIGIMDA BANA BÝR OK OLUÞTUR. ============= Instantiate : OLUÞTUR DEMEKTÝR.                        [Bu Instantiate Metotunun 1'inci Overloadýný kullandýk.]

        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);   // Transform.Position : Player'ýn bulundugu konumda oluþsun ok. Quaternion : Dönüþ prensibi  identity : o anki ROTASYONUNU(DÖNÜÞÜNÜ) KORUSUN.
        okumuz.transform.parent = GameObject.Find("Arrows").transform; // Artýk  Oyun baþladýgýnda her bir objemiz Arrow objesinin içinde olur böylece Hieracy'deki Çirkin görüntü kaybolmuþ olur.  Parent : ebeveyn

        // okumuz Player sag ve sola dönsede hep SAÐA dogru bir ok atýyor.Bunu þöyle düzelticez : 

        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(20f, 0f);  // okumuza x yönünde 5 hýz kazandýrmýþ olduk.

        }
        else
        {
            Vector3 okumuzScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);

            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f, 0f);
        }

        arrowNumber--;
        arrowNumberText.text = arrowNumber.ToString();

    }  // Player'ýn ok atmasýnýn kontrolü için oluþturulan bir FONKSÝYON (METOT)

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Bir objeye çarptýk.");
        // Debug.Log(collision.gameObject.name); // Böylece Player 'ýn çarptýgý obje Console kýsmýnda yazmýþ oldu.

        // -----------------------------------------------------------------------------------------------------------------------------------------

        //if(collision.gameObject.name=="Cambaz") // Player'ýn ÇARPTIGI OBJE CANAVARLARDAN CAMBAZSA BANA ÞU ÝÞLEMLERÝ YAP : 
        //{
        //  myAnimator.SetTrigger("Die"); // Artýk Player Cambaz canavarýna çarptýgý anda ÖLÜM ANÝMASYONU GERÇEKLEÞMÝÞ OLACAK.
        //} 

        // Bu kodu þu nedenle yorum satýrý haline getirdik : Bizim oyunumuzda 50 tane canavar olabilir bu 50 tane canavarla çarpýstým diyelim bunu kod satýrýnda 50 tane if koduyla yazcak mýyým ? HAYIR !!!!!
        // BUNUN ÝÇÝN ETÝKET(TAG) OLUÞTURCAM.NASIL OLUÞTURULACAGI DEFTERDE YAZIYOR.

        // -----------------------------------------------------------------------------------------------------------------------------------------

        if (collision.gameObject.CompareTag("Enemy"))// Çarptýgým objenin Tag'i (etiketi) Enemy(Düþman) ise Bana þu iþlemleri yap :      
        {
            GetComponent<TimeControl>().enabled = false;

            // myAnimator.SetTrigger("Die");  // Çarptýgým objenin Tag'i Enemy'ise Die animasyonunu çalýþacak.
            Die();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            /*winPanel.active = true;
            Time.timeScale = 0;// Bütün her sey (UI DAki hersey) durmasýný saðlar.*/
            Destroy(collision.gameObject);
            StartCoroutine(Wait(true));
        }

    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Die()
    {
        GameObject.Find("Sound Controller").GetComponent<AudioSource>().clip = null;       // Player objesi öldügünde AuidoClip null olacakki ÖLÜM MÜZÝÐÝ EKLEYEBÝLELÝM. \\\\ GameObject.Find Diyince Hierarcy'deki objemizi bulmaya çalýþýyo.
        GameObject.Find("Sound Controller").GetComponent<AudioSource>().PlayOneShot (dieMusic);

        myAnimator.SetFloat("Speed", 0);               //  Ölüm gerçekleþtigi anda  Player'ýn hýzý 0 olacak.
        myAnimator.SetTrigger("Die");

        //  myBody.constraints = RigidbodyConstraints2D.FreezePosition; // Player'ýn TAA OYUNUN BAÞINDA  OYUNU BAÞLATTIGIMIZDA Z EKSENÝ ETRAFINDA DÖNMESÝNÝ ENGELLEMÝÞTÝK ÞÝMDÝ BU KODLA ÝLAVETEN  PLAYER ÖLDÜGÜNDE X  VE Y YÖNÜNDEDE HAREKET EDEMEYECEK ! 
        myBody.constraints = RigidbodyConstraints2D.FreezeAll; //HEM ROTASYONLARI HEM POZÝSYONLARI KAPATMIÞ OLDUK  !  !   !   !    !

        // Evet öldügümde hareket edemiyorum ama Scale'dan dolayý gene yön degiþtiriyorum.Bunu Þöyle yapcaz : 

        enabled = false; // Yani PlayerCOntroller Scriptinin aktifligini PASÝFLEÞTÝRDÝK. [ enabled bizim PlayerController Scriptini Kontrol ediyor ]

        //losePanel.SetActive(true);  // losePanel.active = true;

        // Time.timeScale = 0; // Bütün her sey (UI DAki hersey) durmasýný saðlar.

        StartCoroutine(Wait(false));
    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator Wait(bool win)        //IEnumerator bir interface'dir.
    {
        yield return new WaitForSecondsRealtime(2f); // Gerçek zamanda 2 saniye  sonra Paneller açýlacak artýk.
        Time.timeScale = 0;

        if (win == true)
            winPanel.SetActive(true); // winpanel.active=true ; bu 2 kodda ayný iþlevi görür.
        else
            losePanel.SetActive(true);      
    }
}
