namespace FacebookSharp.Web.JavascriptSdk
{
    using System.Text;

    public partial class FacebookJavscriptSdk
    {
        public string Logout()
        {
            return "Fb.logout(function(cb){});";
        }

        public string Logout(string callbackFunctionName)
        {
            return string.Format("FB.logout({0});", callbackFunctionName);
        }

        public string Logout(string callBackFunctionVarName, string body)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Fb.logout(function({0}{", callBackFunctionVarName);
            sb.AppendLine();
            sb.AppendLine(body);
            sb.AppendLine("});");

            return sb.ToString();
        }
    }
}