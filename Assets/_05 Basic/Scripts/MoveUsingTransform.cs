using UnityEngine;

/// <summary>
/// Transform コンポーネントを使って、入力に応じてキャラクターを動かすコンポーネント
/// ※Transform を使った移動は「物理法則で動くのではない」ことに注意すること
/// </summary>
public class MoveUsingTransform : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    public float m_speed = 1f;
    /// <summary>モードを切り替える</summary>
    [SerializeField] int m_mode = 0;

    void Update()
    {
        // m_mode に設定した値によって異なる処理をする
        if (m_mode == 0)
        {
            MoveUsingTransformTranslate();
        }
        else if (m_mode == 1)
        {
            MoveUsingTransformPosition();
        }
    }

    /// <summary>
    /// Transform.Translate() 関数を使って GameObject を動かす
    /// ※Update() を使ってこの処理を行うのは本当はよくない。フレームレートによって動く速さが変わってしまうため。
    /// ※解決方向としては、移動する距離に Time.deltaTime を掛け算する
    /// </summary>
    void MoveUsingTransformTranslate()
    {
        // 上下左右の入力を取得する
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // Transform.Translate 関数を使って動かす
        this.transform.Translate(h * m_speed, v * m_speed, 0f);
    }

    /// <summary>
    /// Transform.position プロパティを使って GameObject を動かす。
    /// ※Update() を使ってこの処理を行うのは本当はよくない。フレームレートによって動く速さが変わってしまうため。
    /// ※解決方法としては、deltaPosition に Time.deltaTime を掛け算する
    /// </summary>
    void MoveUsingTransformPosition()
    {
        // 上下左右の入力を取得する
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 動く方向を求めて、そこに動かす
        Vector3 deltaPosition = new Vector3(h, v, 0).normalized * m_speed;
        this.transform.position += deltaPosition;

        // 上下左右が入力されている時は、その方向に向きを変える
        if (deltaPosition != Vector3.zero)
        {
            this.transform.up = deltaPosition;
        }
    }
}
