using TMPro;
using UnityEngine;
using WhacAMole.Interfaces;

namespace WhacAMole.Screens
{
    public class GameSettings : MonoBehaviour, IMenuScreen
    {
        public bool IsActive => content.activeSelf;

        [SerializeField] private TMP_InputField amountOfMolesInputField;
        private TextMeshProUGUI amountOfMolesPlaceholderText;

        [SerializeField] private TMP_Dropdown timerDropDown;
        [SerializeField] private GameObject content;

        private string wrongAmountOfMolesGiven = "Please give a number between bigger then 0 and smaller then 13";

        private const int minimumOfMoles = 1;
        private const int maximumOfMoles = 12;
        private const int timerIncrease = 30;

        // Start is called before the first frame update
        void Start()
        {
            amountOfMolesPlaceholderText = amountOfMolesInputField.placeholder.GetComponent<TextMeshProUGUI>();
        }
        public void Show()
        {
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }

        /// <summary>
        /// Called when the value of the amount of moles input field changes.
        /// </summary>
        public void OnValueChanged(string newValue)
        {
            ValidateInput();
        }

        /// <summary>
        /// Validates the input value from the amount of moles input field
        /// </summary>
        /// <returns>Returns the parsed integer value if valid; otherwise, returns -1.</returns>
        public int ValidateInput()
        {
            int value = 0;
            int.TryParse(amountOfMolesInputField.text, out value);

            if (value < minimumOfMoles || value > maximumOfMoles)
            {
                SetErrorMessage();
                return -1;
            }

            return value;
        }

        /// <summary>
        /// Sets an error message on the amount of moles placeholder text
        /// </summary>
        private void SetErrorMessage()
        {
            amountOfMolesPlaceholderText.text = wrongAmountOfMolesGiven;
            amountOfMolesPlaceholderText.color = Color.red;
            amountOfMolesInputField.text = "";
        }

        /// <summary>
        /// Retrieves the selected timer value from the dropdown.
        /// </summary>
        public int GetSelectedTimer()
        {
            return timerDropDown.value + 1;
        }

        /// <summary>
        /// Resets the input field and dropdown to their default states
        /// </summary>
        private void Reset()
        {
            amountOfMolesInputField.text = "";
            timerDropDown.value = 0;
            amountOfMolesPlaceholderText.color = Color.black;
        }
    }
}