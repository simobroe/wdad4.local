import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
// call country service
import { RoomService } from '../../providers/room-service';

import { RoomPage } from '../room/room'

@Component({
  selector: 'page-city',
  templateUrl: 'city.html',
  providers: [RoomService]
})
export class CityPage {
  private posts: any;
  private cityId: string;
  private items: any;
  private roomId: string;
  private city: string;
  private cityName: string;

  constructor(public navCtrl: NavController, public navParams: NavParams, public RoomService: RoomService) {
    this.navCtrl = navCtrl;
    this.cityId = this.navParams.get("cityId");
    this.city = this.navParams.get("cityName");
    this.loadRooms(this.cityId);
  }
  
  loadRooms(cityId){
    this.RoomService.load(cityId)
    .then(data => {
      this.posts = data;
      this.initializeItems();
    });
  }

  initializeItems() {
    this.items = this.posts;
  }

  getItems(ev: any) {
    // Reset items back to all of the items
    this.initializeItems();

    // set val to the value of the searchbar
    let val = ev.target.value;

    // if the value is an empty string don't filter the items
    if (val && val.trim() != '') {
      this.items = this.items.filter((item) => {
        return (item.name.toLowerCase().indexOf(val.toLowerCase()) > -1);
      })
    }
  }

  pushRoom(id) {
    this.roomId = id;
    this.navCtrl.push(RoomPage , {roomId: this.roomId});
  }
}
