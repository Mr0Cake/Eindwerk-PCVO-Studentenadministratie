using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MvvmValidation;

namespace StudentApplication.Model
{
    /// <summary>
    /// clsBaseModel bevat properties en methodes voor de Events van PropertyChanged
    /// </summary>
    public class clsBaseModel : INotifyPropertyChanged, IDataErrorInfo
    {
        protected static BLL.clsCommonBLL bllCommon = new BLL.clsCommonBLL();
        protected static BLL.clsCustomBLL bllCustom = new BLL.clsCustomBLL();
        protected clsBaseModel _Backup;

        public clsBaseModel()
        {
            Validator = new ValidationHelper();
            DataErrorInfoAdapter = new DataErrorInfoAdapter(Validator);

        }

        public clsBaseModel Backup
        {
            get
            {
                return _Backup;
            }
            set
            {
                _Backup = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string myPropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(myPropertyName));
            }
        }
        private int mySelectedIndex;
        public int MySelectedIndex
        {
            get { return mySelectedIndex; }
            set
            {
                mySelectedIndex = value;
                RaisePropertyChanged("MySelectedIndex");
            }
        }
        private bool isDirty = false;
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }
        private bool isFocus = false;
        public bool IsFocus
        {
            get { return isFocus; }
            set
            {
                isFocus = value;
                RaisePropertyChanged("IsFocus");
            }
        }
        private object controlField;
        public object ControleVeld
        {
            get { return controlField; }
            set { controlField = value; }
        }

        private string createdBy;

        public string AangemaaktDoor
        {
            get { return createdBy; }
            set { createdBy = value; }
        }


        private DateTime createdOn;
        public DateTime AangemaaktOp
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        private DateTime modifiedOn;
        public DateTime AangepastOp
        {
            get { return modifiedOn; }
            set { modifiedOn = value; }
        }
        private bool isDeleted;
        public bool IsGedelete
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string Error
        {
            get { return DataErrorInfoAdapter.Error; }
        }

        /// <summary>
        /// Getter voor het ID van een object dat over erft van clsBaseModel.
        /// </summary>
        protected int _ID = -1;
        public virtual int getID
        {
            get { return _ID; }
        }

        #region Validation
        public ValidationHelper Validator { get; private set; }

        private DataErrorInfoAdapter DataErrorInfoAdapter { get; set; }

        protected virtual void ConfigureValidationRules()
        {
        }


        /// <summary>
        /// Validation style not triggering on empty fields -> Quick slow fix
        /// 
        /// </summary>
        public void NotifyAll()
        {
            foreach(var v in this.GetType().GetProperties())
            {
                Type t = v.PropertyType;
                if (t.IsPrimitive || t == typeof(Decimal) || t == typeof(String) || t == typeof(byte[]))
                {
                    RaisePropertyChanged(v.Name);
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                return DataErrorInfoAdapter[columnName];
            }
        }

        #endregion
    }
}
