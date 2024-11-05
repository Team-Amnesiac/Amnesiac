using UnityEngine;

public class NightcrawlerBattle : MonoBehaviour
{
    public float maxHealth = 150f;

    private bool specialAttackUsed = false;

    void Start()
    {

    }

    public void ExecuteAttack()
    {
        float damage = 15f;
        if (!specialAttackUsed && Random.value > 0.9f)
        {
            damage = 40f;
            specialAttackUsed = true;
            Debug.Log("Nightcrawler used Earthshatter! Deals 40 damage.");
        }
        else
        {
            Debug.Log("Nightcrawler used a standard attack. Deals 15 damage.");
        }

        //BattleManager.Instance.DealDamageToPlayer(damage);
    }

    public void ResetSpecialAttack()
    {
        specialAttackUsed = false;
    }
}
