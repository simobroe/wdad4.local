import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { AuthenticationService } from "../../providers/authentication-service";
import { Home } from '../home/home';

@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
  providers: [AuthenticationService]
})
export class LoginPage {
  registerCredentials = {email: '', password: ''};

  constructor(public navCtrl: NavController, private Authenticate: AuthenticationService) {}

  public login() {
    this.Authenticate.login(this.registerCredentials).then(data => {
      if (this.Authenticate.allowed == true) {
        this.navCtrl.setRoot(Home)
      } else {
        console.log("Access Denied");
      }
    });
  }

}
