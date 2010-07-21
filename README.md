Facebook#
=========
Facebook Graph API for .Net
This library is a port from the original Facebook Android SDK written in Java with more features added.

## Usage
* Reference FacebookSharp.Core
* add using FacebookSharp;

	var Facebook = new Facebook();
	// If you want to specify AccessToken then 
	var Facebook = new Facebook("access_token");
	var user = facebook.Request&lt;User>("me");
	Console.WriteLine(user.Name);
	
For more easy access FacebookSharp.Extensions has also been created.

	using FacebookSharp.Extensions;
	string profilePictureUrl = facebook.GetMyProfilePictureUrl();
	facebook.PutComment("id","some comment message");
	facebook.DeleteObject("id");
	facebook.PutLike("id");
	facebook.PutWallPost("message",null);

#### Getting Facebook Access Token
Please review the samples found in the source control to get Facebook Access Token.
Facebook# already contains a windows form dialog, for easy login to facebook and retriving access token.

Same api for retriving the access token is used for both windows and web applications.
For web applications:
In the post-authorize page.

protected void Page_Load(object sender, EventArgs e)
{
	FacebookSettings fbSettings  = new FacebookSettings();
	fbSettings.PostAuthorizeUrl  = "http://localhost:16443/FacebookSharp.Samples.Website/FacebookAuthorize.aspx"; // change this to your appropriate post authorize url
	fbSettings.ApplicationKey 	 = "application_key";
	fbSettings.ApplicationSecret = "application_secret";
	
	FacebookAuthenticationResult fbAuthResult = new FacebookAuthenticationResult(HttpContext.Current.Request.Url.ToString(),fbSettings);
	if (fbAuthResult.IsSuccess)
	{
		string accessToken = fbAuthResult.AccessToken;
		int expiresIn = fbAuthResult.ExpiresIn;
		// save this access token for future reference ....
		// then do your application logic: might be redirect?
	}
	else
	{
		string errorReason = fbAuthResult.ErrorReasonText;
		// you can also display the error reason text,
		// or mite be tell the user that you must allow access to facebook
		// before using this app ....
	}
}

for website authentication, facebook return the ?code=some_code 
FacebookAuthenticationResult is smart enough to recognize it and return you the access token transparently in the background.
You don't have to create any request or ask for anything.

Incase you are developging desktop applications. You can also do the same concept. But for easy access, a Facebook Login Dialog has been created.
Due to the nature of Facebook Authentication, you will need to provide only ApplicationKey for Desktop applications, but for web application you will need to provide PostAuthorizeUrl,ApplicationKey, ApplicationSecret.
FacebookSettings fbSettings = new FacebookSettings { ApplicationKey = "your application key" };
FacebookLoginForm fbLoginDlg = new FacebookLoginForm(fbSettings);
FacebookAuthenticationResult fbAuthResult;

if (fbLoginDlg.ShowDialog() == DialogResult.OK)
{
	MessageBox.Show("You are logged in.");
	fbAuthResult = fbLoginDlg.FacebookAuthenticationResult;
	txtAccessToken.Text = fbAuthResult.AccessToken;
	txtExpiresIn.Text = fbAuthResult.ExpiresIn.ToString();
}
else
{
	MessageBox.Show("You must login inorder to access Facebook features.");
	fbAuthResult = fbLoginDlg.FacebookAuthenticationResult;
	MessageBox.Show(fbAuthResult.ErrorReasonText);
}

You can specify extened permissions by specifify it in the FacebookSettings.
fbSettings.DefaultApplicationPermissions = new[] { "publish_stream","create_event" } };
Please refer to http://developers.facebook.com/docs/authentication/permissions for more information on extended permissions.

#### IFacebookMembershipProvider
// To have easy link between your MembershipProvider and FacebookMembershipProvider IFacebookMembershipProvider has been created. This interface contains methods such as 

bool HasLinkedFacebook(string membershipUsername);
void LinkFacebook(string membershipUsername, string facebookId, string accessToken);
void UnlinkFacebook(string membershipUsername);
string GetFacebookAccessToken(string membershipUsername);

// SqlServer, SQLite and MySql implementation has been provided:
// Table structure for SqlFacebookMembershipProvider

CREATE TABLE [FacebookUsers](

  [Username] VARCHAR(60), -- membershipUsername
  
  [FacebookId] VARCHAR(50) NOT NULL UNIQUE,
  
  [AccessToken] VARCHAR(256),
  
  PRIMARY KEY ([Username])
  
);

More providers comming soon.

##Supported Platforms

### .NET 3.5 and .NET 4.0
Supported. (Client profile supported too.)

### Silverlight
Comming soon

### Windows Phone
Comming soon

## Future releases
Plan for deep integration with ASP.NET and ASP.NET MVC with action filters and controllers.

## Download the latest binaries

You can download the latest binaries at [http://github.com/prabirshrestha/FacebookSharp/downloads](http://github.com/prabirshrestha/FacebookSharp/downloads)

## License

Facebook# is intended to be used in both open-source and commercial environments. It is licensed under New BSD (3-clause") license. (This license doesn't apply for Newtonsoft.Json). Please review LICENSE.txt for more details.

[Prabir Shrestha](http://www.prabir.me)

Follow me on twitter [@prabirshrestha](http://www.twitter.com/prabirshrestha)