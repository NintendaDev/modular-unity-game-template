using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Wallets.UI.Views
{
    public sealed class WalletView : MonoBehaviour
    {
        [SerializeField, Required] private Image _icon;
        [SerializeField, Required] private TMP_Text _amountLabel;

        public void SetIcon(Sprite sprite) =>
            _icon.sprite = sprite;

        public void SetAmount(string amount) =>
            _amountLabel.text = amount;
    }
}
