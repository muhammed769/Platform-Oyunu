using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float mySpeedX;

    [SerializeField] float speed; // SerializeField PR�VATE bir �ZELL�KT�R.Biz BUNU YAZDIGIMIZDA UN�TY �ZER�NDE Speed KISMI GEL�R VE BURAYA  KEND�M B�R HIZ DE�ER� VEREB�L�R�M. (Yani Unity'nin ara y�z�nden bu speed de�erini degi�tirebilirim.)
    [SerializeField] float jumpPower;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround; // Bu kod  Player zemin �zerinde mi degil mi bunun kontrol�n� yapcak.
    private bool canDoubleJump; // 2 kere z�play�p z�plamad�g�n� kontrol�n� yapcaz.
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked; // Player'�n atak durumunda m� de�il mi bunu kontrol eden bir �zellik tan�mlad�k.
    [SerializeField] float currentAttackTimer; // Mevcut atak zamanl�y�c� �zelligini tan�mlad�k.
    [SerializeField] float defaultAttackTimer; // Varsay�lan atak zamanl�y�c� �zelligini tan�mlad�k.
    private Animator myAnimator;
    [SerializeField] int arrowNumber;
    [SerializeField] Text arrowNumberText;        // Unity k�sm�nda Becent'teki Arrow Number  a 3 yazm��t�k i�te bunu UI'daki(Ara Y�zdeki)  Ok Say�s� alan�ndada g�stermek i�in bu kod blogunu yazd�k.
    // Bu kod blogunun ��EYEB�LMES�N ���N YANINDAK� SARI KUTUCUKTAN IU K�T�PHANES�N� SE�EREK  EN �STTE UI K�T�PHANES�N�N EKLENM�� OLMASI GEREK�R.

    [SerializeField] AudioClip dieMusic;

    [SerializeField] GameObject winPanel, losePanel;


    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Start() // Oyun ba�lad�g� anda sadece bir kez �agr�l�r, yani ilk de�er atama i�lemlerinde kullan�yoruz.Yani oyun ba�lad�g�nda ilk yap�lmas� gerekenler yap�ls�n   daha sonra bu metotlar bi daha devreye girmesin.Daha sonra zaten update uzerinden devam ediyor i�lemler.
    {
        attacked = false;  // Oyun ba�lad�g� anda atak durumunda OLMADIGIMIZI S�STEME S�YLEM�� OLDUK.
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale; //  Oyun i�erisinde diyelim mySpeedX 'i begenmedik o y�zden bunu D�NAM�K HALE GET�RM�� OLDUK.
        arrowNumberText.text = arrowNumber.ToString();
    }


    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update() // Her FRAME ' DE(�ERCEVEDE) YEN�LEN�R !!! 1 saniyede ortalama 25-50 aras�nda Frame yenilenir.YAN� BEN�M BURAYA YAZDIGIM KODLAR UNITY'DE  SAN�YEDE ORTALAMA 40 DEFA �A�RILIYOR !!!!!!!!!!!!!!!
    {

        #region Player'�n yatay eksendeki h�z�
        //  Debug.Log(Input.GetAxis("Horizontal"));

        // Debug.Log("Frame Mant���");

        mySpeedX = Input.GetAxis("Horizontal"); // Yataydaki h�zlar�m� alm�� oldum.  -1 LE +1 ARASINDA SA� VE SOL OK TU�UNA BASILMA S�RES�NE BA�LI OLARAK DE�ERLER GELECEK.

        //************************************************************************************************* �OK �NEML� ************************************************************************************************************

        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));  //Mathf.Abs diyerek MUTLAK de�er i�indeki h�z� ald�k ve  mySpeedX 'i( HIZIMIZI ) SCALER B�R B�Y�KL�K(GER�EK) HAL�NE GET�RM�� OLDUK. 

              /*        - PlayerController Scriptimiz Hangi obje i�erisindeyse git ondan bile�en al.Neyi als�n?Animator bile�enini als�n.
                        - B�YLEL�KLE Player objemin Animator B�LE�EN�NE ULA�MI� OLDUM.Float veritipini KUR DED�M VE PARAMETRE �SM�N� VE DE�ER�N� G�RM�� OLDUM.
              */

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



       // GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeedX, GetComponent<Rigidbody2D>().velocity.y);  
        
                                                                   // Bu Script Zaten Player objesine ait oldugu i�in Direk GetComponent D�yerek Obje al dedik. Player objesinde hangi objeyi als�n dedik RigidBody2d objesini als�n demi� olduk.
                                                                   // velocity 'nin(S�RAT) tool type'inde Vector2 yaz�yo yani bunun tipi Vector2 'ymi�. o y�zden new Vector2 dedik.
                                                                   // X EKSEN�NDEK� HIZLARINI B�Z OK TU�LARIYLA BEL�RLEM�� OLDUK.AMA Y EKSEN�NDEK� HIZINI �SE PROGRAMIN KEND�S�N�N HIZI NE �SE ONU KULLANMI� OLDUK.

      

     // GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeedX  * speed, GetComponent<Rigidbody2D>().velocity.y);  // Yani ARTIK UNITY KISMINDA Speed k�sm�na yazd�g�m say�yla PROGRAMDAN OTOMATIK OLARAK GELEN -1 LE +1 ARASINDA
                                                                                                                         // GELEN  SAYI �ARPILIP ARTIK BEN�M X EKSEN�NDEK� HIZIM O OLMU� OLACAK !!!


        myBody.velocity=new Vector2(mySpeedX * speed, myBody.velocity.y);

        #endregion


        #region Player'�n sa� ve sol hareket y�n�ne g�re y�z�n�n d�nmesi

        if (mySpeedX>0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z); // h�z�m�n pozitif oldugunda Player'�m�n y�z� Sa� tarafa bakacak   �ekilde dursun demi� olduk.
        }

        else if (mySpeedX<0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);  // h�z�m�n negatif oldugunda Player'�m�n y�z� Sol tarafa bakacak   �ekilde dursun demi� olduk.
        }

        #endregion


        #region Player'�n z�plamas�n�n kontrol edilmesi 

        if (Input.GetKeyDown(KeyCode.Space)) //INPUT = KULLANICININ KOMUT VERMES�;  KEY = TU� GETKEYDOWN = KLAVYEN�N TU�UNA B�R KEZ BASILDIGINDA ;KOD SATIRININ TAMAMININ ANLAMI: KULLANICI BO�LUK TU�UNA BASTIGINDA BANA �U ��LEMLER� YAP ! ! !

        {
            //  Debug.Log("Bo�luk tu�una bas�ld�");

            if(onGround==true) // Yani OnGround'daysa( ZEM�N �ZER�NDEYSEK bu bo�luk tu�uyla z�playabilelim.)
                              
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = true; // 1 kere z�plad�g�m�z 1 kere daha z�plama hakk� tan�m�� oldum.
                myAnimator.SetTrigger("Jump");
            }

            // myBody.velocity = new Vector2(myBody.velocity.x, 5f); // Bo�luk tu�una bast�g�mda x ekseninde s�rati sistemden gelen otomatik s�rati korusun  y eksenimdeki s�rati ise 5 kat� olsun.

            else
            {
                if(canDoubleJump==true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = false; // 2 'inci defada z�plad� art�k b� daha z�plama durumunu false diyerek kapatt�k.
                }
            }
        }

        #endregion


        #region Player'�n ok atmas�n�n kontrol�

        if(Input.GetMouseButtonDown(0) && arrowNumber>0) // MAUSUN SOLUNA 1 KEZ t�kland�g�nda ve ok say�s� 1 den b�y�kse  bana �u i�lemleri yap : 
        {
            if(attacked==false)
            {
                attacked = true;
                myAnimator.SetTrigger("Attack");
                Invoke("Fire",0.5f);//  YAN� ATAK AN�MASYONU �ALI�ACAK YARIM SAN�YE SONRADA Fire(Ate� etme) metotu �al��acak.     [Invoke = Metodu SAN�YE C�NS�NDEN �A�IRIR.] 
            }
            
        }
        if (attacked == true) // Player Atak DURUMUNDAYSA (OKUN ATILMASI DURUMU YAN�)  BANA �U ��LEMLER� YAP : 
        {
            currentAttackTimer -= Time.deltaTime;     // Atag�n �zerinden yani ok 'la B�RKEZ  ATE� ETT���MDE  bunun �zerinden ka� saniye ge�tigini G�REB�LD�K. ///////  Time.deltaTime : 2 frame aras�ndaki �al��ma s�resini ayarlar.
                                                      // Debug.Log(currentAttakTimer); 
        }

        else
        {
            currentAttackTimer = defaultAttackTimer;  // Yani atak bitiginde (Ok atmad�g�m�zda) mevcutAttakZamal�y�c�m Varsay�lanAttakZamanlay�c�ma e�it olmu� olacak. [DefaultAttackTimer De�erine Unity Aray�z�nde 1 yazd�k ]
        }

        if (currentAttackTimer <= 0)
        {
            attacked = false;  // Art�k   2 ,3 , 4 '�nc� oklar� atabilecegiz.[ EN BA�TAK� �F KONTROL�NE G�DECEK VE TEKRAR OK ATMA ��LEM� B�YLECE SA�LANAB�LECEK. ]
        }

        #endregion

    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Fire()
    {
        // Instantiate(arrow);   // MAUSUN SOL TU�UNA 1 KEZ BASTIGIMDA BANA B�R OK OLU�TUR. ============= Instantiate : OLU�TUR DEMEKT�R.                        [Bu Instantiate Metotunun 1'inci Overload�n� kulland�k.]

        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);   // Transform.Position : Player'�n bulundugu konumda olu�sun ok. Quaternion : D�n�� prensibi  identity : o anki ROTASYONUNU(D�N���N�) KORUSUN.
        okumuz.transform.parent = GameObject.Find("Arrows").transform; // Art�k  Oyun ba�lad�g�nda her bir objemiz Arrow objesinin i�inde olur b�ylece Hieracy'deki �irkin g�r�nt� kaybolmu� olur.  Parent : ebeveyn

        // okumuz Player sag ve sola d�nsede hep SA�A dogru bir ok at�yor.Bunu ��yle d�zelticez : 

        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(20f, 0f);  // okumuza x y�n�nde 5 h�z kazand�rm�� olduk.

        }
        else
        {
            Vector3 okumuzScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);

            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f, 0f);
        }

        arrowNumber--;
        arrowNumberText.text = arrowNumber.ToString();

    }  // Player'�n ok atmas�n�n kontrol� i�in olu�turulan bir FONKS�YON (METOT)

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Bir objeye �arpt�k.");
        // Debug.Log(collision.gameObject.name); // B�ylece Player '�n �arpt�g� obje Console k�sm�nda yazm�� oldu.

        // -----------------------------------------------------------------------------------------------------------------------------------------

        //if(collision.gameObject.name=="Cambaz") // Player'�n �ARPTIGI OBJE CANAVARLARDAN CAMBAZSA BANA �U ��LEMLER� YAP : 
        //{
        //  myAnimator.SetTrigger("Die"); // Art�k Player Cambaz canavar�na �arpt�g� anda �L�M AN�MASYONU GER�EKLE�M�� OLACAK.
        //} 

        // Bu kodu �u nedenle yorum sat�r� haline getirdik : Bizim oyunumuzda 50 tane canavar olabilir bu 50 tane canavarla �arp�st�m diyelim bunu kod sat�r�nda 50 tane if koduyla yazcak m�y�m ? HAYIR !!!!!
        // BUNUN ���N ET�KET(TAG) OLU�TURCAM.NASIL OLU�TURULACAGI DEFTERDE YAZIYOR.

        // -----------------------------------------------------------------------------------------------------------------------------------------

        if (collision.gameObject.CompareTag("Enemy"))// �arpt�g�m objenin Tag'i (etiketi) Enemy(D��man) ise Bana �u i�lemleri yap :      
        {
            GetComponent<TimeControl>().enabled = false;

            // myAnimator.SetTrigger("Die");  // �arpt�g�m objenin Tag'i Enemy'ise Die animasyonunu �al��acak.
            Die();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            /*winPanel.active = true;
            Time.timeScale = 0;// B�t�n her sey (UI DAki hersey) durmas�n� sa�lar.*/
            Destroy(collision.gameObject);
            StartCoroutine(Wait(true));
        }

    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Die()
    {
        GameObject.Find("Sound Controller").GetComponent<AudioSource>().clip = null;       // Player objesi �ld�g�nde AuidoClip null olacakki �L�M M�Z��� EKLEYEB�LEL�M. \\\\ GameObject.Find Diyince Hierarcy'deki objemizi bulmaya �al���yo.
        GameObject.Find("Sound Controller").GetComponent<AudioSource>().PlayOneShot (dieMusic);

        myAnimator.SetFloat("Speed", 0);               //  �l�m ger�ekle�tigi anda  Player'�n h�z� 0 olacak.
        myAnimator.SetTrigger("Die");

        //  myBody.constraints = RigidbodyConstraints2D.FreezePosition; // Player'�n TAA OYUNUN BA�INDA  OYUNU BA�LATTIGIMIZDA Z EKSEN� ETRAFINDA D�NMES�N� ENGELLEM��T�K ��MD� BU KODLA �LAVETEN  PLAYER �LD�G�NDE X  VE Y Y�N�NDEDE HAREKET EDEMEYECEK ! 
        myBody.constraints = RigidbodyConstraints2D.FreezeAll; //HEM ROTASYONLARI HEM POZ�SYONLARI KAPATMI� OLDUK  !  !   !   !    !

        // Evet �ld�g�mde hareket edemiyorum ama Scale'dan dolay� gene y�n degi�tiriyorum.Bunu ��yle yapcaz : 

        enabled = false; // Yani PlayerCOntroller Scriptinin aktifligini PAS�FLE�T�RD�K. [ enabled bizim PlayerController Scriptini Kontrol ediyor ]

        //losePanel.SetActive(true);  // losePanel.active = true;

        // Time.timeScale = 0; // B�t�n her sey (UI DAki hersey) durmas�n� sa�lar.

        StartCoroutine(Wait(false));
    }

    // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator Wait(bool win)        //IEnumerator bir interface'dir.
    {
        yield return new WaitForSecondsRealtime(2f); // Ger�ek zamanda 2 saniye  sonra Paneller a��lacak art�k.
        Time.timeScale = 0;

        if (win == true)
            winPanel.SetActive(true); // winpanel.active=true ; bu 2 kodda ayn� i�levi g�r�r.
        else
            losePanel.SetActive(true);      
    }
}
