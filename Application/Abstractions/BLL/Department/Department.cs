using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.BLL.Department
{
    public class Department
    {
    private int _DepartmentID;
        private string _IDLink;
        private string _DepartmentName;
        public int DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }
        public string IDLink
        {
            get { return _IDLink; }
            set { _IDLink = value; }
        }
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }
    }
}