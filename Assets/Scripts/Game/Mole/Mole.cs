using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Interfaces;

namespace WhacAMole.Moles
{
    public class Mole : MonoBehaviour, ITubeAnimal
    {
        public Action Hit { get; set; }

        public bool IsActive => isActive;
        private bool isActive = false;

        [SerializeField] private Animator moleAnimator;
        [SerializeField] private Button button;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Coroutine moleCoroutine = null;
        private Coroutine changeColorCoroutine = null;
        private readonly Color hitColor = new Color(0.8117647f, 0.4078431f, 0.172549f);
        private readonly Color moleBaseColor = Color.white;
        private readonly float changeColorTime = 0.35f;

        public void Show(float moleActiveTime)
        {
            // Activate Popup anim
            moleAnimator.SetBool("Active", true);
            moleCoroutine = StartCoroutine(MoleCoroutine(moleActiveTime));
        }

        public void Hide()
        {
            moleCoroutine = null;

            // Activate Hide Animation
            moleAnimator.SetBool("Active", false);
        }

        /// <summary>
        /// Callback method when the mole's show animation completes 
        /// </summary>
        public void OnAnimationComplete()
        {
            isActive = true;
            button.interactable = true;

            if (changeColorCoroutine != null)
            {
                StopCoroutine(changeColorCoroutine);
                changeColorCoroutine = null;
            }
        }

        /// <summary>
        /// Callback method when the mole's hide animation completes
        /// </summary>
        public void OnReverseAnimationComplete()
        {
            button.interactable = false;
            isActive = false;
        }

        public void OnHit()
        {
            Hit?.Invoke();
            changeColorCoroutine = StartCoroutine(ChangeColorCoroutine());
            button.interactable = false;

            if (moleCoroutine != null)
            {
                StopCoroutine(moleCoroutine);
                moleCoroutine = null;
            }
            Hide();
        }

        /// <summary>
        /// Coroutine to control how long the mole remains active
        /// </summary>
        /// <param name="moleActiveTime">The seconds the Mole should stay active.</param>
        private IEnumerator MoleCoroutine(float moleActiveTime)
        {
            yield return new WaitForSeconds(moleActiveTime);
            Hide();
        }

        /// <summary>
        /// Coroutine to temporarily change mole's color when hit
        /// </summary>
        private IEnumerator ChangeColorCoroutine()
        {
            spriteRenderer.color = hitColor;
            yield return new WaitForSeconds(changeColorTime);
            spriteRenderer.color = moleBaseColor;
        }
    }
}