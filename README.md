
#  EcommAlgebra

## Requirements :

Locally installed : SQL server and Visual Studio or other IDE of your choice that is configured for ASP.NET C#.

## Run Locally

Clone the project

```bash
  git clone https://github.com/tomislavmarkota/EcommAlgebra.git
```

Update database - Package Manager Console

```bash
  update-database
```

## Description : 

This is a e-commerce app with user authentication and authorization. 

User with role Admin and Editor have available CRUD functionality for managing other users, products and categories.

User with role User can only see listed products and product details.


## How to use : 

There are two projects in the solution, first one is **EcommAlgebra** - MVC app and the second is **EcommAlgebraWebApi** - web API.

First you need to change your **database connection string** in the appsettings.json file if necessary. 

After that in the package manager console run : **update-database**

You are now ready to run the application, you have to start the **EcommAlgebra** first. Then you can login with seeded user as a administrator with these credentials : 

**Admin user**: 

email: admin@admin.com

password: password


After you ran **EcommAlgebra** project you can start **EcommAlgebraWebApi** which it will open up swagger where you can test endpoints. You can see more info in the API Reference section



## API Reference

#### Get all items

```http
  GET /api/Products
```


#### Get item

```http
  GET /api/Products/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |












