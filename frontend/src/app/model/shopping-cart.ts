import { from } from "rxjs";
import {Item} from "src/app/model/Item"

export class ShoppingCart{
    ShoppingCart_Id: string;
    items: Item[];
    User_Id: string;

    constructor(ShoppingCart_Id : string, User_Id: string){
        this.ShoppingCart_Id = ShoppingCart_Id;
        this.items = [];
        this.User_Id = User_Id;
    }

}