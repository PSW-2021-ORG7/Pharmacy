import { Component, OnInit } from '@angular/core';
import { OrderService } from '../order-service';
@Component({
  selector: 'app-order-pharmacist',
  templateUrl: './order-pharmacist.component.html',
  styleUrls: ['./order-pharmacist.component.css'],
  providers: [OrderService]
})
export class OrderPharmacistComponent implements OnInit {

  orders: any= [
    {
      Order_Id : 1,
      OrderItems:[
        {
      Medicine: {
        Id : 3,
        Name : 'Andol',
        Dosage : '300',
        Description: 'Lek za oralnu upotrebu smanjuje bolove glave'
      },
      Quantiy : 3
    },
    {
      Medicine: {
        Id : 3,
        Name : 'Andol',
        Dosage : '300',
        Description: 'Lek za oralnu upotrebu smanjuje bolove glave'
      },
      Quantiy : 2
    }
      ],
    User :{
      UserId : 2
    },
    Date: new Date(2021, 10, 11),
    OrderStatus: 'PickUpRequest'
    },
    {
      Order_Id : 2,
      OrderItems:[
        {
      Medicine: {
        Id : 3,
        Name : 'Andol',
        Dosage : '300',
        Description: 'Lek za oralnu upotrebu smanjuje bolove glave'
      },
      Quantiy : 2
    }
      ],
    User :{
      UserId : 2
    },
    Date: new Date(2021, 10, 11),
    OrderStatus: 'OrderRequest'
    },
    {
      Order_Id : 3,
      OrderItems:[
        {
      Medicine: {
        Id : 3,
        Name : 'Andol',
        Dosage : '300',
        Description: 'Lek za oralnu upotrebu smanjuje bolove glave'
      },
      Quantiy : 2
    }
      ],
    User :{
      UserId : 2
    },
    Date: new Date(2021, 10, 11),
    OrderStatus: 'PickUpRequest'
    },
   
  ];
  constructor(private _orderService: OrderService) { }

  getStatus(status : String){
    if(status == "PickUpRequest") return "Pick up request"
    else return "Order request"
  }
  getButton(status : String){
    if(status == "PickUpRequest") return "Confirm pick up request"
    else return "Assign courier"
  }
  orderAgain(order : any){
     if(confirm("Are you sure you want to confirm request?")){
        const orderDto = {
          Order_id : order.Order_Id,
          OrderStatus : order.OrderStatus
        }
        this._orderService.changeStatus(orderDto);
     }  
  }

  ngOnInit(): void {
  }

}
