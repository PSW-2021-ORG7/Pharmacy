import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, retry } from "rxjs";
import { ShoppingCart} from "src/app/model/shopping-cart"
import {environment} from "src/environments/environment"


@Injectable({
    providedIn: 'root',
})
export class ShoppingCartService{
    private base_url=environment.baseUrlPharmacy+"ShoppingCart";

    constructor(private _http: HttpClient){}

 getShoppingCart(LoggedUserId: string) : Observable<ShoppingCart>{
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this._http.get<ShoppingCart>(this.base_url+"/79a5e866-8b08-427f-a436-ac6b22ff7da5")
        
    }
makeAnOrder(order : any) : Observable<ShoppingCart>{
    return this._http.post<ShoppingCart>(this.base_url+"/make_an_order", order);
}

updateQuantity(UpdateQu : any) : Observable<ShoppingCart>{
    
    return this._http.post<ShoppingCart>(this.base_url+"/update_quantity", UpdateQu);
}
}