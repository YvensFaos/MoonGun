    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShieldRestorationConfirmation : MonoBehaviour
    {
        [SerializeField] private Text shieldCostText;
        private readonly string _shieldDefaultText = "Restoring the shields will cost {0} ASTEROIDS.";
        
        [SerializeField] private AudioClip upgradeSuccessSound;
        [SerializeField] private AudioClip upgradeFailedSound;

        private void OnEnable()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(_shieldDefaultText, GameLogic.Instance.ShieldRestorationCost);
            shieldCostText.text = stringBuilder.ToString();
        }

        public void ConfirmRestoration()
        {
            var cost = GameLogic.Instance.ShieldRestorationCost;
            if (GameLogic.Instance.CheckFunds(cost, 0))
            {
                GameLogic.Instance.PayForUpgrade(cost, 0);
                
                GameLogic.Instance.PlayUISound(upgradeSuccessSound);
                GameLogic.Instance.Shield.RestoreShield();
                GameLogic.Instance.IncreaseShieldRestorationCost();
                gameObject.SetActive(false);
            }
            else
            {
                GameLogic.Instance.PlayUISound(upgradeFailedSound);
            }
        }
    }
