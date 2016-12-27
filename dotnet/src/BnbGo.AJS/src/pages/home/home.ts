import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

// call country service
import { CityService } from '../../providers/city-service';
import { AuthenticationService } from '../../providers/authentication-service';

import { CityPage } from '../city/city'

@Component({
  selector: 'page-home',
  templateUrl: 'home.html',
  providers: [CityService, AuthenticationService]
})

export class Home {
  private cityId: string;
  private cityName: string;
  private posts: any;
  public loggedIn: boolean;

  constructor(public navCtrl: NavController, public navParams: NavParams, public CityService: CityService, private Authentication: AuthenticationService) {
    this.navCtrl = navCtrl;
    this.loadCities();
  }

  loadCities(){
    this.CityService.load(0)
    .then(data => {
      this.posts = data;
    });
  }

  getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  pushCity() {
    this.cityId = '1';
    //this.cityId = this.getRandomInt(1, this.posts.length);
    this.navCtrl.setRoot(CityPage , {cityId: this.cityId});
  }

  pushCityById(id, name) {
    this.cityId = id;
    this.cityName = name;
    this.navCtrl.setRoot(CityPage , {cityId: this.cityId, cityName: this.cityName});
  }
}
