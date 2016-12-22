import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

// call country service
import { RoomService } from '../../providers/room-service';

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
}
