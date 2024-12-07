using AMS.Domain._SharedKernel.Enum;

namespace AMS.Domain._SharedKernel.DTOs.Request
{
    public class SortingModel
    {
        public string Column { get; set; }

        private string _Direction;
        public string Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                _Direction = value;

                if (value == "asc")
                    SortingDirection = SortDirectionEnum.Ascending;
                else
                    SortingDirection = SortDirectionEnum.Descending;
            }
        }
        public SortDirectionEnum SortingDirection { get; set; }
    }
}
