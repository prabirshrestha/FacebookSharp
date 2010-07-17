Facebook#
=========
Facebook Graph API for .Net
This library is a port from the original Facebook Android SDK written in Java with more features added.

## Usage
* Reference FacebookSharp.Core and add using FacebookSharp;

var Facebook = new Facebook();

If you want to specify AccessToken then 
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

#### IFacebookMembershipProvider
To have easy link between your MembershipProvider and FacebookMembershipProvider IFacebookMembershipProvider has been created. This interface contains methods such as 

bool HasLinkedFacebook(string membershipUsername);
void LinkFacebook(string membershipUsername, string facebookId, string accessToken);
void UnlinkFacebook(string membershipUsername);
string GetFacebookAccessToken(string membershipUsername);

MySql implementation has been provided:
Table structure for MySqlFacebookMembershipProvider

CREATE TABLE `facebook_users` (
  `user_name` VARCHAR(60), -- membershipUsername, primary key already enforced as unique and not null
  `facebook_id` VARCHAR(50) NOT NULL UNIQUE,
  `access_token` VARCHAR(256),
  PRIMARY KEY (`user_name`)
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