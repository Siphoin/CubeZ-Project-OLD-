using UnityEngine;
public class DynamicObjectTransliter : MonoBehaviour
    {
    private const int DISTANCE_ON_DESTROY = 300;


    [Header("Скорость движение объекта по траектории")]
    [SerializeField] private float speed = 10;
    [Header("Скорость поворотв объекта по траектории")]
    [SerializeField] private float speedAngle = 10;

    [Header("Тип направленмя")]
    [SerializeField] private DirectionType dirType;

    [Header("Удалять объект если он вне камеры и дистануия крайне высока")]
    [SerializeField] private bool destroyMoving = true;

    public float Speed { get => speed;}
    public DirectionType DirType { get => dirType; }

    // Use this for initialization
    void Start()
        {
        if (speed <= 0)
        {
            throw new DynamicObjectTransliterException("speed <= 0!");
        }

        if (speedAngle <= 0)
        {
            throw new DynamicObjectTransliterException("speed angle <= 0!");
        }
    }

    private void Update()
    {
        Move();

    }

    public virtual void Move()
    {
        if (destroyMoving)
        {
        CheckDistance();
        }

        transform.Translate(dirType == DirectionType.Right ? transform.right : transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (distance >= DISTANCE_ON_DESTROY)
        {
            Destroy(gameObject);
        }
    }
}