using _1p_atom_carmanager.backend.core.Entities;

namespace _1p_atom_carmanager.backend.core.Requests
{
    public class GetCarUsingCostRequest
    {
        public List<Fine>? fines { get; set; }

        public decimal fuelCost { get; set; }

        public string number { get; set; }
    }
}
