import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

// call country service
import { RoomService } from '../../providers/room-service';
import { ReservationPage } from '../reservation/reservation';
import { ProfilePage } from '../profile/profile';

@Component({
  selector: 'page-room',
  templateUrl: 'room.html',
  providers: [RoomService]
})
export class RoomPage {
  private roomId: string;
  private items: any;

  constructor(public navCtrl: NavController, public navParams: NavParams, public RoomService: RoomService) {
    this.roomId = this.navParams.get("roomId");
    this.loadDetails(this.roomId);
  }

  loadDetails(roomId){
    this.RoomService.loadDetail(roomId)
    .then(data => {
      this.items = data;
    });
  }

  pushReservation(id, priceBase, pricePerNight) {
    this.navCtrl.setRoot(ReservationPage, {roomId: id, priceBase: priceBase, pricePerNight: pricePerNight})
  }

  pushProfile(id) {
    this.navCtrl.setRoot(ProfilePage, {userId: id});
  }
}
