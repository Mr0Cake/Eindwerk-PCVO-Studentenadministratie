using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using StudentApplication.Model;
using StudentApplication.Model.Helpers;
using BLL;

/// <summary>
/// Summary description for clsModuleByOpleiding
/// </summary>
public class clsModuleByOpleiding
{
    private string officieleNaam;

    public string OfficieleNaam
    {
        get { return officieleNaam; }
        set { officieleNaam = value; }
    }

    private int aantalUren;

    public int TotaalAantalUren
    {
        get { return aantalUren; }
        set { aantalUren = value; }
    }
    private string voornaam;

    public string Voornaam
    {
        get { return voornaam; }
        set { voornaam = value; }
    }
    private string naam;

    public string Naam
    {
        get { return naam; }
        set { naam = value; }
    }
    private int idModule;

    public int IDModule
    {
        get { return idModule; }
        set { idModule = value; }
    }
    private DateTime startDatum;

    public DateTime StartDatum
    {
        get { return startDatum; }
        set { startDatum = value; }
    }

    private int iDInschrijving;

    public int IDInschrijving
    {
        get { return iDInschrijving; }
        set { iDInschrijving = value; }
    }

    private bool ingeschreven;

    public bool Ingeschreven
    {
        get
        {
            ObservableCollection<clsInschrijving> insch =
                clsBLLHelper.CustomBLL.GetInschrijvingByModuleIDGebruikerID<clsInschrijving>(IDModule, Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            if (insch.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        set { ingeschreven = value; }
    }

    private ObservableCollection<clsGebruiker> leraren;

    public ObservableCollection<clsGebruiker> Leraar
    {
        get
        {

            return leraren = leraren ?? clsBLLHelper.CustomBLL.GetLerarenByModuleID<clsGebruiker>(IDModule);
        }
        set { leraren = value; }
    }


}