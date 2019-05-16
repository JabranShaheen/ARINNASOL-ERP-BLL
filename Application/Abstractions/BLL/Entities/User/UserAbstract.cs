using Abstractions.BLL.Entity;
using System;

namespace Abstractions.BLL.Entities.User
{
    public class UserAbstract : IEntity
    {
        private string _ID;
        private string _LoginName;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private DateTime _DOB;
        private string _Qualifications;
        private string _Address1;
        private string _Address2;
        private string _PostCode;
        private string _Reference1;
        private string _Reference2;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string Qualifications
        {
            get { return _Qualifications; }
            set { _Qualifications = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string PostCode
        {
            get { return _PostCode; }
            set { _PostCode = value; }
        }
        public string Reference1
        {
            get { return _Reference1; }
            set { _Reference1 = value; }
        }
        public string Reference2
        {
            get { return _Reference2; }
            set { _Reference2 = value; }
        }
    }
}