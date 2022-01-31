import { Component, OnInit } from '@angular/core';
import { OrderService } from '../order-service';
import Swal from 'sweetalert2'
@Component({
  selector: 'app-order-pharmacist',
  templateUrl: './order-pharmacist.component.html',
  styleUrls: ['./order-pharmacist.component.css'],
  providers: [OrderService]
})
export class OrderPharmacistComponent implements OnInit {

  orders: any= [];
  constructor(private _orderService: OrderService) { }

  getStatus(status : String){
    if(status == "1") return "Pick up request"
    else return "Order request"
  }
  getButton(status : String){
    if(status == '1') return "Confirm pick up request"
    else return "Assign courier"
  }
  updateStatus(order : any){
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })
    
    swalWithBootstrapButtons.fire({
      title: 'Are you sure you want to confirm request?',
      text: "",
      icon: 'info',
      showCancelButton: true,
      confirmButtonText: 'Yes!',
      cancelButtonText: 'Nol!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        console.log(order)
       if(order.Status == '1')order.Status =2;
       else {order.Status = 3}
      order.UserId = order.User.UserId
      this._orderService.changeStatus(order).subscribe(data =>{
        console.log(data)
        this.orders=data
      })
        swalWithBootstrapButtons.fire(
          'Success!',
          'The order is confirmed.',
          'success'
        )
      }
    })
  }
  getPrice(orderItems : any) : number{
    let sum = 0
    orderItems.forEach(function (orderItem: { PriceForSingleEntity: number; Quantity: number; }) {
      sum += orderItem.PriceForSingleEntity * orderItem.Quantity
  });
  return sum
}

  ngOnInit(): void {
    this._orderService.getRequests().subscribe(data =>{
      console.log(data)
      this.orders = data
    })
  }

}
