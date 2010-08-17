Facebook#
=========
Facebook Graph API for .Net
This library is a port from the original Facebook Android SDK written in Java with more features added.

## Usage
* Reference FacebookSharp.Core
* add using FacebookSharp:

	Facebook fb = new Facebook();

	Facebook fb = new Facebook("access_token"); // If you want to specify AccessToken then

	var user = facebook.Get&lt;User>("/me");

	Console.WriteLine(user.Name);
	
For more easy access FacebookSharp.Extensions has also been created.

	using FacebookSharp.Extensions;
	string profilePictureUrl = facebook.GetMyProfilePictureUrl();
	facebook.PostComment("id","some comment message");
	facebook.DeleteObject("id");
	facebook.PutLike("id");
	facebook.PostToWall("message",null);

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
	
		FacebookAuthenticationResult fbAuthResult = FacebookAuthenticationResult.Parse(
                HttpContext.Current.Request.Url.ToString(), FacebookContext.FacebookContext.Settings);

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

for website authentication, facebook returns the ?code=some_code 
FacebookAuthenticationResult is smart enough to recognize it and return you the access token transparently in the background.
You don't have to create any request or ask for anything.

Incase you are using FacebookAuthenticationResult.Parse method inside IFrame canvas, you need to enable Canvas Session Parameter and OAuth 2.0 for Canvas (beta) in Migration Tab in application settings. Incase the validation  of signed_request fails, InvalidSignedRequest exception will be thrown. Incase you want to manually extract the information from signed_request you can call FacebookAuthenticationResult.ValidateSignedRequest(string signedRequest, string applicationSecret, out IDictionary<string, object> jsonObject). signedRequest parameter is the same one you get from facebook.

Incase you are developing desktop applications. You can also use the same concept. But for easy access, a Facebook Login Dialog has been created.
Due to the nature of Facebook Authentication, you will need to provide only ApplicationKey for Desktop applications, but for web application you will need to provide PostAuthorizeUrl,ApplicationKey and ApplicationSecret.

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

You can specify extened permissions by specifying it in the FacebookSettings.

	fbSettings.DefaultApplicationPermissions = new[] { "publish_stream","create_event" } };

Please refer to [http://developers.facebook.com/docs/authentication/permissions](http://developers.facebook.com/docs/authentication/permissions) for more information on extended permissions.

#### IFacebookMembershipProvider

To have easy link between your MembershipProvider and FacebookMembershipProvider IFacebookMembershipProvider have been provided. This interface contains methods such as 

	bool HasLinkedFacebook(string membershipUsername);
	void LinkFacebook(string membershipUsername, string facebookId, string accessToken);
	void UnlinkFacebook(string membershipUsername);
	string GetFacebookAccessToken(string membershipUsername);

SqlServer, SQLite and MySql implementation has been provided:
Table structure for SqlFacebookMembershipProvider

	CREATE TABLE [FacebookUsers](
		[Username] VARCHAR(60), -- membershipUsername
  		[FacebookId] VARCHAR(50) NOT NULL UNIQUE,
		[AccessToken] VARCHAR(256),
		PRIMARY KEY ([Username])  
	);
	
#### Converting Json strings to real exceptions

You can easily convert Json strings to Facebook Exceptions in C#. If the json string doesn't contain exception it returns null instead.

string jsonString = "some fb json string error goes here.";
var ex = (FacebookException)jsonString;

It simple as explicitly casting a string to FacebookException.

#### Post-Authorize and Post-Remove Callbacks

These callbacks can be automatically processed and verified as well. For canvas users that have OAuth 2.0 Beta enabled in their application migrations, you can simply do the following:
      
        string appSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];
        IDictionary<string, object> data;
        bool success = FacebookAuthenticationResult.ValidateSignedRequest(Request.Form.ToString(),appSecret,out data);
        // data will contain keys: user_id, oauth_token, expires
        // you can then use data["oauth_token"].ToString() in a new Facebook object

For all other users (without OAuth 2.0 Beta enabled in application migrations) you may do the following:

        // in your post-remove page
        string appSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];
        PostRemoveCallback remove = FacebookPostCallback.Parse(Request.Form.ToString(),appSecret);
        if (remove != null)
        {
          // do something
          // remove will have all the properties you expect to receive
        }
        
        // in your post-authorize page
        string appSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];
        PostAuthorizeCallback auth = FacebookPostCallback.Parse(Request.Form.ToString(),appSecret);
        if (auth != null)
        {
          // do something
          // auth will have all the properties you expect to receive
        }

You may also choose to validate the signature of the POST callback manually by using FacebookPostCallback.ValidateSignature

### Some other useful extensions

Incase you want to add extra parameters to IDictionary<string,string> you will basically need to use the Add method.

var p = new Dictionary<string,string>();
p.Add("limit",10);
p.Add("offset",5");

Instead of doing that there are extensions methods which is in FacebookSharp.Extensions namespace. So now you will just need to call them.
var p = new Dictionary<string,string>().LimitTo(10).Offset(5);

Facebook# also contains more selections as described http://developers.facebook.com/docs/api#selection

In order to make a request to facebok such as https://graph.facebook.com/?fields=id,picture&ids=123741737666932,100001241534829&oauth_token=your_oauth_token. You could easily do so using FacebookSharp.Extensions.

var result = fb.Get(
					string.Empty,
                    new Dictionary<string, string>()
						.SelectFields(new[] { "picture" })
						.SelectIds(new[] { "123741737666932", "100001241534829" })
						.SelectField("id"));

You can either use the array by calling SelectFields or SelectIds or can pass single value using the SelectField or SelectId. Facebook# is smart enough to append if the fields already exists.


#### Date and Time Helpers

Facebook makes heavy use of ISO-8601 formatted date. In order to convert the .NET DateTime object to facebook recognizable ISO-8601 formatted date, a Helper method can be called.

string iso8601FromattedDate = FacebookUtils.Date.ToIso8601FormattedDateTime(DateTime.UtcNow);

In case you are using FacebookSharp.Extensions. You can also call the extension method ToIso8601FormattedDateTime();

string iso8601FromattedDate = DateTime.UtcNow.ToIso8601FormattedDateTime();

To convert .NET date time object to ISO-8601 formatted date time string, call the method FromIso8601FormattedDateTime.

DateTime dt = FacebookUtils.Date.FromIso8601FormattedDateTime("2010-08-06T05:07:25.9883130Z");

Or incase you are using extensions methods

DateTime dt = "2010-08-06T05:07:25.9883130Z".FromIso8601FormattedDateTime();

##Supported Platforms

### .NET 3.5 and .NET 4.0
Supported. (Client profile supported too.)

Contains MVC Action filters for easing the development.

### Silverlight
Comming soon

### Windows Phone
Comming soon

## Download the latest binaries

You can download the latest binaries at [http://github.com/prabirshrestha/FacebookSharp/downloads](http://github.com/prabirshrestha/FacebookSharp/downloads)

## License

Facebook# is intended to be used in both open-source and commercial environments. It is licensed under New BSD (3-clause") license. (This license doesn't apply for Newtonsoft.Json). Please review LICENSE.txt for more details.

[Prabir Shrestha](http://www.prabir.me)

Follow me on twitter [@prabirshrestha](http://www.twitter.com/prabirshrestha)