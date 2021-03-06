﻿/*
 * CKEditor Html Editor Provider for DNN
 * ========
 * https://github.com/w8tcha/dnnckeditor
 * Copyright (C) Ingo Herbote
 *
 * The software, this file and its contents are subject to the CKEditor Provider
 * License. Please read the license.txt file before using, installing, copying,
 * modifying or distribute this file or part of its contents. The contents of
 * this file is part of the Source Code of the CKEditor Provider.
 */

namespace WatchersNET.CKEditor
{
    #region

    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Tabs;

    #endregion

    /// <summary>
    /// Renders the Tab Java Script
    /// </summary>
    public class Tabs : PortalModuleBase, IHttpHandler
    {
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable => false;

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            var portalId = context.Request.QueryString["PortalID"] != null
                               ? int.Parse(context.Request.QueryString["PortalID"])
                               : this.PortalSettings.PortalId;

            // Generate Pages Array
            var pagesArray = new StringBuilder();

            pagesArray.Append("var dnnpagesSelectBox = new Array(");

            var domainName = string.Format("http://{0}", Globals.GetDomainName(context.Request, true));

            foreach (var tab in TabController.GetPortalTabs(portalId, -1, false, null, true, false, true, true, true))
            {
                var tabUrl =
                    PortalController.GetPortalSettingAsBoolean("ContentLocalizationEnabled", portalId, false)
                    && !string.IsNullOrEmpty(tab.CultureCode)
                        ? Globals.FriendlyUrl(
                            tab,
                            string.Format("{0}&language={1}", Globals.ApplicationURL(tab.TabID), tab.CultureCode))
                        : Globals.FriendlyUrl(tab, Globals.ApplicationURL(tab.TabID));

                tabUrl = Globals.ResolveUrl(Regex.Replace(tabUrl, domainName, "~", RegexOptions.IgnoreCase));

                var tabName = Microsoft.JScript.GlobalObject.escape(tab.TabName);

                if (tab.Level.Equals(0))
                {
                    pagesArray.AppendFormat("new Array('| {0}','{1}'),", tabName, tabUrl);
                }
                else
                {
                    var separator = new StringBuilder();

                    for (var index = 0; index < tab.Level; index++)
                    {
                        separator.Append("--");
                    }

                    pagesArray.AppendFormat("new Array('|{0} {1}','{2}'),", separator, tabName, tabUrl);
                }
            }

            if (pagesArray.ToString().EndsWith(","))
            {
                pagesArray.Remove(pagesArray.Length - 1, 1);
            }

            pagesArray.Append(");");

            context.Response.ContentType = "text/javascript";
            context.Response.Write(pagesArray.ToString());
        }
    }
}