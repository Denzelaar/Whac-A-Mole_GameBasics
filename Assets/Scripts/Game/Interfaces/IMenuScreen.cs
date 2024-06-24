namespace WhacAMole.Interfaces
{
    public interface IMenuScreen
    {
        bool IsActive { get; }

        /// <summary>
        /// Activates the UI content.
        /// </summary>
        void Show();

        /// <summary>
        /// Deactivates the UI content.
        /// </summary>
        void Hide();
    }
}