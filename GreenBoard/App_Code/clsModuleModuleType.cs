using StudentApplication.Model;
using StudentApplication.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsModuleModuleType
/// </summary>
public class clsModuleModuleType
{
    private string officieleNaam;

    public string OfficieleNaam
    {
        get { return officieleNaam; }
        set { officieleNaam = value; }
    }
    private DateTime startdatum;

    public DateTime StartDatum
    {
        get { return startdatum; }
        set { startdatum = value; }
    }
    private DateTime einddatum;

    public DateTime EindDatum
    {
        get { return einddatum; }
        set { einddatum = value; }
    }

    private int iDModule;

    public int IDModule
    {
        get { return iDModule; }
        set { iDModule = value; }
    }
    private int iDModuleType;

    public int IDModuleType
    {
        get { return iDModuleType; }
        set { iDModuleType = value; }
    }
    private string datatextfield;

    public string Datatextfield
    {
        get
        {
            return officieleNaam + " - Startdatum:  " + StartDatum.ToShortDateString();
        }
        set { datatextfield = value; }
    }
}