using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class EnemyCtrl : MonoBehaviour
{
    //�� ����
    public enum EnemyState { None, Idle, Move, Wait, GoTarget, Atk, Damage, Die }

    //�ذ� �⺻ �Ӽ�
    [Header("�⺻ �Ӽ�")]
    //�ذ� �ʱ� ����
    public EnemyState enemyState = EnemyState.None;
    //�ذ� �̵� �ӵ�
    public float spdMove = 1f;
    //�ذ��� �� Ÿ��
    public GameObject targetCharactor = null;
    //�ذ��� �� Ÿ�� ��ġ���� (�Ź� �� ã������)
    public Transform targetTransform = null;
    //�ذ��� �� Ÿ�� ��ġ(�Ź� �� ã����)
    public Vector3 posTarget = Vector3.zero;

    //�ذ� �ִϸ��̼� ������Ʈ ĳ�� 
    private Animation EnemyAnimation = null;
    //�ذ� Ʈ������ ������Ʈ ĳ��
    private Transform EnemyTransform = null;

    [Header("�ִϸ��̼� Ŭ��")]
    public AnimationClip IdleAnimClip = null;
    public AnimationClip MoveAnimClip = null;
    public AnimationClip AtkAnimClip = null;

    private PlayerCtrl player;
    //public AnimationClip DamageAnimClip = null;
    //public AnimationClip DieAnimClip = null;

    [Header("�����Ӽ�")]
    //�ذ� ü��
    [SerializeField]
    private int hp = 150;
    //�ذ� ���� �Ÿ�
    public float AtkRange = 1.5f;
    //�ذ� ���� ����Ʈ
    public GameObject effectDie = null;
    public GameObject effectDamage = null;

    [SerializeField]
    private int enemyCoin = 5;
    

    //private Tweener effectTweener = null;
    //private SkinnedMeshRenderer skinnedMeshRenderer = null;


    void OnAtkAnmationFinished()
    {

    }

    void OnDmgAnmationFinished()
    {

    }

    void OnDieAnmationFinished()
    {
        Debug.Log("Die Animation finished");

        //���� ���� �̺�Ʈ 
        Instantiate(effectDie, EnemyTransform.position, Quaternion.identity);

        //���� ���� 
        Destroy(gameObject);
    }

    /// <summary>
    /// �ִϸ��̼� �̺�Ʈ�� �߰����ִ� ��. 
    /// </summary>
    /// <param name="clip">�ִϸ��̼� Ŭ�� </param>
    /// <param name="funcName">�Լ��� </param>
    void OnAnimationEvent(AnimationClip clip, string funcName)
    {
        //�ִϸ��̼� �̺�Ʈ�� ����� �ش�
        AnimationEvent retEvent = new AnimationEvent();
        //�ִϸ��̼� �̺�Ʈ�� ȣ�� ��ų �Լ���
        retEvent.functionName = funcName;
        //�ִϸ��̼� Ŭ�� ������ �ٷ� ������ ȣ��
        retEvent.time = clip.length - 0.1f;
        //�� ������ �̺�Ʈ�� �߰� �Ͽ���
        clip.AddEvent(retEvent);
    }

    void Start()
    {
        //ó�� ���� ������
        enemyState = EnemyState.Idle;

        //�ִϸ���, Ʈ������ ������Ʈ ĳ�� : �������� ã�� ������ �ʰ�
        EnemyAnimation = GetComponent<Animation>();
        EnemyTransform = GetComponent<Transform>();

        //�ִϸ��̼� Ŭ�� ��� ��� ����
        EnemyAnimation[IdleAnimClip.name].wrapMode = WrapMode.Loop;
        EnemyAnimation[MoveAnimClip.name].wrapMode = WrapMode.Loop;
        EnemyAnimation[AtkAnimClip.name].wrapMode = WrapMode.Once;
        //EnemyAnimation[DamageAnimClip.name].wrapMode = WrapMode.Once;

        //�ִϸ��̼� ���� ���� ũ�� �ø�
        //EnemyAnimation[DamageAnimClip.name].layer = 10;
        //EnemyAnimation[DieAnimClip.name].wrapMode = WrapMode.Once;
        //EnemyAnimation[DieAnimClip.name].layer = 10;

        //���� �ִϸ��̼� �̺�Ʈ �߰�
        OnAnimationEvent(AtkAnimClip, "OnAtkAnmationFinished");
        //OnAnimationEvent(DamageAnimClip, "OnDmgAnmationFinished");
        //OnAnimationEvent(DieAnimClip, "OnDieAnmationFinished");

        //��Ų�Ž� ĳ��
        //skinnedMeshRenderer = EnemyTransform.Find("UD_light_infantry").GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// �ذ� ���¿� ���� ������ �����ϴ� �Լ� 
    /// </summary>
    void CkState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                //�̵��� ���õ� RayCast��
                setIdle();
                break;
            case EnemyState.GoTarget:
            case EnemyState.Move:
                setMove();
                break;
            case EnemyState.Atk:
                setAtk();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        CkState();
        AnimationCtrl();
    }

    /// <summary>
    /// �ذ� ���°� ��� �� �� ���� 
    /// </summary>
    void setIdle()
    {
        if (targetCharactor == null)
        {
            posTarget = new Vector3(EnemyTransform.position.x + Random.Range(-10f, 10f),
                                    EnemyTransform.position.y + 1000f,
                                    EnemyTransform.position.z + Random.Range(-10f, 10f)
                );
            Ray ray = new Ray(posTarget, Vector3.down);
            RaycastHit infoRayCast = new RaycastHit();
            if (Physics.Raycast(ray, out infoRayCast, Mathf.Infinity) == true)
            {
                posTarget.y = infoRayCast.point.y;
            }
            enemyState = EnemyState.Move;
        }
        else
        {
            enemyState = EnemyState.GoTarget;
        }
    }

    /// <summary>
    /// �ذ� ���°� �̵� �� �� �� 
    /// </summary>
    void setMove()
    {
        //����� ������ �� ������ ���� 
        Vector3 distance = Vector3.zero;
        //��� ������ �ٶ󺸰� ���� �ִ��� 
        Vector3 posLookAt = Vector3.zero;

        //�ذ� ����
        switch (enemyState)
        {
            //�ذ��� ���ƴٴϴ� ���
            case EnemyState.Move:
                //���� ���� ��ġ ���� ���ΰ� �ƴϸ�
                if (posTarget != Vector3.zero)
                {
                    //��ǥ ��ġ���� �ذ� �ִ� ��ġ ���� ���ϰ�
                    distance = posTarget - EnemyTransform.position;

                    //���࿡ �����̴� ���� �ذ��� ��ǥ�� �� ���� ���� ���� 
                    if (distance.magnitude < AtkRange)
                    {
                        //��� ���� �Լ��� ȣ��
                        StartCoroutine(setWait());
                        //���⼭ ����
                        return;
                    }

                    //��� ������ �ٶ� �� ����. ���� ����
                    posLookAt = new Vector3(posTarget.x,
                                            //Ÿ���� ���� ���� ��찡 ������ y�� üũ
                                            EnemyTransform.position.y,
                                            posTarget.z);
                }
                break;
            //ĳ���͸� ���ؼ� ���� ���ƴٴϴ�  ���
            case EnemyState.GoTarget:
                //��ǥ ĳ���Ͱ� ���� ��
                if (targetCharactor != null)
                {
                    //��ǥ ��ġ���� �ذ� �ִ� ��ġ ���� ���ϰ�
                    distance = targetCharactor.transform.position - EnemyTransform.position;
                    //���࿡ �����̴� ���� �ذ��� ��ǥ�� �� ���� ���� ���� 
                    if (distance.magnitude < AtkRange)
                    {
                        //���ݻ��·� �����մ�.
                        enemyState = EnemyState.Atk;
                        //���⼭ ����
                        return;
                    }
                    //��� ������ �ٶ� �� ����. ���� ����
                    posLookAt = new Vector3(targetCharactor.transform.position.x,
                                            //Ÿ���� ���� ���� ��찡 ������ y�� üũ
                                            EnemyTransform.position.y,
                                            targetCharactor.transform.position.z);
                }
                break;
            default:
                break;

        }

        //�ذ� �̵��� ���⿡ ũ�⸦ ���ְ� ���⸸ ����(normalized)
        Vector3 direction = distance.normalized;

        //������ x,z ��� y�� ���� �İ� ���Ŷ� ����
        direction = new Vector3(direction.x, 0f, direction.z);

        //�̵��� ���� ���ϱ�
        Vector3 amount = direction * spdMove * Time.deltaTime;

        //ĳ���� ��Ʈ���� �ƴ� Ʈ���������� ���� ��ǥ �̿��Ͽ� �̵�
        EnemyTransform.Translate(amount, Space.World);
        //ĳ���� ���� ���ϱ�
        EnemyTransform.LookAt(posLookAt);

    }

    /// <summary>
    /// ��� ���� ���� �� 
    /// </summary>
    /// <returns></returns>
    IEnumerator setWait()
    {
        //�ذ� ���¸� ��� ���·� �ٲ�
        enemyState = EnemyState.Wait;
        //����ϴ� �ð��� �������� �ʰ� ����
        float timeWait = Random.Range(1f, 3f);
        //��� �ð��� �־� ��.
        yield return new WaitForSeconds(timeWait);
        //��� �� �ٽ� �غ� ���·� ����
        enemyState = EnemyState.Idle;
    }

    /// <summary>
    /// �ִϸ��̼��� ��������ִ� �� 
    /// </summary>
    void AnimationCtrl()
    {
        //�ذ��� ���¿� ���� �ִϸ��̼� ����
        switch (enemyState)
        {
            //���� �غ��� �� �ִϸ��̼� ��.
            case EnemyState.Wait:
            case EnemyState.Idle:
                //�غ� �ִϸ��̼� ����
                EnemyAnimation.CrossFade(IdleAnimClip.name);
                break;
            //������ ��ǥ �̵��� �� �ִϸ��̼� ��.
            case EnemyState.Move:
            case EnemyState.GoTarget:
                //�̵� �ִϸ��̼� ����
                EnemyAnimation.CrossFade(MoveAnimClip.name);
                break;
            //������ ��
            case EnemyState.Atk:
                //���� �ִϸ��̼� ����
                EnemyAnimation.CrossFade(AtkAnimClip.name);
                break;
            //�׾��� ��
            case EnemyState.Die:
                //���� ���� �ִϸ��̼� ����
                //EnemyAnimation.CrossFade(DieAnimClip.name);
                break;
            default:
                break;

        }
    }

    ///<summary>
    ///�þ� ���� �ȿ� �ٸ� Trigger �Ǵ� ĳ���Ͱ� ������ ȣ�� �ȴ�.
    ///�Լ� ������ ��ǥ���� ������ ��ǥ���� �����ϰ� �ذ��� Ÿ�� ��ġ�� �̵� ��Ų�� 
    ///</summary>
    void OnCkTarget(GameObject target)
    {
        //��ǥ ĳ���Ϳ� �Ķ���ͷ� ����� ������Ʈ�� �ְ� 
        targetCharactor = target;
        //��ǥ ��ġ�� ��ǥ ĳ������ ��ġ ���� �ֽ��ϴ�. 
        targetTransform = targetCharactor.transform;

        //��ǥ���� ���� �ذ��� �̵��ϴ� ���·� ����
        enemyState = EnemyState.GoTarget;

    }

    /// <summary>
    /// �ذ� ���� ���� ���
    /// </summary>
    void setAtk()
    {
        //�ذ�� ĳ���Ͱ��� ��ġ �Ÿ� 
        float distance = Vector3.Distance(targetTransform.position, EnemyTransform.position); //���̴�

        //���� �Ÿ����� �� ���� �Ÿ��� �־� ���ٸ� 
        if (distance > AtkRange + 0.5f)
        {
            //Ÿ�ٰ��� �Ÿ��� �־����ٸ� Ÿ������ �̵� 
            enemyState = EnemyState.GoTarget;
        }
    }

    /// <summary>
    /// �ذ� �ǰ� �浹 ���� 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //���࿡ �ذ��� ĳ���� ���ݿ� �¾Ҵٸ�
        if (other.gameObject.CompareTag("PlayerAtk") == true)
        { 
            PlayerCtrl player = targetCharactor.GetComponent<PlayerCtrl>();

            //�ذ� ü���� 10 ����
            hp -= 10 + (int)player.STRENGTH/2;
            if (hp >= 0)
            {
                //�ǰ� ����Ʈ 
                Instantiate(effectDamage, other.transform.position, Quaternion.identity);

                //ü���� 0 �̻��̸� �ǰ� �ִϸ��̼��� ���� �ϰ� 
                //EnemyAnimation.CrossFade(DamageAnimClip.name);

                //�ǰ� Ʈ���� ����Ʈ
                //effectDamageTween();
            }
            else
            {
                //0 ���� ������ �ذ��� ���� ���·� �ٲپ��
                enemyState = EnemyState.Die;
                player.Coin += enemyCoin;
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// �ǰݽ� ���� ������ ��½��½ ȿ���� �ش�
    /// </summary>
    //void effectDamageTween()
    //{
    //    //Ʈ���� ������ �� Ʈ�� �Լ��� ����Ǹ� ������ ������ �� �� �־ 
    //    //Ʈ�� �ߺ� üũ�� �̸� ������ ���ش�
    //    if (effectTweener != null && effectTweener.isComplete == false)
    //    {
    //        return;
    //    }

    //    //��½�̴� ����Ʈ ������ �������ش�
    //    Color colorTo = Color.red;

    //    //Ʈ���� Ÿ���� ��Ų�Ž�������, �ð��� 0.2��, �Ķ���ͷδ� ���� , �ݺ�. �ݹ��Լ�
    //    effectTweener = HOTween.To(skinnedMeshRenderer, 0.2f, new TweenParms()
    //                            //������ ��ü
    //                            .Prop("color", colorTo)
    //                            // �ݺ��� 1���� ��並 �Ⱦ��� ������ 1ȸ ��� 1ȸ �ؾ� �Ѵ�. 
    //                            .Loops(1, LoopType.Yoyo)
    //                            //�ǰ� ����Ʈ ����� �̺�Ʈ �Լ� ȣ��
    //                            .OnStepComplete(OnDamageTweenFinished)
    //        );
    //}

    /// <summary>
    /// �ǰ�����Ʈ ����� �̺�Ʈ �Լ� ȣ��
    /// </summary>
    //void OnDamageTweenFinished()
    //{
    //    //Ʈ���� ������ �Ͼ������ Ȯ���� ������ �����ش�
    //    skinnedMeshRenderer.material.color = Color.white;
    //}
}