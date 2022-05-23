# net-interviewing-project-v2
 
Use the Order Controller
  1) Create order for specific account:
     Post - http://localhost:5000/api/insurance/order, 
     body:
     {
       "accountId": 33
     }
  
     Add product specific order:
     Post - http://localhost:5000/api/insurance/order/33, 
     body:
     {
       "productId": 735246
     }
  
     Get specific order with it's insurance value and related products:
     Get - http://localhost:5000/api/insurance/order/33
