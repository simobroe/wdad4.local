import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { ReservationService } from "../../providers/reservation-service";

import { ReservationConfirmedPage } from "../reservation-confirmed/reservation-confirmed";
import { LoginPage } from "../login/login";

@Component({
  selector: 'page-reservation',
  templateUrl: 'reservation.html',
  providers: [ReservationService]
})
export class ReservationPage {
  
  public roomId: any;
  public priceBase: any;
  public pricePerNight: any;
  public userId: any;
  public days: any;
  public myDate: String = new Date().toISOString();
  public myDateName: String = new Date().toLocaleDateString();

  reservationCredentials = {name: "", description: "", roomId: '', userId: '', arrival: this.myDate, departure: this.myDate, priceTotal: "", amountOfGuests: ""};

  constructor(public navCtrl: NavController, public navParams: NavParams, public ReservationService: ReservationService) {
    this.roomId = this.navParams.get("roomId");
    this.priceBase = this.navParams.get("priceBase");
    this.pricePerNight = this.navParams.get("pricePerNight");

    console.log(localStorage.getItem("userId"));

    if (localStorage.getItem("userId") === null) {
      this.navCtrl.setRoot(LoginPage, {
        reservationStep: "yes",
        roomId: this.roomId,
        priceBase: this.priceBase,
        pricePerNight: this.pricePerNight
      })
    }

    this.userId = localStorage.getItem("userId");
  }

  reserve() {
    this.reservationCredentials.roomId = this.roomId;
    this.days = Math.floor(( Date.parse(this.reservationCredentials.departure.toString()) - Date.parse(this.reservationCredentials.arrival.toString()) ) / 86400000);
    this.reservationCredentials.priceTotal = this.priceBase + this.pricePerNight * (this.days) ;
    this.reservationCredentials.userId = this.userId;
    this.reservationCredentials.name = "reservation on " + this.myDateName;
    this.reservationCredentials.description = this.reservationCredentials.arrival + " - " + this.reservationCredentials.departure;

    this.ReservationService.reserve(this.reservationCredentials)
    this.navCtrl.setRoot(ReservationConfirmedPage)
  }

}
