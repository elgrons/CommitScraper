<h1 align="center"> Commit Scraper</h1>

<h4 align="center">By Eliot Gronstal & Stephen Zook 6.14.23</h4>

<h4 align="center"> A GitHub App created to scrape repositories via webhook for commit messages and push them to an associated Client Portal to be summarized by OpenAI for sprint cycle status updates. This GitHub App is hosted via Azure and requires the repository owner's approval for installation to begin listening.</h5>

## Technologies Used

* _C#_
* _.NET Core 6.0_
* _Microsoft Azure_
* _GitHub_

## Setup and Installation

_Requires console application such as Git Bash, Terminal, or PowerShell_

1. Open Git Bash or PowerShell if using Windows and Terminal if using Mac

2. Run the command

   `git clone https://github.com/elgrons/GratShiftSaveApi.Solution`

3. Run the command and open the project file.

   `cd GratShiftSaveApi.Solution`

4. Create a `.gitignore` file in the root folder. This project will require use of sensitive information that should not be publicly shared.

5. You should now have a folder `GratShiftSaveApi` with the following structure.
    <pre>GratShiftSaveApi.Solution
    ├── .gitignore 
    ├── ... 
    └── GratShiftSaveApi
        ├── Controllers
        ├── Models
        ├── ...
        ├── README.md</pre>

6. Create a `.gitignore` file in the root folder. This project will require use of sensitive information that should not be publicly shared.

7. Copy and paste the following information into the .gitignore file:
```text
obj
bin
appsettings.json
serviceaccountkey.json
```

8. Create two files in the following location, inside the GratShiftSaveApi folder:

  * One named `appsettings.json`
  * Another named `serviceaccountkey.json`

<pre>GratShiftSaveApi.Solution
├── .gitignore 
├── ... 
└── GratShiftSaveApi
    ├── Controllers
    ├── Models
    ├── ...
    └── <strong>serviceaccountkey.json</strong>
    └── <strong>appsettings.json</strong></pre>

9. Copy and paste the text below into serviceaccountkey.json. 

```json
{
  "type": "service_account",
  "project_id": "[YOUR-PROJECT-ID]",
  "private_key_id": "[YOUR-PRIVATE-KEY-ID]",
  "private_key": "[YOUR-PRIVATE-KEY]",
  "client_email": "[YOUR-CLIENT-EMAIL]",
  "client_id": "[YOUR-CLIENT-ID]",
  "auth_uri": "[YOUR-AUTH-URI]",
  "token_uri": "[YOUR-TOKEN-URI]",
  "auth_provider_x509_cert_url": "[YOUR-AUTH-PROVIDER-CERT-URL]",
  "client_x509_cert_url": "[YOUR-CLIENT-CERT-URL]",
  "universe_domain": "googleapis.com"
}
```

11. Copy and paste below JSON text in appsettings.json.

```json
{
   "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
   "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=gratshiftsave_api;uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
  },
  "JWT": {
    "ValidAudience": "http://localhost:4200",
    "ValidIssuer": "http://localhost:5000",
    "Secret": "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"
  },
    "Firestore": {
        "ProjectId": "[YOUR-PROJECT-ID]"
    }
}
```

Run the following command in the console

`dotnet build`

Then run the following command in the console

`dotnet run`

This program was built using _`Microsoft .NET SDK 6.0`_, and may not be compatible with other versions. The performance of this app with other versions is not insinuated nor assured.

## API Documentation

Explore the API endpoints in Postman or in a browser. You will not be able to utilize authentication in a browser so using Postman is recommended.

### Swagger Documentation

To view the Swagger documentation for the GratShiftSaveApi, launch the project using `dotnet run` using Terminal or Powershell, then input the following URL into your browser: `https://localhost:5001/swagger/index.html`

![swaggerendpoints](GratShiftSaveApi/wwwroot/img/SwaggerEndpoints.png)
## Example Query

```
https://localhost:5001/api/GratShift/1
```

or using the hosted domain

```
https://grat-shift-save-api.azurewebsites.net/api/GratShift/1
```

## Sample JSON Response

For an instance of creating a GratShift entry:
```
{
  "gratShiftId": 1,
  "cashTip": 80,
  "creditTip": 300,
  "shiftSales": 2200,
  "shiftDate": "2023-05-01T00:00:00"
}
```
## Known Bugs

- _Reach out with any questions or concerns to [eliot.lauren@gmail.com](eliot.lauren@gmail.com)_

## License

[MIT](/LICENSE)

Copyright 2023 Eliot Gronstal, Stephen Zook
