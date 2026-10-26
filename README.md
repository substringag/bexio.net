> :warning: **This is a work in progress**

# Bexio API

A simple REST API client in .NET 8.0 for [Bexio](https://www.bexio.com/) which is a powerful cloud based software for
business administrations.

Find official API documentation [here](https://docs.bexio.com/)

## Status of this implementation

As this is still a work in progress, only some API calls are implemented and tested.

### Main API Categories

* [x] **Contacts** - Complete implementation (contacts, salutations, titles, groups, relations, additional addresses)
* [x] **Invoices** - Complete implementation with CRUD operations, actions, and payment management
* [x] **Sales Order Management** - Partial implementation (quotes, orders, invoices, deliveries, documents)
* [x] **Projects & Time Tracking** - Complete implementation (projects, timesheets, business activities)
* [x] **User Management** - Complete implementation (users, fictional users)
* [ ] **Purchase** - Planned (bills, expenses, purchase orders, outgoing payments)
* [ ] **Accounting** - Planned (accounts, currencies, manual entries, taxes, VAT periods)
* [ ] **Banking** - Planned (bank accounts, IBAN payments, QR payments)
* [ ] **Items & Products** - Partial (single article fetch only, stock locations and areas missing)
* [ ] **Files** - Planned (file management and operations)

### Other Endpoints

* [ ] **Company Profile** - Company information endpoints
* [ ] **Countries** - Country management endpoints
* [ ] **Languages** - Language management endpoints
* [ ] **Notes** - Note management endpoints
* [ ] **Payment Types** - Payment type endpoints
* [ ] **Permissions** - Permission and access management
* [ ] **Tasks** - Task management endpoints
* [ ] **Units** - Unit management endpoints

## Installation

Dot.NET

- NET >= 8.x.x -> https://dotnet.microsoft.com/en-us/download

## Usage

1. Obtain a Personal Access Token from [here](https://developer.bexio.com/pat)
2. Check the **bexio.net.Example** project on how to use it
   1. Rename **appsettings.example.json** to **appsettings.json**
   2. Add your api token to the **appsettings.json**
   3. Run the example project and choose your option
   4. All GET request will be printed to the console and saved to a file in **Responses** folder

## Authentication

`BexioApi` accepts either a fixed token or an `IAccessTokenProvider`:

```csharp
// Personal Access Token (https://developer.bexio.com/pat) - valid for 60 days only
var bexio = new BexioApi("<pat>");

// OAuth: the provider is asked for a token on every request and told via Invalidate() when
// bexio answered 401, so it can refresh short-lived access tokens transparently
var bexio = new BexioApi(myAccessTokenProvider);
```

The OAuth [Authorization Code Flow](https://docs.bexio.com/#section/Authentication/Authorization-Code-Flow)
itself (consent, token exchange, refresh token storage) belongs to the hosting application and is
not part of this client. If the provider throws, the request is not sent and an
`AccessTokenException` is thrown regardless of `UnsuccessfulReturnStyle`.

---

# Copyright

&copy; Substring AG

# Contact

[hello@substring.ch](mailto:hello@substring.ch)

[https://substring.ch/](https://substring.ch/)

