import { Component, OnInit } from '@angular/core';
import { OrderService } from '../order-service';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css'],
  providers: [OrderService]
})
export class OrderHistoryComponent implements OnInit {
  orders: any= [
    {
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
    OrderStatus: 'Delivered'
    },
    {
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
    OrderStatus: 'Delivered'
    },
    {
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
    OrderStatus: 'Delivered'
    },
   
  ];
  constructor(private _orderService: OrderService) { }

  changeDate(date : Date){
    return date.toLocaleDateString("sr-RS")
  }
  orderAgain(order : object){
    this._orderService.saveOrder(order)
    alert('Order succesfully created!')
  }
  ngOnInit(): void {
  }

}
