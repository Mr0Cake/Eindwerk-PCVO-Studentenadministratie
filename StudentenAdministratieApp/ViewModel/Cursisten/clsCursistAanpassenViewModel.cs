using StudentApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using BLL.Extensions;
using System.Windows;

namespace StudentenAdministratieApp.ViewModel.Cursisten
{
    public class clsCursistAanpassenViewModel : clsInschrijvingenBaseViewModel
    {


        public override void Save()
        {
            
            MvvmValidation.ValidationResult res = SelectedCursist.Validator.ValidateAll();
            if (res.IsValid)
            {
                BLL.UpdateData(SelectedCursist);
                ExecuteOnSave.Values.ToList().ForEach(p => p());
                Opgeslagen = true;
                ExecuteOnSave.Clear();
                BLL.Fill(SelectedCursist as clsGebruiker, SelectedCursist.Backup as clsGebruiker);
                DelayedAction(() => Opgeslagen = false, 5000);
            }
            else
            {
                ValidationErrors = res.ToString();
            }
        }

        public override void Cancel()
        {
            BLL.Fill(SelectedCursist.Backup as clsGebruiker, SelectedCursist as clsGebruiker);
        }

        public override void DoeInschrijving(bool input, clsInschrijving inschr, clsKlas klas)
        {
            if (SelectedCursist != null)
            {
                string inschrijving = "Inschrijving:" + SelectedCursist.IDGebruiker + ":" + klas.IDModule + ":" + klas.IDKlas;
                string uitschrijving = "Uitschrijving:" + SelectedCursist.IDGebruiker + ":" + klas.IDModule + ":" + klas.IDKlas;
                AddtoExecuteOnSave(input, inschrijving, uitschrijving, () => { SchrijfIn(inschr, klas); }, () => { SchrijfUit(inschr); });
            }
        }

        public void DoeUpdate(clsInschrijving inschr)
        {
            string key = "Inschrijving:" + SelectedCursist.IDGebruiker + ":" + inschr.IDModule;
            string actionKey = ExecuteOnSave.Keys.ToList().Find(x => x.ToLower().Contains(key.ToLower()));
            if(!string.IsNullOrEmpty(actionKey))
            if (ExecuteOnSave.ContainsKey(actionKey))
            {


                ExecuteOnSave.Remove(actionKey);

            }


            ExecuteOnSave.Add(key + ":" + inschr.IDKlas, () => UpdateInschrijving(inschr));

        }

        public override void vulInschrijvingen(clsGebruiker sgebruiker)
        {
            base._GebruikerInschrijvingen = new ObservableCollection<clsInschrijvingListItem>();
            List<clsInschrijving> isIngeschreven = new List<clsInschrijving>();
            if (sgebruiker != null)
            {
                clsModuleType type;
                isIngeschreven = Inschrijvingen.ToList().Where(x => x.IDGebruiker == sgebruiker.IDGebruiker).ToList();

                Modules.Where(x => isIngeschreven.FindAll(q => q.IDModule == x.IDModule).Count == 0 && x.KanInschrijven() && VoorafgaandeModules.ToList().FindIndex(p => p.IDModuleType == x.IDModuleType) < 0).ToList().ForEach(
                    o =>
                    {
                        type = ModuleTypes.Where(t => t.IDType == o.IDModuleType).FirstOrDefault();
                        if (type != null)
                        {
                            clsInschrijvingListItem li = new clsInschrijvingListItem(o, Klassen.Where(z => z.IDModule == o.IDModule).ToList(), sgebruiker, (type).InterneNaam, this.DoeInschrijving, UpdateInschrijving);

                            trycatch(() => li.SelectedStudiegebied = StudieGebieden.ToList().Find(p => p.IDStudieGebied == Opleidingen.ToList().Find(x => x.IDOpleiding == li.Module.IDOpleiding).IDStudieGebied));
                            trycatch(() => li.Opleiding = Opleidingen.ToList().Find(p => p.IDOpleiding == li.Module.IDOpleiding));
                            base._GebruikerInschrijvingen.Add(li);
                        }
                    });
            }
            if (isIngeschreven.Count > 0)
                isIngeschreven.ForEach(p => _GebruikerInschrijvingen.Add(
                    new clsInschrijvingListItem(p,
                    Modules.ToList().Find(q => q.IDModule == p.IDModule),
                    Klassen.Where(z => p.IDModule == z.IDModule).ToList(),
                    Klassen.ToList().Find(klas => p.IDKlas == klas.IDKlas),
                    sgebruiker,
                    ModuleTypes.Where(t => t.IDType == Modules.ToList().FindLast(m => m.IDModule == p.IDModule).IDModuleType).FirstOrDefault().InterneNaam, DoeInschrijving, DoeUpdate)));
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inout"></param>
        /// <param name="action"></param>
        /// <param name="oppositeAction"></param>
        /// <param name="handler"></param>
        /// <param name="oppositeHandler"></param>
        public void AddtoExecuteOnSave(bool inout, string action, string oppositeAction, Action handler, Action oppositeHandler)
        {

            if (inout)
            {
                if (!ExecuteOnSave.ContainsKey(action))
                {
                    if (ExecuteOnSave.ContainsKey(oppositeAction))
                    {
                        ExecuteOnSave.Remove(oppositeAction);
                    }
                    else
                    {
                        ExecuteOnSave.Add(action, handler);
                    }
                }
            }
            else
            {
                if (!ExecuteOnSave.ContainsKey(oppositeAction))
                {
                    if (ExecuteOnSave.ContainsKey(action))
                    {
                        ExecuteOnSave.Remove(action);
                    }
                    else
                    {
                        ExecuteOnSave.Add(oppositeAction, oppositeHandler);
                    }
                }
            }

        }






    }
}
