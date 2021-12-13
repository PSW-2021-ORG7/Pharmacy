import { from } from "rxjs";
import {Item} from "src/app/model/Item"

export class ShoppingCart{
    public ShoppingCart_Id: string;
    public items: Item[];
    public User_Id: string;
    public finalPrice: string;

    constructor(ShoppingCart_Id : string, User_Id: string, items: Item[], finalPrice: string){
        this.ShoppingCart_Id = ShoppingCart_Id;
        this.items = [];
        this.User_Id = User_Id;
        this.finalPrice = finalPrice;
    }

}