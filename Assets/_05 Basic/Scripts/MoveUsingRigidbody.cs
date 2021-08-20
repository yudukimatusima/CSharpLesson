using UnityEngine;

/// <summary>
/// Rigidbody2D コンポーネントを使って、入力に応じてキャラクターを動かすコンポーネント
/// </summary>
public class MoveUsingRigidbody : MonoBehaviour
{
    /// <summary>動く速さ（力の大きさ）</summary>
    public float m_speed = 1f;  // メンバ変数を public にしておくと、Inspector から設定できる
    /// <summary>モードを切り替える</summary>
    [SerializeField] int m_mode = 0;    // private なメンバ変数に [SerializeField] をつけておくと、Inspector から設定できる
    /// <summary>Rigidbody2D コンポーネントを取得して入れておく変数</summary>
    Rigidbody2D m_rb = default; // default は初期値を入れておく。初期値を入れなくてもプログラムは動くが、警告が出るのでこのようにしている。

    void Start()
    {
        // Rigidbody2D コンポーネントを取得しておく
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // m_mode に設定した値によって異なる処理をする
        if (m_mode == 0)
        {
            MoveUsingRigidbodyVelocity();
        }
        else if (m_mode == 1)
        {
            MoveUsingRigidbodyAddForce();
        }
        else if (m_mode == 2)
        {
            MoveUsingRigidbodyMovePosition();
        }
    }

    /// <summary>
    /// Rigidbody2D.velocity プロパティを使って GameObject を動かす
    /// </summary>
    void MoveUsingRigidbodyVelocity()
    {
        // 上下左右の入力を取得する
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力された値から速度ベクトルを求めてセットする
        Vector2 velo = new Vector2(h, v).normalized * m_speed;
        m_rb.velocity = velo;

        // 上下左右が入力されている時は、その方向に向きを変える
        if (velo != Vector2.zero)
        {
            this.transform.up = velo;
        }
    }

    /// <summary>
    /// Rigidbody2D.AddForce 関数を使って GameObject を動かす
    /// ※Update() を使ってこの処理を行うのは本当はよくない。
    /// ※フレームレートによって力を加える頻度が変わり、結果として加速の挙動が変わってしまうため。
    /// ※解決方法としては、「入力の取得」を Update() で行い、「力を加える」処理を FixedUpdate() で行う。
    /// </summary>
    void MoveUsingRigidbodyAddForce()
    {
        // 上下左右の入力を取得する
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 入力された値から力を加える方向と大きさを求めて、力を加える
        Vector2 f = new Vector2(h, v).normalized * m_speed;
        m_rb.AddForce(f);

        // 進んでいる方向に向ける
        this.transform.up = m_rb.velocity;
    }

    /// <summary>
    /// Rigidbody2D.MovePosition 関数を使って GameObject を動かす
    /// ※Update() を使ってこの処理を行うのは本当はよくない。
    /// ※フレームレートによって動く速度が変わってしまうため。
    /// ※解決方法としては、deltaPosition に Time.deltaTime を掛け算する
    /// </summary>
    void MoveUsingRigidbodyMovePosition()
    {
        // 上下左右の入力を取得する
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 動く方向を求めて、そこに動かす
        Vector3 deltaPosition = new Vector3(h, v, 0f).normalized * m_speed;
        m_rb.MovePosition(this.transform.position + deltaPosition); // MovePosition() の引数は「移動先の座標」であることに注意

        // 上下左右が入力されている時は、その方向に向きを変える
        if (deltaPosition != Vector3.zero)
        {
            this.transform.up = deltaPosition;
        }
    }
}
