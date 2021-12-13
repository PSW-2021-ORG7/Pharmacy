import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ShoppingCart} from "src/app/model/shopping-cart"
import {environment} from "src/environments/environment"


@Injectable()
export class ShoppingCartService{
    private url=environment.baseUrlPharmacy;
    constructor(private _http: HttpClient){}

 getShoppingCart(LoggedUserId: number) : Observable<ShoppingCart>{
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this._http.get<ShoppingCart>(this.url + "/ShoppingCart/9d958778-280c-41d9-9b40-2099e9c56739")
    }
}