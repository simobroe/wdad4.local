import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

import { AuthenticationService } from "../../providers/authentication-service";
import { Home } from '../home/home';
import { ReservationPage } from '../reservation/reservation';

@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
  providers: [AuthenticationService]
})
export class LoginPage {
  registerCredentials = {email: '', password: ''};

  constructor(public navCtrl: NavController, public navParams: NavParams, private Authenticate: AuthenticationService) {}

  public login() {
    this.Authenticate.login(this.registerCredentials).then(data => {
      if (this.Authenticate.allowed == true) {
        if (this.navParams.get("reservationStep") != null) {
          this.navCtrl.setRoot( ReservationPage, { roomId:this.navParams.get("roomId"), priceBase:this.navParams.get("priceBase"), pricePerNight:this.navParams.get("pricePerNight")})
        } else {
          this.navCtrl.setRoot(Home)
        }
      } else {
        console.log("Access Denied");
      }
    });
  }

}
