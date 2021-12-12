import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";


@Injectable()
export class OrderService{
    private _serverUrl='http://localhost:64677/orders'
    constructor(private _http: HttpClient){}

    saveOrder(order: Object): Observable<boolean>{
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this._http.post<boolean>(this._serverUrl,order,{headers: headers})
    }
}