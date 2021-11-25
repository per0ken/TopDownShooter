using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Lives
{
    public Rigidbody2D rb;
    private GameObject Player;

    /* private float movementDuration = 2.0f;
     private float waitBeforeMoving = 2.0f;
     private bool Arrived = false;*/

    private float speed = 5.0f;
    private float waitTime;
    public float startWaitTime = 0.75f;

    public Vector2 moveSpot;
    public float minX = -10.0f;
    public float maxX = 10.0f;
    public float minY = -5.0f;
    public float maxY = 4.0f;

    protected bool canExplode = true;
    protected bool canPlaySound = true;
    protected bool canMove = true;
    protected bool canLook = true;
    protected bool canShoot = false;
    //a boolok azért vannak hogy egyszer tudjon animációt lejátszani a halott enemy és ne tudjon mozogni illetve a playerre nézni, 1 boollal nem engedte

    BoxCollider2D ObjectCollider; //bemegyek a mapba, utána megakad a dolgokba ha MoveTowards alapú lesz a mozgása

    private Animator mAnimator; //robbanásanimáció

    private RuntimeAnimatorController soldierExplodeController;


    void OnEnable()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        soldierExplodeController = Resources.Load<RuntimeAnimatorController>("MySoldierExplodeAnim");
        ObjectCollider = GetComponent<BoxCollider2D>();
        mAnimator = GetComponent<Animator>(); //getanimator
        RuntimeAnimatorController thisController = Instantiate(soldierExplodeController);
        waitTime = startWaitTime;
        moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if (thisController != null)
            mAnimator.runtimeAnimatorController = thisController;
        //a fenti sor ráállytja az animatior component animator controllerjére a SoldierExplode7-et
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet")) enemyLives--;
        if (collider.gameObject.CompareTag("Bullet")) Destroy(collider.gameObject);
        if (enemyLives <= 0)
        {
            SoundEffect(); //robbanáshang
            Explosion(); //robbanásanimáció
            Destroy(this.gameObject,1.5f); //törli a szétrobbant objectet 1.5 mp után, de elõtte még mozog és újra lejátssza a robbanáshangot és az animációt is pedig nem lövök rá
            canMove = false;
            canLook = false;
            canShoot = false;
            ObjectCollider.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bush"))
            ObjectCollider.isTrigger = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bush"))
            ObjectCollider.isTrigger = false;
    }

        private void Update()
    {
        /*if (!Arrived)
        {
            Arrived = true;
            int randomX = Random.Range(-10, 10);
            int randomY = Random.Range(4, -5);
            StartCoroutine(MoveToPoint(new Vector2(randomX,randomY)));
        }*/

        if (canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpot) < 0.6f || // nem kap új irányt hiába megy közel a kövekhez
                Vector2.Distance(transform.position, rockThree.Three.position) < 0.8f ||
                Vector2.Distance(transform.position, rockTwo.Two.position) < 0.8f ||
                Vector2.Distance(transform.position, rockOne.One.position) < 0.8f)
            {
                if (waitTime <= 0)
                {
                    moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

    }

    void FixedUpdate()
    {
        Vector2 Playerr = Player.transform.position;
        Vector2 lookDir = Playerr - rb.position;
        if (canLook == true)
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

   /* private IEnumerator MoveToPoint(Vector2 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        if (canMove == true)
        {
            while (timer < movementDuration)
            {
                timer += Time.deltaTime;
                float t = timer / movementDuration;
                t = t * t * t * (t * (6f * t - 15f) + 10f);
                transform.position = Vector3.Lerp(startPos, targetPos, t);

                yield return null;
            }
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        Arrived = false;
    }*/

    void Explosion()
    {
        if (canExplode == true)
        {
            mAnimator.SetTrigger("TrExplode");
            canExplode = false;
        }

    }

    void SoundEffect()
    {
        if (canPlaySound == true)
        {
            SoundController.enemyDied.Invoke();
            canPlaySound = false;
        }
    }
}
