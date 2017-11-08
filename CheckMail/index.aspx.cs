// automatic header
// <copyright file="index.aspx.cs" company="selligent">
//     Selligent
// </copyright>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ContentRenderer.DataAccess;
using ContentRenderer.General;
using ContentRenderer.Settings;
using ContentRenderer.SimSpecific;
using Routing;

/// <summary>
/// ContentRenderer for Contests Notice that the methods starting with "this" are methods derived
/// from the base class SIM-page. This is done to Keep a clear overview between methods derived from
/// its base class and methods of this index itself.
/// </summary>
public partial class Index : SimPage
{
    #region initializers

    /// <summary>
    /// Page load event handler
    /// </summary>
    /// <param name="sender">the sending object</param>
    /// <param name="e">the event arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // **************************************************************************************************************************************
        // modifications can be made here (settings, simRequest, simPageManager) (defaults loaded in
        // SimPage constructor) ***********************
        // ************************************************************************************************************************************** examples:
        // - direct entry (adapt simrequest id): this.simRequest.RequestId =
        //   this.simPageManager.generator.generateRequestId(0, 0, 0, 0, 0);
        // - change settings: this.settings.metrics.Adobe.taggingLocation = "new location";
        // - ... **************************************************************************************************************************************

        // check for application errors and show box
        if (HttpContext.Current.Application["serverError"] != null)
        {
            Body.Controls.AddAt(0, BuildErrorPopUp());
        }

        // check for first page load
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(this.SimRequest.RequestId))
            {
                this.SimRequest.RequestId = this.SimPageManager.Generator.GenerateDefaultRequestIdForUser(0);
            }
        }

        // Without setting the friendlyurls, the templates wont work.
        SetFriendlyUrls();

        // add media path to simrequest for default images from template
        this.SimRequest.AddAttribute("REQUEST.MEDIAPATH_LANDING", ConfigurationManager.AppSettings["MediaPathLanding"]);

        SimResult simResult = this.GetSimResult;                          // get SIM data (befor this the request can be manipulated by adapted this.simRequest

        this.headContent.Controls.Add(new LiteralControl(simResult.IncHead));    // handle SIM data
        this.BodyContent.Controls.Add(new LiteralControl(simResult.IncBody));

        this.SetLanguageControl(this.DesktopLanguages, this.languages, simResult);               // set the usercontrols where the languages need to located
        this.SetDynamicContent();                                                // set translated data (meta-tags, legals, ...) (from resources)

        this.SetMetricsData();                                                   // set metrics (Adobe)
        this.SetMetrics(this.Body, simResult.MessageTag);                        // Set metrics on index

        ////Load Css, Js
        DateTime? cacheDate = new Nullable<DateTime>();
        if (IsProduction)
        {
            cacheDate = DataAccess.GetConfigAssetsCaching(Defaults.AssetsCachingCampaign);
        }
        this.SetCssFiles(cacheDate);
        this.SetJsFiles(cacheDate);

        //// TODO_SELLIGENT: check if these are still relevant
        ////if (!String.IsNullOrEmpty(Request["MAIL"])) { CheckIngEmployee(Request["MAIL"]); } else { CheckIngEmployee(); }

        ////if (ShowDebug) {
        ////    strDebug = mWebResult.GetAttribute("*");
        ////    Response.Write(strDebug);
        ////}
    }

    #endregion initializers

    #region helpers

    /// <summary>
    /// Generates a general error box
    /// </summary>
    /// <returns>Error box control</returns>
    private static Control BuildErrorPopUp()
    {
        using (HtmlGenericControl overlay = new HtmlGenericControl("div"))
        {
            overlay.Attributes.Add("class", "error-notification");
            overlay.Controls.Add(new LiteralControl("<p>An Error Occured: Please contact the ing.</p>"));

            return overlay;
        }
    }

    /// <summary>
    /// Sets friendly links
    /// </summary>
    private static void SetFriendlyUrls()
    {
        FriendlyUrl.ClearData();
        FriendlyUrl.AddData(1, "LANDING", "Landing");
        FriendlyUrl.AddData(2, "KEUZE", "Keuze");
    }
    #region Metrics

    /// <summary>
    /// Sets metrics data
    /// </summary>
    private void SetMetricsData()
    {
        // **************************************************************************************************************************************
        // metrics data can be set here *********************************************************************************************************
        // ************************************************************************************************************************************** examples:
        // - this.settings.metrics.Adobe.addData("LANDING", "pageName", "testpagina");
        // - this.settings.metrics.Adobe.addData("THANX", "eerste property");
        // - ... ! specify an empty string as the first argument (message-tag) to add a global metric
        // 
        // Set trackOrientationChange and trackResize to enable this tracking

        // Enable metrics
        this.Settings.Metrics.Adobe.Enabled = true;

        // set metrics data
        this.Settings.Metrics.Adobe.AddData(string.Empty, "prop11", "global property");
        this.Settings.Metrics.Adobe.AddData("LANDING", "pageName", "testpagina");
        this.Settings.Metrics.Adobe.AddData("thanx", "pageName", "testpagina");
        this.Settings.Metrics.Adobe.AddData("THANX", "prop1", "eerste property");

        // enable tracking for orientationChanges and Resize
        this.Settings.Metrics.Adobe.TrackOrientationChange = true;
        this.Settings.Metrics.Adobe.TrackResize = true;
    }

    #endregion Metrics

    #region general

    /// <summary>
    /// Sets the dynamic content
    /// </summary>
    private void SetDynamicContent()
    {
        this.headContent.Controls.Add(new LiteralControl("<link rel=\"shortcut icon\" href=\"" + Defaults.MediaPath + "images/favicon.ico\" type=\"image/vnd.microsoft.icon\" />"));
        this.SetPageData();
        this.SetMetaData();
        this.SetLegals();
        this.SetSocials();
    }

    /// <summary>
    /// Set social content footer
    /// </summary>
    private void SetSocials()
    {
        if (Request.Form["FOOTER_SOCIAL"] == "1")
        {
            this.SocialInner.InnerHtml =
                        this.Translator.GetTranslation("social_facebook") +
                        this.Translator.GetTranslation("social_twitter") +
                        this.Translator.GetTranslation("social_linkedin") +
                        "<div class='clear'></div>";
        }
    }

    /// <summary>
    /// Sets legal links
    /// </summary>
    private void SetLegalLinks()
    {
        string legalsSep = this.Translator.GetTranslation("footer_sep");

        this.LegalMentionsInnerOne.InnerHtml = this.Translator.GetTranslation("footer_terms") + legalsSep +
                                   this.Translator.GetTranslation("footer_privacy") + legalsSep +
                                   this.Translator.GetTranslation("footer_security") + legalsSep +
                                   this.Translator.GetTranslation("footer_ing");

        this.LegalMentionsInnerTwo.InnerHtml = string.Format(CultureInfo.CurrentCulture, this.Translator.GetTranslation("footer_rights"), DateTime.Now.Year.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Sets legal mentions
    /// </summary>
    private void SetLegals()
    {
        List<string> legals = new List<string>();

        //// **************************************************************************************************************************************
        //// Legals are set here  *****************************************************************************************************************
        //// ************************************************************************************************************************************** 
        //// when adding legals MAKE SURE YOU ADD THESE LEGAL in the resources
        //// for the example below:
        //// - add legal_title
        //// - add legal_text
        //// **************************************************************************************************************************************
        legals.Add("legal");

        this.SetLegalDropdowns(this.LegalMentionsLandingMore, legals);
        this.SetLegalLinks();
    }

    /// <summary>
    /// Sets metadata
    /// </summary>
    private void SetMetaData()
    {
        using (HtmlMeta htmlMetaKeywords = new HtmlMeta())
        {
            using (HtmlMeta htmlMetaDesciption = new HtmlMeta())
            {
                htmlMetaKeywords.Name = "keywords";
                htmlMetaKeywords.Content = this.Translator.GetTranslation("meta_keywords");

                htmlMetaDesciption.Name = "description";
                htmlMetaDesciption.Content = this.Translator.GetTranslation("meta_description");

                this.Meta.Controls.Add(htmlMetaKeywords);
                this.Meta.Controls.Add(htmlMetaDesciption);
            }
        }
    }

    /// <summary>
    /// Sets page data
    /// </summary>
    private void SetPageData()
    {
        Page.Header.Title = this.Translator.GetTranslation("meta_title");
    }

    #endregion general

    #region css/js assetloader functions

    /// <summary>
    /// Sets CSS files
    /// </summary>
    /// <param name="cacheDate">a cache date</param>
    private void SetCssFiles(DateTime? cacheDate)
    {
        List<string> cssFiles = new List<string>();

        string mediaPathLanding = ConfigurationManager.AppSettings["MediaPathLanding"];

        if (string.Empty + Request.QueryString["_NORENDERER"] != "TRUE")
        {
            if (this.IsProduction)
            {
                // minified css needs to be generated yet
                cssFiles.Add(mediaPathLanding + "css/landingspage.min.css");
            }
            else
            {
                cssFiles.Add(mediaPathLanding + "css/general.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-footer-legals.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-footer-social.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-general.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-header.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-header-video.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-advantages.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-breadcrumb.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-faq.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-products.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-promo.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-shares.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-steps.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-testimonials.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-testimonials-video.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-top.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-top-form.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-nps-scores.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-shares.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-module-intro-text.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-sticky-header.css");
                cssFiles.Add(mediaPathLanding + "css/owl.carousel.css");
                cssFiles.Add(mediaPathLanding + "css/owl.theme.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-owl-carousel.css");
                cssFiles.Add(mediaPathLanding + "css/landingspage-retina.css");
            }

            this.AddCssFiles(this.CSS, cssFiles, cacheDate);
        }
    }

    /// <summary>
    /// Sets JS files
    /// </summary>
    /// <param name="cacheDate">a cache date</param>
    private void SetJsFiles(DateTime? cacheDate)
    {
        List<string> jsfiles = new List<string>();

        string mediaPathLanding = ConfigurationManager.AppSettings["MediaPathLanding"];

        if (string.Empty + Request.QueryString["_NORENDERER"] != "TRUE")
        {
            if (this.IsProduction)
            {
                jsfiles.Add(Defaults.MediaPath + "js/styleguide.min.js");
                jsfiles.Add(mediaPathLanding + "js/landingspage.min.js");

                /**********************************************************************************
                 * Example of a custom script
                 *
                 * jsFiles.Add("js/custom.min.js");
                 **********************************************************************************/
            }
            else
            {
                // Vendor scripts jsFiles.Add(defaults.mediaPath + "js/vendor/jquery/jquery.validate.1.14.min.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/jquery/jquery-2.1.4.min.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/jquery/jquery.validate.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/jquery/jquery-alphanumeric.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/jquery/jquery-datepick.pack.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/jquery/jquery-maskedinput-1.4.1.min.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/jquery/jquery.ata.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/jquery/jquery.ui.touch-punch.min.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/modernizr/modernizr.custom.js");
                jsfiles.Add(Defaults.MediaPath + "js/vendor/strictly-software/custom.encoder.js");

                // default scripts jsFiles.Add(defaults.mediaPath + "js/social-shares.js");
                jsfiles.Add(Defaults.MediaPath + "js/default.helpers.js");
                jsfiles.Add(Defaults.MediaPath + "js/default.jquery.customExtensions.js");
                jsfiles.Add(Defaults.MediaPath + "js/default.metrics.js");
                jsfiles.Add(Defaults.MediaPath + "js/default.validation.js");
                jsfiles.Add(Defaults.MediaPath + "js/default.init.js");

                // Landingspage JS files
                jsfiles.Add(mediaPathLanding + "js/circles.js");
                jsfiles.Add(mediaPathLanding + "js/owl.carousel.js");
                jsfiles.Add(mediaPathLanding + "js/landingspage-video-caroussel.js");
                jsfiles.Add(mediaPathLanding + "js/jquery-mobile-touch-1.4.5-min.js");

                /**********************************************************************************
                 * Example of a custom script
                 *
                 * jsFiles.Add("js/custom/jquery/extensions/default.jquery.customExtensions.js");
                 **********************************************************************************/
            }

            this.AddJavaScriptFiles(this.JS, jsfiles, cacheDate);
        }
    }

    #endregion css/js assetloader functions

    #endregion helpers
}