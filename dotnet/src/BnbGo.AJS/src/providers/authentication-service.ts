import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Events } from 'ionic-angular';

@Injectable()
export class AuthenticationService {
  public currentUser: any;
  public data: any;
  public allowed: boolean;
 
  constructor(public http: Http, public events: Events) {

  }

  public login(credentials) {
    if (credentials.email === null || credentials.password === null) {
      console.log("no input given");
    } else {
      return new Promise(resolve => {
      // We're using Angular HTTP provider to request the data,
      // then on the response, it'll map the JSON data to a parsed JS object.
      // Next, we process the data and resolve the promise with the new data.
      this.http.get('http://localhost:5000/api/users/byEmail/' + credentials.email)
        .map(res => res.json())
        .subscribe(data => {
          // we've got back the raw data, now generate the core schedule data
          // and save the data for later reference

          if (data.email != null) {
            if (data.plainPassword == credentials.password) {
              this.currentUser = data.id;
              localStorage.setItem("userId", this.currentUser);
              this.events.publish('user:created', this.currentUser, Date.now());
              this.allowed = true;
            } else {
              console.log("password incorrect");
              this.allowed = false;
            }
          } else {
              console.log("no user with this email");
              this.allowed = false;
          }
          
          this.data = data;
          resolve(this.data);
        });
        
      });
    }
  }

  public register(credentials) {
    if (credentials.email === null || credentials.plainPassword === null) {
      console.log("no input given");
    } else {
      return new Promise(resolve => {

        let headers = new Headers({"Content-Type":"application/json"});
        let options = new RequestOptions({ headers: headers});

        this.http.post('http://localhost:5000/api/users/', JSON.stringify(credentials), options)
          .map(res => res.json())
          .subscribe(data => {
            console.log("data recieved");
          });
        
      });
    }
  }
 
  public getUserInfo() {
    console.log(this.currentUser);
    return this.currentUser;
  }
 
  public logout() {
    this.currentUser = null;
    this.events.publish('user:created', this.currentUser, Date.now());
    localStorage.removeItem("userId");
  }

}
