import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Events } from 'ionic-angular';

import { AuthenticationService } from "../../providers/authentication-service";
import { UserService } from "../../providers/user-service";

@Component({
  selector: 'page-profile',
  templateUrl: 'profile.html',
  providers: [AuthenticationService, UserService]
})
export class ProfilePage {

  private items: any;
  private userId: string;

  constructor(public navCtrl: NavController, public navParams: NavParams, public Authentication: AuthenticationService, public UserService: UserService, public events: Events) {
    if (this.navParams.get("userId") != null) {
      this.userId = this.navParams.get("userId");
    } else {
      this.userId = localStorage.getItem("userId");
    }
    this.loadUser(this.userId);
  }

  loadUser(userId) {
    this.UserService.load(userId)
    .then(data => {
      this.items = data;
    });
  }

}
