namespace Abstractions.BusinessLogic.Messaging
{
    public class MessageAbstract
    {
        private string _MessageTitle;
        private string _body;
        private string _techicianID;
        private int _partialID;
        private int _partialIndex;
        private int _partialSize;

        public string MessageTitle
        {
            get => _MessageTitle;
            set
            {
                _MessageTitle = value;
            }
        }

        public string Body
        {
            get => _body;
            set
            {
                _body = value;
            }
        }

        public string TechnicianID
        {
            get => _techicianID;
            set
            {
                _techicianID = value;
            }
        }

        public int PartialID
        {
            get => _partialID;
            set
            {
                _partialID = value;
            }
        }

        public int PartialIndex
        {
            get => _partialIndex;
            set
            {
                _partialIndex = value;
            }
        }

        public int PartialSize
        {
            get => _partialSize;
            set
            {
                _partialSize = value;
            }
        }
    }
}
