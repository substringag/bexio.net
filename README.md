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

1. Obtain an api token from [here](https://office.bexio.com/admin/apiTokens)
2. Check the **bexio.net.Example** project on how to use it
   1. Rename **appsettings.example.json** to **appsettings.json**
   2. Add your api token to the **appsettings.json**
   3. Run the example project and choose your option
   4. All GET request will be printed to the console and saved to a file in **Responses** folder

## Authentication

We decided to use the direct API Token approach as described 
[here](https://docs.bexio.com/#section/Authentication/API-Tokens). 
The Implementation of oauth 'Authorization Code Grant' flow is described
in the official documentation but not implemented in this API client.

---

# Copyright

&copy; Substring AG

# Contact

[hello@substring.ch](mailto:hello@substring.ch)

[https://substring.ch/](https://substring.ch/)

