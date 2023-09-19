using _1p_atom_carmanager.backend.core.Entities;

namespace _1p_atom_carmanager.backend.core.Requests
{
    /// <summary>
    /// Запрос на получение стоимости пользования авто
    /// </summary>
    public class GetCarUsingCostRequest
    {
        /// <summary>
        /// Штрафы и услуги
        /// </summary>
        public List<Fine>? fines { get; set; }

        /// <summary>
        /// Стоимость топлива за 1 литр
        /// </summary>
        public decimal fuelCost { get; set; }

        /// <summary>
        /// Vin или License plate, смотря от запроса
        /// </summary>
        public string number { get; set; }
    }
}
