using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal
{
    class FormClass
    {
        private string formIn;
        private string formOut;
        private string formCheck;
        private string formRoom;
        private string formID;
        private string formComm;
        public FormClass()
        {
            this.formIn = string.Empty;
            this.formOut = string.Empty;
            this.formCheck = string.Empty;
            this.formRoom = string.Empty;
            this.formID = string.Empty;
            this.formComm = string.Empty;
        }
        public string cFormCheck
        {
            get
            {
                return this.formCheck;
            }
            set
            {
                if (value != string.Empty)
                {
                    this.formCheck = value;
                }
            }
        }
        public string cFormIn
        {
            get
            {
                return this.formIn;
            }
            set
            {
                if (value != string.Empty)
                {
                    this.formIn = value;
                }
            }
        }
        public string cFormOut
        {
            get
            {
                return this.formOut;
            }
            set
            {
                if (value != string.Empty)
                {
                    this.formOut = value;
                }
            }
        }
        public string cFormRoom
        {
            get
            {
                return this.formRoom;
            }
            set
            {
                if (value != string.Empty)
                {
                    this.formRoom = value;
                }
            }
        }
        public string cFormID
        {
            get
            {
                return this.formID;
            }
            set
            {
                if (value != string.Empty)
                {
                    this.formID = value;
                }
            }
        }
        public string cFormComm
        {
            get
            {
                return this.formComm;
            }
            set
            {
                if (value != string.Empty)
                {
                    this.formComm = value;
                }
            }
        }
    }
}
