import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

import { AuthenticationService } from "../../providers/authentication-service";
import { Home } from '../home/home';
import { ReservationPage } from '../reservation/reservation';
import { LoginPage } from '../login/login';

@Component({
  selector: 'page-register',
  templateUrl: 'register.html',
  providers: [AuthenticationService]
})
export class RegisterPage {
  registerCredentials = {email: '', plainPassword: '', dayOfBirth: "", firstName: "", SurName: "", cityId: "1", regionId: "1", countryId: "176"};

  constructor(public navCtrl: NavController, public navParams: NavParams, private Authenticate: AuthenticationService) {}

  public register() {
    this.Authenticate.register(this.registerCredentials)
    this.navCtrl.setRoot( LoginPage, {reservationStep: this.navParams.get("reservationStep"), roomId:this.navParams.get("roomId"), priceBase:this.navParams.get("priceBase"), pricePerNight:this.navParams.get("pricePerNight")})
  }

  
}
