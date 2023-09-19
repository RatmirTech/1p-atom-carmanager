namespace _1p_atom_carmanager.backend.core.Entities
{
    /// <summary>
    /// Штрафы и услуги
    /// </summary>
    public class Fine
    {
        /// <summary>
        /// Стомость штрафа или услуги
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Название штрафа или услуги
        /// </summary>
        public string Title { get; set; } = string.Empty;
    }
}
