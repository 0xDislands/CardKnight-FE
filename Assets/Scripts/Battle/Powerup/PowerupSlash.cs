using UnityEngine;
using DG.Tweening;

public class PowerupSlash : MonoBehaviour
{
    public EffectSlash effect;
    public ParticleSystem attackEffect;
    public Vector2Int pos;
    public Card card;
    public void OnClick()
    {
        var newEffect = SimpleObjectPool.Instance.GetObjectFromPool(effect, card.transform.position);
        newEffect.DoEffect();
        var newAttackEffect = SimpleObjectPool.Instance.GetObjectFromPool(attackEffect, card.transform.position);

        var monster = card.GetComponent<Monster>();
        if (monster != null)
        {
            var damage = new DamageData();
            damage.damage = 1;
            monster.TakeDamage(damage, out bool dead);
        }
        var slashes = FindObjectsOfType<PowerupSlash>();
        for (int i = 0; i < slashes.Length; i++)
        {
            slashes[i].gameObject.SetActive(false);
        }
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}