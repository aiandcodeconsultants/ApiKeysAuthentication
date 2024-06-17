# ApiKeysAuthentication

This is a simple .NET 8.0 minimal API project with a custom API Keys authentication handler.

See the files in the ApiKeysAuthentication.Api\Authentication folder for the implementation.

## Usage

* Install .NET 8.0 SDK
* Clone project with `git clone https://github.com/aiandcodeconsultants/ApiKeysAuthentication.git`
* Change to API folder e.g. `cd ApiKeysAuthentications\ApiKeysAuthentication.Api`
* Run the project `dotnet run`
* The startup logging will inform you where it is listening, such as `http://localhost:5210` so open `/swagger` within this,
e.g. `http://localhost:5210/swagger`
* Try accessing the `/weatherforecast` endpoint and you should get a 401 response
* Click `Authorize` and enter `T35t` as the API Key, Login, Close popup and Retry and you should get a weather response