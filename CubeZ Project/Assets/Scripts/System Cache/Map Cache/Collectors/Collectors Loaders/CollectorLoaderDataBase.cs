using UnityEngine;

public class CollectorLoaderDataBase : CollectorLoaderBase
    {

    [Header("ID коллекции для загрузки данных")]
    [SerializeField] protected string idCollection;

    protected void Ini ()
    {
        if (string.IsNullOrEmpty(idCollection))
        {
            throw new CollectorLoaderException("id collection is emtry");
        }
    }
        }
    