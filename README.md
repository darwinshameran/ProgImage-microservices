ProgImage-microservices
=========
A set of microservices to store and transform images.

Built With
----------
* .NET/Core 3.1
* Entity Framework
* RabbitMQ
* PostgreSQL
* Ocelot
* Docker


Deploy
-------
Using Docker:
    
    docker network create -d bridge progimage
    docker-compose build && docker-compose up -d
   
   
Migrations
----------
Using `dotnet` cli:
    
    dotnet ef database update

Tests
-------
Using `dotnet` cli:

    dotnet test
 

REST API
========

Base API path: */api/v1/progimage*


Get transformation job status
------------------------------
**Request**

`GET /api/v1/progimage/transformation/status/{statusId}`

    curl -i -H "http://localhost:8080/api/v1/progimage/transformation/status/{statusId}"
    
Get image
-------------
**Request**

`GET /api/v1/progimage/storage/{id}[.ext]`

    curl -i -H "http://localhost:8080/api/v1/progimage/storage/{id}"

    curl -i -H "http://localhost:8080/api/v1/progimage/storage/{id}.png"

  
Create new image
------------------
**Request**

`POST /api/v1/progimage/storage`

    curl -i -F "image=@<image_name>" "http://localhost:8080/api/v1/progimage/storage"
  
Rotate image
---------------
**Request**

*Direct data upload:*

`POST /api/v1/progimage/transformation/rotate?degrees={degrees}`

    curl -i -F "image=@<image_name>" "http://localhost:8080/api/v1/progimage/transformation/rotate?degrees={degrees}"
    
*Image id:*

`POST /api/v1/progimage/transformation/{imageId}/rotate?degrees={degrees}`

    curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/{imageId}/rotate?degrees={degrees}"
   
*Url:*

`POST /api/v1/progimage/transformation/rotate?url={url}degrees={degrees}`

     curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/rotate?url={url}&degrees={degrees}"
 
   
Create thumbnail
------------------

**Request**

*Direct data upload:*

`POST /api/v1/progimage/transformation/thumbnail?width={width}&height={height}`

    curl -i -F "image=@<image_name>" "http://localhost:8080/api/v1/progimage/transformation/thumbnail?width={width}&height={height}"
    
*Image id:*

`POST /api/v1/progimage/transformation/{imageId}/thumbnail?width={width}&height={height}`

    curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/{imageId}/thumbnail?width={width}&height={height}"
   
*Url:*

`POST /api/v1/progimage/transformation/thumbnail?url={url}&width={width}&height={height}`

    curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/thumbnail?url={url}&width={width}&height={height}"

Compress image
-----------------

**Request**

*Direct data upload:*

`POST /api/v1/progimage/transformation/compress?quality={quality}`

    curl -i -F "image=@<image_name>" "http://localhost:8080/api/v1/progimage/transformation/compress?quality={quality}"
    
*Image id:*

`POST /api/v1/progimage/transformation/{imageId}/compress?quality={quality}`

    curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/{imageId}/compress?quality={quality}"
   
*Url:*

`POST /api/v1/progimage/transformation/compress?url={url}&quality={quality}`

    curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/compress?url={url}&quality={quality}"

Blur image
-----------------

**Request**

*Direct data upload:*

`POST /api/v1/progimage/transformation/blur?radius={radius}&sigma={sigma}`

    curl -i -F "image=@<image_name>" "http://localhost:8080/api/v1/progimage/transformation/blur?radius={radius}&sigma={sigma}"
    
*Image id:*

`POST /api/v1/progimage/transformation/{imageId}/blur?radius={radius}&sigma={sigma}`

    curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/{imageId}/blur?radius={radius}&sigma={sigma}"
   
*Url:*

`POST /api/v1/progimage/transformation/blur?radius={radius}&sigma={sigma}`

    curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/compress?url={url}&quality={quality}"
    
 Mask image
 -----------------
 
 **Request**
 
 *Direct data upload:*
 
 `POST /api/v1/progimage/transformation/mask`
 
     curl -i -F "image=@<image_name>" "http://localhost:8080/api/v1/progimage/transformation/mask"
     
 *Image id:*
 
 `POST /api/v1/progimage/transformation/{imageId}/mask`
 
     curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/{imageId}/mask"
    
 *Url:*
 
 `POST /api/v1/progimage/transformation/mask?url={url}`
 
     curl -i -X POST "http://localhost:8080/api/v1/progimage/transformation/mask?url={url}"
