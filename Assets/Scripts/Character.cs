using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Character : Unit
{
    [SerializeField] public float speed = 3.0f;
    [SerializeField] public int lives = 5;
    [SerializeField] private float jumpForce = 3.0f;
    [SerializeField] Text text;
    [SerializeField] Text _score;
    [SerializeField] Text time;
    [SerializeField] Image redPanel;
    [SerializeField] AudioSource breath;
    [SerializeField] AudioSource fail;
    [SerializeField] AudioSource getDamage;
    [SerializeField] AudioSource jumpAudio;


    private Color panelColor = new Color(1f, 0f, 0f, 0.5f); // стартовый набор цвета для "панели смерти"


    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool IsGrounded = false;
    public Transform GroundCheck;
    private float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    private bool IsBoosted = false;
    private float move;
    bool facingRight = true;
    public int score = 0;
    public float timer;
    public bool isDamaged = false;



    public CharState State  // свойство, работающее напрямую с аниматором
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Start()
    {
        Cursor.visible = false;
    }
    private void Awake()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = 70.0f;
    }
    private void FixedUpdate()
    {

        move = Input.GetAxis("Horizontal");
        Run();

        if (IsGrounded && Input.GetKey("space") && lives >= 1) // если чар на земле, допускается прыжок
        {
            Jump();
        }

        if (isDamaged)
        {
            DamageAction();
        }

        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, whatIsGround); // логика "проверки на землю"
        if (!IsGrounded)
        {
            return;
        }

    }
    private void Update()
    {

        CheckUI();
        if (IsGrounded && lives >= 1) State = CharState.Idle; // если на земле и не двигается , то анимация ИДЛЕ
        if (IsGrounded && Input.GetButton("Horizontal") && lives >= 1) State = CharState.Run; // если на земле и нажаты клавиши лево/право то анимация бега
        if (IsGrounded && Input.GetButton("Jump")) State = CharState.Jump; // аналогоично с прыжком

        if (!IsGrounded) jumpAudio.Stop();  // если персонаж уже в воздухе звук прыжка отключить

        if (IsBoosted) StartCoroutine(BoostSpeed()); // триггер на скорость
        if (speed != 3.5f) IsBoosted = true;

    }

    private void CheckUI() // метод с некоторыми ЮИ элементами
    {
        int _timer = (int)timer;

        if (timer > 0) // метод панели смерти, а так же счетчика
        {
            timer -= Time.deltaTime;
            redPanel.color = Color.Lerp(redPanel.color, Color.clear, 0.2f * Time.deltaTime);
        }


        if (timer < 0)  // если время вышло, воспроизвести звук и окрасить канвас в оттенок красного
        {
            breath.Play();
            redPanel.color = panelColor;
            ReceiveDamage();
            timer = 70f;
        }

        time.text = "Time: " + _timer.ToString();

        _score.text = "Score: " + score.ToString();
        text.text = lives.ToString();

        if (lives <= 0)    //метод смерти чара , его анимация, звук
        {
            if (!fail.isPlaying) fail.Play();
            text.text = "0";
            State = CharState.Fail;
            Destroy(gameObject, 0.7f);
        }
    }

    IEnumerator BoostSpeed()  // корутина для ускорения
    {
        yield return new WaitForSeconds(5);
        speed = 3.5f;
        IsBoosted = false;
    }
    private void Run() // метод бега
    {
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }
    private void Flip() // разворот. Чекает булеву переменную на направление право / лево, и обращается в соответсвии к скэйлу 
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void Jump() // прыжок.  Анимация и приложении силы через импульс
    {
        if (!jumpAudio.isPlaying) jumpAudio.Play();
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public override void ReceiveDamage() // логика получение урона
    {
        isDamaged = true;

    }

    public void DamageAction()
    {
        getDamage.Play();    // звук урона
        State = CharState.GetDamage; // анимация урона (?! пофиксить)
        rigidbody.velocity = Vector3.zero; // скорость тела в ноль
        if (lives > 1)
        {
            Vector3 direction = transform.up; // новый вектор направления вверх
            if (facingRight)  // в зависимости куда смотрит игрок, в ту сторону смещаем вектор
            {
                direction.x += 1.5f;
                direction.y += 1.5f;
            }
            else
            {
                direction.x -= 1.5f;
                direction.y += 1.5f;
            }
            rigidbody.AddForce(direction * 3f, ForceMode2D.Impulse); // толчок тела, используя вектор направления
        }

        lives--;   // минус хп
        if (score > 0) score -= 5; // минус очки
        isDamaged = false;
    }


    private void OnTriggerEnter2D(Collider2D collider) // логика нанесение урона врагу
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && (unit is Monster || unit is MoveableMonster || unit is ShootMonster || unit is YellowDude))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(transform.up * 5.0f, ForceMode2D.Impulse);
            score += 10;
        }
    }

}
public enum CharState
{
    Idle,
    Run,
    Jump,
    Fail,
    GetDamage
}
