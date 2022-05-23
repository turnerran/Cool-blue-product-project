# net-interviewing-project-v2

Assumptions - I used a cache service to keep the state of the order and products. this information will be deleted once the server is terminated.
 
Use the Order Controller
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
