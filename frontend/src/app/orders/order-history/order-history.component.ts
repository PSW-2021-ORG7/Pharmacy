import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { OrderService } from '../order-service';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css'],
  providers: [OrderService]
})
export class OrderHistoryComponent implements OnInit {
  orders: any= [];
  constructor(private _orderService: OrderService) { }

  changeDate(date : Date){
    let date1 = new Date(date)
    console.log(date)
    return date1.toLocaleDateString("sr-RS")
  }
  getOrder(order : Object){
    console.log(order)
  }

  orderAgain(order : any){
    order.Status = 1
    order.OrderDate =new Date(Date.now())
    delete order.Order_Id
    order.UserId = order.User.UserId;
    this._orderService.updateReorder(order).subscribe(data =>{
      console.log(data)
       this.orders = data
       Swal.fire(
        'Success!',
        'The order is successfully created!',
        'success'
      )
    })
  }
  getStatus (status : any){
    if(status == 6) return 'Delivered';
    else return 'Requested for delivery!'
  }
  getPrice(orderItems : any) : number{
    let sum = 0
    orderItems.forEach(function (orderItem: { PriceForSingleEntity: number; Quantity: number; }) {
      sum += orderItem.PriceForSingleEntity * orderItem.Quantity
  });
  return sum
  }

  ngOnInit(): void {
    this._orderService.getOrderHistory().subscribe(data =>{
      console.log(data)
      this.orders = data
    })
  }

}
