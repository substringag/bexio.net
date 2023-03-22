> :warning: **This is a work in progress**

# Bexio API

A simple REST API client in .NET 6.0 for [Bexio](https://www.bexio.com/) which is a powerful cloud based software for
business administrations.

Find official API documentation [here](https://docs.bexio.com/)

## Status of this implementation

As this is still a work in progress, only some API calls are implemented and tested.

* [x] Contacts
* [ ] Sales Order Management
* [ ] Purchase
* [ ] Accounting
* [ ] Banking
* [ ] Items & Products
* [x] Projects & Time Tracking
* [ ] Files
* [ ] Other
    * [ ] Company Profile
    * [ ] Countries
    * [ ] Languages
    * [ ] Notes
    * [ ] Payment Types
    * [ ] Permissions
    * [ ] Tasks
    * [ ] Units
    * [x] User Management

## Installation & Usage

1. Obtain an api token from [here](https://office.bexio.com/admin/apiTokens)
2. Check the Example project on how to use it

## Authentication

We decided to use the direct API Token approach as described 
[here](https://docs.bexio.com/#section/Authentication/API-Tokens). 
The Implementation of oauth 'Authorization Code Grant' flow is described
in the official documentation but not implemented in this API client.

