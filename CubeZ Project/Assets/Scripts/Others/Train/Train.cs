using System.Collections;
using UnityEngine;

    public class Train : DynamicObjectTransliter
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        Move();
        }

    public override void Move()
    {

        transform.Translate(new Vector3(0, 0, 1) * Speed * Time.deltaTime, Space.World);
    }
}