# net-interviewing-project-v2

I was able to do the first 4 requirements (didn't have time to do the fifth)

Assumptions - I used a cache service to keep the state of the order and products. this information will be deleted once the server is terminated.

Running tests - Open visual studio and hit run tests in the Insurance.Tests project
 
Use the Order Controller - 
  1) Create order for specific account:
     Post - http://localhost:5000/api/insurance/order, 
     body:
     {
       "accountId": 33
     }
  2)
     Add product specific order:
     Post - http://localhost:5000/api/insurance/order/33, 
     body:
     {
       "productId": 735246
     }
  3)
     Get specific order with it's insurance value and related products:
     Get - http://localhost:5000/api/insurance/order/33
