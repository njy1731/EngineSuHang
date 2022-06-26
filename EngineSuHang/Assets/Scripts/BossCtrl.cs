using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class BossCtrl : MonoBehaviour
{
    public enum BossState { None, Idle, Move, Wait, GoTarget, Atk, Damage, Die }
    [Header("�⺻ �Ӽ�")]
    public BossState bossState = BossState.None;
    public float spdMove = 1f;
    public GameObject targetCharactor = null;
    public Transform targetTransform = null;
    public Vector3 posTarget = Vector3.zero;
    private Animation BossAnimation = null;
    private Transform BossTransform = null;

    [Header("�ִϸ��̼� Ŭ��")]
    public AnimationClip IdleAnimClip = null;
    public AnimationClip MoveAnimClip = null;
    public AnimationClip AtkAnimClip = null;

    [Header("�����Ӽ�")]
    [SerializeField]
    private int hp = 1000;
    public float AtkRange = 3f;

    public ParticleSystem effect;

    [SerializeField]
    private int bossCoin = 150;

    private bool isHit = false;

    void OnAtkAnmationFinished()
    {

    }

    void OnDmgAnmationFinished()
    {

    }

    void OnDieAnmationFinished()
    {
        Debug.Log("Die Animation finished");
        Destroy(gameObject);
    }

    /// <summary>
    /// �ִϸ��̼� �̺�Ʈ�� �߰����ִ� ��. 
    /// </summary>
    /// <param name="clip">�ִϸ��̼� Ŭ�� </param>
    /// <param name="funcName">�Լ��� </param>
    void OnAnimationEvent(AnimationClip clip, string funcName)
    {
        AnimationEvent retEvent = new AnimationEvent();
        retEvent.functionName = funcName;
        retEvent.time = clip.length - 0.1f;
        clip.AddEvent(retEvent);
    }

    void Start()
    {

        bossState = BossState.Idle;
        BossAnimation = GetComponent<Animation>();
        BossTransform = GetComponent<Transform>();
        BossAnimation[IdleAnimClip.name].wrapMode = WrapMode.Loop;
        BossAnimation[MoveAnimClip.name].wrapMode = WrapMode.Loop;
        BossAnimation[AtkAnimClip.name].wrapMode = WrapMode.Once;
        OnAnimationEvent(AtkAnimClip, "OnAtkAnmationFinished");
    }

    /// <summary>
    /// ���� ���¿� ���� ������ �����ϴ� �Լ� 
    /// </summary>
    void CkState()
    {
        switch (bossState)
        {
            case BossState.Idle:
                //�̵��� ���õ� RayCast��
                setIdle();
                break;
            case BossState.GoTarget:
            case BossState.Move:
                setMove();
                break;
            case BossState.Atk:
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
    /// ���� ���°� ��� �� �� ���� 
    /// </summary>
    void setIdle()
    {
        if (targetCharactor == null)
        {
            posTarget = new Vector3(BossTransform.position.x + Random.Range(-10f, 10f),
                                    BossTransform.position.y + 1000f,
                                    BossTransform.position.z + Random.Range(-10f, 10f)
                );
            Ray ray = new Ray(posTarget, Vector3.down);
            RaycastHit infoRayCast = new RaycastHit();
            if (Physics.Raycast(ray, out infoRayCast, Mathf.Infinity) == true)
            {
                posTarget.y = infoRayCast.point.y;
            }
            bossState = BossState.Move;
        }
        else
        {
            bossState = BossState.GoTarget;
        }
    }

    /// <summary>
    /// ���� ���°� �̵� �� �� �� 
    /// </summary>
    void setMove()
    {
        Vector3 distance = Vector3.zero;
        Vector3 posLookAt = Vector3.zero;
        switch (bossState)
        {
            case BossState.Move:
                if (posTarget != Vector3.zero)
                {
                    distance = posTarget - BossTransform.position;
                    if (distance.magnitude < AtkRange)
                    {
                        StartCoroutine(setWait());
                        return;
                    }

                    posLookAt = new Vector3(posTarget.x,
                                            BossTransform.position.y,
                                            posTarget.z);
                }
                break;
            case BossState.GoTarget:
                if (targetCharactor != null)
                {
                    distance = targetCharactor.transform.position - BossTransform.position;
                    if (distance.magnitude < AtkRange)
                    {
                        bossState = BossState.Atk;
                        return;
                    }
                    posLookAt = new Vector3(targetCharactor.transform.position.x,
                                            BossTransform.position.y,
                                            targetCharactor.transform.position.z);
                }
                break;
            default:
                break;
        }

        Vector3 direction = distance.normalized;
        direction = new Vector3(direction.x, 0f, direction.z);
        Vector3 amount = direction * spdMove * Time.deltaTime;
        BossTransform.Translate(amount, Space.World);
        BossTransform.LookAt(posLookAt);
    }

    /// <summary>
    /// ��� ���� ���� �� 
    /// </summary>
    /// <returns></returns>
    IEnumerator setWait()
    {
        bossState = BossState.Wait;
        float timeWait = Random.Range(1f, 3f);
        yield return new WaitForSeconds(timeWait);
        bossState = BossState.Idle;
    }

    /// <summary>
    /// �ִϸ��̼��� ��������ִ� �� 
    /// </summary>
    void AnimationCtrl()
    {
        switch (bossState)
        {
            case BossState.Wait:
            case BossState.Idle:
                BossAnimation.CrossFade(IdleAnimClip.name);
                break;
            case BossState.Move:
            case BossState.GoTarget:
                BossAnimation.CrossFade(MoveAnimClip.name);
                break;
            //������ ��
            case BossState.Atk:
                BossAnimation.CrossFade(AtkAnimClip.name);
                break;
            //�׾��� ��
            case BossState.Die:
                break;
            default:
                break;

        }
    }

    ///<summary>
    ///�þ� ���� �ȿ� �ٸ� Trigger �Ǵ� ĳ���Ͱ� ������ ȣ�� �ȴ�.
    ///�Լ� ������ ��ǥ���� ������ ��ǥ���� �����ϰ� ������ Ÿ�� ��ġ�� �̵� ��Ų�� 
    ///</summary>
    void OnCkTarget(GameObject target)
    {
        targetCharactor = target;
        targetTransform = targetCharactor.transform;
        bossState = BossState.GoTarget;

    }

    /// <summary>
    /// ���� ���� ���� ���
    /// </summary>
    void setAtk()
    {
        if (PlayerCtrl.Instance.HP <= 0)
            return;
        float distance = Vector3.Distance(targetTransform.position, BossTransform.position); //���̴�
        if (distance > AtkRange + 0.5f)
        {
            bossState = BossState.GoTarget;
        }
    }

    /// <summary>
    /// ���� �ǰ� �浹 ���� 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAtk") == true)
        {
            if (isHit) return;

            isHit = true;
            Instantiate(effect, transform).transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            PlayerCtrl player = targetCharactor.GetComponent<PlayerCtrl>();

            hp -= 10 + (int)player.STRENGTH;
            if (hp >= 0)
            {
                isHit = false;
            }
            else
            {
                bossState = BossState.Die;
                player.Coin += bossCoin;
                Destroy(gameObject);
            }
        }
    }

    public void PlayerDie()
    {
        bossState = BossState.Idle;
    }
}