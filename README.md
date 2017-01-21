Gustnado is a C# wrapper for the SoundCloud API attempting to make its consumption simple by hiding its idiosyncrasies, while keeping its general structure.

##Usage
```
var client = new SoundCloudHttpClient(clientId, clientSecret, new GustnadoRestClient());
var me = SoundCloudApi.Users[165178579].Get().Execute(client);
//or alternatively
client.Authenticate(username, password);
me = SoundCloudApi.Me.Get().Execute(client);//also me
```
