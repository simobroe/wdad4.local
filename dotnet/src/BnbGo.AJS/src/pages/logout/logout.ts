import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { AuthenticationService } from "../../providers/authentication-service";
import { Home } from '../home/home';

@Component({
  selector: 'page-logout',
  templateUrl: 'logout.html',
  providers: [AuthenticationService]
})
export class LogoutPage {

  constructor(public navCtrl: NavController, public Authentication: AuthenticationService) {}

  logout() {
    this.Authentication.logout();
    this.navCtrl.setRoot(Home);
  }

}
