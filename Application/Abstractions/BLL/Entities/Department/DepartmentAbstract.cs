using Abstractions.BLL.Entity;

namespace Abstractions.BLL.Entities.Department
{
    public class DepartmentAbstract : IEntity
    {
        private string _ID;
        private string _DepartmentName;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }
    }
}