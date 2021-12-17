import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import {environment} from "src/environments/environment"

@Injectable()
export class OrderService{
    private _serverUrl=environment.baseUrlPharmacy+'orders'
    constructor(private _http: HttpClient){}

    updateReorder(order: Object): Observable<Object>{
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this._http.post<any>(this._serverUrl+"/update-reorder",order,{headers: headers})
    }

    changeStatus(order: Object): Observable<Object>{
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this._http.put<Object>(this._serverUrl+'/update-status',order,{headers: headers})
    }

    getOrderHistory() : Observable<Object>{
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this._http.get<Object>(this._serverUrl+'/history/b0bc0a9c-e6bd-462c-ad3e-e6a8238c5858')
    }

    getRequests() : Observable<Object>{
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this._http.get<Object>(this._serverUrl+'/requests',{headers: headers})
    }
}