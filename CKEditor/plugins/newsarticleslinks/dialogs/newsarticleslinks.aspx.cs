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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Tabs;
    using DotNetNuke.Services.Localization;

    using Ventrian.NewsArticles;

    /// <summary>
    /// The News Article Links Selector Page
    /// </summary>
    public partial class NewsArticlesLinks : Page
    {
        /// <summary>
        ///   Gets Current Language from Url
        /// </summary>
        protected string LangCode => CultureInfo.CurrentCulture.Name;

        /// <summary>
        ///   Gets the Name for the Current Resource file name
        /// </summary>
        protected string ResXFile => this.ResolveUrl(
            string.Format(
                "~/Providers/HtmlEditorProviders/CKEditor/{0}/Options.aspx.resx",
                Localization.LocalResourceDirectory));

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.FillModuleList();
            }

            ArticlesList.Items.Clear();

            var articleController = new ArticleController();

            var newsArticlesModuleId = 0;
            var newsArcticlesTabId = 0;

            if (ModuleListDropDown.Items.Count > 0)
            {
                var values = ModuleListDropDown.SelectedValue.Split(Convert.ToChar("-"));

                if (values.Length == 2)
                {
                    newsArcticlesTabId = Convert.ToInt32(values[0]);
                    newsArticlesModuleId = Convert.ToInt32(values[1]);
                }
            }

            var articleList = articleController.GetArticleList(newsArticlesModuleId, true);

            foreach (var article in articleList)
            {
                var articleUrl = Globals.NavigateURL(
                    newsArcticlesTabId,
                    string.Empty,
                    string.Format("articletype=ArticleView&articleId={0}", article.ArticleID));

                ArticlesList.Items.Add(new ListItem { Text = article.Title, Value = articleUrl });
            }
        }

        /// <summary>
        /// Fills the module list.
        /// </summary>
        private void FillModuleList()
        {
            PortalSettings portalSettings = PortalController.Instance.GetCurrentPortalSettings();

            List<TabInfo> objTabs = TabController.GetPortalTabs(portalSettings.PortalId, -1, true, true);

            var objTabController = new TabController();

            var objDesktopModuleInfo = DesktopModuleController.GetDesktopModuleByModuleName(
                "DnnForge - NewsArticles",
                portalSettings.PortalId);

            if (objDesktopModuleInfo == null)
            {
                return;
            }

            foreach (var objTab in objTabs.Where(tab => !tab.IsDeleted))
            {
                var objModules = new ModuleController();

                foreach (KeyValuePair<int, ModuleInfo> pair in objModules.GetTabModules(objTab.TabID))
                {
                    ModuleInfo objModule = pair.Value;

                    if (objModule.IsDeleted)
                    {
                        continue;
                    }

                    if (objModule.DesktopModuleID != objDesktopModuleInfo.DesktopModuleID)
                    {
                        continue;
                    }

                    string strPath = objTab.TabName;
                    TabInfo objTabSelected = objTab;

                    while (objTabSelected.ParentId != Null.NullInteger)
                    {
                        objTabSelected = objTabController.GetTab(objTabSelected.ParentId, objTab.PortalID, false);
                        if (objTabSelected == null)
                        {
                            break;
                        }

                        strPath = string.Format("{0} -> {1}", objTabSelected.TabName, strPath);
                    }

                    var objListItem = new ListItem
                                          {
                                              Value =
                                                  string.Format(
                                                      "{0}-{1}",
                                                      objModule.TabID,
                                                      objModule.ModuleID),
                                              Text = string.Format(
                                                  "{2}: {0} -> {3}: {1}",
                                                  strPath,
                                                  objModule.ModuleTitle,
                                                  Localization.GetString(
                                                      "Page.Text",
                                                      this.ResXFile,
                                                      this.LangCode),
                                                  Localization.GetString(
                                                      "ModuleInstance.Text",
                                                      this.ResXFile,
                                                      this.LangCode))
                                          };

                    ModuleListDropDown.Items.Add(objListItem);
                }
            }
        }
    }
}