﻿<%@ Page language="c#" Codebehind="Options.aspx.cs" AutoEventWireup="True" Inherits="WatchersNET.CKEditor.Options" %>
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta name="language" content="en" />
    <title>CKEditor Options</title>
    <asp:placeholder id="favicon" runat="server"></asp:placeholder>
    <script type="text/javascript">
        var saveGroupName = '<%= DotNetNuke.Services.Localization.Localization.GetString("SaveGroupName.Text", this.ResXFile, this.LangCode) %>';
        var deleteGroup = '<%= DotNetNuke.Services.Localization.Localization.GetString("DeleteGroup.Text", this.ResXFile, this.LangCode) %>';
        var editGroupName = '<%= DotNetNuke.Services.Localization.Localization.GetString("EditGroupName.Text", this.ResXFile, this.LangCode) %>';
        var newGroupName = '<%= DotNetNuke.Services.Localization.Localization.GetString("NewGroupName.Text", this.ResXFile, this.LangCode) %>';
        var deleteToolbar = '<%= DotNetNuke.Services.Localization.Localization.GetString("DeleteToolbarButton.Text", this.ResXFile, this.LangCode) %>';
        var newRowName = '<%= DotNetNuke.Services.Localization.Localization.GetString("RowBreak.Text", this.ResXFile, this.LangCode) %>';
	</script>
  </head>
  <body>
    <form id="ckOptionsForm" runat="server">
      <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
	  <asp:PlaceHolder id="phControls" runat="server"></asp:PlaceHolder>
	</form>
  </body>
</html>
