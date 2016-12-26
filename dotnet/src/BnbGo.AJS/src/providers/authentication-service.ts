import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { Events } from 'ionic-angular';

export class User {
  id: string;
  name: string;
  email: string;
 
  constructor(id: string, name: string, email: string) {
    this.id = id;
    this.name = name;
    this.email = email;
  }
}

@Injectable()
export class AuthenticationService {
  public currentUser: User;
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
              this.currentUser = new User(data.id, data.firstName + " " + data.surName, data.email);
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
 
  public getUserInfo() {
    console.log(this.currentUser.id);
    return this.currentUser.id;
  }
 
  public logout() {
    this.currentUser = null;
    this.events.publish('user:created', this.currentUser, Date.now());
  }

}
