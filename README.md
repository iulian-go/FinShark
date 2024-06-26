# FinShark

Quickly find relevant financial data and create a portfolio with favorite stocks.

This is a learning project, following [Teddy Smith](https://github.com/teddysmithdev)'s React & .NET Core [Course](https://www.youtube.com/watch?v=XSLm9PHnkxI&list=PL82C6-O4XrHcNJd4ejg8pX5fZaIDZmXyn).

## Technologies

* Requirements
  - node.js
  - Microsoft Sql Server
  - .NET 8
* Frontend
  - React TypeScript
  - tailwindcss
  - axios
* Backend
  - .NET Core Web API
  - Entity Framework Core
  - Microsoft Identity

## Installation

1. Clone the repository
2. Open project in Visual Studio Code
3. Create a free account with [https://site.financialmodelingprep.com](https://site.financialmodelingprep.com)
   - go to **Dashboard** and create a new **API Key**
   - copy the newly created API Key to **.env** file locate in **frontend** folder
4. Update *Connection String* with your Sql Server in **appsettings.json** file located in **backend** folder
5. In **backend** directory run
   - `dotnet ef database update` - creates the database
   - `dotnet watch run` - runs the web api
6. In **frontend** directory run
   - `npm install` - installs all dependencies
   - `npm start` - runs the client app

## License

[MIT](https://choosealicense.com/licenses/mit/)
