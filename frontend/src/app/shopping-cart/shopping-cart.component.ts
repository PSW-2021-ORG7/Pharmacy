import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import {ShoppingCartService} from "../service/shopping-cart-service"
import { ShoppingCart } from '../model/shopping-cart';


@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  public shoppingCart : any;
  public selected_pickup : any;

  constructor(private _shoppingCartService : ShoppingCartService) {}

  ngOnInit() : void{
    this._shoppingCartService.getShoppingCart("2").subscribe(data => this.shoppingCart = data);
    console.log(this.shoppingCart)
    this.selected_pickup = "pickup";
  }

  checkout(){
    let makeAnOrder = {
      "shoppingCartId" : this.shoppingCart.ShoppingCart_Id,
      "delivery" : this.selected_pickup
    }
    this._shoppingCartService.makeAnOrder(makeAnOrder).subscribe(data => this.shoppingCart = data)
  }

  change_quantity(id_item : number){
    
    let changeQuantity ={
      "ShoppingCarts_Id" : this.shoppingCart.ShoppingCart_Id,
      "newQuantity" : "5",
      "ShoppingCartsItem_Id" : id_item
    }
    this._shoppingCartService.updateQuantity(changeQuantity).subscribe(data => this.shoppingCart = data)
  }
}
