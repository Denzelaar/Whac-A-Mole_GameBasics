using System;
namespace WhacAMole.Interfaces
{
    public interface ITubeAnimal
    {
        bool IsActive { get; }
        Action Hit { get; set; }

        /// <summary>
        /// Shows the mole and starts its active coroutine
        /// </summary>
        /// <param name="activeTime">The seconds the <see cref="ITubeAnimal"/> should stay active.</param>
        void Show(float activeTime);

        /// <summary>
        /// Hides the mole and stops its active coroutine 
        /// </summary>
        void Hide();

        /// <summary>
        /// Triggered when the <see cref="ITubeAnimal"/>  is hit by the player
        /// </summary>
        void OnHit();
    }
}