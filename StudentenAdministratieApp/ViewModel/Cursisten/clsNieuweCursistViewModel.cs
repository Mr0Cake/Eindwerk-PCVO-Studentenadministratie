using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApplication.Model;
using BLL;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BLL.Extensions;


namespace StudentenAdministratieApp.ViewModel.Cursisten
{
    public class clsNieuweCursistViewModel : clsInschrijvingenBaseViewModel
    {
        
        public clsNieuweCursistViewModel()
        {
            _SelectedCursist = new clsGebruiker();
        }

        public override void Cancel()
        {
            SelectedCursist = new clsGebruiker();
        }


        public override void Save()
        {
            MvvmValidation.ValidationResult res = SelectedCursist.Validator.ValidateAll();
            if (res.IsValid)
            {
                if (SelectedCursist.IDGebruiker < 1)
                {
                    BLL.InsertData(SelectedCursist);
                    BLL.InsertData(new clsGebruikers_TypeGebruikers { IDGebruiker = SelectedCursist.IDGebruiker, IDType = GebruikerTypes.ToList().FindLast(p => p.TypeNaam.Equals("Student")).IDType });
                }
                else
                {
                    BLL.UpdateData(SelectedCursist);
                }
                int ID = SelectedCursist.IDGebruiker;
                
                ExecuteOnSave.Values.ToList().ForEach(p => p());
                Opgeslagen = true;
                DelayedAction(() => Opgeslagen = false, 5000);
            }
            else
            {
                ValidationErrors = res.ToString();
                SelectedCursist.NotifyAll();
            }
        }
        
        

        



    }
}
