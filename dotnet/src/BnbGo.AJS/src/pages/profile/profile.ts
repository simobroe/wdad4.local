import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Events } from 'ionic-angular';

import { AuthenticationService } from "../../providers/authentication-service";
import { UserService } from "../../providers/user-service";
import { RoomPage } from "../../pages/room/room";

@Component({
  selector: 'page-profile',
  templateUrl: 'profile.html',
  providers: [AuthenticationService, UserService]
})
export class ProfilePage {

  private items: any;
  private userId: string;
  public isMyPage: boolean;

  constructor(public navCtrl: NavController, public navParams: NavParams, public Authentication: AuthenticationService, public UserService: UserService, public events: Events) {
    if (this.navParams.get("userId") != null) {
      this.userId = this.navParams.get("userId");
      this.isMyPage = false;
    } else {
      this.userId = localStorage.getItem("userId");
      this.isMyPage = true;
    }
    this.loadUser(this.userId);
  }

  loadUser(userId) {
    this.UserService.load(userId)
    .then(data => {
      this.items = data;
    });
  }

  pushRoom(id) {
    this.navCtrl.setRoot(RoomPage, {roomId: id});
  }

}
