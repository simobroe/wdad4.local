import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { Home } from "../home/home"

@Component({
  selector: 'page-reservation-confirmed',
  templateUrl: 'reservation-confirmed.html'
})
export class ReservationConfirmedPage {

  constructor(public navCtrl: NavController) {}

  goHome() {
    this.navCtrl.setRoot(Home);
  }

}
