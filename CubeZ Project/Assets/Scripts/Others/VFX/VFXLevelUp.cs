using System;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class VFXLevelUp : MonoBehaviour, IRemoveObject, IInvokerMono
    {

    private const string NAME_ANIM_DESTROY = "level_up_end";


    [Header("Задержка перед исчезновением")]
    [SerializeField] private float timeOutDestroy;

    private Animator animatorVFX;

   
        // Use this for initialization
        void Start()
        {
        if (!TryGetComponent(out animatorVFX))
        {
            throw new VFXLevelUpException($"{name} vfx not have component Ahimator");
        }

        CallInvokingMethod(Remove, timeOutDestroy);
        }


        public void CallInvokingEveryMethod(Action method, float time)
        {
            InvokeRepeating(method.Method.Name, time, time);
        }

        public void CallInvokingMethod(Action method, float time)
        {
            Invoke(method.Method.Name, time);
        }

    public void Remove()
    {
        animatorVFX.Play(NAME_ANIM_DESTROY);
    }
}