<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FacebookSharp.IFacebookContext>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FacebookAuthorize</title>
    <script type="text/javascript">
            <%= Model.FacebookContext.Settings.FacebookCanvasLoginJavascript %>
    </script>
</head>
<body>
    <div>
    
    </div>
</body>
</html>
