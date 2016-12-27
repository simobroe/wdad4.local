import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Events } from 'ionic-angular';

@Injectable()
export class ReservationService {
  public currentUser: any;
  public data: any;
  public allowed: boolean;
 
  constructor(public http: Http, public events: Events) {

  }

  public reserve(credentials) {
    if (credentials.arrival === null || credentials.departure === null || credentials.amountOfGuests === null) {
      console.log("no input given");
    } else {
      return new Promise(resolve => {

        let headers = new Headers({"Content-Type":"application/json"});
        let options = new RequestOptions({ headers: headers});

        this.http.post('http://localhost:5000/api/reservations/', JSON.stringify(credentials), options)
          .map(res => res.json())
          .subscribe(data => {
            console.log("data recieved");
          });
        
      });
    }
  }
}
